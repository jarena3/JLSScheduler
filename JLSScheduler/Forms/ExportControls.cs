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
using Microsoft.Office.Interop.Word;

namespace JLSScheduler.Forms
{

    public partial class ExportControls : Form
    {

        private enum saveType
        {
            DOC,
            PDF,
            HTML,
            XPS,
            ODT
        }


        private Main _main;

        public ExportControls(Main main)
        {
            InitializeComponent();
            _main = main;
            DOCBTN.Text = "Export as .DOC" + Environment.NewLine + " (Requires MS Word installed on this system)";
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
            WinwordSaveAs(saveType.DOC);
        }
        private void HTMLBTN_Click(object sender, EventArgs e)
        {
            WinwordSaveAs(saveType.HTML);
        }

        private void PDFBTN_Click(object sender, EventArgs e)
        {
            WinwordSaveAs(saveType.PDF);
        }
        private void XPSBTN_Click(object sender, EventArgs e)
        {
            WinwordSaveAs(saveType.XPS);
        }
        private void ODTBTN_Click(object sender, EventArgs e)
        {
            WinwordSaveAs(saveType.ODT);
        }

        private void WinwordSaveAs(saveType sType)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;

                var classDayString = ScheduleBuilder.GetWeekday(_main.LoadedClassData.classDayIndex).ToString();
                var _classTimeString = _main.LoadedClassData.classTimeString;

                var classTimeString = new string((from c in _classTimeString
                    where char.IsWhiteSpace(c) || char.IsLetterOrDigit(c)
                    select c).ToArray()).Insert(3, "_");

                var dir =
                    Directory.CreateDirectory(Path.Combine(dialog.SelectedPath,
                        classDayString + "_" + classTimeString));

                var tpath = DOCExportWriter.WriteToDOC(_main.LoadedClassData,
                    ScheduleBuilder.BuildWeeksList(_main.LoadedClassData), Path.GetTempPath());

                //wait a moment for windows to close the filestream

                //this will catch the tempfiles that winword makes
                var validFiles = tpath.GetFiles().Where(s => !s.FullName.Contains("$"));

                foreach (FileInfo f in validFiles)
                {
                    Debug.WriteLine(f.FullName);

                    //Create an instance for word app
                    Microsoft.Office.Interop.Word.Application winword =
                        new Microsoft.Office.Interop.Word.Application();

                    //Create a missing variable for missing value
                    object missing = System.Reflection.Missing.Value;

                    try
                    {
                        Cursor.Current = Cursors.WaitCursor;
                            
                        //Set animation status for word application
                        winword.ShowAnimation = false;

                        //Set status for word application is to be visible or not.
                        winword.Visible = false;

                        //Create a new document
                        Microsoft.Office.Interop.Word.Document document = winword.Documents.Open(f.FullName);

                        //Save the document
                        var rawFilename = f.ToString().Substring(0, f.ToString().Length - 5);

                        //pick doctype
                        object filename = string.Empty;
                        switch (sType)
                        {
                            case saveType.DOC:
                                filename = dir.FullName + "/" + rawFilename + ".doc";
                                string v = winword.Version;
                                
                            switch (v)
                                {
                                    case "7.0":
                                    case "8.0":
                                    case "9.0":
                                    case "10.0":
                                        document.SaveAs2000(ref filename);
                                        break;
                                    case "11.0":
                                    case "12.0":
                                        document.SaveAs(ref filename);
                                        break;
                                    case "14.0":
                                        document.SaveAs2(ref filename);
                                        break;
                                        
                                    default:
                                        break;
                                }

                                document.Close(ref missing, ref missing, ref missing);
                                document = null;
                                break;
                            case saveType.PDF:
                                var pdffilename = dir.FullName + "/" + rawFilename + ".pdf";
                                document.ExportAsFixedFormat(pdffilename, WdExportFormat.wdExportFormatPDF);
                                document.Close(ref missing, ref missing, ref missing);
                                document = null;
                                break;
                            case saveType.HTML:
                                filename = dir.FullName + "/" + rawFilename + ".html";
                                object fileformat = WdSaveFormat.wdFormatHTML;
                                document.SaveAs2(ref filename, ref fileformat);
                                document.Close(ref missing, ref missing, ref missing);
                                document = null;
                                break;
                            case saveType.ODT:
                                filename = dir.FullName + "/" + rawFilename + ".odt";
                                object fileformat2 = WdSaveFormat.wdFormatOpenDocumentText;
                                document.SaveAs2(ref filename, ref fileformat2);
                                document.Close(ref missing, ref missing, ref missing);
                                document = null;
                                break;
                            case saveType.XPS:
                                var xpsfilename = dir.FullName + "/" + rawFilename + ".xps";
                                document.ExportAsFixedFormat(xpsfilename, WdExportFormat.wdExportFormatXPS);
                                document.Close(ref missing, ref missing, ref missing);
                                document = null;
                                break;
                        }

                        MessageBox.Show("Document created successfully! " + filename);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                    winword.Quit(ref missing, ref missing, ref missing);
                    winword = null;

                    Cursor.Current = Cursors.Default;
                }
            }
        }






    }
}