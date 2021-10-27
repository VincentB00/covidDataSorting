using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace Project_2
{
    class Program
    {
        static void Main(string[] args)
        {
            
            String header = "";
            //readdata
            //String absolutePath = "C:\\Users\\vince\\OneDrive\\study\\Oswego\\CSC365\\Project 2\\Data\\TestFile.csv";
            String absolutePath = "C:\\Users\\vince\\OneDrive\\study\\Oswego\\CSC365\\Project 2\\Data\\VAERS_COVID_DataAugust2021.csv";
            String folderPath = absolutePath.Substring(0, absolutePath.LastIndexOf('\\'));


            List<Row> RowList = new List<Row>();
            readCSV(RowList, ref header, absolutePath);

            Quick_Sort(RowList, 1, RowList.Count - 1);

            BPTree tree = new BPTree(3);

            Console.WriteLine("Begin inserting");

            try
            {
                int max = RowList.Count;

                for (int count = 0; count < max; count++)
                {
                    tree.insert(RowList[count]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            String visualizePath = folderPath + "visualize.txt";

            Console.WriteLine("Done inserting");

            tree.visualize(visualizePath);

            OldBatCommand("Start notepad " + visualizePath);

            Console.WriteLine("Done");
        }
        public static void readCSV(List<Row> pairList, ref String header, String absolutePath)
        {
            using (var steamReader = new StreamReader(absolutePath))
            {
                header = steamReader.ReadLine();

                while (!steamReader.EndOfStream)
                    pairList.Add(new Row(steamReader.ReadLine().ToString()));
            }
        }

        public static void writeCSV(List<Row> pairList, String header, String absolutePath)
        {
            using (StreamWriter sw = new StreamWriter(absolutePath))
            {
                sw.WriteLine(header);
                foreach (Row pair in pairList)
                {
                    sw.WriteLine(pair.line);
                }
            }
        }

        private static void Quick_Sort(List<Row> pairList, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(pairList, left, right);

                Quick_Sort(pairList, left, pivot - 1);

                Quick_Sort(pairList, pivot + 1, right);

            }

        }

        private static int Partition(List<Row> pairList, int left, int right)
        {
            //int pivot = pairList[right].id;
            int pivot = pairList[(left + right) / 2].id;

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

        public static void swap(List<Row> pairList, int index1, int index2)
        {
            Row temp1 = new Row(pairList[index1].line);
            pairList[index1] = null;
            pairList[index1] = pairList[index2];
            pairList[index2] = temp1;
        }



        private static void OldBatCommand(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
