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

        }
    }
}
