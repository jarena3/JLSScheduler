using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Novacode;

namespace JLSScheduler
{
    internal class DOCExportWriter
    {
        const int tableStringLength = 30;

        private static string classDayString;
        private static string classLevelString;
        private static string classTimeString;

        public static void WriteToDOC(ClassData cd, List<Week> weeks, string savePath)
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
 //           WriteKTPage(weeks, folder);
 //           WriteStudentPages(weeks, folder);

        }

        private static void WriteNTPage(List<Week> weeks, DirectoryInfo folder, ClassData cd)
        {

            using (DocX doc = DocX.Create(folder.FullName + "/NT_" + classDayString + "_" + classLevelString + "_" + classTimeString + "_" + ".docx"))
            {
                Paragraph p1 = doc.InsertParagraph();
                p1.Append(string.Format("{0}, {1} : {2}", classDayString, cd.classTimeString, classLevelString))
                    .FontSize(14)
                    .Bold();
                p1.Append(Environment.NewLine);
                p1.Append(string.Format("NT: {0} / KT: {1}", cd.NTname, cd.KTname))
                    .FontSize(11)
                    .Color(Color.DarkGray);

                Paragraph p2 = doc.InsertParagraph();
                p2.Append("Schedule:")
                    .FontSize(12)
                    .Bold();
                p2.Append(Environment.NewLine);

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
                        p2.Append(string.Format("Week {0} ({1})", week.WeekNumber, week.Date))
                            .FontSize(11)
                            .Bold();
                        p2.Append(Environment.NewLine);

                    }
                    foreach (var hw in week.HomeworkList)
                    {
                        p2.Append(hw.Title);
                        p2.Append("   " + hw.Body)
                            .Italic();
                    }
                    p2.Append(Environment.NewLine);
                }

                p2.Append(Environment.NewLine);


                Paragraph p3 = doc.InsertParagraph();

                p3.Append("Students:")
                    .FontSize(12)
                    .Bold();
                p3.Append(Environment.NewLine);

                var allHomework = weeks.SelectMany(w => w.HomeworkList).ToList();

                string homeworkChecklist = string.Empty;

                for (int i = 0; i < allHomework.Count; i++)
                {
                    string n1 = "□  " + allHomework[i].Title;
                    string n2, n3;

                    if (i + 2 > allHomework.Count || i + 3 > allHomework.Count)
                    {
                        n2 = n3 = string.Empty;
                    }
                    else
                    {
                        n2 = "□  " + allHomework[i + 1].Title;
                        n3 = "□  " + allHomework[i + 2].Title;
                    }

                    var strings = new[] {n1, n2, n3};

                    for (int index = 0; index < strings.Length; index++)
                    {
                        string s = strings[index];

                        if (s.Length > tableStringLength)
                        {
                            s = s.Substring(0, tableStringLength);
                        }
                        else
                        {
                            if (s.Length < tableStringLength)
                            {
                                s = s.PadRight(tableStringLength);
                            }
                        }
                    }

                    homeworkChecklist += strings[0] + strings[1] + strings[2] + Environment.NewLine;
                }
                    
   

                foreach (var s in cd.studentList)
                {
                    p3.Append(string.Format("{0} ({1})", s.Item1, s.Item2))
                        .FontSize(10);
                    p3.Append(Environment.NewLine);

                    p3.Append(homeworkChecklist)
                        .FontSize(9);
                    p3.Append(Environment.NewLine);


                }



                doc.Save();
                Debug.WriteLine("Created in: " + folder);

                }


            }


        }
    }

