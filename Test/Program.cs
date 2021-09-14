using System;
using System.Collections.Generic;
using System.Linq;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //String data = "0855055,01/02/2020,,,,,U,,\"shivering; soreness; feel bad; This case was reported by a consumer via call center representative and described the occurrence of shivering in a adult patient who received Herpes zoster(Shingrix) for prophylaxis.On an unknown date, the patient received the 1st dose of Shingrix.On an unknown date, unknown after receiving Shingrix, the patient experienced shivering, pain and malaise.On an unknown date, the outcome of the shivering, pain and malaise were not recovered / not resolved.It was unknown if the reporter considered the shivering, pain and malaise to be related to Shingrix.Additional details were provided as follows: The age at vaccination was not reported.The age group of the patient was not reported but selected as an adult as per vaccine indication. The patient received Shingrix vaccine and felt bad, no fever but shivering and soreness.The patient asked, how long did the side effects last.\",,,,,,,,,N,,,,,UNK,,,,,,USGLAXOSMITHKLINEUS2019AM,2,01/01/2020,,,,";
            //String data = "\"something, something\",,,,,N,,\"something, something\",";
            //List<String> list = splitCSVData(data);

            //foreach (String dataT in list)
            //{
            //    if (dataT.CompareTo("") == 0)
            //        Console.WriteLine("None");
            //    else
            //        Console.WriteLine(dataT);
            //}
            String data = "something, NY";
            Console.WriteLine(data.Substring(data.IndexOf(',') + 1));


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
                    int nextDQouteIndex = tempString.IndexOf('\"');

                    if (tempString.Substring(nextDQouteIndex + 1).Trim().Length > 2)
                        line = tempString.Substring(nextDQouteIndex + 2);
                    else
                        line = tempString.Substring(nextDQouteIndex + 1);

                    tempString = tempString.Substring(0, nextDQouteIndex + 1);
                    currentString = currentString + tempString;
                }
                list.Add(currentString);

                //Console.WriteLine("Current Line: " + line);

            }

            if (line.CompareTo("") != 0)
                list.Add(line);

            return list;
        }
    }
}
