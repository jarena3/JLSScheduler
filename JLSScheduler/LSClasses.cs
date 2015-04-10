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

}
