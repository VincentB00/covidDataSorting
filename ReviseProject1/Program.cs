using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ReviseProject1
{
    class Program
    {
        static void Main(string[] args)
        {
            String absolutePath;
            String folderPath = "C:\\VincentFile\\School Work\\Study Document\\Oswego\\Fall 2021\\CSC365\\Project 1\\Data";

            List<Row> task1RowList = new List<Row>();
            List<Row> task2RowList = new List<Row>();
            String header1 = "";
            String header2 = "";

            Task task1 = Task.Run(() =>
            {
                task1RowList = CombineFile(folderPath + "\\2020VAERSData.csv", folderPath + "\\2020VAERSSYMPTOMS.csv", folderPath + "\\2020VAERSVAX.csv", ref header1);
            });

            Task task2 = Task.Run(() =>
            {
                task2RowList = CombineFile(folderPath + "\\2021VAERSData.csv", folderPath + "\\2021VAERSSYMPTOMS.csv", folderPath + "\\2021VAERSVAX.csv", ref header2);
            });

            task1.Wait();
            task2.Wait();

            Console.WriteLine("Begin merging new task 2 file into task 1 file");
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

            Dictionary<int, Row> task1Dict = new Dictionary<int, Row>();
            Dictionary<int, Row> task2Dict = new Dictionary<int, Row>();

            Task task4 = Task.Run(() =>
            {
                foreach (Row row in task1RowList)
                {
                    task1Dict.TryAdd(row.id, row);
                }
            });

            Task task5 = Task.Run(() =>
            {
                foreach (Row row in task2RowList)
                {
                    task2Dict.TryAdd(row.id, row);
                }
            });

            task4.Wait();
            task5.Wait();

            List<Row> mergeRowList = new List<Row>();

            while (begin <= end)
            {
                Row row1;
                bool foundList1 = task1Dict.TryGetValue(begin, out row1);
                Row row2;
                bool foundList2 = task2Dict.TryGetValue(begin, out row2);

                if (foundList1)
                    mergeRowList.Add(new Row(row1));
                else if (foundList2)
                    mergeRowList.Add(new Row(row2));

                begin++;
            }

            Console.WriteLine("Done merging new task 2 file into task 1 file");

            Console.WriteLine("Begin write merge csv file");
            writeCSV(mergeRowList, header1, folderPath + "\\mergeFile.csv");
            Console.WriteLine("Done write merge csv file");

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
                foreach (Row row in dataFile)
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

            foreach (Row row in vaxFile)
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
                else
                    Console.WriteLine("skip: " + currentID);
            }

            return newRowList;
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
    }
}
