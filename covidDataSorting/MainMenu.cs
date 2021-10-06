using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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

        //task 3 variable
        List<Group> groupList;


        //class variable
        FileLocation newCSVFileLocation;

        public MainMenu()
        {
            InitializeComponent();
        }

        //-----------------------------------------Local function-------------------------------------------------------------
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

        private void displayGroupTree()
        {
            treeView1.Nodes.Clear();

            foreach (Group group in groupList)
            {
                treeView1.Nodes.Add(group.name);
                displayGroupNode(group, treeView1.Nodes[treeView1.Nodes.Count - 1]);
            }
        }

        private void displayGroupNode(Group group, TreeNode treeNode)
        {

            if (group.childGroupList == null)
                return;

            foreach (Group child in group.childGroupList)
            {
                treeNode.Nodes.Add(child.name);
                TreeNode nextTreeNode = treeNode.Nodes[treeNode.Nodes.Count - 1];
                displayGroupNode(child, nextTreeNode);
            }
        }

        private int getTreeNodeRootIndex(TreeNode treeNode)
        {
            if (treeNode.Parent == null)
            {
                return treeNode.Index;
            }
            else
            {
                return getTreeNodeRootIndex(treeNode.Parent);
            }
        }

        private List<String> getAllVaxName(RowDataSet rds)
        {
            List<String> nameList = new List<string>();

            foreach (int index in rds.orderList)
            {
                Row row = rds.rowList[index];
                String currentVaxName = row.columns[3];
                bool isInList = false;
                foreach (String name in nameList)
                {
                    if (name.CompareTo(currentVaxName) == 0)
                        isInList = true;
                }
                if (!isInList)
                    nameList.Add(currentVaxName);
            }

            return nameList;
        }

        private List<String> getAllSymtomName(RowDataSet rds)
        {
            List<String> symtomList = new List<String>();

            foreach (int index in rds.orderList)
            {
                Row row = rds.rowList[index];
                String currentSymtomName = row.columns[5];
                bool isInList = false;
                foreach (String name in symtomList)
                {
                    if (name.CompareTo(currentSymtomName) == 0)
                        isInList = true;
                }
                if (!isInList)
                    symtomList.Add(currentSymtomName);
            }

            return symtomList;
        }

        private void groupData(String name, int minAge, int maxAge)
        {
            groupList.Add(new Group(name));
            int groupRootIndex = groupList.Count - 1;

            //group by age
            List<int> ageGroupList = FM.GMs[0].groupByAge(minAge, maxAge);
            groupList[groupRootIndex].copyOrderList(ageGroupList);

            //group by male
            groupList[groupRootIndex].addChildGroup("Gender: M");
            List<int> maleGenderList = FM.GMs[0].groupByColumn(ageGroupList, 2, "M");
            groupList[groupRootIndex].search("Gender: M").copyOrderList(maleGenderList);

            //group by female
            groupList[groupRootIndex].addChildGroup("Gender: F");
            List<int> femaleGroupList = FM.GMs[0].groupByColumn(ageGroupList, 2, "F");
            groupList[groupRootIndex].search("Gender: F").copyOrderList(femaleGroupList);

            //group by N/A
            groupList[groupRootIndex].addChildGroup("Gender: N/A");
            List<int> nullGenderGroupList = FM.GMs[0].groupByColumn(ageGroupList, 2, "");
            groupList[groupRootIndex].search("Gender: N/A").copyOrderList(nullGenderGroupList);

            //group by vacince name
            foreach (Group group in groupList[groupRootIndex].childGroupList)
            {
                RowDataSet rds = new RowDataSet(FM.GMs[0].rowList, group.rowOrderList);
                List<String> vacinceNameList = getAllVaxName(rds);
                foreach (String vacinceName in vacinceNameList)
                {
                    group.addChildGroup(vacinceName);
                    group.childGroupList.Last().copyOrderList(FM.GMs[0].groupByColumn(group.rowOrderList, 3, vacinceName));
                }
            }

            //group by symtom name
            foreach (Group outerGroup in groupList[groupRootIndex].childGroupList)
            {
                if (outerGroup.childGroupList != null)
                    foreach (Group group in outerGroup.childGroupList)
                    {
                        RowDataSet rds = new RowDataSet(FM.GMs[0].rowList, group.rowOrderList);
                        List<String> symtomNameList = getAllSymtomName(rds);
                        foreach (String symtomName in symtomNameList)
                        {
                            group.addChildGroup(symtomName);
                            group.childGroupList.Last().copyOrderList(FM.GMs[0].groupByColumn(group.rowOrderList, 5, symtomName));
                        }
                    }
            }
        }

        private void printDebug(Object data)
        {
            tabControl1.SelectedIndex = 3;
            DebugConsole.AppendText(DateTime.Now + " --> " + data.ToString() + "\n");
        }

        //--------------------------------------------Swing function----------------------------------------------------------

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
            

            DialogResult DR = saveFileDialog1.ShowDialog();

            if(DR == DialogResult.OK)
            {
                bool stopClock = false;
                int h = 0, m = 0, s = 0;

                Task clock = new Task(() =>
                {
                    while (!stopClock)
                    {
                        label7.Invoke((MethodInvoker)delegate ()
                        {
                            label7.Text = nextSecondTick(ref h, ref m, ref s);
                        });

                        wait(1000);
                    }
                });

                clock.Start();

                Task task = Task.Run(() =>
                {
                    bool finishRead = true;

                    debugLabel.Invoke((MethodInvoker)delegate ()
                    {
                        debugLabel.Text = "Begin Read CSV file";
                    });

                    FileLocation[] FL = getAllFileLocation();

                    CSVFileManager FMT = new CSVFileManager();

                    Task t1 = Task.Run(() => {
                        finishRead = FMT.readCSVFile(FL);
                    });

                    t1.Wait();

                    if (!finishRead)
                    {
                        printDebug("Please make sure to close all related file before read and combine\n");
                        debugLabel.Invoke((MethodInvoker)delegate ()
                        {
                            debugLabel.Text = "Error";
                        });
                        return;
                    }

                    debugLabel.Invoke((MethodInvoker)delegate ()
                    {
                        debugLabel.Text = "Begin combining files";
                    });


                    Task t2 = Task.Run(() =>
                    {
                        FMT.combineCSVFile();
                    });
                    t2.Wait();


                    debugLabel.Invoke((MethodInvoker)delegate ()
                    {
                        debugLabel.Text = "Begin Wrtie CSV file to " + newCSVFileLocation.absolutePath;
                    });


                    Task t3 = Task.Run(() => {
                        FMT.writeCSVFile(newCSVFileLocation.absolutePath);
                    });
                    t3.Wait();

                    debugLabel.Invoke((MethodInvoker)delegate ()
                    {
                        debugLabel.Text = "Job Done";
                    });

                    stopClock = true;

                    FMT = null;

                    using (StreamWriter sw = new StreamWriter(newCSVFileLocation.getFolderLocation() + "\\RunResult.TXT"))
                    {
                        sw.WriteLine(DateTime.Now.ToString() + " --> Run time: " + String.Format("{0}:{1}:{2}", h.ToString(), m.ToString(), s.ToString()));
                        sw.WriteLine("Extract to file name --> " + newCSVFileLocation.fileName);
                    }
                });


            }
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            newCSVFileLocation = new FileLocation(sender.ToString().Substring(sender.ToString().IndexOf("FileName:") + "FileName: ".Length));
            absolutePathLabel.Text = newCSVFileLocation.fileName;
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
                    finishRead = FM.readCSVFile(newCSVFileLocation.absolutePath, true);
                });
                t1.Wait();
                if (!finishRead)
                {
                    printDebug("Please make sure to close all related file before read\n");
                    return;
                }

                absolutePathLabel.Text = newCSVFileLocation.fileName;

                debugLabel.Text = "Job Done";

                label4.Text = "N/A";
            }
        }

        private void filterTask1Button_Click(object sender, EventArgs e)
        {
            var dialog = saveFileDialog1.ShowDialog();

            if(dialog == DialogResult.OK)
            {
                Row header = FM.GMs[0].rowList.First();

                List<int> symtomColumnIndex = new List<int>();

                for(int count = 42; count < header.columns.Count; count++)
                {
                    if(count % 2 == 0)
                    {
                        symtomColumnIndex.Add(count);
                    }
                }

                List<int> indexs = new List<int>() { 0, 3, 6, 41, 7, 9, 10, 8 };

                indexs.InsertRange(5, symtomColumnIndex);

                debugLabel.Text = "Begin filter Task 1 file";

                Task t1 = Task.Run(() =>
                {
                    FM.GMs[0].filterColumn(indexs);
                });
                t1.Wait();

                for(int count = 0; count < symtomColumnIndex.Count; count++)
                {
                    symtomColumnIndex[count] = 5 + count;
                }

                //indexs = new List<int>() { 5, 6, 7, 8, 9 };

                debugLabel.Text = "Begin Split Symtom Column on Task 1 file";

                Task t2 = Task.Run(() =>
                {
                    FM.GMs[0].splitSymptomColumn(symtomColumnIndex);
                });
                t2.Wait();

                if (suffleCheckBox.Checked)
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

                absolutePathLabel.Text = newCSVFileLocation.fileName;

                label4.Text = newCSVFileLocation.fileName;

                label2.Text = newCSVFileLocation.fileName;

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

                    GridManager.QuickSort(rds, 1, rds.rowList.Count - 1);

                    stopClock = true;

                    FM.writeCSVFile(tempFileLocation.absolutePath, 0, rds.orderList);
                });

                absolutePathLabel.Text = newCSVFileLocation.fileName;

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

                    GridManager.InsertionSort(rds);

                    rds.orderList.Insert(0, 0);

                    stopClock = true;

                    FM.writeCSVFile(tempFileLocation.absolutePath, 0, rds.orderList);
                });

                absolutePathLabel.Text = newCSVFileLocation.fileName;

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

                    GridManager.SelectionSort(rds);

                    rds.orderList.Insert(0, 0);

                    stopClock = true;

                    FM.writeCSVFile(tempFileLocation.absolutePath, 0, rds.orderList);
                });

                absolutePathLabel.Text = newCSVFileLocation.fileName;

            }
        }
        private void bubbleSortButton_Click(object sender, EventArgs e)
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
                        bubbleSortTimerLabel.Invoke((MethodInvoker)delegate ()
                        {
                            bubbleSortTimerLabel.Text = nextSecondTick(ref h, ref m, ref s);
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

                    GridManager.BubbleSort(rds);

                    rds.orderList.Insert(0, 0);

                    stopClock = true;

                    FM.writeCSVFile(tempFileLocation.absolutePath, 0, rds.orderList);
                });

                absolutePathLabel.Text = newCSVFileLocation.fileName;

            }
        }
        private void groupFileButton_Click(object sender, EventArgs e)
        {
            bool doneFiltering = false;
            groupList = new List<Group>();

            bool stopClock = false;
            int h = 0, m = 0, s = 0;

            Task clock = new Task(() =>
            {
                while (!stopClock)
                {
                    label3.Invoke((MethodInvoker)delegate ()
                    {
                        label3.Text = nextSecondTick(ref h, ref m, ref s);
                    });

                    wait(1000);
                }
            });

            debugLabel.Text = "Begin Grouping data tree";

            clock.Start();

            Task t1 = Task.Run(() =>
            {
                if(groupingSortCheckBox.Checked)
                {
                    RowDataSet rds = new RowDataSet(FM.GMs[0].rowList);

                    GridManager.QuickSort(rds, 1, rds.rowList.Count - 1);
                }

                groupData("age: < 1", 0, 1);

                groupData("age: 1 - 3", 1, 3);

                groupData("age: 4 - 11", 4, 11);

                groupData("age: 12 - 18", 12, 18);

                groupData("age: 19 - 30", 19, 30);

                groupData("age: 31 - 40", 31, 40);

                groupData("age: 41 - 50", 41, 50);

                groupData("age: 51 - 60", 51, 60);

                groupData("age: 61 - 70", 61, 70);

                groupData("age: 71 - 80", 71, 80);

                groupData("age: > 80", 81, 120);

                groupData("age: N/A", -1, -1);

                doneFiltering = true;

                stopClock = true;

                int numberOfDeathCases = FM.GMs[0].calculateTotalNumberOfDeathCases();

                //Number of death cases: ...
                label5.Invoke((MethodInvoker)delegate ()
                {
                    label5.Text = "Total number of death cases: ...: " + numberOfDeathCases;
                });
            });

            Task t2 = Task.Run(() =>
            {
                while(true)
                {
                    if(doneFiltering)
                    {
                        debugLabel.Invoke((MethodInvoker)delegate ()
                        {
                            debugLabel.Text = "Begin displaying tree";
                            displayGroupTree();
                            debugLabel.Text = "job done";
                        });
                        break;
                    }
                    else
                    {
                        wait(1000);
                    }
                }
            });

            
        }

        private void exportToCSVFileButton_Click(object sender, EventArgs e)
        {
            TreeNode currentSelectedNode = treeView1.SelectedNode;

            var dialog = saveFileDialog1.ShowDialog();

            if(dialog == DialogResult.OK && currentSelectedNode != null)
            {
                int rootIndex = getTreeNodeRootIndex(currentSelectedNode);
                String groupName = currentSelectedNode.Text;

                absolutePathLabel.Text = newCSVFileLocation.fileName;

                Group currentGroup = groupList[rootIndex].search(groupName);

                if (currentGroup.rowOrderList.Count <= 0)
                {
                    currentGroup.rowOrderList = new List<int>();
                    currentGroup.rowOrderList.Add(0);
                }
                else if(currentGroup.rowOrderList.First() != 0)
                    currentGroup.rowOrderList.Insert(0, 0);

                FM.writeCSVFile(newCSVFileLocation.absolutePath, 0, currentGroup.rowOrderList);
            }
        }

        //this is just a test button
        private void button1_Click(object sender, EventArgs e)
        {
            //RowDataSet rds = new RowDataSet(FM.GMs[0].rowList);

            //List<String> nameList = getAllSymtomName(rds);

            //foreach (String name in nameList)
            //    printDebug(name);

            //TreeNode currentSelectedNode = treeView1.SelectedNode;

            //int rootIndex = getTreeNodeRootIndex(currentSelectedNode);
            //String groupName = currentSelectedNode.Text;

            //Group currentGroup = groupList[rootIndex].search(groupName);

            //printDebug(rootIndex);
            //printDebug(currentGroup.name);

            TreeNode currentSelectedNode = treeView1.SelectedNode;

            int rootIndex = getTreeNodeRootIndex(currentSelectedNode);
            String groupName = currentSelectedNode.Text;

            Group currentGroup = groupList[rootIndex].search(groupName);

            if (currentGroup.rowOrderList.Count <= 0)
            {
                currentGroup.rowOrderList = new List<int>();
                currentGroup.rowOrderList.Add(0);
            }
            else if (currentGroup.rowOrderList.First() != 0)
                currentGroup.rowOrderList.Insert(0, 0);

            foreach(int index in currentGroup.rowOrderList)
            {
                printDebug(FM.GMs[0].rowList[index]);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            DialogResult DR = saveFileDialog1.ShowDialog();

            if (DR == DialogResult.OK)
            {
                printDebug(newCSVFileLocation.getFolderLocation());
            }
        }

        private void calNumberOfDeathButton_Click(object sender, EventArgs e)
        {
            TreeNode currentSelectedNode = treeView1.SelectedNode;

            int rootIndex = getTreeNodeRootIndex(currentSelectedNode);
            String groupName = currentSelectedNode.Text;

            Group currentGroup = groupList[rootIndex].search(groupName);

            if (currentGroup.rowOrderList.Count <= 0)
            {
                currentGroup.rowOrderList = new List<int>();
                currentGroup.rowOrderList.Add(0);
            }
            else if (currentGroup.rowOrderList.First() != 0)
                currentGroup.rowOrderList.Insert(0, 0);

            int numberOfDeath = FM.GMs[0].calculateNumberOfDeathCases(currentGroup);

            numberOfDeathLabel.Text = numberOfDeath.ToString();

        }

        private void notepadButton_Click(object sender, EventArgs e)
        {
            OldBatCommand("Start notepad " + newCSVFileLocation.absolutePath);
        }

        private void folderButton_Click(object sender, EventArgs e)
        {
            OldBatCommand("%SystemRoot%\\explorer.exe " + newCSVFileLocation.getFolderLocation());
        }
    }
}
