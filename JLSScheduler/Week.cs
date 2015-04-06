using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JLSScheduler
{
    /// <summary>
    /// This holds the week data for the class, as a collection item for the schedule
    /// Its Print function produces output appropriate for the passed format
    /// </summary>
    class Week
    {
        public Week(DateTime date, string time, string LSclass, Dictionary<int, string>LSBook, int weekNumber)
        {
            this._homeworkList = new List<string>();
            this._date = date;
            this._time = time;
            this._book = LSBook;
            this._lsClass = LSclass;
            this._weekNumber = weekNumber;
        }
        
        
        private DateTime _date;
        private string _time;
        private Dictionary<int, string> _book; 

        private List<string> _homeworkList;
        private int _weekNumber;
        private string _lsClass;

        public string Date { get { return _date.ToString("U"); } }
        public string Time { get { return _time; } }


        public string Title
        {
            get { return string.Format("{0} - {1}", this.Date, this.Time); }
        }

        public string Subtitle
        {
            get { return string.Format("Speaking Tree {0}, Unit {1}", _lsClass, _book[_weekNumber]);}
        }

        public string Homework
        {
            get
            {
                if (_homeworkList.Count > 0)
                {
                    string hw = string.Empty;
                    foreach (string s in _homeworkList)
                    {
                        hw += "+ " + s;
                    }
                    return hw;
                }

                return "No homework this week.";

            }
        }

        public void AddHomework(string homeworkItem)
        {
            _homeworkList.Add(homeworkItem);
        }

    }
}
