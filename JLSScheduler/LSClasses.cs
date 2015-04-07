using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JLSScheduler
{
    public static class LSClasses
    {
        public static string[] Classes()
        {
            return JsonConvert.DeserializeObject<string[]>(Properties.Resources.ClassLevels);
        }

        public static string[] ClassDays = 
        {
            "MON","TUE","WED","THU","FRI"
        };

        public static string[] MWFTimes()
        {
            return JsonConvert.DeserializeObject<string[]>(Properties.Resources.MWFTimes);
        }

        public static string[] TRTimes() 
        {
            return JsonConvert.DeserializeObject<string[]>(Properties.Resources.TRTimes);
        }
    }

    public static class LSBooks
    {
        //import these from external data

        public static Dictionary<int, string> LSA1;
        public static Dictionary<int, string> LSA2;
        public static Dictionary<int, string> LSB1;
        public static Dictionary<int, string> LSB2;
        public static Dictionary<int, string> LSC1;
        public static Dictionary<int, string> LSC2;
        public static Dictionary<int, string> LSD1;
        public static Dictionary<int, string> LSD2;
        }
}
