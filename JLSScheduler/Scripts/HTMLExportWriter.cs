using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JLSScheduler
{
    internal static class HTMLExportWriter
    {

        private static string classDayString;
        private static string classLevelString;

        public static void WriteToHtml(ClassData cd, List<Week> weeks, string savePath)
        {
            classDayString = ScheduleBuilder.GetWeekday(cd.classDayIndex).ToString();
            classLevelString = LSClasses.Classes()[cd.classLevelIndex];

            var sanitizedTime = new string((from c in cd.classTimeString
                where char.IsWhiteSpace(c) || char.IsLetterOrDigit(c)
                select c
                ).ToArray()).Insert(4, "_");

            var folder = Directory.CreateDirectory(savePath + "/" + classDayString + "_" +
                                      sanitizedTime);

            

            WriteNTPage(weeks, folder, cd);
            WriteKTPage(weeks, folder);
            WriteStudentPages(weeks, folder);

        }

        private static void WriteNTPage(List<Week> weeks, DirectoryInfo folder, ClassData cd)
        {
            using (FileStream fs = new FileStream(folder.FullName + "/NT.htm", FileMode.Create))
            {
                using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                {
                    w.Write(string.Format("<h1>{0}, {1} : {2}</h1>", classDayString, cd.classTimeString, classLevelString));
                    w.Write(string.Format("<h6>NT: {0} / KT: {1}<h6>", cd.NTname, cd.KTname));
                    w.Write("<hr>");

                    w.Write("<h3>Schedule:<h3>");
                    foreach (Week week in weeks)
                    {
                        if (week.IsHoliday)
                        {
                            w.Write(string.Format("<div class=\"WeekTitle\">{0}</div>", week.HolidayTitle));  
                        }
                        else
                        {
                            w.Write(string.Format("<div class=\"WeekTitle\">Week {0} ({1})</div>", week.WeekNumber, week.Date));  
                        }
                        foreach (var hw in week.HomeworkList)
                        {
                            w.Write(string.Format("<div class=\"HomeworkTitle\">{0}</div>", hw.Title));
                            w.Write(string.Format("<p>{0}</p>", hw.Body));
                        }
                    }

                    w.Write("<hr>");

                    w.Write("<h3>Students:<h3>");
                    foreach (var s in cd.studentList)
                    {
                        w.Write("<TABLE>");
                        w.Write(string.Format("<CAPTION>{0} ({1})</CAPTION>", s.Item1, s.Item2));

                        w.Write("<TR>");

                        var hw1 = weeks.Aggregate(string.Empty,
                            (current1, week) =>
                                week.HomeworkList.Aggregate(current1,
                                    (current, hw) => current + string.Format("<TD>{0}</TD>", hw.Title)));
                        w.Write(string.Format("<p>{0}</p>", hw1));

                        w.Write("</TR>");
                        w.Write("<TR>");

                        var hw2 = weeks.Aggregate(string.Empty,
                            (current1, week) =>
                                week.HomeworkList.Aggregate(current1,
                                    (current, hw) => current + string.Format("<TD> </TD>")));
                        w.Write(string.Format("<p>{0}</p>", hw2));

                        w.Write("<TR>");

                    }


                }
            }
        }

        private static void WriteKTPage(List<Week> weeks, DirectoryInfo folder)
        {
            using (FileStream fs = new FileStream(folder.FullName + "/KT.htm", FileMode.Create))
            {
                using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                {
                    w.WriteLine("<H1>Hello</H1>");
                }
            }
        }

        private static void WriteStudentPages(List<Week> weeks, DirectoryInfo folder)
        {
            using (FileStream fs = new FileStream(folder.FullName + "/Student_Handouts.htm", FileMode.Create))
            {
                using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                {
                    w.WriteLine("<H1>Hello</H1>");
                }
            }
        }

        /* Should be written into the student pages (syllabus-homework sheet-parent pages)
        private static void WriteParentPages(List<Week> weeks, DirectoryInfo folder)
        {
            using (FileStream fs = new FileStream(folder.FullName + "/test.htm", FileMode.Create))
            {
                using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                {
                    w.WriteLine("<H1>Hello</H1>");
                }
            }
        }*/


    }
}
