using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using JLSScheduler.Forms;
using Newtonsoft.Json;

namespace JLSScheduler
{
    public partial class Main : Form
    {
        public ClassData LoadedClassData;

        #region init
        public Main()
        {
            //lock these to en-US
            //allowing system culture seems to cause problems with DocX
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            InitializeComponent();
            //deserialize the book lists for later use
            ScheduleBuilder.Init();
        }

        private void MainLoad(object sender, EventArgs e)
        {
            LoadedClassData = new ClassData();

            //load class selection data from JSON
            ClassLevelComboBox.Items.AddRange(LSClasses.Classes());
                ClassLevelComboBox.SelectedIndex = 0;
            ClassDayComboBox.Items.AddRange(LSClasses.ClassDays);
                ClassDayComboBox.SelectedIndex = 0;

            //create new lists for custom homework and holidays
            LoadedClassData.customHomeworkList = new List<HomeworkTask>();
            LoadedClassData.customHolidaysList = new Dictionary<DateTime, string>();

        }



        private void SetDayTimeRange(object sender, EventArgs e)
        {
            ClassTimeComboBox.Items.Clear();

            var i = ClassDayComboBox.SelectedIndex;
            if (i == 1 || i == 3 || i == 5)
            {
                ClassTimeComboBox.Items.AddRange(LSClasses.TRTimes());
            }
            else
            {
                ClassTimeComboBox.Items.AddRange(LSClasses.MWFTimes());
            }
            ClassTimeComboBox.SelectedIndex = 0;
            LoadedClassData.classTimeIndex = 0;
            LoadedClassData.classDayIndex = ClassDayComboBox.SelectedIndex;
            LoadedClassData.classTimeString = ClassTimeComboBox.SelectedText;
        }
        #endregion

        #region menuStrip
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AboutBox dialog = new AboutBox())
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
            var path = saveFileDialog.FileName;

            File.WriteAllText(path, JsonConvert.SerializeObject(LoadedClassData));
        }

        private void loadFromJLSHUFiileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openJLSCFileDialog.ShowDialog();
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            var path = openJLSCFileDialog.FileName;
            try
            {
                LoadedClassData = JsonConvert.DeserializeObject<ClassData>(File.ReadAllText(path));
                ReInitClass();
            }
            catch (Exception)
            {
                //TODO: add exception handling... all over...
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
            var path = Path.GetFullPath(openXLSFileDialog.FileName);

            var classXLS = new LinqToExcel.ExcelQueryFactory(path);

            var studentsKRnames = from c in classXLS.Worksheet(0)
                                  select c["학생"];

            LoadedClassData.studentList = new List<Tuple<string, string>>();

            foreach (var s in studentsKRnames)
            {
                LoadedClassData.studentList.Add(new Tuple<string, string>("", s.ToString()));
            }

            ReInitClass();
        }

        #endregion

        #region studentList

        private void StudentListAddButton_Click(object sender, EventArgs e)
        {
            using (AddStudentForm dialog = new AddStudentForm())
            {
               DialogResult result = dialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    var s = new Tuple<string, string>(dialog.EnglishNameText, dialog.KoreanNameText);
                    LoadedClassData.studentList.Add(s);
                    StudentListBox.Items.Add(s);
                    UpdateStudentCount();
                }

            }
        }

        private void StudentListEditButton_Click(object sender, EventArgs e)
        {
            var i = StudentListBox.SelectedIndex;

            if (i > -1)
            {
                using (AddStudentForm dialog = new AddStudentForm())
                {
                    dialog.Text = "Edit Student";
                    dialog.AddButton.Text = "Edit";
                    dialog.EnglishName.Text = LoadedClassData.studentList[i].Item1;
                    dialog.KoreanName.Text = LoadedClassData.studentList[i].Item2;


                    DialogResult result = dialog.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        var s = new Tuple<string, string>(dialog.EnglishNameText, dialog.KoreanNameText);
                        LoadedClassData.studentList[i] = s;
                        StudentListBox.Items[i] = s;
                    }

                }
            }
        }

        private void StudentListRemoveButton_Click(object sender, EventArgs e)
        {
            var i = StudentListBox.SelectedIndex;

            if (i > -1)
            {
                LoadedClassData.studentList.RemoveAt(i);
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
            StudentListBox.Items.AddRange(LoadedClassData.studentList.ToArray());
            UpdateStudentCount();
            ClassLevelComboBox.SelectedIndex = LoadedClassData.classLevelIndex;
            ClassTimeComboBox.SelectedIndex = LoadedClassData.classTimeIndex;
            ClassDayComboBox.SelectedIndex = LoadedClassData.classDayIndex;
            SemesterStartPicker.Value = LoadedClassData.semesterStart;
            SemesterEndPicker.Value = LoadedClassData.semesterEnd;
            IgnoreKRHolidaysCB.Checked = LoadedClassData.ignoreKoreanHolidays;
            IgnoreJLSHolidaysCB.Checked = LoadedClassData.ignoreJLSHolidays;
            WeeklyReadingCB.Checked = LoadedClassData.weeklyReading;
            WeeklyReadingCT.Value = LoadedClassData.weeklyReadingCount;
            WeeklyListeningCB.Checked = LoadedClassData.weeklyListening;
            WeeklyListeningCT.Value = LoadedClassData.weeklyListeningCount;
            WeeklyRecitationCB.Checked = LoadedClassData.weeklyRecitation;
            WeeklyRecitationCT.Value = LoadedClassData.weeklyRecitationCount;
            WeeklyCustomCB.Checked = LoadedClassData.weeklyCustom;
            WeeklyCustomCT.Value = LoadedClassData.weeklyCustomCount;
            WeeklyCustomTB.Text = LoadedClassData.weeklyCustomText;
            FirstPresentationCB.Checked = LoadedClassData.firstPresentation;
            FirstPresentationFreeTopicCB.Checked = LoadedClassData.firstPresentationFreeTopic;
            FirstPresentationCustomReqCB.Checked = LoadedClassData.firstPresentationCustomReq;
            FirstPresentationCustomReqTB.Text = LoadedClassData.firstPresentationCustomText;
            SecondPresentationCB.Checked = LoadedClassData.secondPresentation;
            SecondPresentationFreeTopicCB.Checked = LoadedClassData.secondPresentationFreeTopic;
            SecondPresentationCustomReqCB.Checked = LoadedClassData.secondPresentationCustomReq;
            SecondPresentationCustomReqTB.Text = LoadedClassData.secondPresentationCustomText;
            ReviewCB.Checked = LoadedClassData.endOfSemesterReviewDays;
        }

        private void StoreClassData()
        {
            LoadedClassData.NTname = NTNameBox.Text;
            LoadedClassData.KTname = KTNameBox.Text;
            LoadedClassData.classLevelIndex = ClassLevelComboBox.SelectedIndex;
            LoadedClassData.classDayIndex = ClassDayComboBox.SelectedIndex;
            LoadedClassData.classTimeIndex = ClassTimeComboBox.SelectedIndex;
            LoadedClassData.semesterStart = SemesterStartPicker.Value;
            LoadedClassData.semesterEnd = SemesterEndPicker.Value;
            LoadedClassData.ignoreKoreanHolidays = IgnoreKRHolidaysCB.Checked;
            LoadedClassData.ignoreJLSHolidays = IgnoreJLSHolidaysCB.Checked;
            LoadedClassData.weeklyReading = WeeklyReadingCB.Checked;
            LoadedClassData.weeklyReadingCount = (int) WeeklyReadingCT.Value;
            LoadedClassData.weeklyListening = WeeklyListeningCB.Checked;
            LoadedClassData.weeklyListeningCount = (int) WeeklyListeningCT.Value;
            LoadedClassData.weeklyRecitation = WeeklyRecitationCB.Checked;
            LoadedClassData.weeklyRecitationCount = (int) WeeklyRecitationCT.Value;
            LoadedClassData.weeklyCustom = WeeklyCustomCB.Checked;
            LoadedClassData.weeklyCustomCount = (int) WeeklyCustomCT.Value;
            LoadedClassData.weeklyCustomText = WeeklyCustomTB.Text;
            LoadedClassData.firstPresentation = FirstPresentationCB.Checked;
            LoadedClassData.firstPresentationFreeTopic = FirstPresentationFreeTopicCB.Checked;
            LoadedClassData.firstPresentationCustomReq = FirstPresentationCustomReqCB.Checked;
            LoadedClassData.firstPresentationCustomText = FirstPresentationCustomReqTB.Text;
            LoadedClassData.secondPresentation = SecondPresentationCB.Checked;
            LoadedClassData.secondPresentationFreeTopic = SecondPresentationFreeTopicCB.Checked;
            LoadedClassData.secondPresentationCustomReq = SecondPresentationCustomReqCB.Checked;
            LoadedClassData.secondPresentationCustomText = SecondPresentationCustomReqTB.Text;
            LoadedClassData.endOfSemesterReviewDays = ReviewCB.Checked;
            LoadedClassData.classTimeString = ClassTimeComboBox.Items[LoadedClassData.classTimeIndex].ToString();
        }



        private void GenerateButton_Click(object sender, EventArgs e)
        {
            StoreClassData();
            GenerateSchedule();
        }

        private void GenerateSchedule()
        {
            var outputBox = string.Empty;
            var nl = Environment.NewLine + "----------" + Environment.NewLine;

            if (DateCheck())
            {
                outputBox += "Schedule Generation Complete." + Environment.NewLine +
                             "This is a preview of the class schedule. Please review this to ensure the homework tasks and holiday exceptions are correct." + Environment.NewLine +
                             "Then, press [Export] to generate a folder containing formatted, printable handouts.";
                outputBox += nl;
                outputBox += "";
                outputBox += "NT: " + LoadedClassData.NTname + "   |    KT: " + LoadedClassData.KTname + Environment.NewLine;
                outputBox += "Semester starts on: " + LoadedClassData.semesterStart.ToLongDateString() + Environment.NewLine;
                outputBox += "Semester ends on: " + LoadedClassData.semesterEnd.ToLongDateString() + Environment.NewLine;
                outputBox += "Students: " + LoadedClassData.studentList.Count + Environment.NewLine;
                foreach (var s in LoadedClassData.studentList)
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

                outputBox = ScheduleBuilder.BuildPreviewSchedule(LoadedClassData).Aggregate(outputBox, (current, s) => current + s + Environment.NewLine);

            }
            SyllabusPreviewBox.Text = outputBox;
        }

        private void AddHomeworkButton_Click(object sender, EventArgs e)
        {
            StoreClassData();
            using (CustomHomeworkForm chf = new CustomHomeworkForm(this))
            {
                chf.ShowDialog();
            }
        }

        private void AddHolidayButton_Click(object sender, EventArgs e)
        {
            StoreClassData();
            if (DateCheck())
            {
                using (CustomHolidayForm hf = new CustomHolidayForm(this))
                {
                    hf.ShowDialog();
                }
            }
        }

        private bool DateCheck()
        {
            if (LoadedClassData.semesterStart >= LoadedClassData.semesterEnd)
            {
                MessageBox.Show("The semester starting date cannot be the same as, or later than, the semester end date.\n Please fix this before proceeding.",
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
                using (ExportControls ec = new ExportControls(this))
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
