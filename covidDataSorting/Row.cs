using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace covidDataSorting
{
    class Row
    {
        List<String> columns;
        public Row()
        {
            columns = new List<string>();
        }

        public void addData(String line)
        {
            List<String> lineList = line.Split(',').ToList<String>();
            foreach(String data in lineList)
            columns.Add(data);
        }

        public override string ToString()
        {
            String result = "";
            foreach (String data in columns)
                result += data + ",";
            result = result.Substring(0, result.Length - 1);

            return result;
        }
    }
}
