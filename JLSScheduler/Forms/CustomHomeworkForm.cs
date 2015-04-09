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
    public partial class CustomHomeworkForm : Form
    {
        private List<HomeworkTask> tasks;
        private Main _main;
        private int taskIterator;

        public CustomHomeworkForm(Main mainForm)
        {
            InitializeComponent();
            _main = mainForm;
            tasks = mainForm.LoadedClassData.customHomeworkList;
            RefreshTasksList();
        }

        private void OkBTN_Click(object sender, EventArgs e)
        {
            _main.LoadedClassData.customHomeworkList = tasks;
            this.Close();
        }

        void RefreshTasksList()
        {
            TasksList.Items.Clear();
            TasksList.Items.AddRange(tasks.Select(s => s.ToString()).ToArray());
        }

        private void TasksList_SelectedIndexChanged(object sender, EventArgs e)
        {
            taskIterator = TasksList.SelectedIndex;
            if (taskIterator > -1)
            {
                SaveBtn.Enabled = true;

                TitleTB.Text = tasks[taskIterator].Title;
                DescriptionTB.Text = tasks[taskIterator].Body;
                WeekNumberCT.Value = tasks[taskIterator].DueWeek;
                RepeatsCB.Checked = tasks[taskIterator].Repeats;
                RepeatsCT.Value = tasks[taskIterator].RepeatEvery;
            }
            else
            {
                SaveBtn.Enabled = false;
            }
        }

        private void AddNewBTN_Click(object sender, EventArgs e)
        {
            tasks.Add(new HomeworkTask("New Homework Task", 1));
            TextBoxesClear();
            RefreshTasksList();
        }

        private void TextBoxesClear()
        {
            TitleTB.Text = string.Empty;
            DescriptionTB.Text = string.Empty;
            RepeatsCB.Checked = false;
            RepeatsCT.Value = 1;
            WeekNumberCT.Value = 1;
        }

        private void RepeatsCB_CheckedChanged(object sender, EventArgs e)
        {
            if (RepeatsCB.Checked == false)
            {
                RepeatsCB.ForeColor = Color.DarkGray;
            }
            else
            {
                RepeatsCB.ForeColor = Color.Black;
            }
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (taskIterator > -1 && TasksList.Items.Count > 0)
            {
                tasks[taskIterator].Title = TitleTB.Text;
                tasks[taskIterator].Body = DescriptionTB.Text;
                tasks[taskIterator].DueWeek = (int) WeekNumberCT.Value;
                tasks[taskIterator].Repeats = RepeatsCB.Checked;
                tasks[taskIterator].RepeatEvery = (int) RepeatsCT.Value;
            }

            RefreshTasksList();
        }

        private void DeleteBTN_Click(object sender, EventArgs e)
        {
            if (TasksList.SelectedIndex > -1)
            {
                tasks.RemoveAt(TasksList.SelectedIndex);
                TextBoxesClear();
            }
            RefreshTasksList();
            if (TasksList.Items.Count == 0)
            {
                SaveBtn.Enabled = false;
            }
        }


    }
}
