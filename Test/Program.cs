using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using covidDataSorting;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //String data = "0855055,01/02/2020,,,,,U,,\"shivering; soreness; feel bad; This case was reported by a consumer via call center representative and described the occurrence of shivering in a adult patient who received Herpes zoster(Shingrix) for prophylaxis.On an unknown date, the patient received the 1st dose of Shingrix.On an unknown date, unknown after receiving Shingrix, the patient experienced shivering, pain and malaise.On an unknown date, the outcome of the shivering, pain and malaise were not recovered / not resolved.It was unknown if the reporter considered the shivering, pain and malaise to be related to Shingrix.Additional details were provided as follows: The age at vaccination was not reported.The age group of the patient was not reported but selected as an adult as per vaccine indication. The patient received Shingrix vaccine and felt bad, no fever but shivering and soreness.The patient asked, how long did the side effects last.\",,,,,,,,,N,,,,,UNK,,,,,,USGLAXOSMITHKLINEUS2019AM,2,01/01/2020,,,,";
            //String data = "\"something, something\",,,,,N,,\"something, something\",w,";
            //String vexData = "0902440,COVID19,PFIZER\\BIONTECH,EH 9899,1,SYR,LA,COVID19 (COVID19 (PFIZER-BIONTECH))";
            //String symData = "0902440,Headache,23.10,,,,,,,,";
            String data1 = "4,12/15/2020,KS,46.0,46,,F,,\"rPfizer - BionNTech COVID - 19 Vaccine EUA  5 - 7 minutes after the vaccine Associate stated she did not feel right, mentioned chest pain. \"\"My chest feels funny. It feels like when you have really bad heartburn coming on\"\". \"\"I feel flushed like when you get contrast for a CT\"\".Pulse 90 BP 160 / 90 checked later 130 / 90\",,,,,Y,1,,,Y,12/14/2020,12/14/2020,0,Unknown but sent to SICU for monitoring,OTH,,unknown,No,unknown,,,2,12/15/2020,,,,No,\"Syracuse, NY\"";
            String data2 = "2,12/15/2020,AZ,35.0,35,,F,,C/O Headache,,,,,,,,,Y,12/15/2020,12/15/2020,0,none,PVT,,,,,,,2,12/15/2020,,,,";
            String data3 = "1,12/17/2020,ND,33.0,33,,M,,\"While waiting the 15 minute after vaccination the Patient noticed a \"\"tingling\"\" feeling in throat about 5 minutes after vaccine was given.Time: 0930 - Oxygen Saturation was 98 % Pulse Rate 100.Patient stated that the feeling went away and then came back.   Time: 0938 - Again vital signs were taken BP 156 / 88 Oxygen Saturation 100 % Pulse Rate 83.Time: 0942 - 50 mg of Benadryl PO were given with water at this time.Patient was taken to ER for evaluation.\",,,,,,,,,U,12/17/2020,12/17/2020,0,,PVT,,None,None,Congenital One Kidney,,,2,12/17/2020,,,Y,\", at Dander - Reaction of Rash, Itchy Eyes\",COVID19,PFIZER\\BIONTECH,EH9899,1,SYR,RA,COVID19 (COVID19 (PFIZER-BIONTECH)),Pharyngeal paraesthesia,23.10,,,,,,,,";
            String data4 = "3,12/15/2020,,59.0,59,,M,,\"Within 1 minute, patient complained of symptoms of lightheadedness, flushing, asked for water.Symptoms persisted, reported vagal and \"\"spacey\"\", vitals were 117 / 91, HR 67, O2 sat 99 % on room air.Reported chest heaviness, shortness of breath and within 5 minutes developed rigors and  urge to defecate. 911 called, repeat vital 150 / 89 HR 113 O2sat 97 %, continues to want to defecate.  1 loose BM, transferred to ED\",,,,,,,,,N,12/15/2020,12/15/2020,0,Trransferred to ED,PVT,,unknown,no,unknown,\"vaso - vagal type symptoms, similar presentation 12 hours post vaccine\",,2,12/15/2020,,,Y,unknown,COVID19,PFIZER\\BIONTECH,EH9899,1,IM,LA,COVID19 (COVID19 (PFIZER-BIONTECH)),Dyspnoea,24.00,Feeling abnormal,24.00,Flushing,24.00,Presyncope,24.00,,";
            String data5 = "5,12/16/2020,MD,32.0,32,,F,,\"redness around the injection spot, fever chills, Stomach Ache, Body Ache, Short of breath(walking up stairs ), Headaches no appetite, Dry heating .\",,,,,,,,,N,12/14/2020,12/14/2020,0,n/a,PVT,,\"Daily 60mg Prozac, Vitamin D Supplement, Tylenol  6mg melatonin\",None,None,,vsafe,2,12/16/2020,,Y,,Morphine,COVID19,PFIZER\\BIONTECH,EH9899,1,IM,LA,COVID19 (COVID19 (PFIZER-BIONTECH)),Injection site erythema,23.10,Pain,23.10,Pyrexia,23.10,Retching,23.10,,";
            String data6 = "9,12/17/2020,AK,57.0,57,,F,,\"Heart rate increase, flushed for about 20 minutes.She has an internal monitor/defibrillator.States \"\"it feels like when they do a dvice check.\"\"\",,,,,,,,,Y,12/17/2020,12/17/2020,0,,PVT,,unknown,none,\"obesity, cardiac disease with internal monitor/defibrillator\",,,2,12/17/2020,,,,unknown,COVID19,PFIZER\\BIONTECH,EK5730,1,IM,RA,COVID19 (COVID19 (PFIZER-BIONTECH)),Flushing,23.10,Heart rate increased,23.10,,,,,,";
            String data7 = "7,12/21/2020,IL,52.0,52,,M,,\", I had slight fever 99.6 - 101.0 and chills for a period of 8 hours\",,,,,,,,,Y,12/17/2020,12/18/2020,1,none,PVT,,\"Metformin, Atorvastatin, Carvedilol, Aspirin, Lisinopril\",none,\"Diabetic, Hypertension\",,,2,12/21/2020,,,,NKA,COVID19,PFIZER\\BIONTECH,EK5730,1,SYR,LA,COVID19 (COVID19 (PFIZER-BIONTECH)),Chills,24.00,Pyrexia,24.00,,,,,,";


            string[] result = Regex.Split(data1, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

            //List<String> result = splitCSVData(data1);

            foreach (String data in result)
            {
                if (data.CompareTo("") == 0)
                {
                    Console.WriteLine("null");
                }
                else
                    Console.WriteLine(data);
            }

            //Row row1 = new Row();
            //Row row2 = new Row();
            //Row row3 = new Row();
            //Row temp = new Row();
            //row1.addData(data);
            //row2.addData(vexData);
            //row3.addData(symData);
            ////row1.columns.RemoveAt(0);
            //temp.addData(row1.ToString());

            //foreach (String dataT in row1.columns)
            //{
            //    if (dataT.CompareTo("") == 0)
            //        Console.WriteLine("None");
            //    else
            //        Console.WriteLine(dataT);
            //}

            //List<String> list = splitCSVData(data);

            //foreach (String dataT in list)
            //{
            //    if (dataT.CompareTo("") == 0)
            //        Console.WriteLine("None");
            //    else
            //        Console.WriteLine(dataT);
            //}

            //Console.WriteLine("length: " + list.Count);

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


            //String something = "s\",omething, NY, boston,something";
            //string searchString = "\",";
            //Console.WriteLine(something.IndexOf(searchString));

            //int[] array = new int[20];
            //Random random = new Random();
            //for(int count = 0; count < array.Length; count++)
            //{
            //    array[count] = random.Next(0, array.Length);
            //}

            ////Quick_Sort(array, 0, array.Length - 1);
            //insertionSort(array);

            //foreach (int num in array)
            //{
            //    Console.Write(num + " | ");
            //}

            //GridManager gm = new GridManager();
            //gm.addRow(data1);
            //gm.addRow(data2);
            //gm.addRow(data3);
            //gm.addRow(data4);
            //gm.addRow(data5);
            //gm.addRow(data6);
            //gm.addRow(data7);

            //RowDataSet rds = new RowDataSet(gm.rowList);
            //gm.quickSort(rds, 0, rds.rowList.Count - 1);
            //gm.InsertionSort(rds);
            //gm.SelectionSort(rds);
            //rds.swap(1, 2);
            //rds.swap(1, 2);
            //rds.setOrderIndexAt(0, 1);

            //foreach (int index in rds.orderList)
            //{
            //    Console.WriteLine(rds.rowList[index].columns[0]);
            //}
        }

        static void insertionSort(int[] arr)
        {
            int n = arr.Length;
            for (int i = 1; i < n; ++i)
            {
                int j = i;

                while (j > 0 && arr[j - 1] > arr[j])
                {
                    int temp = arr[j];
                    arr[j] = arr[j - 1];
                    arr[j - 1] = temp;


                    j = j - 1;
                }
            }
        }




        private static void Quick_Sort(int[] arr, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(arr, left, right);

                Quick_Sort(arr, left, pivot - 1);
                
                Quick_Sort(arr, pivot + 1, right);
                
            }

        }

        private static int Partition(int[] arr, int left, int right)
        {
            int pivot = arr[right];

            int i = left - 1;

            for(int j = left; j <= right - 1; j++)
            {
                if(arr[j] < pivot)
                {
                    i++;
                    swap(arr, i, j);
                }
            }

            swap(arr, i + 1, right);

            return i + 1;
            
        }

        public static void swap(int[] array, int index1, int index2)
        {
            int temp = index1;
            array[index1] = index2;
            array[index2] = temp;
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
                else if (currentString.First() == '\"' && currentString.Last() == '\"')
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
                else if(data[index - 1] == '\"' && data[index - 2] == '\"')
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

        //public static int getIndexNextQoute(String data)
        //{
        //    bool skip = false;
        //    int index = data.IndexOf('\"');
        //    while (!skip)
        //    {
        //        index = data.IndexOf('\"');
        //        if (data.Length == index + 1)
        //        {
        //            return index;
        //        }

        //        if (data[index + 1] == '\"')
        //        {
        //            data = data.Substring(0, index) + "00" + data.Substring(index + 2);
        //            skip = false;
        //        }
        //        else if (data[index + 1] == ',')
        //        {
        //            return index;
        //        }
        //        else
        //        {
        //            return index;
        //        }
        //    }
        //    return index;
        //}



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
