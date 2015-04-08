using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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
            InitializeComponent();
            ScheduleBuilder s = new ScheduleBuilder();
            //deserialize the book lists for later use
            s.Init();
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
            LoadedClassData.customHolidaysList = new List<DateTime>();

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
            var classXLS = new LinqToExcel.ExcelQueryFactory(openXLSFileDialog.FileName);

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
        }



        private void GenerateButton_Click(object sender, EventArgs e)
        {
            StoreClassData();
            GenerateSchedule();
        }

        private void GenerateSchedule()
        {
            var outputBox = SyllabusPreviewBox.Text;
        }

        private void AddHomeworkButton_Click(object sender, EventArgs e)
        {
            using (CustomHomeworkForm chf = new CustomHomeworkForm(this))
            {
                chf.ShowDialog();
            }
        }



    }
}
