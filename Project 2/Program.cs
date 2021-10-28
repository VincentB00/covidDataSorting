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
            List<Row> RowList = new List<Row>();
            List<Row> testList = new List<Row>();
            List<Row> symtomList = new List<Row>();
            List<Row> vaxList = new List<Row>();
            List<Row> dataList = new List<Row>();
            List<Task> taskList = new List<Task>();
            String header = "";
            int maxDegree = 4;
            BPTree tree = new BPTree(maxDegree);

            //readdata
            //String absolutePath = "C:\\Users\\vince\\OneDrive\\study\\Oswego\\CSC365\\Project 2\\Data\\TestFile.csv";
            String absolutePath = "C:\\Users\\vince\\OneDrive\\study\\Oswego\\CSC365\\Project 2\\Data\\VAERS_COVID_DataAugust2021.csv";
            String folderPath = absolutePath.Substring(0, absolutePath.LastIndexOf('\\'));

            //for(int count = 1; count < 1001; count++)
            //{
            //    testList.Add(new Row(count + ", something"));
            //}

            Console.WriteLine("Begin reading csv file");

            readCSV(RowList, ref header, absolutePath);

            Console.WriteLine("Done reading csv file");

            //Quick_Sort(RowList, 1, RowList.Count - 1);

            //-------------------------------task 2-------------------------------------
            Console.WriteLine("Begin read 3 new file");
            readCSV(symtomList, ref header, folderPath + "\\2021VAERSSYMPTOMSSeptember.csv");
            readCSV(vaxList, ref header, folderPath + "\\2021VAERSVAXSeptember.csv");
            readCSV(dataList, ref header, folderPath + "\\2021VAERSDataSeptember.csv");
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
                filterSymtom(symtomList);
            });

            Task task2 = Task.Run(() =>
            {
                filterVax(vaxList);
            });

            task1.Wait();
            task2.Wait();
            
            Console.WriteLine("Done filtering all file");

            return;

            //----------------------main loop-------------------------------------------
            String input = "";

            while(input.CompareTo("EXIT") != 0)
            {
                Console.Write("Command: ");
                input = Console.ReadLine();
                input = input.ToUpper();
                String extraInput = "";

                switch(input)
                {
                    case "HELP":
                        Console.WriteLine("HELP\ninserting\ni(inserting)\nvisualize\nv(visualize)\nsearch\ns(search)\nfolder<open folder location>");
                        break;

                    case "INSERTING":
                    case "I":
                        tree = new BPTree(maxDegree);
                        Console.Write("enter inserting max: ");
                        extraInput = Console.ReadLine();
                        Console.WriteLine("Begin inserting");
                        inserting(tree, RowList, Int32.Parse(extraInput));
                        Console.WriteLine("Done inserting");
                        break;
                    case "SEARCH":
                    case "S":
                        Console.Write("Please enter search ID: ");
                        extraInput = Console.ReadLine();
                        Console.WriteLine(tree.find(Int32.Parse(extraInput.Trim())).toString());
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

            Console.WriteLine("Exiting");
        }

        public static void inserting(BPTree tree, List<Row> rowList, int max)
        {
            if (max == -1)
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


        public static void filterVax(List<Row> rowList)
        {
            List<Row> newRowList = new List<Row>();
            foreach (Row row in rowList)
            {
                if (row.columns[1].CompareTo("COVID19") == 0)
                {
                    newRowList.Add(row);
                }
            }
            rowList.Clear();
            rowList = newRowList;

            //filterDupliacate(rowList);

        }

        public static int filterSymtom(List<Row> rowList)
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
            rowList = newRowList;

            return maxSymtomNum;
        }

        public static void filterDupliacate(List<Row> rowList)
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
            rowList = newRowList;

            //Console.WriteLine("after filter dup: " + rowList.Count);
        }






        public static void readCSV(List<Row> pairList, ref String header, String absolutePath)
        {
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
