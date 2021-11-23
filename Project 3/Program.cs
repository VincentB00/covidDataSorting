using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Project_3
{
    class Program
    {
        static void Main(string[] args)
        {
            String header = "";
            String input = "";
            List<Row> task1File = new List<Row>();

            String folderPath = "C:\\Users\\vince\\OneDrive\\study\\Oswego\\CSC365\\Project 3\\data";

            Console.WriteLine("Do you want to read and combine 2021 3 file to VERSData_ML.csv <Y|N>: ");
            input = Console.ReadLine();

            if(input.ToUpper().CompareTo("Y") == 0)
            {
                task1File = CombineFile(folderPath + "\\2021VAERSDATA.csv", folderPath + "\\2021VAERSSYMPTOMS.csv", folderPath + "\\2021VAERSVAX.csv", ref header);
                Console.WriteLine("Begin write task 1 file");
                writeCSV(task1File, header, folderPath + "\\VAERSData_ML.csv");
                Console.WriteLine("Done write task 1 file");
            }
            else
            {
                Console.WriteLine("Begin read task 1 file");
                readCSV(task1File, ref header, folderPath + "\\VAERSData_ML.csv");
                Console.WriteLine("done read task 1 file");
            }


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
            //fileHeader += "," + header.Substring(header.IndexOf(",") + 1);
            Console.WriteLine("Done read 3 new file");

            List<int> columnIndex = new List<int>() { 0, 36, 1, 3, 6, 9, 10, 18 };

            Row headerRow = new Row(fileHeader, false);
            headerRow.breakDownLine();
            headerRow = new Row(headerRow, columnIndex, false);

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

            Task task3 = Task.Run(() =>
            {
                dataList = filterData(dataList);
            });

            task1.Wait();
            task2.Wait();
            task3.Wait();

            Console.WriteLine("Done filtering all file");

            List<Row> file = new List<Row>();
            Console.WriteLine("Begin Comebine file");
            int maxSymtom = 0;
            file = combineFile(dataList, symtomList, vaxList, ref maxSymtom);
            Console.WriteLine("Done Comebine file");

            taskList.Clear();
            symtomList.Clear();
            vaxList.Clear();
            dataList.Clear();

            for(int count = 0; count < maxSymtom; count ++)
            {
                headerRow.columns.Add("symptom_" + (count + 1));
            }

            fileHeader = headerRow.ToString();

            return file;
        }

        public static List<Row> combineFile(List<Row> dataFile, List<Row> symtomFile, List<Row> vaxFile, ref int maxSymtom)
        {
            List<Row> newRowList = new List<Row>();
            //BPTree dataTree = new BPTree(3);
            //BPTree symtomTree = new BPTree(3);
            Dictionary<int, Row> dataDict = new Dictionary<int, Row>();
            Dictionary<int, Row> symtomDict = new Dictionary<int, Row>();
            bool skip = false;
            Row dataRow, vaxRow, symtomRow, tempRow;
            int maxDataColumn = 5;
            int maxVaxColumn = 2;
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

                    dataRow.columns.RemoveAt(0);

                    for (int count = 1; count < vaxRow.columns.Count; count++)
                        tempRow.columns.Add(vaxRow.columns[count]);

                    tempRow.addData(dataRow);

                    for (int count = 1; count < symtomRow.columns.Count; count++)
                        tempRow.columns.Add(symtomRow.columns[count]);

                    //increase max symtom
                    if (symtomRow.columns.Count > maxSymtom)
                        maxSymtom = symtomRow.columns.Count;

                    tempRow.columns.Insert(0, currentID + "");

                    newRowList.Add(new Row(tempRow.ToString()));
                }
                else
                    Console.WriteLine("skip: " + currentID);
            }

            return newRowList;
        }

        public static List<Row> filterVax(List<Row> rowList)
        {
            List<Row> newRowList = new List<Row>();
            foreach (Row row in rowList)
            {
                if (row.columns[1].CompareTo("COVID19") == 0)
                {
                    List<int> columnIndex = new List<int>() {0, 2};
                    newRowList.Add(new Row(row, columnIndex));
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
                    if(column %2 != 0)
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

                    newRowList.Add(new Row(row.ToString()));

                    row.clear();
                }
            }

            rowList.Clear();

            return newRowList;


        }

        public static List<Row> filterData(List<Row> rowList)
        {
            List<Row> newRowList = new List<Row>();
            foreach (Row row in rowList)
            {
                List<int> columnIndex = new List<int>() { 0, 1, 3, 6, 9, 10, 18 };
                newRowList.Add(new Row(row, columnIndex));
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
    }
}
