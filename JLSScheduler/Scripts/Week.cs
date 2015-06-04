using System;
using System.Collections.Generic;
using System.Linq;

namespace JLSScheduler
{
    /// <summary>
    /// This holds the week data for the class, as a collection item for the schedule
    /// Its Print function produces output appropriate for the passed format
    /// </summary>
    class Week
    {
        /// <summary>
        /// Not a holiday. Not a presentation week.
        /// </summary>
        public Week(int weekNumber, DateTime date, Book LSBook)
        {
            this.HomeworkList = new List<HomeworkTask>();
            this.DateTime = date;
            this._book = LSBook;
            this._weekNumber = weekNumber;
        }

        /// <summary>
        /// A holiday
        /// </summary>
        public Week(DateTime date, string holidayName)
        {
            DateTime = date;
            HomeworkList = new List<HomeworkTask>();
            IsHoliday = true;
            _holidayName = holidayName;
        }

        /// <summary>
        /// A presentation week
        /// </summary>
        public Week(DateTime date, bool firstPresentation)
        {
            DateTime = date;
            HomeworkList = new List<HomeworkTask>();
            PresentationNumber = firstPresentation ? 1 : 2;

        }
          
        public DateTime DateTime;
        private Book _book;
        public bool IsHoliday;

        public int PresentationNumber;

        public int WeekNumber { get { return _weekNumber; } }

        public List<HomeworkTask> HomeworkList;
        private int _weekNumber;
        private string _lsClass;

        public string Date { get { return DateTime.ToString("D"); } }

        private string _holidayName;

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

        public string Title;

        public string Subtitle;

        public void AddHomework(HomeworkTask homeworkItem)
        {
            HomeworkList.Add(homeworkItem);
        }

        public override string ToString()
        {
            string output = Title + Environment.NewLine + Subtitle + Environment.NewLine;
            return HomeworkList.Aggregate(output, (current, hw) => current + (hw.Title + Environment.NewLine + "  - " + hw.Body + Environment.NewLine));
        }
    }
}
