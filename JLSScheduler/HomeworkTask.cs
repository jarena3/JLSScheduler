using System;

namespace JLSScheduler
{
    public class HomeworkTask
    {
        public int DueWeek;
        public string Title;
        public string Body;
        public bool Repeats;
        public int RepeatEvery;

        public HomeworkTask(string title, int week)
        {
            this.Title = title;
            this.DueWeek = week;
            this.Body = string.Empty;
            this.Repeats = false;
            this.RepeatEvery = 1;
        }


        public override string ToString()
        {
            return Title + ", due week " + DueWeek;
        }

    }
}