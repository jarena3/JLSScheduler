using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JLSScheduler
{
    public partial class Main : Form
    {
        private List<string> studentList; 


        public Main()
        {
            InitializeComponent();
        }

        private void MainLoad(object sender, EventArgs e)
        {
            studentList = new List<string>();

            ClassLevelComboBox.Items.AddRange(LSClasses.Classes);
                ClassLevelComboBox.SelectedIndex = 0;
            ClassDayComboBox.Items.AddRange(LSClasses.ClassDays);
                ClassDayComboBox.SelectedIndex = 0;

        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            GenerateSchedule();
        }

        private void GenerateSchedule()
        {
            var outputBox = SyllabusPreviewBox.Text;


        }

        private void SetDayTimeRange(object sender, EventArgs e)
        {
            ClassTimeComboBox.Items.Clear();

            var i = ClassDayComboBox.SelectedIndex;
            if (i == 1 || i == 3 || i == 5)
            {
                ClassTimeComboBox.Items.AddRange(LSClasses.TRTimes);
            }
            else
            {
                ClassTimeComboBox.Items.AddRange(LSClasses.MWFTimes);
            }
            ClassTimeComboBox.SelectedIndex = 0;
        }

        private void StudentListAddButton_Click(object sender, EventArgs e)
        {
            using (AddStudentForm dialog = new AddStudentForm())
            {
               DialogResult result = dialog.ShowDialog();

               switch (result)
               {
                // put in how you want the various results to be handled
                // if ok, then something like var x = dialog.MyX;
               }

            }
        }
    }
}
