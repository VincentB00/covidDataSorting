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
        public void combineCSVFile()
        {
            try
            {
                //make a new Grid manager and add all header
                GM = new GridManager();
                GM.addHeader(GMs[0].getHeader());
                GM.addHeader(GMs[2].getHeader());
                GM.addHeader(GMs[1].getHeader());


                //note: GMs[0] is data file, GMs[1] is symtom file, GMs[2] is Vax file
                //now add the data for each groupd of 3 file
                do
                {
                    Console.WriteLine(GMs.Count);
                    //loop variable
                    String currentID;

                    //remove all header for each file
                    GMs[0].removeRowAt(0);
                    GMs[1].removeRowAt(0);
                    GMs[2].removeRowAt(0);

                    GMs[0].filterDupliacate();
                    GMs[1].filterDupliacate();
                    GMs[2].filterVax();

                    while (!GMs[2].isEmpty())
                    {
                        Row tempRow = new Row();
                        Row vaxRow = GMs[2].popFirst();

                        currentID = vaxRow.columns[0];
                        bool skip = false;


                        //handle data
                        Row dataRow = GMs[0].popFirst();
                        Task t1 = Task.Run(() =>
                        {
                            while (dataRow != null && dataRow.columns[0].CompareTo(currentID) != 0 && !skip)
                            {
                                if (GMs[0].rowList.Count > 1 && Int32.Parse(GMs[0].rowList[0].columns[0]) > Int32.Parse(currentID))
                                {
                                    skip = true;
                                    break;
                                }
                                else
                                    dataRow = GMs[0].popFirst();
                            }
                        });

                        //handle symtom data
                        Row symtomRow = GMs[1].popFirst();
                        Task t2 = Task.Run(() =>
                        {
                            while (symtomRow != null && symtomRow.columns[0].CompareTo(currentID) != 0 && !skip)
                            {
                                if (GMs[1].rowList.Count > 1 && Int32.Parse(GMs[1].rowList[0].columns[0]) > Int32.Parse(currentID))
                                {
                                    skip = true;
                                    break;
                                }
                                else
                                    symtomRow = GMs[1].popFirst();
                            }
                        });

                        
                        t1.Wait();
                        t2.Wait();

                        if (dataRow == null || symtomRow == null)
                            break;

                        if (!skip)
                        {
                            tempRow.addData(dataRow.ToString());
                            vaxRow.columns.RemoveAt(0);
                            tempRow.addData(vaxRow.ToString());
                            symtomRow.columns.RemoveAt(0);
                            tempRow.addData(symtomRow.ToString());
                            //Console.WriteLine(tempRow);
                            GM.addRow(tempRow);
                        }
                    }

                    GMs.RemoveAt(2);
                    GMs.RemoveAt(1);
                    GMs.RemoveAt(0);
                }
                while (GMs.Count >= 3);
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
                    row.clear();
                }
            }
        }
    }
}
