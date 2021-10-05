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
        public CSVFileManager()
        {
            GMs = new List<GridManager>();
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
            try
            {
                //make a new Grid manager and add all header
                GM = new GridManager();
                GM.addHeader(GMs[0].getHeader());
                GM.addHeader(GMs[2].getHeader());
                //GM.addHeader(GMs[1].getHeader());

                int maxSymtomColumn = 0;

                //note: GMs[0] is data file, GMs[1] is symtom file, GMs[2] is Vax file
                //now add the data for each groupd of 3 file
                do
                {
                    //loop variable
                    int currentID;
                    int maxDataColumn = GMs[0].rowList[0].columns.Count;
                    int maxVaxColumn = GMs[2].rowList[0].columns.Count;

                    //remove all header for each file
                    GMs[0].removeRowAt(0);
                    GMs[1].removeRowAt(0);
                    GMs[2].removeRowAt(0);

                    GMs[1].filterSymtom();
                    GMs[2].filterVax();

                    currentID = GMs[2].rowList.First().getIDColumn();

                    while (!GMs[2].isEmpty())
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
                                if(vaxRow.getIDColumn() < currentID)
                                {
                                    GMs[2].rowList.RemoveAt(counter3);
                                }
                                else
                                    counter3++;

                                if (counter3 >= GMs[2].rowList.Count)
                                {
                                    skip = true;
                                    Console.WriteLine("skip: " + currentID);
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
                                if (dataRow.getIDColumn() < currentID)
                                {
                                    GMs[0].rowList.RemoveAt(counter1);
                                }
                                else
                                    counter1++;

                                if (counter1 >= GMs[0].rowList.Count)
                                {
                                    skip = true;
                                    Console.WriteLine("skip: " + currentID);
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
                                if (symtomRow.getIDColumn() < currentID)
                                {
                                    GMs[1].rowList.RemoveAt(counter2);
                                }
                                else
                                    counter2++;

                                if(counter2 >= GMs[1].rowList.Count)
                                {
                                    skip = true;
                                    Console.WriteLine("skip: " + currentID);
                                    break;
                                }

                                symtomRow = GMs[1].getData(counter2);
                            }
                        });


                        t1.Wait();
                        t2.Wait();
                        t3.Wait();

                        if (dataRow == null || symtomRow == null)
                            break;


                        if (!skip)
                        {
                            while (dataRow.columns.Count < maxDataColumn)
                                dataRow.columns.Add("");
                            tempRow.addData(dataRow);

                            while (vaxRow.columns.Count < maxVaxColumn)
                                vaxRow.columns.Add("");

                            vaxRow.columns.RemoveAt(0);
                            tempRow.addData(vaxRow);

                            symtomRow.columns.RemoveAt(0);
                            tempRow.addData(symtomRow);

                            if (maxSymtomColumn < symtomRow.countValidColumn())
                            {
                                maxSymtomColumn = symtomRow.countValidColumn();
                                //Console.WriteLine(currentID);
                            }

                            GMs[0].rowList.RemoveAt(counter1);
                            GMs[1].rowList.RemoveAt(counter2);
                            GMs[2].rowList.RemoveAt(counter3);

                            GM.addRow(tempRow);

                            
                        }

                        currentID++;
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
            }
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
