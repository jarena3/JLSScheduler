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
                    MessageBox.Show("Documents created successfully !");
                }

            }

        }

        private void CloseBTN_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DOCBTN_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var classDayString = ScheduleBuilder.GetWeekday(_main.LoadedClassData.classDayIndex).ToString();
                    var _classTimeString = _main.LoadedClassData.classTimeString;

                    var classTimeString = new string((from c in _classTimeString
                                                  where char.IsWhiteSpace(c) || char.IsLetterOrDigit(c)
                                                  select c ).ToArray()).Insert(3, "_");

                    var dir =
                        Directory.CreateDirectory(Path.Combine(dialog.SelectedPath, classDayString + "_" + classTimeString));

                    var tpath = DOCExportWriter.WriteToDOC(_main.LoadedClassData,
                        ScheduleBuilder.BuildWeeksList(_main.LoadedClassData), Path.GetTempPath());

                    //wait a moment for windows to close the filestream

                    var validFiles = tpath.GetFiles().Where(s => s.Extension == ".docx");

                    foreach (FileInfo f in validFiles)
                    {
                        Debug.WriteLine(f.FullName);
                        try
                        {
                            //Create an instance for word app
                            Microsoft.Office.Interop.Word.Application winword =
                                new Microsoft.Office.Interop.Word.Application();

                            //Set animation status for word application
                            winword.ShowAnimation = false;

                            //Set status for word application is to be visible or not.
                            winword.Visible = false;

                            //Create a missing variable for missing value
                            object missing = System.Reflection.Missing.Value;

                            //Create a new document
                            Microsoft.Office.Interop.Word.Document document = winword.Documents.Open(f.FullName);

                            //Save the document
                            var rawFilename = f.ToString().Substring(0,f.ToString().Length - 5);
                            object filename = dir.FullName + "/"+ rawFilename + ".doc";
                            document.SaveAs2(ref filename);
                            document.Close(ref missing, ref missing, ref missing);
                            document = null;
                            winword.Quit(ref missing, ref missing, ref missing);
                            winword = null;
                            MessageBox.Show("Document created successfully !" + filename);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
        }
    }
}