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
                //TODO: add exception handling
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

        #region teacherNames

        private void NTNameBox_TextChanged(object sender, EventArgs e)
        {
            LoadedClassData.NTname = NTNameBox.Text;
        }

        private void KTNameBox_TextChanged(object sender, EventArgs e)
        {
            LoadedClassData.KTname = KTNameBox.Text;
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

        #region timeAndLevel

        private void ClassTimeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadedClassData.classTimeIndex = ClassTimeComboBox.SelectedIndex;
        }

        private void ClassLevelComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadedClassData.classLevelIndex = ClassLevelComboBox.SelectedIndex;
        }

        private void SemesterStartPicker_ValueChanged(object sender, EventArgs e)
        {
            LoadedClassData.semesterStart = SemesterStartPicker.Value;
        }

        private void SemesterEndPicker_ValueChanged(object sender, EventArgs e)
        {
            LoadedClassData.semesterStart = SemesterEndPicker.Value;
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
        }

        private void StoreClassData()
        {
            LoadedClassData.NTname = NTNameBox.Text;
            LoadedClassData.KTname = KTNameBox.Text;
        }



        private void GenerateButton_Click(object sender, EventArgs e)
        {
            GenerateSchedule();
        }

        private void GenerateSchedule()
        {
            var outputBox = SyllabusPreviewBox.Text;
        }

        private void IgnoreKRHolidaysCB_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void IgnoreJLSHolidaysCB_CheckedChanged(object sender, EventArgs e)
        {

        }






    }
}
