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
    public partial class ExportControls : Form
    {
        private Main _main;

        public ExportControls(Main main)
        {
            InitializeComponent();
            _main = main;
        }

        private void ExportHTMLBTN_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var path = dialog.SelectedPath;
                    HTMLExportWriter.WriteToHtml(_main.LoadedClassData,
                        ScheduleBuilder.BuildWeeksList(_main.LoadedClassData), path);
                }

            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var path = dialog.SelectedPath;
                    DOCExportWriter.WriteToDOC(_main.LoadedClassData,
                        ScheduleBuilder.BuildWeeksList(_main.LoadedClassData), path);
                }

            }

        }
    }
}
