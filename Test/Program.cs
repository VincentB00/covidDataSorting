using System;
using System.Collections.Generic;
using System.Linq;
using covidDataSorting;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //String data = "0855055,01/02/2020,,,,,U,,\"shivering; soreness; feel bad; This case was reported by a consumer via call center representative and described the occurrence of shivering in a adult patient who received Herpes zoster(Shingrix) for prophylaxis.On an unknown date, the patient received the 1st dose of Shingrix.On an unknown date, unknown after receiving Shingrix, the patient experienced shivering, pain and malaise.On an unknown date, the outcome of the shivering, pain and malaise were not recovered / not resolved.It was unknown if the reporter considered the shivering, pain and malaise to be related to Shingrix.Additional details were provided as follows: The age at vaccination was not reported.The age group of the patient was not reported but selected as an adult as per vaccine indication. The patient received Shingrix vaccine and felt bad, no fever but shivering and soreness.The patient asked, how long did the side effects last.\",,,,,,,,,N,,,,,UNK,,,,,,USGLAXOSMITHKLINEUS2019AM,2,01/01/2020,,,,";
            //String data = "\"something, something\",,,,,N,,\"something, something\",w,";
            //String data = "0902479,12/15/2020,KS,46.0,46,,F,,\"rPfizer - BionNTech COVID - 19 Vaccine EUA  5 - 7 minutes after the vaccine Associate stated she did not feel right, mentioned chest pain. \"\"My chest feels funny. It feels like when you have really bad heartburn coming on\"\". \"\"I feel flushed like when you get contrast for a CT\"\".Pulse 90 BP 160 / 90 checked later 130 / 90\",,,,,Y,1,,,Y,12/14/2020,12/14/2020,0,Unknown but sent to SICU for monitoring,OTH,,unknown,No,unknown,,,2,12/15/2020,,,,No,\"Syracuse, NY\"";
            String data = "0902440,12/15/2020,AZ,35.0,35,,F,,C/O Headache,,,,,,,,,Y,12/15/2020,12/15/2020,0,none,PVT,,,,,,,2,12/15/2020,,,,";
            String vexData = "0902440,COVID19,PFIZER\\BIONTECH,EH 9899,1,SYR,LA,COVID19 (COVID19 (PFIZER-BIONTECH))";
            String symData = "0902440,Headache,23.10,,,,,,,,";


            Row row1 = new Row();
            Row row2 = new Row();
            Row row3 = new Row();
            Row temp = new Row();
            row1.addData(data);
            row2.addData(vexData);
            row3.addData(symData);
            //row1.columns.RemoveAt(0);
            temp.addData(row1.ToString());

            foreach (String dataT in temp.columns)
            {
                if (dataT.CompareTo("") == 0)
                    Console.WriteLine("None");
                else
                    Console.WriteLine(dataT);
            }

            //List<String> list = splitCSVData(data);


            //foreach (String dataT in list)
            //{
            //    if (dataT.CompareTo("") == 0)
            //        Console.WriteLine("None");
            //    else
            //        Console.WriteLine(dataT);
            //}
            //String data = "something, NY";
            //Console.WriteLine(data.Substring(data.IndexOf(',') + 1));

            //CSVFileManager cSVFileManager = new CSVFileManager();
            //cSVFileManager.readCSVFile("C:\\Users\\vince\\OneDrive\\study\\Oswego\\CSC365\\Project 1\\Data\\2020VAERSData.csv");
            //cSVFileManager.GMs[0].removeRowAt(0);
            //cSVFileManager.GMs[0].filterVax();
            //cSVFileManager.GMs[0].filterDupliacate();
            //List<Row> rowList = cSVFileManager.GMs[0].rowList;

            //Console.WriteLine("------------------------------------");

            //foreach (Row row in rowList)
            //    Console.WriteLine(row.ToString());

            //foreach(Row row in cSVFileManager.GMs[0].rowList)
            //    Console.WriteLine(row.ToString());
        }




        public static List<String> splitCSVData(String line)
        {
            List<String> list = new List<string>();

            while(line.IndexOf(',') >= 0)
            {
                String currentString = line.Substring(0, line.IndexOf(','));

                //Console.WriteLine("current String: " + currentString);

                if (currentString.IndexOf('\"') < 0)
                {
                    line = line.Substring(line.IndexOf(',') + 1);
                }
                else if(currentString.IndexOf('\"') >= 0)
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
                if(data.Length == index + 1)
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

        public static int indexOf(String data, String regrex)
        {
            bool skip = true;
            int index = data.IndexOf(regrex[0]);
            do
            {
                if (regrex.Length > 0)
                {
                    for (int count = 1; count < regrex.Length; count++)
                    {
                        if (data[index + count].CompareTo(regrex[count]) != 0)
                        {
                            data = data.Substring(0, index) + "0" + data.Substring(index + 1);
                            skip = false;
                        }
                    }
                }
                index = data.IndexOf(regrex[0]);
            }
            while (!skip);

            return index;
        }
    }
}
