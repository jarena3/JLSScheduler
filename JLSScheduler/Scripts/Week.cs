using System;
using System.Collections.Generic;
using System.Linq;

namespace JLSScheduler
{
    /// <summary>
    ///     This holds the week data for the class, as a collection item for the schedule
    ///     Its Print function produces output appropriate for the passed format
    /// </summary>
    internal class Week
    {
        private readonly string _holidayName;
        private readonly int _weekNumber;
        public DateTime DateTime;
        public List<HomeworkTask> HomeworkList;
        public bool IsHoliday;

        public string Subtitle;
        public string Title;

        /// <summary>
        ///     Not a holiday. Not a presentation week.
        /// </summary>
        public Week(int weekNumber, DateTime date)
        {
            HomeworkList = new List<HomeworkTask>();
            DateTime = date;
            _weekNumber = weekNumber;
        }

        /// <summary>
        ///     A holiday
        /// </summary>
        public Week(DateTime date, string holidayName)
        {
            DateTime = date;
            HomeworkList = new List<HomeworkTask>();
            IsHoliday = true;
            _holidayName = holidayName;
        }

        public int WeekNumber
        {
            get { return _weekNumber; }
        }

        public string Date
        {
            get { return DateTime.ToString("D"); }
        }

        public string HolidayTitle
        {
            get
            {
                if (!string.IsNullOrEmpty(_holidayName))
                {
                    return _holidayName;
                }

                return "No Class";
            }
        }

        public void AddHomework(HomeworkTask homeworkItem)
        {
            HomeworkList.Add(homeworkItem);
        }

        public override string ToString()
        {
            string output = Title + Environment.NewLine + Subtitle + Environment.NewLine;
            return HomeworkList.Aggregate(output,
                (current, hw) => current + (hw.Title + Environment.NewLine + "  - " + hw.Body + Environment.NewLine));
        }
    }
}