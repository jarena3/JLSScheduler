using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using JLSScheduler.Forms;
using LinqToExcel;
using Newtonsoft.Json;

namespace JLSScheduler
{
    public partial class Main : Form
    {
        public ClassData LoadedClassData;

        #region init

        public Main()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            InitializeComponent();
            //deserialize the book lists for later use
            ScheduleBuilder.Init();
        }

        private void MainLoad(object sender, EventArgs e)
        {
            LoadedClassData = new ClassData();

            ClassLevelComboBox.SelectedIndex = 0;
            ClassDayComboBox.SelectedIndex = 0;

            //create new lists for custom homework and holidays
            LoadedClassData.CustomHomeworkList = new List<HomeworkTask>();
            LoadedClassData.CustomHolidaysList = new Dictionary<DateTime, string>();

            ClassTimePicker.Format = DateTimePickerFormat.Custom;
            ClassTimePicker.CustomFormat = "HH:mm";
            ClassTimePicker.ShowUpDown = true;

        }

        #endregion

        #region menuStrip

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dialog = new AboutBox())
            {
                dialog.ShowDialog();
            }
        }

        private void saveClassDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StoreClassData();
            saveFileDialog.ShowDialog();
        }

        private void saveFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            string path = saveFileDialog.FileName;

            File.WriteAllText(path, JsonConvert.SerializeObject(LoadedClassData));
        }

        private void loadFromJLSHUFiileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openJLSCFileDialog.ShowDialog();
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            string path = openJLSCFileDialog.FileName;
            try
            {
                LoadedClassData = JsonConvert.DeserializeObject<ClassData>(File.ReadAllText(path));
                ReInitClass();
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "This file is of the wrong type, corrupted, or malformed. Class data cannot be constructed from this file.",
                    "ERROR",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
            }
        }

        private void newClassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadedClassData = new ClassData();
            ReInitClass();
        }

        private void loadFromToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openXLSFileDialog.ShowDialog();
        }

        private void openXLSFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            string path = Path.GetFullPath(openXLSFileDialog.FileName);

            var classXls = new ExcelQueryFactory(path);

            IQueryable<Cell> studentsKRnames = from c in classXls.Worksheet(0)
                select c["학생"];

            LoadedClassData.StudentList = new List<Tuple<string, string>>();

            foreach (Cell s in studentsKRnames)
            {
                LoadedClassData.StudentList.Add(new Tuple<string, string>("", s.ToString()));
            }

            ReInitClass();
        }

        #endregion

        #region studentList

        private void StudentListAddButton_Click(object sender, EventArgs e)
        {
            using (var dialog = new AddStudentForm())
            {
                DialogResult result = dialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    var s = new Tuple<string, string>(dialog.EnglishNameText, dialog.KoreanNameText);
                    LoadedClassData.StudentList.Add(s);
                    StudentListBox.Items.Add(s);
                    UpdateStudentCount();
                }
            }
        }

        private void StudentListEditButton_Click(object sender, EventArgs e)
        {
            int i = StudentListBox.SelectedIndex;

            if (i > -1)
            {
                using (var dialog = new AddStudentForm())
                {
                    dialog.Text = "Edit Student";
                    dialog.AddButton.Text = "Edit";
                    dialog.EnglishName.Text = LoadedClassData.StudentList[i].Item1;
                    dialog.KoreanName.Text = LoadedClassData.StudentList[i].Item2;


                    DialogResult result = dialog.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        var s = new Tuple<string, string>(dialog.EnglishNameText, dialog.KoreanNameText);
                        LoadedClassData.StudentList[i] = s;
                        StudentListBox.Items[i] = s;
                    }
                }
            }
        }

        private void StudentListRemoveButton_Click(object sender, EventArgs e)
        {
            int i = StudentListBox.SelectedIndex;

            if (i > -1)
            {
                LoadedClassData.StudentList.RemoveAt(i);
                StudentListBox.Items.RemoveAt(i);
                UpdateStudentCount();
            }
        }

        private void UpdateStudentCount()
        {
            ClassCount.Text = "Count: " + StudentListBox.Items.Count;
        }

        #endregion

        private void ReInitClass()
        {
            NTNameBox.Text = LoadedClassData.NTname;
            KTNameBox.Text = LoadedClassData.KTname;
            StudentListBox.Items.Clear();
            StudentListBox.Items.AddRange(LoadedClassData.StudentList.ToArray());
            UpdateStudentCount();
            ClassLevelComboBox.SelectedIndex = LoadedClassData.ClassLevelIndex;
            ClassTimePicker.Value = LoadedClassData.ClassTime;
            ClassDayComboBox.SelectedIndex = LoadedClassData.ClassDayIndex;
            SemesterStartPicker.Value = LoadedClassData.SemesterStart;
            SemesterEndPicker.Value = LoadedClassData.SemesterEnd;
            IgnoreKRHolidaysCB.Checked = LoadedClassData.IgnoreKoreanHolidays;
            IgnoreJLSHolidaysCB.Checked = LoadedClassData.IgnoreJlsHolidays;
            WeeklyReadingCB.Checked = LoadedClassData.WeeklyReading;
            WeeklyReadingCT.Value = LoadedClassData.WeeklyReadingCount;
            WeeklyListeningCB.Checked = LoadedClassData.WeeklyListening;
            WeeklyListeningCT.Value = LoadedClassData.WeeklyListeningCount;
            WeeklyRecitationCB.Checked = LoadedClassData.WeeklyRecitation;
            WeeklyRecitationCT.Value = LoadedClassData.WeeklyRecitationCount;
            WeeklyCustomCB.Checked = LoadedClassData.WeeklyCustom;
            WeeklyCustomCT.Value = LoadedClassData.WeeklyCustomCount;
            WeeklyCustomTB.Text = LoadedClassData.WeeklyCustomText;
            FirstPresentationCB.Checked = LoadedClassData.FirstPresentation;
            FirstPresentationFreeTopicCB.Checked = LoadedClassData.FirstPresentationFreeTopic;
            FirstPresentationCustomReqCB.Checked = LoadedClassData.FirstPresentationCustomReq;
            FirstPresentationCustomReqTB.Text = LoadedClassData.FirstPresentationCustomText;
            SecondPresentationCB.Checked = LoadedClassData.SecondPresentation;
            SecondPresentationFreeTopicCB.Checked = LoadedClassData.SecondPresentationFreeTopic;
            SecondPresentationCustomReqCB.Checked = LoadedClassData.SecondPresentationCustomReq;
            SecondPresentationCustomReqTB.Text = LoadedClassData.SecondPresentationCustomText;
            ReviewCB.Checked = LoadedClassData.EndOfSemesterReviewDays;
        }

        private void StoreClassData()
        {
            LoadedClassData.NTname = NTNameBox.Text;
            LoadedClassData.KTname = KTNameBox.Text;
            LoadedClassData.ClassLevelIndex = ClassLevelComboBox.SelectedIndex;
            LoadedClassData.ClassLevel = ClassLevelComboBox.Text;
            LoadedClassData.ClassDayIndex = ClassDayComboBox.SelectedIndex;
            LoadedClassData.ClassTime = ClassTimePicker.Value;
            LoadedClassData.SemesterStart = SemesterStartPicker.Value;
            LoadedClassData.SemesterEnd = SemesterEndPicker.Value;
            LoadedClassData.IgnoreKoreanHolidays = IgnoreKRHolidaysCB.Checked;
            LoadedClassData.IgnoreJlsHolidays = IgnoreJLSHolidaysCB.Checked;
            LoadedClassData.WeeklyReading = WeeklyReadingCB.Checked;
            LoadedClassData.WeeklyReadingCount = (int) WeeklyReadingCT.Value;
            LoadedClassData.WeeklyListening = WeeklyListeningCB.Checked;
            LoadedClassData.WeeklyListeningCount = (int) WeeklyListeningCT.Value;
            LoadedClassData.WeeklyRecitation = WeeklyRecitationCB.Checked;
            LoadedClassData.WeeklyRecitationCount = (int) WeeklyRecitationCT.Value;
            LoadedClassData.WeeklyCustom = WeeklyCustomCB.Checked;
            LoadedClassData.WeeklyCustomCount = (int) WeeklyCustomCT.Value;
            LoadedClassData.WeeklyCustomText = WeeklyCustomTB.Text;
            LoadedClassData.FirstPresentation = FirstPresentationCB.Checked;
            LoadedClassData.FirstPresentationFreeTopic = FirstPresentationFreeTopicCB.Checked;
            LoadedClassData.FirstPresentationCustomReq = FirstPresentationCustomReqCB.Checked;
            LoadedClassData.FirstPresentationCustomText = FirstPresentationCustomReqTB.Text;
            LoadedClassData.SecondPresentation = SecondPresentationCB.Checked;
            LoadedClassData.SecondPresentationFreeTopic = SecondPresentationFreeTopicCB.Checked;
            LoadedClassData.SecondPresentationCustomReq = SecondPresentationCustomReqCB.Checked;
            LoadedClassData.SecondPresentationCustomText = SecondPresentationCustomReqTB.Text;
            LoadedClassData.EndOfSemesterReviewDays = ReviewCB.Checked;
            LoadedClassData.ClassTimeString = ClassTimePicker.Value.ToShortTimeString();
        }


        private void GenerateButton_Click(object sender, EventArgs e)
        {
            StoreClassData();
            GenerateSchedule();
        }

        private void GenerateSchedule()
        {
            string outputBox = string.Empty;
            string nl = Environment.NewLine + "----------" + Environment.NewLine;

            if (DateCheck())
            {
                outputBox += "Schedule Generation Complete." + Environment.NewLine +
                             "This is a preview of the class schedule. Please review this to ensure the homework tasks and holiday exceptions are correct." +
                             Environment.NewLine +
                             "Then, press [Export] to generate a folder containing formatted, printable handouts.";
                outputBox += nl;
                outputBox += "";
                outputBox += "NT: " + LoadedClassData.NTname + "   |    KT: " + LoadedClassData.KTname +
                             Environment.NewLine;
                outputBox += "Semester starts on: " + LoadedClassData.SemesterStart.ToLongDateString() +
                             Environment.NewLine;
                outputBox += "Semester ends on: " + LoadedClassData.SemesterEnd.ToLongDateString() + Environment.NewLine;
                outputBox += "Students: " + LoadedClassData.StudentList.Count + Environment.NewLine;
                foreach (var s in LoadedClassData.StudentList)
                {
                    if (s.Item2 != null)
                    {
                        outputBox += string.Format("{0} ({1}), ", s.Item1, s.Item2);
                    }
                    else
                    {
                        outputBox += s.Item1 + ", ";
                    }
                }
                outputBox += nl;

                outputBox = ScheduleBuilder.BuildPreviewSchedule(LoadedClassData)
                    .Aggregate(outputBox, (current, s) => current + s + Environment.NewLine);
            }
            SyllabusPreviewBox.Text = outputBox;
        }

        private void AddHomeworkButton_Click(object sender, EventArgs e)
        {
            StoreClassData();
            using (var chf = new CustomHomeworkForm(this))
            {
                chf.ShowDialog();
            }
        }

        private void AddHolidayButton_Click(object sender, EventArgs e)
        {
            StoreClassData();
            if (DateCheck())
            {
                using (var hf = new CustomHolidayForm(this))
                {
                    hf.ShowDialog();
                }
            }
        }

        private bool DateCheck()
        {
            if (LoadedClassData.SemesterStart >= LoadedClassData.SemesterEnd)
            {
                MessageBox.Show(
                    "The semester starting date cannot be the same as, or later than, the semester end date.\n Please fix this before proceeding.",
                    "ERROR",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                return false;
            }
            return true;
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            StoreClassData();
            if (DateCheck())
            {
                using (var ec = new ExportControls(this))
                {
                    ec.ShowDialog();
                }
            }
        }

        private void usageGuideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://jarena3.github.io/JLSScheduler/");
        }

        private void reportBugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://goo.gl/forms/ZD34SdhlOv");
        }
    }
}