using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_3
{
    internal class FPGrowth
    {
        public class Node
        {
            bool root;
            public Object item = null;
            public int supportCount = 0;
            public Node parrentNode = null;

            public Dictionary<Object, Node> childs;

            public Node(bool root)
            {
                this.root = root;
                childs = new Dictionary<Object, Node>();
            }

            public Node(Object item, int supportCount)
            {
                root = false;
                this.item = item;
                this.supportCount = supportCount;
                childs = new Dictionary<Object, Node>();
            }

            public Node getChild(Object item)
            {
                if(!containChild(item))
                {
                    return null;
                }
                else
                {
                    return childs[item];
                }
            }


            public bool containChild(Object item)
            {
                return childs.ContainsKey(item);
            }

            public void addChild(Object item, int supportCount)
            {
                Node newNode = new Node(item, supportCount);
                newNode.parrentNode = this;
                childs.Add(item, newNode);
            }

            public void addChild(Node newNode)
            {
                newNode.parrentNode = this;
                childs.Add(newNode.item, newNode);
            }

            public bool isRoot() { return root; }
        }

        public class Tree
        {
            public Node root;

            public Tree()
            {
                root = new Node(true);
            }

            public Node insert(Node currentNode, Object item)
            {
                if (!currentNode.isRoot() && currentNode.item.ToString().CompareTo(item.ToString()) == 0)
                {
                    currentNode.supportCount++;
                    return currentNode;
                }
                else if (currentNode.containChild(item))
                {
                    return insert(currentNode.getChild(item), item);
                }
                else
                {
                    Node newNode = new Node(item, 1);
                    currentNode.addChild(newNode);
                    return newNode;
                }
            }
            
            public void buildTree(List<List<Object>> l1, List<Row> rowList)
            {
                Dictionary<Object, List<Object>> dict = new Dictionary<Object, List<Object>>();
                foreach(List<Object> l in l1)
                {
                    dict.Add(l[0], l);
                }

                foreach (Row row in rowList)
                {
                    Node currentNode = root;
                    List<String> tempList = getAndsortRowData(row, dict);
                    foreach(String data in tempList)
                    {
                        currentNode = insert(currentNode, data);
                    }
                }
            }

            private List<String> getAndsortRowData(Row row, Dictionary<Object, List<Object>> dict)
            {
                Dictionary<int, String> tempDict = new Dictionary<int, String>();

                if (row.columns == null || row.columns.Count <= 0)
                    row.breakDownLine();

                for(int count = 1; count < row.columns.Count; count++)
                {
                    String data = row.columns[count];
                    int supCount = getSupCount(dict, data);
                    if(supCount > 0 && data != null && data.CompareTo("") != 0)
                        tempDict.TryAdd(supCount, data);
                }


                List<String> list = new List<String>();
                foreach (var item in tempDict.OrderByDescending(key => key.Value))
                {
                    list.Add(item.Value);
                }

                return list;
            }

            private int getSupCount(Dictionary<Object, List<Object>> dict, Object key)
            {
                if(dict.ContainsKey(key))
                    return getSupCount(dict[key]);
                else
                    return -1;
            }

            private int getSupCount(List<Object> list)
            {
                return Convert.ToInt32(list[1]);
            }
            
            public Dictionary<string, List<CPB>> generateCPB()
            {
                Dictionary<string, List<CPB>> resultDict = new Dictionary<string, List<CPB>>();

                traverseNode(root, resultDict, new List<Object>());

                return resultDict;
            }

            private void traverseNode(Node currentNode, Dictionary<string, List<CPB>> resultDict, List<Object> traverseList)
            {
                if(currentNode.childs == null || currentNode.childs.Count <= 0)
                {
                    return;
                }
                else if(currentNode.isRoot())
                {
                    foreach (KeyValuePair<Object, Node> entry in currentNode.childs)
                    {
                        Node child = entry.Value;
                        string item = child.item.ToString();
                        
                        List<Object> tempList = new List<Object>();

                        tempList.Add(item);

                        traverseNode(child, resultDict, tempList);
                    }
                }
                else
                {
                    foreach (KeyValuePair<Object, Node> entry in currentNode.childs)
                    {
                        Node child = entry.Value;
                        int supportCount = child.supportCount;
                        String item = child.item.ToString();
                        CPB tempCPB = new CPB();
                        tempCPB.itemPointer = item;
                        tempCPB.supportCount = supportCount;
                        foreach(Object obj in traverseList)
                        {
                            tempCPB.objects.Add(obj);
                        }

                        List<CPB> cpbPs;
                        if(resultDict.ContainsKey(item.ToString()))
                        {
                            resultDict.TryGetValue(item.ToString(), out cpbPs);
                            cpbPs.Add(tempCPB);
                        }
                        else
                        {
                            cpbPs = new List<CPB>() { tempCPB };
                            resultDict.Add(item, cpbPs);
                        }


                        List<Object> tempList = new List<Object>();

                        foreach(Object obj in traverseList)
                            tempList.Add(obj);
                        tempList.Add(item);

                        traverseNode(child, resultDict, tempList);
                    }
                }
            }

        }

        //-------------------------------------------------------------------------------------------------------------------------------

        private int minSupportCount = 0;
        private int maxSupportCount = 0;
        public Tree tree;

        public FPGrowth()
        {
            tree = new Tree();
        }

        public void buildTree(List<List<Object>> l1, List<Row> rowList)
        {
            tree.buildTree(l1, rowList);

        }

        public List<List<Object>> getC1(List<Row> dataSet)
        {
            Dictionary<String, List<Object>> dict = new Dictionary<string, List<Object>>(); 
            List<List<Object>> itemSets = new List<List<Object>>();



            /*Scan all the dataSet*/
            foreach (Row row in dataSet)
            {
                
                if(row.columns == null || row.columns.Count <= 0)
                    row.breakDownLine();

                /*See for each dataSet item, add in list if not present otherwise increment count, each dataSet will have 14 item*/
                foreach (String item in row.columns)
                {

                    string itemTemp = item.Trim();
                    
                    if(itemTemp != null && itemTemp.CompareTo("") != 0)
                    {
                        if (!dict.ContainsKey(itemTemp))
                        {
                            dict.TryAdd(itemTemp, new List<Object>() { itemTemp, 1 });
                        }
                        else
                        {
                            List<Object> tempList = new List<Object>();
                            dict.TryGetValue(itemTemp, out tempList);

                            int tempSupportCount = Convert.ToInt32(tempList[tempList.Count - 1]) + 1;

                            tempList[tempList.Count - 1] = tempSupportCount;

                            if (tempSupportCount > maxSupportCount)
                                maxSupportCount = tempSupportCount;
                        }
                    }
                }
            }

            foreach(KeyValuePair<String, List<Object>> entry in dict)
            {
                itemSets.Add(entry.Value);
            }

            return itemSets;
        }

        
        public List<List<Object>> getL1(ref List<List<Object>> Ck)
        {
            List<List<Object>> L1Items = new List<List<Object>>();
            for (int count = 0; count < Ck.Count; count++)
            {
                if (Convert.ToInt32(Ck[count][1]) > minSupportCount)
                {
                    L1Items.Add(Ck[count]);
                }
            }
            return L1Items;
        }

        /**
         * generate L1
         */
        public List<List<Object>> getL1(ref List<List<Object>> Ck, double percentageSupCount)
        {
            minSupportCount = (int)(((double)maxSupportCount / 100) * percentageSupCount);
            return getL1(ref Ck);
        }

        /**
         * generate Conditional FP-tree
         */
        public Dictionary<string, Dictionary<string, int>> generateCFPT(Dictionary<string, List<CPB>> CPB)
        {
            Dictionary<string, Dictionary<string, int>> resultDict = new Dictionary<string, Dictionary<string, int>>();

            foreach (List<CPB> CPBG in CPB.Values)
            {
                string item = CPBG[0].itemPointer.ToString();
                
                
                foreach(CPB cpb in CPBG)
                {
                    if (!resultDict.ContainsKey(item))
                    {
                        resultDict.TryAdd(item, new Dictionary<string, int>());
                    }

                    Dictionary<string, int> tempDict;

                    resultDict.TryGetValue(item, out tempDict);

                    foreach(Object obj in cpb.objects)
                    {
                        string tempItem = obj.ToString();

                        if(!tempDict.ContainsKey(tempItem))
                        {
                            tempDict.Add(tempItem, 1);
                        }
                        else
                        {
                            int tempValue = tempDict[tempItem];
                            tempValue++;

                            tempDict.Remove(tempItem);
                            tempDict.Add(tempItem, tempValue);
                        }
                    }
                }
            }

            return resultDict;
        }

        /**
         * generate frequent pattern
         */
        public Dictionary<string, Dictionary<string, CPB>> generateFPG(List<Row> rowList, int minSup)
        {
            List<List<Object>> c1 = getC1(rowList);
            List<List<Object>> l1 = getL1(ref c1, minSup);
            buildTree(l1, rowList);
            Dictionary<string, List<CPB>> CPB = tree.generateCPB();
            Dictionary<string, Dictionary<string, int>> CFPT = generateCFPT(CPB);

            Dictionary<string, Dictionary<string, CPB>> FPG = new Dictionary<string, Dictionary<string, CPB>>();

            //foreach item in l1 list
            foreach (KeyValuePair<string, Dictionary<string, int>> entryP in CFPT)
            {
                Dictionary<string, CPB> tempDict = new Dictionary<string, CPB>();


                List<CPB> tempList = new List<CPB>();
                
                foreach (KeyValuePair<string, int> entry in entryP.Value)
                {
                    tempList.Add(new CPB());

                    string item = entry.Key;
                    int supportCount = Convert.ToInt32(entry.Value);

                    tempList.Last().objects.Add(item);
                    tempList.Last().supportCount = supportCount;
                }

                List<CPB> tempCPBList = new List<CPB>();
                CPB mainCPB = new CPB();
                mainCPB.objects.Add(entryP.Key);
                mainCPB.supportCount = Int32.MaxValue;

                foreach (CPB b in tempList)
                {

                    CPB tempCPB;
                    tempCPB = b.combineWith(mainCPB);

                    tempCPBList.Add(tempCPB);

                }

                for (int count = tempList.Count - 1; count > 0; count--)
                {
                    CPB tempCPB = new CPB(Int32.MaxValue);
                    for(int loop = 0; loop <= count; loop++)
                    {
                        tempCPB = tempCPB.combineWith(tempList[loop]);
                    }

                    tempCPBList.Add(tempCPB.combineWith(mainCPB));
                }

                foreach(CPB b in tempCPBList)
                {
                    tempDict.Add(b.generatePattern(), b);
                }

                FPG.Add(entryP.Key, tempDict);
            }

            return FPG;
        }

        /**
         * generate frequent pattern
         */
        public Dictionary<string, Dictionary<string, CPB>> generateFPG(Dictionary<string, Dictionary<string, int>> CFPT)
        {
            Dictionary<string, Dictionary<string, CPB>> FPG = new Dictionary<string, Dictionary<string, CPB>>();

            //foreach item in l1 list
            foreach (KeyValuePair<string, Dictionary<string, int>> entryP in CFPT)
            {
                Dictionary<string, CPB> tempDict = new Dictionary<string, CPB>();


                List<CPB> tempList = new List<CPB>();

                foreach (KeyValuePair<string, int> entry in entryP.Value)
                {
                    tempList.Add(new CPB());

                    string item = entry.Key;
                    int supportCount = Convert.ToInt32(entry.Value);

                    tempList.Last().objects.Add(item);
                    tempList.Last().supportCount = supportCount;
                }

                List<CPB> tempCPBList = new List<CPB>();
                CPB mainCPB = new CPB();
                mainCPB.objects.Add(entryP.Key);
                mainCPB.supportCount = Int32.MaxValue;

                foreach (CPB b in tempList)
                {

                    CPB tempCPB;
                    tempCPB = b.combineWith(mainCPB);

                    tempCPBList.Add(tempCPB);

                }

                //for (int count = tempList.Count - 1; count > 0; count--)
                //{
                //    CPB tempCPB = new CPB(Int32.MaxValue);
                //    for (int loop = 0; loop <= count; loop++)
                //    {
                //        tempCPB = tempCPB.combineWith(tempList[loop]);
                //    }

                //    tempCPBList.Add(tempCPB.combineWith(mainCPB));
                //}

                //for (int count = 0; count < tempList.Count; count++)
                //{
                //    CPB tempCPB = new CPB(Int32.MaxValue);
                //    for (int loop = 0; loop <= count; loop++)
                //    {
                //        if (count != loop)
                //            tempCPB = tempCPB.combineWith(tempList[loop]);
                //    }

                //    tempCPBList.Add(tempCPB.combineWith(mainCPB));
                //}

                int begin = 0;
                int end = tempList.Count;
                int count= 0;

                generateCPB(mainCPB, tempCPBList, tempList, begin, end);

                //while (count < 5)
                //{
                //    generateCPB(mainCPB, tempCPBList, tempList, begin, end);
                //    count++;
                //    begin += count;
                //    end -= count;
                //}


                foreach (CPB b in tempCPBList)
                {
                    if(!tempDict.ContainsKey(b.generatePattern()))
                        tempDict.Add(b.generatePattern(), b);
                }

                FPG.Add(entryP.Key, tempDict);
            }

            return FPG;
        }

        private void generateCPB(CPB mainCPB, List<CPB> tempCPBList, List<CPB> tempList, int begin, int end)
        {
            for (int count = end - 1; count > begin; count--)
            {
                CPB tempCPB = new CPB(Int32.MaxValue);
                for (int loop = begin; loop <= count; loop++)
                {
                    tempCPB = tempCPB.combineWith(tempList[loop]);
                }

                tempCPBList.Add(tempCPB.combineWith(mainCPB));
            }

            for (int count = begin; count < end; count++)
            {
                CPB tempCPB = new CPB(Int32.MaxValue);
                for (int loop = begin; loop <= count; loop++)
                {
                    if (count != loop)
                        tempCPB = tempCPB.combineWith(tempList[loop]);
                }

                tempCPBList.Add(tempCPB.combineWith(mainCPB));
            }
        }

        public void generateAssociationRule(string item, Dictionary<string, Dictionary<string, CPB>> FPG, Dictionary<string, List<Object>> L1)
        {
            
            if (FPG.ContainsKey(item))
            {
                Console.WriteLine(String.Format("-------------------------------------------{0}---------------------------------------------------", item));
                Dictionary<string, CPB> dictP = FPG[item];

                //generateAssociationRule(dictP.Values.Last(), dictP, L1);

                foreach (CPB cPB in dictP.Values)
                {
                    printGenerateAssociationRule(cPB, dictP, L1);
                }
            }
            Console.WriteLine();
            //Console.WriteLine(String.Format("----------------------------------------{0} End---------------------------------------------------", item));
        }

        public void printGenerateAssociationRule(CPB cpb, Dictionary<string, CPB> FPG, Dictionary<string, List<Object>> L1)
        {
            List<Object> list = cpb.objects;

            //foreach (Object obj in list)
            //    Console.WriteLine(obj.ToString());  

            //foreach (KeyValuePair<string, CPB> pair in FPG)
            //{
            //    Console.WriteLine(String.Format("key:{0}|value:{1}", pair.Key, pair.Value.ToString()));
            //}

            for (int count = 0; count < list.Count; count++)
            {
                CPB temp = new CPB();

                CPB dependent = new CPB();
                dependent.objects.Add(list[count]);

                for(int count2 = 0; count2 < list.Count; count2++)
                {
                    if(count != count2)
                    {
                        temp.objects.Add(list[count2]);
                    }
                }

                int depSup = getSupCount(L1, dependent.objects[0].ToString());
                int deeSup = getSupCount(FPG, temp.generatePattern());
                if (depSup < 0 || temp.objects.Count == 1)
                    deeSup = getSupCount(L1, temp.objects[0].ToString());

                if(temp.objects.Count > 0 && deeSup > 0)
                {
                    double confident = (((double)deeSup / (double)depSup) * 100);
                    string displayConf = confident < 0 ? "undefine" : confident + "%";
                    Console.WriteLine(dependent.generatePattern() + " --> " + temp.generatePattern() + " confident: " + displayConf);
                    Console.WriteLine("Dependent sup: " + depSup);
                    Console.WriteLine("Dependee sup: " + deeSup);
                }

            }

            //foreach (Object obj in list)
            //    Console.WriteLine(obj.ToString());  


            //Console.WriteLine(cpb.generatePattern());
        }

        public int getSupCount(Dictionary<string, CPB> FPG, string pattern)
        {
            //foreach(KeyValuePair<string, CPB> pair in FPG)
            //{
            //    Console.WriteLine(String.Format("key:{0}|value:{1}", pair.Key, pair.Value.ToString()));
            //}

            //Dictionary<string, CPB> tempDict = FPG[pattern];

            //foreach (KeyValuePair<string, CPB> pair in tempDict)
            //    Console.WriteLine(pair.Value.ToString());


            if (FPG.ContainsKey(pattern))
                return FPG[pattern].supportCount;
            else
                return -1;
        }

        public int getSupCount(Dictionary<string, List<Object>> L1, string item)
        {
            List<Object> tempList;

            L1.TryGetValue(item, out tempList);

            if (tempList != null)
                return Convert.ToInt32(tempList[1]);
            else
                return -1;
        }

        public void filterCFPT(Dictionary<string, Dictionary<string, int>> CFPT, Dictionary<string, List<Object>> L1)
        {
            foreach (KeyValuePair<string, Dictionary<string, int>> entryP in CFPT)
            {
                List<string> removeList = new List<string>();
                string item = entryP.Key;
                int minSupport = getSupCount(L1, item);

                foreach (KeyValuePair<string, int> entry in entryP.Value)
                {
                    if(entry.Value < minSupportCount)
                    {
                        removeList.Add(entry.Key);
                    }
                }

                Dictionary<string, int> tempDict = entryP.Value;

                foreach(string key in removeList)
                {
                    tempDict.Remove(key);
                }
            }
        }

        public void printFPG(Dictionary<string, Dictionary<string, CPB>> FPG)
        {
            foreach (KeyValuePair<string, Dictionary<string, CPB>> entry in FPG)
            {
                Console.Write(entry.Key + "[");
                foreach (KeyValuePair<string, CPB> entryT in entry.Value)
                {
                    Console.Write(entryT.Key + ", ");
                }
                Console.WriteLine("]\n");
            }
        }

        public Dictionary<string, List<Object>> toDict(List<List<Object>> l1)
        {
            Dictionary<string, List<Object>> tempDict = new Dictionary<string, List<Object>>();

            foreach (List<Object> l in l1)
            {
                string item = l[0].ToString();

                tempDict.Add(item, l);
            }

            Dictionary<string, List<Object>> resultDict = new Dictionary<string, List<Object>>();

            foreach (var item in tempDict.OrderByDescending(key => key.Value[1]))
            {
                resultDict.Add(item.Key, item.Value);
            }

            return resultDict;
        }

        

        public void printCFPT(Dictionary<string, Dictionary<string, int>> CFPT)
        {
            foreach (KeyValuePair<string, Dictionary<string, int>> entryP in CFPT)
            {
                Console.Write(entryP.Key + "{");
                foreach (KeyValuePair<string, int> entry in entryP.Value)
                {
                    Console.Write(String.Format("{0}: {1},", entry.Key, entry.Value));
                }
                Console.WriteLine("}\n");
            }
        }

        public void printCPB(Dictionary<string, List<CPB>> CPB)
        {
            foreach (List<CPB> c in CPB.Values)
            {
                Console.Write(c[0].itemPointer.ToString() + "{");
                foreach (CPB b in c)
                {
                    Console.Write(b.ToString() + ",");
                }
                Console.WriteLine("}\n");
            }
        }

        public void printL1(List<List<Object>> l1)
        {
            Console.WriteLine("---------------------------------------------------------L1---------------------------------------------------------------------------");
            Console.WriteLine("Max Support Count: " + maxSupportCount);
            Console.WriteLine("Min Support Count: " + minSupportCount);
            foreach (List<Object> l in l1)
            {
                Console.WriteLine(String.Format("Item:{0}|support count:{1}", l[0].ToString(), l[1].ToString()));
            }
            Console.WriteLine("-------------------------------------------------------END L1--------------------------------------------------------------------------");
        }

        public void printL1(Dictionary<string, List<Object>> l1)
        {
            Console.WriteLine("---------------------------------------------------------L1---------------------------------------------------------------------------");
            Console.WriteLine("Max Support Count: " + maxSupportCount);
            Console.WriteLine("Min Support Count: " + minSupportCount);
            foreach (List<Object> l in l1.Values)
            {
                Console.WriteLine(String.Format("Item:{0}|support count:{1}", l[0].ToString(), l[1].ToString()));
            }
            Console.WriteLine("-------------------------------------------------------END L1--------------------------------------------------------------------------");
        }
    }
}
