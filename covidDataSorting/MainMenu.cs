using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace covidDataSorting
{
    public partial class MainMenu : Form
    {
        GridManager GM;
        FileLocation newCSVFile;
        public MainMenu()
        {
            InitializeComponent();
        }

        private void browseCSVButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string rawFileName = sender.ToString();

            string absolutePath = rawFileName.Substring(rawFileName.IndexOf("FileName:") + "FileName: ".Length);

            FileLocation FL = new FileLocation(absolutePath);

            CSVFileList.Items.Add(FL);
        }

        private void combineCSVFileButton_Click(object sender, EventArgs e)
        {
            DialogResult DR = saveFileDialog1.ShowDialog();

            if(DR == DialogResult.OK)
            {
                FileLocation[] FL = getAllFileLocation();

                GM = new GridManager();

                CSVFileManager FM = new CSVFileManager();

                debugLabel.Text = "Begin Read CSV file";

                foreach (FileLocation fileLocation in FL)
                {
                    Task t1 = Task.Run(() => {
                        FM.readCSVFile(fileLocation.absolutePath);

                    });
                    t1.Wait();
                }


                absolutePathLabel.Text = newCSVFile.fileName;

                debugLabel.Text = "Begin Wrtie CSV file to " + newCSVFile.absolutePath;

                Task t2 = Task.Run(() => {
                    FM.writeCSVFile(newCSVFile.absolutePath);
                });
                t2.Wait();

                debugLabel.Text = "Job Done";
            }
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            newCSVFile = new FileLocation(sender.ToString().Substring(sender.ToString().IndexOf("FileName:") + "FileName: ".Length));
        }

        private void openNewFileButton_Click(object sender, EventArgs e)
        {
            OldBatCommand(newCSVFile.absolutePath);
        }


        //------------------------------------------------------------------------------------------------------
        private FileLocation[] getAllFileLocation()
        {
            int numberOfFile = CSVFileList.Items.Count;
            FileLocation[] FL = new FileLocation[numberOfFile];

            for(int count = 0; count < numberOfFile; count++)
            {
                FL[count] = (FileLocation)CSVFileList.Items[count];
            }

            return FL;
        }
        private void OldBatCommand(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }
        

        private void printDebug(String data)
        {
            DebugConsole.AppendText(data);
        }

        
    }
}
