using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Novacode;

namespace JLSScheduler
{
    internal static class DocExportWriter
    {
        private const int TableStringLength = 27;

        private static string _classDayString;
        private static string _classLevelString;
        private static string _classTimeString;

        public static DirectoryInfo WriteToDoc(ClassData cd, List<Week> weeks, string savePath)
        {
            _classDayString = ScheduleBuilder.GetWeekday(cd.ClassDayIndex).ToString();
            _classLevelString = cd.ClassLevel;

            _classTimeString = string.Format("{0}h{1}m", cd.ClassTime.Hour, cd.ClassTime.Minute);

            DirectoryInfo folder = Directory.CreateDirectory(savePath + "/" + _classDayString + "_" + _classTimeString);

            WriteNtPage(weeks, folder, cd);
            WriteKtPage(weeks, folder, cd);
            WriteStudentPages(weeks, folder, cd);

            return folder;
        }

        private static void WriteNtPage(List<Week> weeks, DirectoryInfo folder, ClassData cd)
        {
            using (
                DocX doc =
                    DocX.Create(folder.FullName + "/NT_" + _classDayString + "_" + _classLevelString + "_" +
                                _classTimeString + "_" + ".docx"))
            {
                WriteTeacherHeader(doc, cd, string.Empty);
                WriteSyllabusSection(doc, weeks, string.Empty);
                WriteAllStudentsChecklistSection(doc, weeks, cd);

                doc.Save();
            }
        }

        private static void WriteKtPage(IEnumerable<Week> weeks, DirectoryInfo folder, ClassData cd)
        {
            using (
                DocX doc =
                    DocX.Create(folder.FullName + "/KT_" + _classDayString + "_" + _classLevelString + "_" +
                                _classTimeString + "_" + ".docx"))

            {
                WriteTeacherHeader(doc, cd, string.Empty);
                WriteSyllabusSection(doc, weeks, string.Empty);

                doc.Save();
            }
        }

        private static void WriteStudentPages(List<Week> weeks, DirectoryInfo folder, ClassData cd)
        {
            using (
                DocX doc =
                    DocX.Create(folder.FullName + "/STUDENTS_" + _classDayString + "_" + _classLevelString + "_" +
                                _classTimeString + "_" + ".docx"))

            {
                foreach (var s in cd.StudentList)
                {
                    WriteStudentSchedule(doc, cd, weeks, s);
                    doc.InsertSectionPageBreak();

                    WriteStudentHomeworkSheet(doc, weeks);

                    doc.InsertSectionPageBreak();

                    WriteParentSchedule(doc, cd, weeks, s);
                    doc.InsertSectionPageBreak();
                }

                doc.Save();
            }
        }

        #region writesections

        private static void WriteStudentSchedule(DocX doc, ClassData cd, IEnumerable<Week> weeks,
            Tuple<string, string> s)
        {
            WriteStudentHeader(doc, cd, s);
            WriteSyllabusSection(doc, weeks,
                "Please complete the homework each week. If you miss a class, please complete the homework for the next week as well.");
        }

        private static void WriteStudentHomeworkSheet(DocX doc, IEnumerable<Week> weeks)

        {
            foreach (Week week in weeks)
            {
                string dueDate = week.DateTime.AddDays(7).ToShortDateString();

                string weeklyHomework = week.HomeworkList.Aggregate(string.Empty,
                    (current, hw) => current + string.Format("{0},  ", hw.Title));

                Table t = doc.AddTable(2, 5);


                t.Alignment = Alignment.center;

                //add content
                t.Rows[0].Cells[0].Paragraphs.First().Append("Date Assigned").Bold().FontSize(6);
                t.Rows[0].Cells[1].Paragraphs.First().Append("Date Due").Bold().FontSize(6);
                t.Rows[0].Cells[2].Paragraphs.First().Append("Assignment").Bold().FontSize(9);
                t.Rows[0].Cells[3].Paragraphs.First().Append("Parent Signature").Bold().FontSize(9);
                t.Rows[0].Cells[4].Paragraphs.First().Append("Teacher Signature").Bold().FontSize(9);
                t.Rows[1].Cells[0].Paragraphs.First().Append(week.DateTime.ToShortDateString()).Italic().FontSize(8);
                t.Rows[1].Cells[1].Paragraphs.First().Append(dueDate).Italic().FontSize(8);
                t.Rows[1].Cells[2].Paragraphs.First().Append(weeklyHomework).FontSize(7);

                //size the columns
                float space = (doc.PageWidth - doc.MarginLeft - doc.MarginRight)*0.9f;


                t.Rows[0].Cells[0].Width = Math.Round(0.08*space);
                t.Rows[1].Cells[0].Width = Math.Round(0.08*space);

                t.Rows[0].Cells[1].Width = Math.Round(0.08*space);
                t.Rows[1].Cells[1].Width = Math.Round(0.08*space);

                t.Rows[0].Cells[2].Width = Math.Round(0.4*space);
                t.Rows[1].Cells[2].Width = Math.Round(0.4*space);

                t.Rows[0].Cells[3].Width = Math.Round(0.2*space);
                t.Rows[1].Cells[3].Width = Math.Round(0.2*space);

                t.Rows[0].Cells[4].Width = Math.Round(0.2*space);
                t.Rows[1].Cells[4].Width = Math.Round(0.2*space);


                //insert into document.
                doc.InsertTable(t);
            }
        }

        private static void WriteParentSchedule(DocX doc, ClassData cd, IEnumerable<Week> weeks,
            Tuple<string, string> tuple)
        {
            Paragraph p0 = doc.InsertParagraph();

            p0.Append("JLS - Speaking Tree Native English Class").FontSize(22);
            p0.Bold();
            p0.UnderlineStyle(UnderlineStyle.thick);

            Paragraph p1 = doc.InsertParagraph();

            string name = !string.IsNullOrEmpty(tuple.Item2) ? tuple.Item2 : tuple.Item1;


            p1.Append(string.Format("{0}안녕하세요.{0}" +
                                    "아래의 표는{1}부터 {2}까지 JLS의 네이티브 선생님 수업을 듣는 여러분의 자녀들을 위해 만들어진 과제 일람표입니다.{0}" +
                                    "{3} 학생이 과제를 잘 따라가고 있는지 확인해보세요." +
                                    "{0}감사합니다." +
                                    "{0}{4}, JLS NT{0}",
                Environment.NewLine, cd.SemesterStart.ToShortDateString(), cd.SemesterEnd.ToShortDateString(), name,
                cd.NTname))
                .FontSize(11);

            foreach (Week week in weeks)
            {
                string dueDate = week.DateTime.AddDays(7).ToShortDateString();

                string weeklyHomework = week.HomeworkList.Aggregate(string.Empty,
                    (current, hw) => current + string.Format("{0},  ", hw.Title));

                Table t = doc.AddTable(2, 3);

                t.Alignment = Alignment.center;
                // Add content to this Table.
                t.Rows[0].Cells[0].Paragraphs.First().Append("Date Assigned").Bold().FontSize(9);
                t.Rows[0].Cells[1].Paragraphs.First().Append("Date Due").Bold().FontSize(9);
                t.Rows[0].Cells[2].Paragraphs.First().Append("Assignment").Bold().FontSize(9);
                t.Rows[1].Cells[0].Paragraphs.First().Append(week.DateTime.ToShortDateString()).Italic().FontSize(8);
                t.Rows[1].Cells[1].Paragraphs.First().Append(dueDate).Italic().FontSize(8);
                t.Rows[1].Cells[2].Paragraphs.First().Append(weeklyHomework).FontSize(7);
                // Insert the Table into the document.
                doc.InsertTable(t);
            }
        }


        private static void WriteTeacherHeader(DocX doc, ClassData cd, string additionalSubheader)
        {
            Paragraph p1 = doc.InsertParagraph();
            p1.Append(string.Format("{0}, {1} : {2}", _classDayString, cd.ClassTimeString, _classLevelString))
                .FontSize(14)
                .Bold();
            p1.Append(Environment.NewLine);
            p1.Append(string.Format("NT: {0} / KT: {1}", cd.NTname, cd.KTname))
                .FontSize(11)
                .Color(Color.DarkGray);
            if (!string.IsNullOrEmpty(additionalSubheader))
            {
                p1.Append(Environment.NewLine + additionalSubheader)
                    .FontSize(10);
            }
        }

        private static void WriteStudentHeader(DocX doc, ClassData cd, Tuple<string, string> student)
        {
            Paragraph p1 = doc.InsertParagraph();
            if (string.IsNullOrEmpty(student.Item2))
            {
                p1.Append(student.Item1)
                    .FontSize(15)
                    .Bold()
                    .Alignment = Alignment.center;
            }
            else
            {
                p1.Append(string.Format("{0} ({1})", student.Item1, student.Item2))
                    .FontSize(15)
                    .Bold()
                    .Alignment = Alignment.center;
            }

            Paragraph p2 = doc.InsertParagraph();

            p2.Append(string.Format("{0}'s Class, {1} at {2}", cd.NTname, _classDayString, cd.ClassTimeString))
                .FontSize(14)
                .Bold();
            p2.Append(Environment.NewLine);
        }

        private static void WriteSyllabusSection(DocX doc, IEnumerable<Week> weeks, string additionalSubtitle)
        {
            Paragraph p2 = doc.InsertParagraph();
            p2.Append("Syllabus:")
                .FontSize(12)
                .UnderlineStyle(UnderlineStyle.singleLine)
                .Bold();
            p2.Append(Environment.NewLine);
            if (!string.IsNullOrEmpty(additionalSubtitle))
            {
                p2.Append(additionalSubtitle)
                    .FontSize(9)
                    .Italic();
                p2.Append(Environment.NewLine);
            }

            foreach (Week week in weeks)
            {
                if (week.IsHoliday)
                {
                    p2.Append(week.HolidayTitle)
                        .FontSize(10)
                        .Bold();
                    p2.Append(Environment.NewLine);
                }
                else
                {
                    p2.Append(string.Format("Week {0} ({1}) - {2}", week.WeekNumber, week.Date, week.Subtitle))
                        .FontSize(10)
                        .Bold();
                    p2.Append(Environment.NewLine);
                }
                foreach (HomeworkTask hw in week.HomeworkList)
                {
                    p2.Append(hw.Title + Environment.NewLine)
                        .FontSize(10);
                    p2.Append("   " + hw.Body + Environment.NewLine)
                        .FontSize(9)
                        .Italic();
                }
                p2.Append(Environment.NewLine);
            }

            p2.Append(Environment.NewLine);
        }

        private static void WriteAllStudentsChecklistSection(DocX doc, IEnumerable<Week> weeks, ClassData cd)
        {
            Paragraph p3 = doc.InsertParagraph();
            p3.InsertPageBreakBeforeSelf();

            p3.Append("Students:")
                .FontSize(12)
                .UnderlineStyle(UnderlineStyle.singleLine)
                .Bold();
            p3.Append(Environment.NewLine);

            List<HomeworkTask> allHomework = weeks.SelectMany(w => w.HomeworkList).ToList();

            string homeworkChecklist = string.Empty;

            for (int i = 0; i < allHomework.Count; i++)
            {
                string n1 = string.Format("□   {0} ({1})", allHomework[i].Title, allHomework[i].DueWeek);
                string n2, n3;

                if (i + 2 > allHomework.Count || i + 3 > allHomework.Count)
                {
                    n2 = n3 = string.Empty;
                }
                else
                {
                    n2 = string.Format("□   {0} ({1})", allHomework[i + 1].Title, allHomework[i + 1].DueWeek);
                    n3 = string.Format("□   {0} ({1})", allHomework[i + 2].Title, allHomework[i + 2].DueWeek);
                }

                var strings = new[] {n1, n2, n3};
                List<string> estrings =
                    strings.Select(
                        s =>
                            s.Length > TableStringLength
                                ? s.Substring(0, TableStringLength)
                                : s.PadRight(TableStringLength)).ToList();

                homeworkChecklist += estrings[0] + estrings[1] + estrings[2] + Environment.NewLine;
            }


            foreach (var s in cd.StudentList)
            {
                Paragraph pn = doc.InsertParagraph();
                pn.Append(string.Format("{0} ({1})", s.Item1, s.Item2))
                    .FontSize(10);
                pn.Append(Environment.NewLine);

                pn.Append(homeworkChecklist)
                    .FontSize(9)
                    .Font(new FontFamily("Courier New"));
                pn.Append(Environment.NewLine);

                pn.InsertPageBreakAfterSelf();
            }
        }

        #endregion
    }
}