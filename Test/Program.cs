using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using covidDataSorting;
using System.Threading;

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
            //String data1 = "4,12/15/2020,KS,46.0,46,,F,,\"rPfizer - BionNTech COVID - 19 Vaccine EUA  5 - 7 minutes after the vaccine Associate stated she did not feel right, mentioned chest pain. \"\"My chest feels funny. It feels like when you have really bad heartburn coming on\"\". \"\"I feel flushed like when you get contrast for a CT\"\".Pulse 90 BP 160 / 90 checked later 130 / 90\",,,,,Y,1,,,Y,12/14/2020,12/14/2020,0,Unknown but sent to SICU for monitoring,OTH,,unknown,No,unknown,,,2,12/15/2020,,,,No,\"Syracuse, NY\"";
            //String data2 = "2,12/15/2020,AZ,35.0,35,,F,,C/O Headache,,,,,,,,,Y,12/15/2020,12/15/2020,0,none,PVT,,,,,,,2,12/15/2020,,,,";
            //String data3 = "1,12/17/2020,ND,33.0,33,,M,,\"While waiting the 15 minute after vaccination the Patient noticed a \"\"tingling\"\" feeling in throat about 5 minutes after vaccine was given.Time: 0930 - Oxygen Saturation was 98 % Pulse Rate 100.Patient stated that the feeling went away and then came back.   Time: 0938 - Again vital signs were taken BP 156 / 88 Oxygen Saturation 100 % Pulse Rate 83.Time: 0942 - 50 mg of Benadryl PO were given with water at this time.Patient was taken to ER for evaluation.\",,,,,,,,,U,12/17/2020,12/17/2020,0,,PVT,,None,None,Congenital One Kidney,,,2,12/17/2020,,,Y,\", at Dander - Reaction of Rash, Itchy Eyes\",COVID19,PFIZER\\BIONTECH,EH9899,1,SYR,RA,COVID19 (COVID19 (PFIZER-BIONTECH)),Pharyngeal paraesthesia,23.10,,,,,,,,";
            //String data4 = "3,12/15/2020,,59.0,59,,M,,\"Within 1 minute, patient complained of symptoms of lightheadedness, flushing, asked for water.Symptoms persisted, reported vagal and \"\"spacey\"\", vitals were 117 / 91, HR 67, O2 sat 99 % on room air.Reported chest heaviness, shortness of breath and within 5 minutes developed rigors and  urge to defecate. 911 called, repeat vital 150 / 89 HR 113 O2sat 97 %, continues to want to defecate.  1 loose BM, transferred to ED\",,,,,,,,,N,12/15/2020,12/15/2020,0,Trransferred to ED,PVT,,unknown,no,unknown,\"vaso - vagal type symptoms, similar presentation 12 hours post vaccine\",,2,12/15/2020,,,Y,unknown,COVID19,PFIZER\\BIONTECH,EH9899,1,IM,LA,COVID19 (COVID19 (PFIZER-BIONTECH)),Dyspnoea,24.00,Feeling abnormal,24.00,Flushing,24.00,Presyncope,24.00,,";
            //String data5 = "5,12/16/2020,MD,32.0,32,,F,,\"redness around the injection spot, fever chills, Stomach Ache, Body Ache, Short of breath(walking up stairs ), Headaches no appetite, Dry heating .\",,,,,,,,,N,12/14/2020,12/14/2020,0,n/a,PVT,,\"Daily 60mg Prozac, Vitamin D Supplement, Tylenol  6mg melatonin\",None,None,,vsafe,2,12/16/2020,,Y,,Morphine,COVID19,PFIZER\\BIONTECH,EH9899,1,IM,LA,COVID19 (COVID19 (PFIZER-BIONTECH)),Injection site erythema,23.10,Pain,23.10,Pyrexia,23.10,Retching,23.10,,";
            //String data6 = "9,12/17/2020,AK,57.0,57,,F,,\"Heart rate increase, flushed for about 20 minutes.She has an internal monitor/defibrillator.States \"\"it feels like when they do a dvice check.\"\"\",,,,,,,,,Y,12/17/2020,12/17/2020,0,,PVT,,unknown,none,\"obesity, cardiac disease with internal monitor/defibrillator\",,,2,12/17/2020,,,,unknown,COVID19,PFIZER\\BIONTECH,EK5730,1,IM,RA,COVID19 (COVID19 (PFIZER-BIONTECH)),Flushing,23.10,Heart rate increased,23.10,,,,,,";
            //String data7 = "7,12/21/2020,IL,52.0,52,,M,,\", I had slight fever 99.6 - 101.0 and chills for a period of 8 hours\",,,,,,,,,Y,12/17/2020,12/18/2020,1,none,PVT,,\"Metformin, Atorvastatin, Carvedilol, Aspirin, Lisinopril\",none,\"Diabetic, Hypertension\",,,2,12/21/2020,,,,NKA,COVID19,PFIZER\\BIONTECH,EK5730,1,SYR,LA,COVID19 (COVID19 (PFIZER-BIONTECH)),Chills,24.00,Pyrexia,24.00,,,,,,";



            bool stopClock = false;
            int h = 0, m = 0, s = 0;

            Task clock = new Task(() =>
            {
                h = 0;
                m = 0;
                s = 0;
                while (!stopClock)
                {
                    Console.WriteLine(nextSecondTick(ref h, ref m, ref s));

                    Thread.Sleep(1000);
                }
            });

            List<String> executeTime = new List<string>();
            List<Pair> pairList = new List<Pair>();
            
            String header = "";
            //readdata
            String absolutePath = "C:\\Users\\vince\\OneDrive\\study\\Oswego\\CSC365\\Project 1\\Data\\Test data\\V04\\Task2.csv";
            //String absolutePath = "C:\\Users\\vince\\OneDrive\\study\\Oswego\\CSC365\\Project 1\\Data\\T\\T2 shuffle.csv";
            String folderPath = absolutePath.Substring(0, absolutePath.LastIndexOf('\\'));
            //readCSV(pairList, ref header, absolutePath);


            //insertionSort
            Console.WriteLine("Begin insertion sort");
            clock.Start();
            insertionSort(pairList);
            executeTime.Add(String.Format("{0}:{1}:{2}", h.ToString(), m.ToString(), s.ToString()));
            //print something
            Console.Write("insertion sort Execute time: ");
            Console.WriteLine(String.Format("{0}:{1}:{2}", h.ToString(), m.ToString(), s.ToString()));

            //write sorted File
            writeCSV(pairList, header, folderPath + "\\InsertionSort.csv");
            pairList.Clear();


            //------------------------------------------------------------------------------------

            //readdata
            readCSV(pairList, ref header, absolutePath);

            //quick sort
            Console.WriteLine("Begin Quick sort");
            h = 0;
            m = 0;
            s = 0;
            Quick_Sort(pairList, 0, pairList.Count - 1);
            executeTime.Add(String.Format("{0}:{1}:{2}", h.ToString(), m.ToString(), s.ToString()));

            //print something
            Console.Write("quick sort Execute time: ");
            Console.WriteLine(String.Format("{0}:{1}:{2}", h.ToString(), m.ToString(), s.ToString()));

            //write sorted File
            writeCSV(pairList, header, folderPath + "\\QuickSort.csv");
            pairList.Clear();


            //------------------------------------------------------------------------------------

            //readdata
            readCSV(pairList, ref header, absolutePath);

            //Heap sort
            Console.WriteLine("Begin Heap sort");
            h = 0;
            m = 0;
            s = 0;
            Quick_Sort(pairList, 0, pairList.Count - 1);
            executeTime.Add(String.Format("{0}:{1}:{2}", h.ToString(), m.ToString(), s.ToString()));

            //print something
            Console.Write("Heap sort Execute time: ");
            Console.WriteLine(String.Format("{0}:{1}:{2}", h.ToString(), m.ToString(), s.ToString()));

            //write sorted File
            writeCSV(pairList, header, folderPath + "\\HeapSort.csv");
            pairList.Clear();

            //-----------------------------------------------------------------------------------------------------

            //print result
            stopClock = true;
            using (StreamWriter sw = File.AppendText(folderPath + "\\Runtime result.TXT"))
            {
                sw.WriteLine(DateTime.Now + " Insertion sort --> " + executeTime[0]);
                sw.WriteLine(DateTime.Now + " Quick sort --> " + executeTime[1]);
                sw.WriteLine(DateTime.Now + " Heap sort --> " + executeTime[2]);
            }

            //-----------------------------------------------------------------------------------------------------
            List<Row> rowListL1 = new List<Row>();
            List<Row> rowList1x3 = new List<Row>();
            List<Row> rowList4x11 = new List<Row>();
            List<Row> rowList12x18 = new List<Row>();
            List<Row> rowList19x30 = new List<Row>();
            List<Row> rowList31x40 = new List<Row>();
            List<Row> rowList41x50 = new List<Row>();
            List<Row> rowList51x60 = new List<Row>();
            List<Row> rowList61x70 = new List<Row>();
            List<Row> rowList71x80 = new List<Row>();
            List<Row> rowListB80 = new List<Row>();
            List<Row> rowListNA = new List<Row>();

            using (var steamReader = new StreamReader(absolutePath))
            {
                header = steamReader.ReadLine();

                while (!steamReader.EndOfStream)
                {
                    String line = steamReader.ReadLine();
                    String temp = line.Substring(line.IndexOf(',') + 1, line.IndexOf(',') + 5);
                    float age;
                    try
                    {
                        age = float.Parse(temp.Substring(0, temp.IndexOf(',')).ToString());
                    }
                    catch(Exception ex)
                    {
                        age = -1;
                    }
                    bool death = line.IndexOf(",Y,") >= 0;

                    if (death)
                    {
                        Row row = new Row(line);

                        death = row.columns[6].CompareTo("Y") == 0;

                        if (death)
                        {
                            if (age < 1)
                                rowListL1.Add(row);
                            else if (age >= 1 && age <= 3)
                                rowList1x3.Add(row);
                            else if (age >= 4 && age <= 11)
                                rowList4x11.Add(row);
                            else if (age >= 12 && age <= 18)
                                rowList12x18.Add(row);
                            else if (age >= 19 && age <= 30)
                                rowList19x30.Add(row);
                            else if (age >= 31 && age <= 40)
                                rowList31x40.Add(row);
                            else if (age >= 41 && age <= 50)
                                rowList41x50.Add(row);
                            else if (age >= 51 && age <= 60)
                                rowList51x60.Add(row);
                            else if (age >= 61 && age <= 70)
                                rowList61x70.Add(row);
                            else if (age >= 71 && age <= 80)
                                rowList71x80.Add(row);
                            else if (age > 80)
                                rowListB80.Add(row);
                            else if (age < 0)
                                rowListNA.Add(row);
                        }
                    }
                        

                }

                using (StreamWriter sw = File.AppendText(folderPath + "\\Death Counter.TXT"))
                {
                    int total = 0;

                    int ageTemp = countdeath(rowListL1);
                    total += ageTemp;
                    sw.WriteLine("age: < 1 --> " + ageTemp);

                    ageTemp = countdeath(rowList1x3);
                    total += ageTemp;
                    sw.WriteLine("age: 1 - 3 --> " + ageTemp);

                    ageTemp = countdeath(rowList4x11);
                    total += ageTemp;
                    sw.WriteLine("age: 4 - 11 --> " + ageTemp);

                    ageTemp = countdeath(rowList12x18);
                    total += ageTemp;
                    sw.WriteLine("age: 12 - 18 --> " + ageTemp);

                    ageTemp = countdeath(rowList19x30);
                    total += ageTemp;
                    sw.WriteLine("age: 19 - 30 --> " + ageTemp);

                    ageTemp = countdeath(rowList31x40);
                    total += ageTemp;
                    sw.WriteLine("age: 31 - 40 --> " + ageTemp);

                    ageTemp = countdeath(rowList41x50);
                    total += ageTemp;
                    sw.WriteLine("age: 41 - 50 --> " + ageTemp);

                    ageTemp = countdeath(rowList51x60);
                    total += ageTemp;
                    sw.WriteLine("age: 51 - 60 --> " + ageTemp);

                    ageTemp = countdeath(rowList61x70);
                    total += ageTemp;
                    sw.WriteLine("age: 61 - 70 --> " + ageTemp);

                    ageTemp = countdeath(rowList71x80);
                    total += ageTemp;
                    sw.WriteLine("age: 71 - 80 --> " + ageTemp);

                    ageTemp = countdeath(rowListB80);
                    total += ageTemp;
                    sw.WriteLine("age: > 80 --> " + ageTemp);

                    ageTemp = countdeath(rowListNA);
                    total += ageTemp;
                    sw.WriteLine("age: N/A --> " + ageTemp);

                    sw.WriteLine("Total death: " + total);
                }
            }
        }

        public static void readCSV(List<Pair> pairList, ref String header, String absolutePath)
        {
            using (var steamReader = new StreamReader(absolutePath))
            {
                header = steamReader.ReadLine();

                while (!steamReader.EndOfStream)
                    pairList.Add(new Pair(steamReader.ReadLine().ToString()));
            }
        }

        public static void writeCSV(List<Pair> pairList, String header, String absolutePath)
        {
            using (StreamWriter sw = new StreamWriter(absolutePath))
            {
                sw.WriteLine(header);
                foreach (Pair pair in pairList)
                {
                    sw.WriteLine(pair.line);
                }
            }
        }

        static int countdeath(List<Row> rowList)
        {
            List<int> idList = new List<int>();
            
            foreach(Row row in rowList)
            {
                bool add = true;
                foreach(int data in idList)
                {
                    if (row.getIDColumn() == data)
                    {
                        add = false;
                    }
                }
                if(add)
                    idList.Add(row.getIDColumn());
            }

            return idList.Count;
        }

        public static String nextSecondTick(ref int h, ref int m, ref int s)
        {
            String currentTime = String.Format("{0}:{1}:{2}", h.ToString(), m.ToString(), s.ToString());

            s++;
            if (s == 60)
            {
                s = 0;
                m++;
            }
            if (m == 60)
            {
                m = 0;
                h++;
            }

            return currentTime;
        }



        public static void SelectionSort(List<Pair> pairList)
        {
            int max = pairList.Count;
            for (int j = 0; j < max - 1; j++)
            {
                int iMin = j;
                for (int i = j + 1; i < max; i++)
                {
                    if (pairList[i].id < pairList[iMin].id)
                    {
                        iMin = i;
                    }
                }
                if (iMin != j)
                {
                    swap(pairList, j, iMin);
                }
            }
        }
        public void HeapSort(List<Pair> pair)
        {
            int n = pair.Count;

            // Build heap (rearrange array)
            for (int i = n / 2 - 1; i >= 0; i--)
                heapify(pair, n, i);

            // One by one extract an element from heap
            for (int i = n - 1; i > 0; i--)
            {
                // Move current root to end
                swap(pair, 0, i);

                // call max heapify on the reduced heap
                heapify(pair, i, 0);
            }
        }

        // To heapify a subtree rooted with node i which is
        // an index in arr[]. n is size of heap
        void heapify(List<Pair> pair, int n, int i)
        {
            int largest = i; // Initialize largest as root
            int l = 2 * i + 1; // left = 2*i + 1
            int r = 2 * i + 2; // right = 2*i + 2

            // If left child is larger than root
            if (l < n && pair[l].id > pair[largest].id)
                largest = l;

            // If right child is larger than largest so far
            if (r < n && pair[r].id > pair[largest].id)
                largest = r;

            // If largest is not root
            if (largest != i)
            {
                swap(pair, i, largest);

                // Recursively heapify the affected sub-tree
                heapify(pair, n, largest);
            }
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

        public static void swap(List<Pair> pairList, int index1, int index2)
        {
            Pair temp1 = new Pair(pairList[index1].line);
            pairList[index1] = null;
            pairList[index1] = pairList[index2];
            pairList[index2] = temp1;
        }

        static void insertionSort(List<Pair> pairList)
        {
            int n = pairList.Count;
            for (int i = 1; i < n; ++i)
            {
                int j = i;

                while (j > 0 && pairList[j - 1].id > pairList[j].id)
                {
                    swap(pairList, j, j - 1);

                    j--;
                }
            }
        }
        private static void Quick_Sort(List<Pair> pairList, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(pairList, left, right);

                Quick_Sort(pairList, left, pivot - 1);

                Quick_Sort(pairList, pivot + 1, right);

            }

        }

        private static int Partition(List<Pair> pairList, int left, int right)
        {
            //int pivot = pairList[right].id;
            int pivot = pairList[(left + right)/2].id;

            int i = left - 1;

            for (int j = left; j <= right - 1; j++)
            {
                if (pairList[j].id < pivot)
                {
                    i++;
                    swap(pairList, i, j);
                }
            }

            swap(pairList, i + 1, right);

            return i + 1;

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
