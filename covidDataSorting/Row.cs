using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            List<String> list = new List<string>();

            while (line.IndexOf(',') >= 0)
            {
                String currentString = line.Substring(0, line.IndexOf(','));

                //Console.WriteLine("current String: " + currentString);

                if (currentString.IndexOf('\"') < 0)
                {
                    line = line.Substring(line.IndexOf(',') + 1);
                }
                else if (currentString.IndexOf('\"') >= 0)
                {
                    String tempString = line.Substring(line.IndexOf(','));
                    int nextDQouteIndex = getIndexNextQoute(tempString);

                    //Console.WriteLine(tempString.Substring(nextDQouteIndex + 1).Length);

                    if (tempString.Substring(nextDQouteIndex + 1).Length == 0)
                    {
                        line = "-1";
                    }
                    else
                        line = tempString.Substring(nextDQouteIndex + 2);

                    tempString = tempString.Substring(0, nextDQouteIndex + 1);
                    currentString = currentString + tempString;
                }
                list.Add(currentString);

                //Console.WriteLine("Current String after format: " + currentString);

                //Console.WriteLine("Current Line: " + line);

            }

            if (line.CompareTo("-1") != 0)
                list.Add(line);

            return list;
        }

        public static int getIndexNextQoute(String data)
        {
            bool skip = false;
            int index = data.IndexOf('\"');
            while (!skip)
            {
                index = data.IndexOf('\"');
                if (data.Length == index + 1)
                {
                    return index;
                }

                if (data[index + 1] == '\"')
                {
                    data = data.Substring(0, index) + "00" + data.Substring(index + 2);
                    skip = false;
                }
                else
                {
                    skip = true;
                    return index;
                }
            }
            return index;
        }
    }
}
