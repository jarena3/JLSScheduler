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
    public partial class AddStudentForm : Form
    {
        public string EnglishNameText;
        public string KoreanNameText;

        public AddStudentForm()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            EnglishNameText = EnglishName.Text;
            KoreanNameText = KoreanName.Text;

            this.DialogResult = DialogResult.OK;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
