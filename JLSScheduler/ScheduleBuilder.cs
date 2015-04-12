using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace JLSScheduler
{
    internal static class ScheduleBuilder
    {
        public static Book[] Books;
        public static Dictionary<DateTime, string> Holidays;

        private static DayOfWeek _classWeekday;
        private static string _classTime;
        private static string _classLevel;
        private static Book _classBook;


        public static void Init()
        {
            //get books into our array
            Books = JsonConvert.DeserializeObject<Book[]>(Properties.Resources.Books);

            //get the holiday lists into our dictionary
            var stringD_KR = JsonConvert.DeserializeObject<Dictionary<string, string>>(Properties.Resources.KRHolidays);
            var stringD_JLS = JsonConvert.DeserializeObject<Dictionary<string, string>>(Properties.Resources.JLSHolidays);
            var stringDict = stringD_KR.Concat(stringD_JLS.Where(kvp => !stringD_KR.ContainsKey(kvp.Key)));

            Holidays = new Dictionary<DateTime, string>();
            foreach (var kvp in stringDict)
            {
                Holidays.Add(DateTime.Parse(kvp.Key), kvp.Value);
            }

        }

        public static List<string> BuildPreviewSchedule(ClassData cd)
        {
            List<string> output = new List<string>();

            var weeks = BuildWeeksList(cd);

            foreach (Week w in weeks)
            {
               output.Add(w.ToString());
            }


            return output;

        }


        public static List<Week> BuildWeeksList(ClassData cd)
        {
            var classWeeks = new List<Week>();

            //collect form data for class information
            _classWeekday = GetWeekday(cd.classDayIndex);
            _classLevel = LSClasses.Classes()[cd.classLevelIndex];
            _classBook = Books.Single(s => s.Title == _classLevel);
            _classTime = cd.classTimeString;    
            
            //first, get a list of all days between our range
            List<DateTime> rawdays = Enumerable.Range(0, 1 + cd.semesterEnd.Subtract(cd.semesterStart).Days)
                .Select(offset => cd.semesterStart.AddDays(offset))
                .ToList();

            //discard anything that isn't the right weekday.
            List<DateTime> days = rawdays.Where(d => d.DayOfWeek == _classWeekday).ToList();

            //now build a list of class weeks
            int classIterator = 1;
            foreach (var d in days)
            {
                //collect holidays
                foreach (DateTime hDT in Holidays.Keys)
                {
                    if (hDT.Date == d.Date)
                    {
                        classWeeks.Add(new Week(d, Holidays[hDT]));
                        Debug.WriteLine("holiday added");
                        continue;
                    }
                }

                foreach (DateTime cDT in cd.customHolidaysList.Keys)
                {
                    if (cDT.Date == d.Date)
                    {
                        classWeeks.Add(new Week(d, cd.customHolidaysList[cDT]));
                        Debug.WriteLine("custom holiday added");
                        continue;
                    }
                }

                //add working class weeks
                classWeeks.Add(new Week(classIterator, d, _classBook));
                classIterator++;
              
            }

            PopulateClassWeeks(classWeeks, cd);

            return classWeeks;
        }

        private static void PopulateClassWeeks(List<Week> weeks, ClassData cd)
        {
            int chapterIterator = 1;

            weeks.Sort((x, y) => DateTime.Compare(x.DateTime, y.DateTime));

            foreach (var w in weeks)
            {
                if (w.IsHoliday)
                {
                    w.Title = string.Format("{0} - {1}", w.Date, w.HolidayTitle);
                    w.Subtitle = string.Empty;
                }
                else
                {
                    //put the working weeks together
                    //if we're within the ST time range
                    if (chapterIterator <= 8)
                    {
                        //check for first presentation (no overlap in ST chapters)
                        if (w.WeekNumber == 4 && cd.firstPresentation)
                        {
                            w.Title = string.Format("{0}, {1} - Presentation #1", w.Date, _classTime);
                            w.Subtitle = "Speaking Tree Topic Presentation";
                            if (cd.firstPresentationFreeTopic)
                            {
                                w.Subtitle = "Free Topic Presentation";
                            }
                            if (cd.firstPresentationCustomReq)
                            {
                                w.Subtitle += "  -  " + cd.firstPresentationCustomText;
                            }
                        }
                            //likewise, check for second presentation
                        else if (w.WeekNumber == 8 && cd.secondPresentation)
                        {
                            w.Title = string.Format("{0}, {1} - Presentation #2", w.Date, _classTime);
                            w.Subtitle = "Speaking Tree Topic Presentation";
                            if (cd.secondPresentationFreeTopic)
                            {
                                w.Subtitle = "Free Topic Presentation";
                            }
                            if (cd.secondPresentationCustomReq)
                            {
                                w.Subtitle += "  -  " + cd.secondPresentationCustomText;
                            }
                        }
                            //if neither, populate as an ST week and increment chapter
                        else
                        {
                            w.Title = string.Format("{0}, {1}", w.Date, _classTime);
                            w.Subtitle = string.Format("Unit {0} : {1}", chapterIterator,
                                _classBook.Units[chapterIterator.ToString()]);


                            //ensure that there's a next chapter to prepare for
                            if (chapterIterator + 1 < 9)
                            {
                                //add weekly homework, if it exists
                                if (cd.weeklyReading)
                                {
                                    w.AddHomework(new HomeworkTask("Weekly Reading",
                                        string.Format("- Reading: Speaking Tree Chapter {0}:{1}, {2} times",
                                            chapterIterator,
                                            _classBook.Units[chapterIterator.ToString()], cd.weeklyReadingCount),
                                        w.WeekNumber));
                                }

                                if (cd.weeklyListening)
                                {
                                    w.AddHomework(new HomeworkTask("Weekly Listening",
                                        string.Format("- Listening: Speaking Tree Chapter {0}:{1}, {2} times",
                                            chapterIterator,
                                            _classBook.Units[chapterIterator.ToString()], cd.weeklyListeningCount),
                                        w.WeekNumber));
                                }

                                if (cd.weeklyRecitation)
                                {
                                    w.AddHomework(new HomeworkTask("Weekly Speaking",
                                        string.Format("- Recitation: Speaking Tree Chapter {0}:{1}, {2} times",
                                            chapterIterator,
                                            _classBook.Units[chapterIterator.ToString()], cd.weeklyRecitationCount),
                                        w.WeekNumber));
                                }

                                if (cd.weeklyCustom)
                                {
                                    w.AddHomework(
                                        new HomeworkTask(
                                            string.Format("- {0}, {1} times", cd.weeklyCustomText, cd.weeklyCustomCount),
                                            string.Empty, w.WeekNumber));
                                }


                            }

                            chapterIterator++;



                        }
                    }
                        //now, if we're done with the book...
                    else
                    {
                        w.Title = string.Format("{0}, {1}", w.Date, _classTime);
                        if (cd.endOfSemesterReviewDays)
                        {
                            w.Subtitle = "Review Day";
                        }
                    }

                }


            }

            //check the custom homework list, and add any applicable ones to the homework list
            var customHomeworkAdds = new List<HomeworkTask>();
            int semesterLength = (((cd.semesterEnd - cd.semesterStart).Days) / 7) + 1;
            Debug.WriteLine("semester length:" + semesterLength);
            foreach (var hwt in cd.customHomeworkList)
            {
                if (hwt.Repeats)
                {
                    Debug.WriteLine("homework repeats...");
                    for (int i = hwt.DueWeek; i <= semesterLength; i += hwt.RepeatEvery)
                    {
                        Debug.WriteLine("adding homework repition at week " + i);
                        customHomeworkAdds.Add(CopyHomeworkTask(hwt, i));
                    }
                }
                else
                {
                    customHomeworkAdds.Add(hwt);
                }
            }

            foreach (HomeworkTask hw in customHomeworkAdds)
            {
                foreach (Week wk in weeks.Where(wk => hw.DueWeek == wk.WeekNumber))
                {
                    wk.AddHomework(hw);   
                }
            }
        }

        private static HomeworkTask CopyHomeworkTask(HomeworkTask hwt, int weekNumber)
        {
            return new HomeworkTask(hwt.Title, hwt.Body, weekNumber);
        }


        public static DayOfWeek GetWeekday(int cdi)
        {
            switch (cdi)
            {
                case (0):
                    return DayOfWeek.Monday;
                case (1):
                    return DayOfWeek.Tuesday;
                case (2):
                    return DayOfWeek.Wednesday;
                case (3):
                    return DayOfWeek.Thursday;
                case (4):
                    return DayOfWeek.Friday;
            }

            //default
            return DayOfWeek.Monday;
        }

    }
}
