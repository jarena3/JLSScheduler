using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace JLSScheduler.Forms
{
    public partial class CustomHomeworkForm : Form
    {
        private readonly Main _main;
        private readonly List<HomeworkTask> _tasks;
        private int _taskIterator;

        public CustomHomeworkForm(Main mainForm)
        {
            InitializeComponent();
            _main = mainForm;
            _tasks = mainForm.LoadedClassData.CustomHomeworkList;
            RefreshTasksList();
        }

        private void OkBTN_Click(object sender, EventArgs e)
        {
            _main.LoadedClassData.CustomHomeworkList = _tasks;
            Close();
        }

        private void RefreshTasksList()
        {
            TasksList.Items.Clear();
            TasksList.Items.AddRange(_tasks.Select(s => s.ToString()).ToArray());
        }

        private void TasksList_SelectedIndexChanged(object sender, EventArgs e)
        {
            _taskIterator = TasksList.SelectedIndex;
            if (_taskIterator > -1)
            {
                SaveBtn.Enabled = true;

                TitleTB.Text = _tasks[_taskIterator].Title;
                DescriptionTB.Text = _tasks[_taskIterator].Body;
                WeekNumberCT.Value = _tasks[_taskIterator].DueWeek;
                RepeatsCB.Checked = _tasks[_taskIterator].Repeats;
                RepeatsCT.Value = _tasks[_taskIterator].RepeatEvery;
            }
            else
            {
                SaveBtn.Enabled = false;
            }
        }

        private void AddNewBTN_Click(object sender, EventArgs e)
        {
            var newTask = new HomeworkTask("New Homework Task", string.Empty, 1);
            if (!string.IsNullOrEmpty(TitleTB.Text))
            {
                newTask.Title = TitleTB.Text;
            }
            if (!string.IsNullOrEmpty(DescriptionTB.Text))
            {
                newTask.Body = DescriptionTB.Text;
            }
            if (WeekNumberCT.Value != 1)
            {
                newTask.DueWeek = (int) WeekNumberCT.Value;
            }
            if (RepeatsCB.Checked)
            {
                newTask.Repeats = true;
                newTask.RepeatEvery = (int) RepeatsCT.Value;
            }

            _tasks.Add(newTask);
            TextBoxesClear();
            RefreshTasksList();
        }

        private void AddEmptyBTN_Click(object sender, EventArgs e)
        {
            var newTask = new HomeworkTask("New Homework Task", string.Empty, 1);

            _tasks.Add(newTask);
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
            RepeatsCB.ForeColor = RepeatsCB.Checked == false ? Color.DarkGray : Color.Black;
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (_taskIterator > -1 && TasksList.Items.Count > 0)
            {
                _tasks[_taskIterator].Title = TitleTB.Text;
                _tasks[_taskIterator].Body = DescriptionTB.Text;
                _tasks[_taskIterator].DueWeek = (int) WeekNumberCT.Value;
                _tasks[_taskIterator].Repeats = RepeatsCB.Checked;
                _tasks[_taskIterator].RepeatEvery = (int) RepeatsCT.Value;
            }

            RefreshTasksList();
        }

        private void DeleteBTN_Click(object sender, EventArgs e)
        {
            if (TasksList.SelectedIndex > -1)
            {
                _tasks.RemoveAt(TasksList.SelectedIndex);
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