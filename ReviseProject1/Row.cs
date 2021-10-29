using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ReviseProject1
{
    class Row
    {
        public String line;
        public int id;
        public List<String> columns;

        public Row()
        {
            columns = new List<string>();
        }
        public Row(String line)
        {
            this.line = line;
            this.id = Int32.Parse(line.Substring(0, line.IndexOf(',')));
        }
        public Row(Row row)
        {
            this.line = row.line;
            this.id = Int32.Parse(line.Substring(0, line.IndexOf(',')));
            copyColumn(row);
        }
        public void copyColumn(Row row)
        {
            if (row.columns != null && row.columns.Count > 0)
            {
                columns = new List<string>();
                foreach (String data in row.columns)
                    columns.Add(data);
            }
        }

        public void breakDownLine()
        {
            columns = splitCSVData(line);
        }

        public static List<String> splitCSVData(String line)
        {
            string[] result = Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

            return result.ToList<String>();
        }
        public String toString()
        {
            return line;
        }







        public int countValidColumn()
        {
            int counter = 0;
            foreach (String data in columns)
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
            foreach (String data in lineList)
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
    }
}
