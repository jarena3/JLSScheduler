using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JLSScheduler.Forms
{
    public partial class CustomHolidayForm : Form
    {
        private Main main;
        private List<DateTime> customHolidays; 

        public CustomHolidayForm(Main m)
        {
            InitializeComponent();
            main = m;
            Init();
        }

        private void Init()
        {
            Calendar.MinDate = main.LoadedClassData.semesterStart;
            Calendar.MaxDate = main.LoadedClassData.semesterEnd;

            customHolidays = main.LoadedClassData.customHolidaysList;

            List<string> holidays = new List<string>();

            foreach (var kvp in ScheduleBuilder.Holidays)
            {
                if (IsDateWithinRange(kvp.Key))
                {
                    holidays.Add(string.Format("{0} - {1}", kvp.Key.ToShortDateString(), kvp.Value));
                }
            }

            AllHolidaysListBox.Items.AddRange(holidays.ToArray());
            UpdateCustomHolidayListBox();
            MarkAllBold();
        }

        private bool IsDateWithinRange (DateTime candidate)
        {
            if (candidate > Calendar.MinDate && candidate < Calendar.MaxDate)
            {
                return true;
            }
            return false;
        }



        private void Calendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            var selection = Calendar.SelectionStart;

            if (customHolidays.Contains(selection))
            {
                customHolidays.Remove(selection);
            }
            else if (!ScheduleBuilder.Holidays.Keys.Contains(selection))
            {
                customHolidays.Add(selection);
            }
            UpdateCustomHolidayListBox();
        }

        private void UpdateCustomHolidayListBox()
        {
            CustomHolidaysListBox.Items.Clear();
            string[] s = customHolidays.Select(a => a.ToShortDateString()).ToArray();

            CustomHolidaysListBox.Items.AddRange(s);
            MarkAllBold();
        }

        private void MarkAllBold()
        {
            var allDates = ScheduleBuilder.Holidays.Keys.Concat(customHolidays).ToArray();
            Calendar.BoldedDates = allDates;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            main.LoadedClassData.customHolidaysList = customHolidays;
            this.Close();
        }
    }
}
