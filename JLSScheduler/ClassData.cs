using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JLSScheduler
{
    public class ClassData
    {
        public string NTname;
        public string KTname;

        public List<Tuple<string, string>> studentList;

        public int classLevelIndex;
        public int classDayIndex;
        public int classTimeIndex;

        public DateTime semesterStart;
        public DateTime semesterEnd;

        public bool ignoreKoreanHolidays;
        public bool ignoreJLSHolidays;

        public bool weeklyReading;
        public int weeklyReadingCount;
        public bool weeklyListening;
        public int weeklyListeningCount;
        public bool weeklyRecitation;
        public int weeklyRecitationCount;
        public bool weeklyCustom;
        public int weeklyCustomCount;
        public string weeklyCustomText;

        public bool firstPresentation;
        public bool firstPresentationFreeTopic;
        public bool firstPresentationCustomReq;
        public string firstPresentationCustomText;

        public bool secondPresentation;
        public bool secondPresentationFreeTopic;
        public bool secondPresentationCustomReq;
        public string secondPresentationCustomText;

        public bool endOfSemesterReviewDays;

        public List<HomeworkTask> customHomeworkList;
        public List<DateTime> customHolidaysList;

        public ClassData()
        {
            studentList = new List<Tuple<string, string>>();
            customHomeworkList = new List<HomeworkTask>();
            customHolidaysList = new List<DateTime>();
            semesterStart = DateTime.Now;
            semesterEnd = DateTime.Now.AddDays(1);
        }

    }
}
