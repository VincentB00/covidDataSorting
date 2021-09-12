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
        GridManager GM;
        public CSVFileManager()
        {
            GM = new GridManager();
        }
        public bool readCSVFile(String absolutePath)
        {
            try
            {
                using (var steamReader = new StreamReader(absolutePath))
                {
                    var header = steamReader.ReadLine();
                    GM.addHeader(header);

                    while (!steamReader.EndOfStream)
                    {
                        var line = steamReader.ReadLine();
                        GM.addRow(line);
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
                    foreach (Row row in GM.rowList)
                        sw.WriteLine(row.ToString());
                }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public List<Row> getGrid()
        {
            return GM.rowList;
        }
    }
}
