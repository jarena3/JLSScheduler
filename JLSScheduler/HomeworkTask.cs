using System;

namespace JLSScheduler
{
    public class HomeworkTask
    {
        public DateTime DueDate;
        public string Title;
        public string Body;
        public bool Repeats;
        public int RepeatEvery;

        public override string ToString()
        {
            return Title +" | " + DueDate.ToShortDateString();
        }
    }
}