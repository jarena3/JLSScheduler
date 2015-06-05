using System;
using System.Collections.Generic;


namespace JLSScheduler
{
    public class ClassData
    {
        public string NTname;
        public string KTname;

        public List<Tuple<string, string>> StudentList;

        public string ClassLevel;
        public int ClassLevelIndex;
        public int ClassDayIndex;
        public DateTime ClassTime;

        public DateTime SemesterStart;
        public DateTime SemesterEnd;

        public bool IgnoreKoreanHolidays;
        public bool IgnoreJlsHolidays;

        public bool WeeklyReading;
        public int WeeklyReadingCount = 1;
        public bool WeeklyListening;
        public int WeeklyListeningCount = 1;
        public bool WeeklyRecitation;
        public int WeeklyRecitationCount = 1;
        public bool WeeklyCustom;
        public int WeeklyCustomCount = 1;
        public string WeeklyCustomText;

        public bool FirstPresentation;
        public bool FirstPresentationFreeTopic;
        public bool FirstPresentationCustomReq;
        public string FirstPresentationCustomText;

        public bool SecondPresentation;
        public bool SecondPresentationFreeTopic;
        public bool SecondPresentationCustomReq;
        public string SecondPresentationCustomText;

        public bool EndOfSemesterReviewDays;

        public List<HomeworkTask> CustomHomeworkList;
        public Dictionary<DateTime, string> CustomHolidaysList;
        public string ClassTimeString;

        public ClassData()
        {
            StudentList = new List<Tuple<string, string>>();
            CustomHomeworkList = new List<HomeworkTask>();
            CustomHolidaysList = new Dictionary<DateTime, string>();
            SemesterStart = DateTime.Now;
            SemesterEnd = DateTime.Now.AddDays(1);
        }

    }
}
