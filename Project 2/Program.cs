using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Project_2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Row> task1RowList = new List<Row>();
            List<Row> task2RowList = new List<Row>();
            List<Row> mergeRowList = new List<Row>();
            String header = "";
            String task2Header = "";
            String input = "";
            int maxDegree = 3;
            BPTree tree = new BPTree(maxDegree);
            List<Row> testList = new List<Row>();

            String absolutePath = "C:\\Users\\vince\\OneDrive\\study\\Oswego\\CSC365\\Project 2\\Data\\VAERS_COVID_DataAugust2021.csv";
            String folderPath = absolutePath.Substring(0, absolutePath.LastIndexOf('\\'));

            //----------------------------------------------------

            //for (int count = 1; count < 600000; count++)
            //{
            //    testList.Add(new Row(count + ", something"));
            //}

            //shuffleRowList(testList);

            //foreach (Row row in testList)
            //{
            //    tree.insert(row);
            //}


            //while (input.CompareTo("EXIT") != 0)
            //{
            //    Console.Write("input: ");
            //    input = Console.ReadLine().Trim();
            //    switch (input.ToLower())
            //    {
            //        case "i":
            //            Console.Write("insert: ");
            //            tree.insert(new Row(Int32.Parse(Console.ReadLine().Trim()) + ", something"));
            //            break;
            //        case "v":
            //            String path = folderPath + "\\visualize.txt";
            //            tree.visualize(path);
            //            OldBatCommand("Start notepad " + path);
            //            break;
            //        case "vv":
            //            String path3 = folderPath + "\\visualizeVertical.txt";
            //            tree.visualizeVertical(path3, 20);
            //            OldBatCommand("Start notepad " + path3);
            //            break;
            //        case "iv":
            //            Console.Write("insert: ");
            //            tree.insert(new Row(Int32.Parse(Console.ReadLine().Trim()) + ", something"));
            //            String path2 = folderPath + "\\visualize.txt";
            //            tree.visualize(path2);
            //            OldBatCommand("Start notepad " + path2);
            //            break;
            //        default:
            //            break;
            //    }
            //}

            //return;

            //----------------------------------------------------

            Console.WriteLine("Begin reading task 1 csv file");

            readCSV(task1RowList, ref header, absolutePath);

            Console.WriteLine("Done reading task 1 csv file");

            Console.Write("Read and combine task 2 file(Y|N): ");
            input = Console.ReadLine().ToUpper();

            if(input.CompareTo("Y") == 0)
            {
                Console.WriteLine("Begin combinding task 2 file");
                task2RowList = CombineFile(folderPath + "\\2021VAERSDataSeptember.csv", folderPath + "\\2021VAERSSYMPTOMSSeptember.csv", folderPath + "\\2021VAERSVAXSeptember.csv", ref task2Header);
                Console.WriteLine("Done Combinding task 2 file");

                Console.WriteLine("Begin Wrtie task 2 csv file");
                writeCSV(task2RowList, task2Header, folderPath + "\\task2File.csv");
                Console.WriteLine("Done Wrtie task 2 csv file");
            }
            else
            {
                Console.WriteLine("Begin read task 2 file");
                readCSV(task2RowList, ref task2Header, folderPath + "\\task2File.csv");
                Console.WriteLine("Done read task 2 file");
            }

            Console.Write("Enter max degree number of node: ");
            input = Console.ReadLine();

            //Console.Write("Merge task 1 file and task 2 file before insert into tree(Y|N): ");
            //input = Console.ReadLine().ToUpper();

            Console.WriteLine("Begin inserting task 1 file into tree");
            inserting(tree, task1RowList, -1);
            Console.WriteLine("Done inserting task 1 file into tree");

            Console.WriteLine("Begin inserting task 2 file into tree");
            foreach (Row row in task2RowList)
            {
                if (tree.tryFind(row.id) == null)
                    tree.insert(row);
            }
            Console.WriteLine("Done inserting task 2 file into tree");

            mergeRowList = task1RowList; //for testing



            //if (input.CompareTo("Y") == 0)
            //{
            //    Console.WriteLine("Begin merge task 1 file and task 2 file");
            //    mergeRowList = meregeTask1AndTask2(task1RowList, task2RowList);
            //    Console.WriteLine("Done merge task 1 file and task 2 file");
            //    Console.WriteLine("Begin write merge csv file");
            //    writeCSV(mergeRowList, header, folderPath + "\\mergeFile.csv");
            //    Console.WriteLine("Done write merge csv file");

            //    Console.WriteLine("Begin inserting merge file into tree");
            //    inserting(tree, mergeRowList, -1);
            //    Console.WriteLine("Done inserting merge file into tree");

            //    task1RowList.Clear();
            //    task2RowList.Clear();
            //}
            //else
            //{
            //    Console.WriteLine("Begin inserting task 1 file into tree");
            //    inserting(tree, task1RowList, -1);
            //    Console.WriteLine("Done inserting task 1 file into tree");

            //    Console.WriteLine("Begin inserting task 2 file into tree");
            //    foreach (Row row in task2RowList)
            //    {
            //        if (tree.tryFind(row.id) == null)
            //            tree.insert(row);
            //    }
            //    Console.WriteLine("Done inserting task 2 file into tree");

            //    mergeRowList = task1RowList; //for testing
            //}




            //----------------------main loop-------------------------------------------


            while (input.CompareTo("EXIT") != 0)
            {
                Console.Write("Command: ");
                input = Console.ReadLine();
                input = input.ToUpper();
                String extraInput = "";
                try
                {
                    switch (input)
                    {
                        case "HELP":
                            Console.WriteLine("HELP\ninserting\ni(inserting)\nVISUALIZEVERTICAL\nVV(VISUALIZEVERTICAL)\nvisualize\nv(visualize)\nsearch\ns(search)\nfolder<open folder location>");
                            break;
                        case "INSERTING":
                        case "I":
                            Console.Write("Enter maximum degree of node: ");
                            maxDegree = Int32.Parse(Console.ReadLine());
                            tree = new BPTree(maxDegree);
                            Console.Write("enter inserting max: ");
                            extraInput = Console.ReadLine();
                            Console.WriteLine("Begin inserting");
                            inserting(tree, mergeRowList, Int32.Parse(extraInput));
                            Console.WriteLine("Done inserting");
                            break;
                        case "SEARCH":
                        case "S":
                            Console.Write("Please enter search ID: ");
                            extraInput = Console.ReadLine();
                            Row rowT = tree.tryFind(Int32.Parse(extraInput.Trim()));
                            Console.WriteLine(rowT == null ? "null" : rowT.toString());
                            break;
                        case "VISUALIZEVERTICAL":
                        case "VV":
                            Console.Write("Please enter Expected ID space: ");
                            extraInput = Console.ReadLine();
                            String path3 = folderPath + "\\visualizeVertical.txt";
                            tree.visualizeVertical(path3, Int32.Parse(extraInput.Trim()));
                            OldBatCommand("Start notepad " + path3);
                            break;
                        case "VISUALIZE":
                        case "V":
                            String visualizePath = folderPath + "\\visualize.txt";
                            tree.visualize(visualizePath);
                            OldBatCommand("Start notepad " + visualizePath);
                            break;
                        case "FOLDER":
                            OldBatCommand("%SystemRoot%\\explorer.exe " + folderPath);
                            break;
                        default:
                            break;
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine("Exiting");
        }


        public static void shuffleRowList(List<Row> rowList)
        {
            Random random = new Random();

            for (int count = 0; count < rowList.Count; count++)
            {
                int ranNum1 = random.Next(0, rowList.Count);
                int ranNum2 = random.Next(0, rowList.Count);

                if (ranNum1 != ranNum2 && ranNum1 != 0 && ranNum2 != 0)
                {
                    Row row1 = new Row(rowList[ranNum1]);
                    Row row2 = new Row(rowList[ranNum2]);

                    rowList[ranNum1] = row2;
                    rowList[ranNum2] = row1;
                }
            }
        }
        public static List<Row> meregeTask1AndTask2(List<Row> task1RowList, List<Row> task2RowList)
        {
            int begin = 0;
            int end = 0;

            if (task1RowList[0].id <= task2RowList[0].id)
                begin = task1RowList[0].id;
            else
                begin = task2RowList[0].id;

            if (task1RowList[task1RowList.Count - 1].id >= task2RowList[task2RowList.Count - 1].id)
                end = task1RowList[task1RowList.Count - 1].id;
            else
                end = task2RowList[task2RowList.Count - 1].id;

            BPTree task1Tree = new BPTree(3);
            BPTree task2Tree = new BPTree(3);

            Task task4 = Task.Run(() =>
            {
                inserting(task1Tree, task1RowList, -1);
            });

            Task task5 = Task.Run(() =>
            {
                inserting(task2Tree, task2RowList, -1);
            });

            task4.Wait();
            task5.Wait();

            List<Row> mergeRowList = new List<Row>();

            while (begin <= end)
            {
                Row row1 = task1Tree.tryFind(begin);
                Row row2 = task2Tree.tryFind(begin);

                if (row1 != null)
                    mergeRowList.Add(new Row(row1));
                else if (row1 != null)
                    mergeRowList.Add(new Row(row2));

                begin++;
            }

            //clean out unessesary data


            return mergeRowList;
        }
        public static List<Row> CombineFile(String dataFilePath, String symtomFilePath, String vaxFilePath, ref String fileHeader)
        {
            List<Task> taskList = new List<Task>();
            List<Row> symtomList = new List<Row>();
            List<Row> vaxList = new List<Row>();
            List<Row> dataList = new List<Row>();
            String header = "";

            Console.WriteLine("Begin read 3 new file");
            fileHeader = "";
            readCSV(dataList, ref header, dataFilePath);
            fileHeader += header;
            readCSV(vaxList, ref header, vaxFilePath);
            fileHeader += "," + header.Substring(header.IndexOf(",") + 1);
            readCSV(symtomList, ref header, symtomFilePath);
            fileHeader += "," + header.Substring(header.IndexOf(",") + 1);
            Console.WriteLine("Done read 3 new file");

            Console.WriteLine("Begin breakdown all row");
            foreach (Row row in symtomList)
            {
                Task task = Task.Run(() =>
                {
                    row.breakDownLine();
                });
                taskList.Add(task);
            }

            foreach (Row row in vaxList)
            {
                Task task = Task.Run(() =>
                {
                    row.breakDownLine();
                });
                taskList.Add(task);
            }

            foreach (Row row in dataList)
            {
                Task task = Task.Run(() =>
                {
                    row.breakDownLine();
                });
                taskList.Add(task);
            }

            foreach (Task task in taskList)
                task.Wait();

            Console.WriteLine("Done breakdown all row");
            taskList.Clear();
            Console.WriteLine("Begin filtering all file");

            Task task1 = Task.Run(() =>
            {
                symtomList = filterSymtom(symtomList);
            });

            Task task2 = Task.Run(() =>
            {
                vaxList = filterVax(vaxList);
            });

            task1.Wait();
            task2.Wait();

            Console.WriteLine("Done filtering all file");

            List<Row> file = new List<Row>();
            Console.WriteLine("Begin Comebine file");
            file = combineFile(dataList, symtomList, vaxList);
            Console.WriteLine("Done Comebine file");

            taskList.Clear();
            symtomList.Clear();
            vaxList.Clear();
            dataList.Clear();

            return file;
        }

        public static List<Row> combineFile(List<Row> dataFile, List<Row> symtomFile, List<Row> vaxFile)
        {
            List<Row> newRowList = new List<Row>();
            //BPTree dataTree = new BPTree(3);
            //BPTree symtomTree = new BPTree(3);
            Dictionary<int, Row> dataDict = new Dictionary<int, Row>();
            Dictionary<int, Row> symtomDict = new Dictionary<int, Row>();
            bool skip = false;
            Row dataRow, vaxRow, symtomRow, tempRow;
            int maxDataColumn = 35;
            int maxVaxColumn = 8;
            int currentID = 0;
            //------------------------start here--------------------------------

            

            Task task1 = Task.Run(() =>
            {
                foreach(Row row in dataFile)
                {
                    dataDict.Add(row.id, row);
                }

                //inserting(dataTree, dataFile, -1);
                dataFile.Clear();
            });

            Task task2 = Task.Run(() =>
            {
                foreach (Row row in symtomFile)
                {
                    symtomDict.Add(row.id, row);
                }

                //inserting(symtomTree, symtomFile, -1);
                symtomFile.Clear();
            });

            task1.Wait();
            task2.Wait();

            foreach(Row row in vaxFile)
            {
                skip = false;

                currentID = row.id;

                //dataRow = dataTree.find(currentID);
                //symtomRow = symtomTree.find(currentID);

                bool foundData = dataDict.TryGetValue(currentID, out dataRow);
                bool foundSymtom = symtomDict.TryGetValue(currentID, out symtomRow);
                vaxRow = row;
                tempRow = new Row();

                if (!foundData || !foundSymtom)
                    skip = true;

                if (!skip)
                {
                    Task task3 = Task.Run(() =>
                    {
                        if (dataRow.columns == null || dataRow.columns.Count == 0)
                            dataRow.breakDownLine();
                    });

                    Task task4 = Task.Run(() =>
                    {
                        if (vaxRow.columns == null || vaxRow.columns.Count == 0)
                            vaxRow.breakDownLine();
                    });

                    Task task5 = Task.Run(() =>
                    {
                        if (symtomRow.columns == null || symtomRow.columns.Count == 0)
                            symtomRow.breakDownLine();
                    });

                    task3.Wait();
                    
                    while (dataRow.columns.Count < maxDataColumn)
                        dataRow.columns.Add("");
                    
                    task4.Wait();

                    while (vaxRow.columns.Count < maxVaxColumn)
                        vaxRow.columns.Add("");
                    
                    task5.Wait();

                    tempRow.addData(dataRow);

                    for (int count = 1; count < vaxRow.columns.Count; count++)
                        tempRow.columns.Add(vaxRow.columns[count]);

                    for (int count = 1; count < symtomRow.columns.Count; count++)
                        tempRow.columns.Add(symtomRow.columns[count]);

                    newRowList.Add(new Row(tempRow.ToString()));
                }
                //else
                //    Console.WriteLine("skip: " + currentID);
            }

            return newRowList;
        }

        public static void inserting(BPTree tree, List<Row> rowList, int max)
        {
            if (max < 0 || max >= rowList.Count)
            {
                max = rowList.Count;
            }

            try
            {
                for (int count = 0; count < max; count++)
                {
                    tree.insert(rowList[count]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static List<Row> filterVax(List<Row> rowList)
        {
            List<Row> newRowList = new List<Row>();
            foreach (Row row in rowList)
            {
                if (row.columns[1].CompareTo("COVID19") == 0)
                {
                    newRowList.Add(new Row(row));
                }
            }
            rowList.Clear();

            return newRowList;

            //filterDupliacate(rowList);

        }

        public static List<Row> filterSymtom(List<Row> rowList)
        {
            int maxSymtomNum = 0;
            List<Row> newRowList = new List<Row>();
            Row row = new Row();

            for (int count = 0; count < rowList.Count; count++)
            {
                Row pointerRow = rowList[count];
                String currentID = pointerRow.columns[0];

                //append all symtom
                for (int column = 1; column < pointerRow.columns.Count; column++)
                {
                    row.columns.Add(pointerRow.columns[column].ToString());
                }

                //if the next one is not the same or if this is the last index
                if ((count + 1 < rowList.Count && currentID.CompareTo(rowList[count + 1].columns[0]) != 0) || (count == rowList.Count - 1))
                {
                    if (maxSymtomNum < row.columns.Count - 1)
                    {
                        maxSymtomNum = row.columns.Count - 1;
                        //Console.WriteLine(currentID);
                    }


                    row.columns.Insert(0, currentID);

                    newRowList.Add(new Row(row.ToString())); //problem here

                    row.clear();
                }
            }

            rowList.Clear();

            return newRowList;

            
        }

        public static List<Row> filterDupliacate(List<Row> rowList)
        {
            //Console.WriteLine("before filter dup: " + rowList.Count);

            List<int> indexList = new List<int>();
            List<Row> newRowList = new List<Row>();

            for (int count = 0; count < rowList.Count; count++)
            {
                String currentID = rowList[count].columns[0];

                if (count + 1 < rowList.Count && currentID.CompareTo(rowList[count + 1].columns[0]) != 0)
                {
                    indexList.Add(count);
                }
                else if (count == rowList.Count - 1) //if this is the last index
                {
                    indexList.Add(count);
                }
            }


            foreach (int index in indexList)
            {
                newRowList.Add(rowList[index]);
            }
            rowList.Clear();

            return newRowList;
        }






        public static void readCSV(List<Row> pairList, ref String header, String absolutePath)
        {
            pairList.Clear();
            using (var steamReader = new StreamReader(absolutePath))
            {
                header = steamReader.ReadLine();

                while (!steamReader.EndOfStream)
                    pairList.Add(new Row(steamReader.ReadLine().ToString()));
            }
        }

        public static void writeCSV(List<Row> pairList, String header, String absolutePath)
        {
            using (StreamWriter sw = new StreamWriter(absolutePath))
            {
                sw.WriteLine(header);
                foreach (Row pair in pairList)
                {
                    sw.WriteLine(pair.line);
                }
            }
        }

        static void insertionSort(List<Row> pairList)
        {
            int n = pairList.Count;
            for (int i = 1; i < n; ++i)
            {
                int j = i;

                while (j > 0 && pairList[j - 1].id > pairList[j].id)
                {
                    swap(pairList, j, j - 1);

                    j--;
                }
            }
        }

        private static void Quick_Sort(List<Row> pairList, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(pairList, left, right);

                Quick_Sort(pairList, left, pivot - 1);

                Quick_Sort(pairList, pivot + 1, right);

            }

        }

        private static int Partition(List<Row> pairList, int left, int right)
        {
            //int pivot = pairList[right].id;
            int pivot = pairList[(left + right) / 2].id;

            int i = left - 1;

            for (int j = left; j <= right - 1; j++)
            {
                if (pairList[j].id < pivot)
                {
                    i++;
                    swap(pairList, i, j);
                }
            }

            swap(pairList, i + 1, right);

            return i + 1;

        }

        public static void swap(List<Row> pairList, int index1, int index2)
        {
            Row temp1 = new Row(pairList[index1].line);
            pairList[index1] = null;
            pairList[index1] = pairList[index2];
            pairList[index2] = temp1;
        }



        private static void OldBatCommand(string url)
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


    }
}
