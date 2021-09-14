using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace covidDataSorting
{
    class CSVFileManager
    {
        List<GridManager> GMs;
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

        public bool writeCSVFile(String absolutePath)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(absolutePath))
                {
                    foreach (GridManager gridFile in GMs)
                    {
                        foreach (Row row in gridFile.rowList)
                            sw.WriteLine(row.ToString());
                    }
                }
                
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
