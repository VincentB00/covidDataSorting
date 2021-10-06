using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace covidDataSorting
{
    public class CSVFileManager
    {
        public List<GridManager> GMs;
        public GridManager GM;

        //task 1 variable
        private int maxSymtomColumn;
        private int maxDataColumn;
        private int maxVaxColumn;

        public CSVFileManager()
        {
            GMs = new List<GridManager>();
        }
        public bool readCSVFile(FileLocation[] fls)
        {
            List<Task> taskList = new List<Task>();
            List<bool> checkFinish = new List<bool>();

            foreach(FileLocation fl in fls)
            {
                GMs.Add(new GridManager());
                int index = GMs.Count - 1;
                Task task = Task.Run(() =>
                {
                    bool finish = readCSVFile(fl.absolutePath, index);
                    checkFinish.Add(finish);
                });
                taskList.Add(task);
            }
            foreach (Task task in taskList)
                task.Wait();

            foreach (bool finish in checkFinish)
                if (!finish)
                    return false;

            return true;

        }
        public bool readCSVFile(String absolutePath, int index)
        {
            try
            {
                using (var steamReader = new StreamReader(absolutePath))
                {
                    var header = steamReader.ReadLine();
                    GMs[index].addHeader(header);

                    while (!steamReader.EndOfStream)
                    {
                        var line = steamReader.ReadLine();

                        GMs[index].addRow(line);
                        //Console.WriteLine(line);
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool readCSVFile(String absolutePath)
        {
            GMs.Add(new GridManager());
            int currentGMIndex = GMs.Count - 1;
            try
            {
                using (var steamReader = new StreamReader(absolutePath))
                {
                    var header = steamReader.ReadLine();
                    GMs[currentGMIndex].addHeader(header);

                    while (!steamReader.EndOfStream)
                    {
                        var line = steamReader.ReadLine();

                        GMs[currentGMIndex].addRow(line);
                        //Console.WriteLine(line);
                    }

                    return true;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool readCSVFile(String absolutePath, bool fillRow)
        {
            GMs.Add(new GridManager());
            int currentGMIndex = GMs.Count - 1;
            try
            {
                using (var steamReader = new StreamReader(absolutePath))
                {
                    var header = steamReader.ReadLine();
                    GMs[currentGMIndex].addHeader(header);

                    int maxColumn = GMs[currentGMIndex].rowList.First().columns.Count;

                    while (!steamReader.EndOfStream)
                    {
                        var line = steamReader.ReadLine();

                        if(fillRow)
                        {
                            Row row = new Row(line);
                            while(row.columns.Count < maxColumn)
                            {
                                row.columns.Add("");
                            }
                            GMs[currentGMIndex].addRow(row);
                        }
                        else
                            GMs[currentGMIndex].addRow(line);
                        //Console.WriteLine(line);
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public void combineCSVFile()
        {

            Row header0 = GMs[0].getData(0);
            Row header1 = GMs[1].getData(0);
            Row header2 = GMs[2].getData(0);

            //make a new Grid manager and add all header
            GM = new GridManager();
            GM.addHeader(GMs[0].getHeader());
            GM.addHeader(GMs[2].getHeader());
            //GM.addHeader(GMs[1].getHeader());

            maxSymtomColumn = 0;

            //note: GMs[0] is data file, GMs[1] is symtom file, GMs[2] is Vax file
            //now add the data for each groupd of 3 file
            do
            {
                //int splitBy = 20000;
                ////split Grid to many grid if nessessary
                //if (GMs[0].rowList.Count > splitBy)
                //{
                //    GridManager T0 = new GridManager();
                //    GridManager T1 = new GridManager();
                //    GridManager T2 = new GridManager();

                //    GMs[0].removeRowAt(0);
                //    GMs[1].removeRowAt(0);
                //    GMs[2].removeRowAt(0);

                //    int splitIndex = splitBy;
                //    int searchID = GMs[0].rowList[splitIndex].getIDColumn();
                    

                //    Task task0 = Task.Run(() =>
                //    {
                //        int SplitIDIndex = GMs[0].getLastIndexOfID(searchID);
                //        for (int count = 0; count <= SplitIDIndex; count++)
                //        {
                //            T0.addRow(GMs[0].popFirst());
                //        }
                //    });

                //    Task task1 = Task.Run(() =>
                //    {
                //        int SplitIDIndex = GMs[1].getLastIndexOfID(searchID);
                //        for (int count = 0; count <= SplitIDIndex; count++)
                //        {
                //            T1.addRow(GMs[1].popFirst());
                //        }
                //    });

                //    Task task2 = Task.Run(() =>
                //    {
                //        int SplitIDIndex  = GMs[2].getLastIndexOfID(searchID);
                //        for (int count = 0; count <= SplitIDIndex; count++)
                //        {
                //            T2.addRow(GMs[2].popFirst());
                //        }
                //    });

                //    task0.Wait();
                //    task1.Wait();
                //    task2.Wait();

                //    GMs[0].rowList.Insert(0, header0);
                //    GMs[1].rowList.Insert(0, header1);
                //    GMs[2].rowList.Insert(0, header2);

                //    T0.rowList.Insert(0, header0);
                //    T1.rowList.Insert(0, header1);
                //    T2.rowList.Insert(0, header2);

                //    GMs.Insert(0, T2);
                //    GMs.Insert(0, T1);
                //    GMs.Insert(0, T0);
                //}


                //loop variable
                int currentID;
                maxDataColumn = GMs[0].rowList[0].columns.Count;
                maxVaxColumn = GMs[2].rowList[0].columns.Count;

                //remove all header for each file
                GMs[0].removeRowAt(0);
                GMs[1].removeRowAt(0);
                GMs[2].removeRowAt(0);


                GMs[1].filterSymtom();
                GMs[2].filterVax();

                if (GMs[2].rowList.Count > 0)
                {
                    currentID = GMs[2].rowList.First().getIDColumn();

                    while (!GMs[2].isEmpty())
                    {
                        int minimumID = currentID;
                        //int numTask = 20;

                        //if (GMs[2].rowList.Count > numTask + 10)
                        //{
                        //    List<Task> taskList = new List<Task>();

                        //    for (int count = 0; count < numTask; count++)
                        //    {
                        //        int id = currentID;
                        //        Task task = Task.Run(() =>
                        //        {
                        //            searchRowByID(id);
                        //        });
                        //        taskList.Add(task);
                        //        currentID++;
                        //    }

                        //    //foreach (Task task in taskList)
                        //    //    task.Start();

                        //    foreach (Task task in taskList)
                        //        task.Wait();

                        //}
                        //else
                        //{
                        //    searchRowByID(currentID);

                        //    currentID++;
                        //}

                        Task task = Task.Run(() =>
                        {
                            searchRowByID(currentID);
                        });

                        task.Wait();

                        currentID++;

                        Task T1 = Task.Run(() =>
                        {
                            deleteOldRow(0, minimumID);
                        });

                        Task T2 = Task.Run(() =>
                        {
                            deleteOldRow(1, minimumID);
                        });
                        Task T3 = Task.Run(() =>
                        {
                            deleteOldRow(2, minimumID);
                        });

                        T1.Wait();
                        T2.Wait();
                        T3.Wait();
                    }
                }

                GMs.RemoveAt(2);
                GMs.RemoveAt(1);
                GMs.RemoveAt(0);
            }
            while (GMs.Count >= 3);

            //add in symtom column
            for(int count = 0; count < maxSymtomColumn / 2; count++)
            {
                int index = count + 1;
                GM.rowList[0].addData("SYMPTOM" + index);
                GM.rowList[0].addData("SYMPTOMVERSION" + index);
            }
            
        }

        private void deleteOldRow(int index, int minimumID)
        {
            int count = 0;
            bool exit = false;
            while (!exit && !GMs[index].isEmpty())
            {
                if (GMs[index].rowList[count].getIDColumn() < minimumID)
                    GMs[index].rowList.RemoveAt(count);
                else if (GMs[index].rowList[count].getIDColumn() >= minimumID)
                    exit = true;
                else
                    count++;
            }
        }

        private void searchRowByID(int currentID)
        {
            bool skip = false;

            Row tempRow = new Row();

            //handle Vax
            int counter3 = 0;
            Row vaxRow = GMs[2].getData(counter3);
            Task t3 = Task.Run(() =>
            {
                while (vaxRow != null && vaxRow.getIDColumn() != currentID)
                {
                    
                    counter3++;

                    if (counter3 >= GMs[2].rowList.Count)
                    {
                        skip = true;
                        //Console.WriteLine("skip: " + currentID);
                        break;
                    }

                    vaxRow = GMs[2].getData(counter3);
                }
            });

            //handle data
            int counter1 = 0;
            Row dataRow = GMs[0].getData(counter1);
            Task t1 = Task.Run(() =>
            {
                while (dataRow != null && dataRow.getIDColumn() != currentID)
                {
                    
                    counter1++;

                    if (counter1 >= GMs[0].rowList.Count)
                    {
                        skip = true;
                        //Console.WriteLine("skip: " + currentID);
                        break;
                    }

                    dataRow = GMs[0].getData(counter1);
                }
            });


            //handle symtom data
            int counter2 = 0;
            Row symtomRow = GMs[1].getData(counter2);
            Task t2 = Task.Run(() =>
            {
                while (symtomRow != null && symtomRow.getIDColumn() != currentID)
                {
                    counter2++;

                    if (counter2 >= GMs[1].rowList.Count)
                    {
                        skip = true;
                        //Console.WriteLine("skip: " + currentID);
                        break;
                    }

                    symtomRow = GMs[1].getData(counter2);
                }
            });


            t1.Wait();
            t2.Wait();
            t3.Wait();

            if (!skip)
            {
                while (dataRow.columns.Count < maxDataColumn)
                    dataRow.columns.Add("");
                
                while (vaxRow.columns.Count < maxVaxColumn)
                    vaxRow.columns.Add("");

                tempRow.addData(dataRow);

                for (int count = 1; count < vaxRow.columns.Count; count++)
                    tempRow.columns.Add(vaxRow.columns[count]);

                for (int count = 1; count < symtomRow.columns.Count; count++)
                    tempRow.columns.Add(symtomRow.columns[count]);

                if (maxSymtomColumn < symtomRow.countValidColumn())
                {
                    maxSymtomColumn = symtomRow.countValidColumn();
                    //Console.WriteLine(currentID);
                }

                GM.addRow(tempRow);
            }
            else
                Console.WriteLine("skip: " + currentID);
        }

        public void writeCSVFile(String absolutePath)
        {
            //write GM to a file
            using (StreamWriter sw = new StreamWriter(absolutePath))
            {
                foreach (Row row in GM.rowList)
                {
                    sw.WriteLine(row.ToString());
                }
            }
        }

        public void writeCSVFile(String absolutePath, int GMsIndex)
        {
            //write GM to a file
            using (StreamWriter sw = new StreamWriter(absolutePath))
            {
                foreach (Row row in GMs[GMsIndex].rowList)
                {
                    sw.WriteLine(row.ToString());
                }
            }
        }

        public void writeCSVFile(String absolutePath, int GMsIndex, List<int> orderList)
        {
            //write GM to a file
            using (StreamWriter sw = new StreamWriter(absolutePath))
            {
                foreach(int index in orderList)
                {
                    sw.WriteLine(GMs[GMsIndex].rowList[index].ToString());
                }
            }
        }
    }
}
