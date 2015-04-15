using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Novacode;

namespace JLSScheduler
{
    internal class DOCExportWriter
    {
        const int tableStringLength = 27;

        private static string classDayString;
        private static string classLevelString;
        private static string classTimeString;

        public static DirectoryInfo WriteToDOC(ClassData cd, List<Week> weeks, string savePath)
        {
            classDayString = ScheduleBuilder.GetWeekday(cd.classDayIndex).ToString();
            classLevelString = LSClasses.Classes()[cd.classLevelIndex];


            classTimeString = new string((from c in cd.classTimeString
                where char.IsWhiteSpace(c) || char.IsLetterOrDigit(c)
                select c
                ).ToArray()).Insert(3, "_");

            var folder = Directory.CreateDirectory(savePath + "/" + classDayString + "_" +
                                                   classTimeString);



            WriteNTPage(weeks, folder, cd);
            WriteKTPage(weeks, folder, cd);
            WriteStudentPages(weeks, folder, cd);

            return folder;

        }

        private static void WriteNTPage(List<Week> weeks, DirectoryInfo folder, ClassData cd)
        {

            using (
                DocX doc =
                    DocX.Create(folder.FullName + "/NT_" + classDayString + "_" + classLevelString + "_" +
                                classTimeString + "_" + ".docx"))
            {
                WriteTeacherHeader(doc, cd, string.Empty);
                WriteSyllabusSection(doc, weeks, string.Empty);
                WriteAllStudentsChecklistSection(doc, weeks, cd);

                doc.Save();
            }


        }

        private static void WriteKTPage(List<Week> weeks, DirectoryInfo folder, ClassData cd)
        {
            using (
                DocX doc =
                    DocX.Create(folder.FullName + "/KT_" + classDayString + "_" + classLevelString + "_" +
                                classTimeString + "_" + ".docx"))
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
                    DocX.Create(folder.FullName + "/STUDENTS_" + classDayString + "_" + classLevelString + "_" +
                                classTimeString + "_" + ".docx"))
            {

                foreach (var s in cd.studentList)
                {
                    WriteStudentSchedule(doc, cd, weeks, s);
                    doc.InsertSectionPageBreak();

                    WriteStudentHomeworkSheet(doc, cd, weeks, s);
                    doc.InsertSectionPageBreak();

                    WriteParentSchedule(doc, cd, weeks, s);
                    doc.InsertSectionPageBreak();
                }

                doc.Save();
            }
        }


        #region writesections


        private static void WriteStudentSchedule(DocX doc, ClassData cd, List<Week> weeks, Tuple<string, string> s)
        {
            WriteStudentHeader(doc, cd, s);
            WriteSyllabusSection(doc, weeks, "Please complete the homework each week. If you miss a class, please complete the homework for the next week as well.");
        }

        private static void WriteStudentHomeworkSheet(DocX doc, ClassData cd, List<Week> weeks, Tuple<string, string> tuple)
        {
            for (int i = 0; i < weeks.Count; i++)
            {
                var week = weeks[i];

                string dueDate = week.DateTime.AddDays(7).ToShortDateString();

                string weeklyHomework = string.Empty;
                foreach (var hw in week.HomeworkList)
                {
                    weeklyHomework += string.Format("{0},  ", hw.Title);
                }

                Table t = doc.AddTable(2, 5);
//                t.AutoFit = AutoFit.ColumnWidth;

                t.Alignment = Alignment.center;

                //add content
                t.Rows[0].Cells[0].Paragraphs.First().Append("Date Assigned").Bold().FontSize(9);
                t.Rows[0].Cells[1].Paragraphs.First().Append("Date Due").Bold().FontSize(9);
                t.Rows[0].Cells[2].Paragraphs.First().Append("Assignment").Bold().FontSize(9);
                t.Rows[0].Cells[3].Paragraphs.First().Append("Parent Signature").Bold().FontSize(9);
                t.Rows[0].Cells[4].Paragraphs.First().Append("Teacher Signature").Bold().FontSize(9);
                t.Rows[1].Cells[0].Paragraphs.First().Append(week.DateTime.ToShortDateString()).Italic().FontSize(8);
                t.Rows[1].Cells[1].Paragraphs.First().Append(dueDate).Italic().FontSize(8);
                t.Rows[1].Cells[2].Paragraphs.First().Append(weeklyHomework).FontSize(7);

                //size the columns
                float space = doc.PageWidth - doc.MarginLeft - doc.MarginRight;

                

                t.Rows[0].Cells[0].Width = Math.Round(0.08 * space);
                t.Rows[1].Cells[0].Width = Math.Round(0.08 * space);

                t.Rows[0].Cells[1].Width = Math.Round(0.08 * space);
                t.Rows[1].Cells[1].Width = Math.Round(0.08 * space);

                t.Rows[0].Cells[2].Width = Math.Round(0.4 * space);
                t.Rows[1].Cells[2].Width = Math.Round(0.4 * space);

                t.Rows[0].Cells[3].Width = Math.Round(0.2 * space);
                t.Rows[1].Cells[3].Width = Math.Round(0.2 * space);

                t.Rows[0].Cells[4].Width = Math.Round(0.2 * space);
                t.Rows[1].Cells[4].Width = Math.Round(0.2 * space);


                //insert into document.
                doc.InsertTable(t);
            }
        }

        private static void WriteParentSchedule(DocX doc, ClassData cd, List<Week> weeks, Tuple<string, string> tuple)
        {
            Paragraph p1 = doc.InsertParagraph();

            string name = !string.IsNullOrEmpty(tuple.Item2) ? tuple.Item2 : tuple.Item1;

            p1.Append(string.Format("안녕하세요.{0}" +
                                    "아래의 표는{1}부터 {2}까지 JLS의 네이티브 선생님 수업을 듣는 여러분의 자녀들을 위해 만들어진 과제 일람표입니다.{0}" +
                                    "{3} 학생이 과제를 잘 따라가고 있는지 확인해보세요." +
                                    "{0}감사합니다." +
                                    "{0}{4}, JLS NT", 
                                    Environment.NewLine, cd.semesterStart.ToShortDateString(), cd.semesterEnd.ToShortDateString(), name, cd.NTname))
                .FontSize(11);

            for (int i = 0; i < weeks.Count; i++)
            {
                var week = weeks[i];

                string dueDate = week.DateTime.AddDays(7).ToShortDateString();

                string weeklyHomework = string.Empty;
                foreach (var hw in week.HomeworkList)
                {
                    weeklyHomework += string.Format("{0},  ", hw.Title);
                }

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



        static void WriteTeacherHeader(DocX doc, ClassData cd, string additionalSubheader)
        {
            Paragraph p1 = doc.InsertParagraph();
            p1.Append(string.Format("{0}, {1} : {2}", classDayString, cd.classTimeString, classLevelString))
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

        static void WriteStudentHeader(DocX doc, ClassData cd, Tuple<string, string> student)
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

            p2.Append(string.Format("{0}'s Class, {1} at {2}", cd.NTname, classDayString, cd.classTimeString))
                .FontSize(14)
                .Bold();
            p2.Append(Environment.NewLine);
        }

        static void WriteSyllabusSection(DocX doc, List<Week> weeks, string additionalSubtitle)
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
                foreach (var hw in week.HomeworkList)
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

        static void WriteAllStudentsChecklistSection(DocX doc, List<Week> weeks, ClassData cd)
        {
            Paragraph p3 = doc.InsertParagraph();
            p3.InsertPageBreakBeforeSelf();

            p3.Append("Students:")
                .FontSize(12)
                .UnderlineStyle(UnderlineStyle.singleLine)
                .Bold();
            p3.Append(Environment.NewLine);

            var allHomework = weeks.SelectMany(w => w.HomeworkList).ToList();

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

                var strings = new[] { n1, n2, n3 };
                var estrings = new List<string>();

                for (int index = 0; index < strings.Length; index++)
                {
                    string s = strings[index];

                    if (s.Length > tableStringLength)
                    {
                        estrings.Add(s.Substring(0, tableStringLength));
                    }
                    else
                    {
                        estrings.Add(s.PadRight(tableStringLength));
                    }
                }

                homeworkChecklist += estrings[0] + estrings[1] + estrings[2] + Environment.NewLine;
            }



            foreach (var s in cd.studentList)
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

