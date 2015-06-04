using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace JLSScheduler.Forms
{
    public partial class CustomHolidayForm : Form
    {
        private readonly Main _main;
        private Dictionary<DateTime, string> _customHolidays;
        private List<DateTime> _allHolidays; 

        public CustomHolidayForm(Main m)
        {
            InitializeComponent();
            _main = m;
            Init();
        }

        private void Init()
        {
            Calendar.MinDate = _main.LoadedClassData.semesterStart;
            Calendar.MaxDate = _main.LoadedClassData.semesterEnd;

            _customHolidays = _main.LoadedClassData.customHolidaysList;

            _allHolidays = new List<DateTime>();
            var holidays = new List<string>();

            foreach (var kvp in ScheduleBuilder.Holidays.Where(kvp => IsDateWithinRange(kvp.Key)))
            {
                holidays.Add(string.Format("{0} - {1}", kvp.Key.ToShortDateString(), kvp.Value));
                _allHolidays.Add(kvp.Key);
            }

            AllHolidaysListBox.Items.AddRange(holidays.ToArray());
            UpdateCustomHolidayListBox();
        }

        private bool IsDateWithinRange (DateTime candidate)
        {
            return candidate > Calendar.MinDate && candidate < Calendar.MaxDate;
        }


        private void Calendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            var selection = Calendar.SelectionStart;

            if (_customHolidays.Keys.Contains(selection))
            {
                _customHolidays.Remove(selection);
                _allHolidays.Remove(selection);
            }
            else if (!ScheduleBuilder.Holidays.Keys.Contains(selection))
            {
                _customHolidays.Add(selection, "No Class");
                _allHolidays.Add(selection);
            }
            UpdateCustomHolidayListBox();
            MarkAllBold();
        }

        private void UpdateCustomHolidayListBox()
        {
            CustomHolidaysListBox.Items.Clear();
            var s = _customHolidays.Keys.Select(a => a.ToShortDateString()).ToArray();

            CustomHolidaysListBox.Items.AddRange(s);
        }

        private void MarkAllBold()
        {
            Calendar.BeginInvoke(new MethodInvoker(CalendarMarkWorkaround));
        }

        private void CalendarMarkWorkaround()
        {
            Calendar.BoldedDates = _allHolidays.ToArray();  
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _main.LoadedClassData.customHolidaysList = _customHolidays;
            Close();
        }
    }
}
