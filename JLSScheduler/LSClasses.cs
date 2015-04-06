using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JLSScheduler
{
    public static class LSClasses
    {
        public static string[] Classes = 
        {
            "DSC","DSD","LSA-1","LSA-2","LSB-1","LSB-2","LSC-1","LSC-2","LSD-1","LSD-2",
            "MSA1","MSA-2","MSB-1","MSB2"
        } ;
        public static string[] ClassDays = 
        {
            "MON","TUE","WED","THU","FRI"
        };
        public static string[] MWFTimes = 
        {
            "","2:30-3:10", "3:10-3:50","3:55-4:45","4:45-5:35","5:40-6:30","6:30-7:20","7:20-7:40","7:45-8:35","8:35-9:45"
        };
        public static string[] TRTimes = 
        {
            "","3:50-4:40","4:40-5:30","5:30-6:20","6:25-7:15","7:15-8:05","8:05-9:25"
        };
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
