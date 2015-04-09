using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JLSScheduler
{
    static class ScheduleBuilder
    {
        public static Book[] Books;
        public static Dictionary<DateTime, string> Holidays;

        public static void Init()
        {
            //get books into our array
            Books = JsonConvert.DeserializeObject<Book[]>(Properties.Resources.Books);

            //get the holiday lists into our dictionary
            var stringD_KR = JsonConvert.DeserializeObject<Dictionary<string, string>>(Properties.Resources.KRHolidays);
            var stringD_JLS = JsonConvert.DeserializeObject<Dictionary<string,string>>(Properties.Resources.JLSHolidays);
            var stringDict = stringD_KR.Concat(stringD_JLS.Where(kvp => !stringD_KR.ContainsKey(kvp.Key)));

            Holidays = new Dictionary<DateTime, string>();
            foreach (var kvp in stringDict)
            {
                Holidays.Add(DateTime.Parse(kvp.Key), kvp.Value);
            }
            
        }

        public static List<HomeworkTask> BuildPreviewSchedule(ClassData cd)
        {
            var output = new List<HomeworkTask>();
            //first, get a list of all days between our range
            List<DateTime> days = Enumerable.Range(0, 1 + cd.semesterEnd.Subtract(cd.semesterStart).Days)
                              .Select(offset => cd.semesterStart.AddDays(offset))
                              .ToList(); 
            //now, discard anything that isn't the right weekday.


            return output;
        }
    }
}
