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
using System.Timers;
using System.Windows.Forms;

namespace covidDataSorting
{
    public partial class MainMenu : Form
    {
        //task 2 variable
        CSVFileManager FM;
        

        //class variable
        FileLocation newCSVFileLocation;

        public MainMenu()
        {
            InitializeComponent();
        }

        //------------------------------------------------------------------------------------------------------
        private String getCurrentRunTime(DateTime time)
        {
            var hours = Math.Abs(time.Hour - DateTime.Now.Hour);
            var minute = Math.Abs(time.Minute - DateTime.Now.Minute);
            var second = Math.Abs(time.Second - DateTime.Now.Second);
            var millisecond = Math.Abs(time.Millisecond - DateTime.Now.Millisecond);

            return String.Format("Run time: {0}:{1}:{2}:{3}", hours, minute, second, millisecond);
        }
        public String nextSecondTick(ref int h, ref int m, ref int s)
        {
            String currentTime = String.Format("{0}:{1}:{2}", h.ToString(), m.ToString(), s.ToString());

            s++;
            if (s == 60)
            {
                s = 0;
                m++;
            }
            if (m == 60)
            {
                m = 0;
                h++;
            }

            return currentTime;
        }

        public void wait(int milliseconds)
        {
            var timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;

            // Console.WriteLine("start wait timer");
            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();

            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
                // Console.WriteLine("stop wait timer");
            };

            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }
        private FileLocation[] getAllFileLocation()
        {
            int numberOfFile = CSVFileList.Items.Count;
            FileLocation[] FL = new FileLocation[numberOfFile];

            for (int count = 0; count < numberOfFile; count++)
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
            tabControl1.SelectedIndex = 3;
            DebugConsole.AppendText(DateTime.Now + " --> " + data);
        }

        //------------------------------------------------------------------------------------------------------

        private void browseCSVButton_Click(object sender, EventArgs e)
        {
            var dialog = openFileDialog1.ShowDialog();

            if(dialog == DialogResult.OK)
            {
                CSVFileList.Items.Add(newCSVFileLocation);
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string rawFileName = sender.ToString();

            string absolutePath = rawFileName.Substring(rawFileName.IndexOf("FileName:") + "FileName: ".Length);

            newCSVFileLocation = new FileLocation(absolutePath);
        }

        private void combineCSVFileButton_Click(object sender, EventArgs e)
        {
            bool finishRead = true;

            DialogResult DR = saveFileDialog1.ShowDialog();

            if(DR == DialogResult.OK)
            {
                var time = DateTime.Now;

                FileLocation[] FL = getAllFileLocation();

                CSVFileManager FMT = new CSVFileManager();

                debugLabel.Text = "Begin Read CSV file";

                foreach (FileLocation fileLocation in FL)
                {
                    Task t1 = Task.Run(() => {
                        finishRead = FMT.readCSVFile(fileLocation.absolutePath);
                    });
                    t1.Wait();
                    if (!finishRead)
                        break;
                }

                if (!finishRead)
                {
                    printDebug("Please make sure to close all related file before read and combine\n");
                    return;
                }

                debugLabel.Text = "Begin combining files";

                Task t2 = Task.Run(() =>
                {
                    FMT.combineCSVFile();
                });
                t2.Wait();

                absolutePathLabel.Text = newCSVFileLocation.fileName;

                debugLabel.Text = "Begin Wrtie CSV file to " + newCSVFileLocation.absolutePath;

                Task t3 = Task.Run(() => {
                    FMT.writeCSVFile(newCSVFileLocation.absolutePath);
                });
                t3.Wait();

                debugLabel.Text = "Job Done";

                FMT = null;

                combineRunTimeLabel.Text = getCurrentRunTime(time);
            }
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            newCSVFileLocation = new FileLocation(sender.ToString().Substring(sender.ToString().IndexOf("FileName:") + "FileName: ".Length));
        }

        private void openNewFileButton_Click(object sender, EventArgs e)
        {
            OldBatCommand(newCSVFileLocation.absolutePath);
        }

        private void CSVFileList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                int currentSlectedIndex = CSVFileList.SelectedIndex;
                CSVFileList.Items.RemoveAt(currentSlectedIndex);
            }
        }

        private void browseTask1Button_Click(object sender, EventArgs e)
        {
            bool finishRead = true;

            FM = new CSVFileManager();

            var dialog = openFileDialog1.ShowDialog();

            if(dialog == DialogResult.OK)
            {
                debugLabel.Text = "Begin read Task 1 file";
                Task t1 = Task.Run(() => {
                    finishRead = FM.readCSVFile(newCSVFileLocation.absolutePath);
                });
                t1.Wait();
                if (!finishRead)
                {
                    printDebug("Please make sure to close all related file before read\n");
                    return;
                }

                label2.Text = newCSVFileLocation.fileName;

                debugLabel.Text = "Job Done";

                label4.Text = "N/A";
            }
        }

        private void filterTask1Button_Click(object sender, EventArgs e)
        {
            var dialog = saveFileDialog1.ShowDialog();

            if(dialog == DialogResult.OK)
            {
                List<int> indexs = new List<int>() { 0, 3, 6, 41, 7, 42, 44, 46, 48, 50, 9, 10, 8 };

                debugLabel.Text = "Begin filter Task 1 file";

                Task t1 = Task.Run(() =>
                {
                    FM.GMs[0].filterColumn(indexs);
                });
                t1.Wait();

                indexs = new List<int>() { 5, 6, 7, 8, 9 };

                debugLabel.Text = "Begin Split Symtom Column on Task 1 file";

                Task t2 = Task.Run(() =>
                {
                    FM.GMs[0].splitColumn(indexs);
                });
                t2.Wait();

                if(suffleCheckBox.Checked)
                {
                    debugLabel.Text = "Begin suffle all the row";

                    Task t4 = Task.Run(() =>
                    {
                        FM.GMs[0].suffleRowList();
                    });
                    t4.Wait();
                }

                debugLabel.Text = "Begin write filter file";

                Task t3 = Task.Run(() =>
                {
                    FM.writeCSVFile(newCSVFileLocation.absolutePath, 0);
                });
                t3.Wait();

                debugLabel.Text = "Job Done";

                label2.Text = "N/A";
            }
        }

        private void browseTask2FileButton_Click(object sender, EventArgs e)
        {
            bool finishRead = true;

            FM = new CSVFileManager();

            var dialog = openFileDialog1.ShowDialog();

            if (dialog == DialogResult.OK)
            {
                debugLabel.Text = "Begin read Task 2 file";
                Task t1 = Task.Run(() => {
                    finishRead = FM.readCSVFile(newCSVFileLocation.absolutePath);
                });
                t1.Wait();
                if (!finishRead)
                {
                    printDebug("Please make sure to close all related file before read\n");
                    return;
                }

                label4.Text = newCSVFileLocation.fileName;

                debugLabel.Text = "Job Done";
            }
        }

        private void quickSortButton_Click(object sender, EventArgs e)
        {
            var dialog = saveFileDialog1.ShowDialog();
            
            if(dialog == DialogResult.OK)
            {
                bool stopClock = false;
                int h = 0, m = 0, s = 0;

                Task clock = new Task(() =>
                {
                    while (!stopClock)
                    {
                        quickSortTimerLabel.Invoke((MethodInvoker)delegate ()
                        {
                            quickSortTimerLabel.Text = nextSecondTick(ref h, ref m, ref s);
                        });

                        wait(1000);
                    }
                });

                Task t1 = Task.Run(() => 
                {
                    FileLocation tempFileLocation = new FileLocation(newCSVFileLocation.absolutePath);
                    RowDataSet rds = new RowDataSet(FM.GMs[0].rowList);

                    clock.Start();

                    FM.GMs[0].QuickSort(rds, 1, rds.rowList.Count - 1);

                    stopClock = true;

                    FM.writeCSVFile(tempFileLocation.absolutePath, 0, rds.orderList);
                });



            }
        }

        private void insertionSortButton_Click(object sender, EventArgs e)
        {
            var dialog = saveFileDialog1.ShowDialog();

            if (dialog == DialogResult.OK)
            {
                bool stopClock = false;
                int h = 0, m = 0, s = 0;

                Task clock = new Task(() =>
                {
                    while (!stopClock)
                    {
                        insertionSortTimerLabel.Invoke((MethodInvoker)delegate ()
                        {
                            insertionSortTimerLabel.Text = nextSecondTick(ref h, ref m, ref s);
                        });

                        wait(1000);
                    }
                });

                Task t1 = Task.Run(() =>
                {
                    FileLocation tempFileLocation = new FileLocation(newCSVFileLocation.absolutePath);

                    RowDataSet rds = new RowDataSet(FM.GMs[0].rowList);

                    clock.Start();

                    rds.orderList.RemoveAt(0);

                    FM.GMs[0].InsertionSort(rds);

                    rds.orderList.Insert(0, 0);

                    stopClock = true;

                    FM.writeCSVFile(tempFileLocation.absolutePath, 0, rds.orderList);
                });

            }
        }

        private void selectionSortButton_Click(object sender, EventArgs e)
        {
            var dialog = saveFileDialog1.ShowDialog();

            if (dialog == DialogResult.OK)
            {
                bool stopClock = false;
                int h = 0, m = 0, s = 0;

                Task clock = new Task(() =>
                {
                    while (!stopClock)
                    {
                        selectionSortTimerLabel.Invoke((MethodInvoker)delegate ()
                        {
                            selectionSortTimerLabel.Text = nextSecondTick(ref h, ref m, ref s);
                        });

                        wait(1000);
                    }
                });

                Task t1 = Task.Run(() =>
                {
                    FileLocation tempFileLocation = new FileLocation(newCSVFileLocation.absolutePath);

                    RowDataSet rds = new RowDataSet(FM.GMs[0].rowList);

                    clock.Start();

                    rds.orderList.RemoveAt(0);

                    FM.GMs[0].SelectionSort(rds);

                    rds.orderList.Insert(0, 0);

                    stopClock = true;

                    FM.writeCSVFile(tempFileLocation.absolutePath, 0, rds.orderList);
                });

            }
        }
    }
}
