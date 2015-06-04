using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using JLSScheduler.Properties;
using Newtonsoft.Json;

namespace JLSScheduler
{
    internal static class ScheduleBuilder
    {
        private static Book[] Books;
        public static Dictionary<DateTime, string> Holidays;

        private static DayOfWeek _classWeekday;
        private static string _classTime;
        private static string _classLevel;
        private static Book _classBook;


        public static void Init()
        {
            //get books into our array
            Books = JsonConvert.DeserializeObject<Book[]>(Resources.Books);

            //get the holiday lists into our dictionary
            var stringDKr = JsonConvert.DeserializeObject<Dictionary<string, string>>(Resources.KRHolidays);
            var stringDJls = JsonConvert.DeserializeObject<Dictionary<string, string>>(Resources.JLSHolidays);
            var stringDict = stringDKr.Concat(stringDJls.Where(kvp => !stringDKr.ContainsKey(kvp.Key)));

            Holidays = new Dictionary<DateTime, string>();

            foreach (var kvp in stringDict)
            {
                Holidays.Add(DateTime.Parse(kvp.Key), kvp.Value);
            }
        }

        public static IEnumerable<string> BuildPreviewSchedule(ClassData cd)
        {
            return BuildWeeksList(cd).Select(w => w.ToString()).ToList();
        }


        public static List<Week> BuildWeeksList(ClassData cd)
        {
            var classWeeks = new List<Week>();

            //collect form data for class information
            _classWeekday = GetWeekday(cd.classDayIndex);
            _classLevel = cd.classLevel;
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
            foreach (DateTime d in days)
            {
                //collect holidays
                foreach (DateTime hDt in Holidays.Keys)
                {
                    if (hDt.Date == d.Date)
                    {
                        classWeeks.Add(new Week(d, Holidays[hDt]));
                        Debug.WriteLine("holiday added");
                    }
                }

                foreach (DateTime cDt in cd.customHolidaysList.Keys)
                {
                    if (cDt.Date == d.Date)
                    {
                        classWeeks.Add(new Week(d, cd.customHolidaysList[cDt]));
                        Debug.WriteLine("custom holiday added");
                    }
                }

                //add working class weeks
                classWeeks.Add(new Week(classIterator, d, _classBook));
                classIterator++;
            }

            //HACK: iterate once more to remove duplicate days (holiday/class day repeats)
            classWeeks = classWeeks.GroupBy(w => w.Date).Select(sel => sel.First()).ToList();

            PopulateClassWeeks(classWeeks, cd);

            return classWeeks;
        }

        private static void PopulateClassWeeks(List<Week> weeks, ClassData cd)
        {
            int chapterIterator = 1;

            weeks.Sort((x, y) => DateTime.Compare(x.DateTime, y.DateTime));

            foreach (Week w in weeks)
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
                        w.Title = string.Format("{0}, {1}", w.Date, _classTime);
                        w.Subtitle = string.Format("Unit {0} : {1}", chapterIterator,
                            _classBook.Units[chapterIterator.ToString(CultureInfo.InvariantCulture)]);

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
                                w.Subtitle += " " + cd.firstPresentationCustomText;
                            }
                        }
                        //likewise, check for second presentation
                        if (w.WeekNumber == 8 && cd.secondPresentation)
                        {
                            w.Title = string.Format("{0}, {1} - Presentation #2", w.Date, _classTime);
                            w.Subtitle = "Speaking Tree Topic Presentation";
                            if (cd.secondPresentationFreeTopic)
                            {
                                w.Subtitle = "Free Topic Presentation";
                            }
                            if (cd.secondPresentationCustomReq)
                            {
                                w.Subtitle += " " + cd.secondPresentationCustomText;
                            }
                        }

                        //now, we have to make sure presentation homework overrides automated homework additions
                        //these are presentation weeks -1, because we're dealing in due dates now
                        if (w.WeekNumber == 3 && cd.firstPresentation)
                        {
                            w.Title = string.Format("{0}, {1}", w.Date, _classTime);
                            w.Subtitle = string.Format("Unit {0} : {1}", chapterIterator,
                                _classBook.Units[chapterIterator.ToString(CultureInfo.InvariantCulture)]);

                            w.AddHomework(new HomeworkTask("Prepare Presentation #1", "", 3));
                        }
                            //likewise, check for second presentation
                        else if (w.WeekNumber == 7 && cd.secondPresentation)
                        {
                            w.Title = string.Format("{0}, {1}", w.Date, _classTime);
                            w.Subtitle = string.Format("Unit {0} : {1}", chapterIterator,
                                _classBook.Units[chapterIterator.ToString(CultureInfo.InvariantCulture)]);

                            w.AddHomework(new HomeworkTask("Prepare Presentation #2", "", 7));
                        }
                            //if neither, populate as an ST week and increment chapter
                        else
                        {
                            //ensure that there's a next chapter to prepare for
                            if (chapterIterator + 1 < 9)
                            {
                                //add weekly homework, if it exists
                                if (cd.weeklyReading)
                                {
                                    w.AddHomework(new HomeworkTask("Weekly Reading",
                                        string.Format("Speaking Tree Chapter {0}: {1}, {2} time(s)",
                                            chapterIterator + 1,
                                            _classBook.Units[
                                                (chapterIterator + 1).ToString(CultureInfo.InvariantCulture)],
                                            cd.weeklyReadingCount),
                                        w.WeekNumber));
                                }

                                if (cd.weeklyListening)
                                {
                                    w.AddHomework(new HomeworkTask("Weekly Listening",
                                        string.Format("Speaking Tree Chapter {0}: {1}, {2} time(s)",
                                            chapterIterator + 1,
                                            _classBook.Units[
                                                (chapterIterator + 1).ToString(CultureInfo.InvariantCulture)],
                                            cd.weeklyListeningCount),
                                        w.WeekNumber));
                                }

                                if (cd.weeklyRecitation)
                                {
                                    w.AddHomework(new HomeworkTask("Weekly Speaking",
                                        string.Format("Speaking Tree Chapter {0}: {1}, {2} time(s)",
                                            chapterIterator + 1,
                                            _classBook.Units[
                                                (chapterIterator + 1).ToString(CultureInfo.InvariantCulture)],
                                            cd.weeklyRecitationCount),
                                        w.WeekNumber));
                                }

                                if (cd.weeklyCustom)
                                {
                                    w.AddHomework(
                                        new HomeworkTask(
                                            string.Format("{0}, {1} times", cd.weeklyCustomText, cd.weeklyCustomCount),
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
            int semesterLength = (((cd.semesterEnd - cd.semesterStart).Days)/7) + 1;
            Debug.WriteLine("semester length:" + semesterLength);
            foreach (HomeworkTask hwt in cd.customHomeworkList)
            {
                if (hwt.Repeats)
                {
                    Debug.WriteLine("homework repeats...");
                    for (int i = hwt.DueWeek; i <= semesterLength; i += hwt.RepeatEvery)
                    {
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