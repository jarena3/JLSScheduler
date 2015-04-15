namespace JLSScheduler
{
    public class HomeworkTask
    {
        public string Body;
        public int DueWeek;
        public int RepeatEvery;
        public bool Repeats;
        public string Title;

        public HomeworkTask(string title, string body, int week)
        {
            Title = title;
            DueWeek = week;
            Body = body;
            Repeats = false;
            RepeatEvery = 1;
        }


        public override string ToString()
        {
            return Title + ", due week " + DueWeek;
        }
    }
}