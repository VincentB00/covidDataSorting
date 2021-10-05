using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace covidDataSorting
{
    public class Row
    {
        public List<String> columns;
        public Row()
        {
            columns = new List<string>();
        }

        public Row(Row row)
        {
            columns = new List<string>();
            foreach (String data in row.columns)
                columns.Add(data);
        }

        public Row(String line)
        {
            columns = new List<string>();
            addData(line);
        }

        public int countValidColumn()
        {
            int counter = 0;
            foreach(String data in columns)
            {
                if (data != null && data.CompareTo("") != 0)
                    counter++;
            }

            return counter;
        }

        public int getIDColumn()
        {
            //assume that id column is alway at column 1
            return Int32.Parse(columns[0]);
        }

        public void addData(Row row)
        {
            foreach (String data in row.columns)
                columns.Add(data);
        }

        public void addData(String line)
        {
            List<String> lineList = Row.splitCSVData(line);
            foreach(String data in lineList)
                columns.Add(data);
        }

        public void addData(String line, int except)
        {
            List<String> lineList = Row.splitCSVData(line);
            lineList.RemoveAt(except);
            foreach (String data in lineList)
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

        public void clear()
        {
            columns.Clear();
        }

        public static List<String> splitCSVData(String line)
        {
            string[] result = Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
            
            return result.ToList<String>();
        }

        public static int getIndexNextQoute(String data)
        {
            bool skip = false;
            int index = data.IndexOf("\"");
            while (!skip)
            {

                index = data.IndexOf("\"");

                if (data.Length == index + 1)
                {
                    return index;
                }

                index = data.IndexOf("\",");

                if (index < 0)
                    return index;
                else if (data[index - 1] == '\"' && data[index - 2] == '\"')
                {
                    return index;
                }
                else if (data[index - 1] == '\"')
                {
                    data = data.Substring(0, index - 1) + "00" + data.Substring(index + 1);
                    skip = false;
                }
                else
                {
                    return index;
                }
            }

            return index;
        }
    }
}
