﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Project_2
{
    class BPTree
    {
        public int maxDegree;
        Node node;
        List<String> lines; //for visualizing
        VisualColumn vc; //for visualizing

        public BPTree()
        {
            this.node = new Node();
        }
        public BPTree(int maxDegree)
        {
            this.maxDegree = maxDegree;
            this.node = new Node(maxDegree);
        }

        public Row tryFind(int id)
        {
            try
            {
                return find(id);
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public Row find(int id)
        {

            Node leafNode = node.Find(node, id); //possible leaf node

            do
            {
                foreach (Row row in leafNode.rowList)
                {
                    if (row.id == id)
                        return new Row(row);
                }
                if (leafNode.rightLeafNode != null)
                    leafNode = leafNode.rightLeafNode;
            }
            while (leafNode.rightLeafNode != null);

            return null;
        }



        public void insert(Row row)
        {
            if (node.isLeafNode()) //if the current node is both root and leaf
            {
                node.insertData(row);
                if (node.isReachMaxDegree())
                {
                    //because this node right now is only a leaf node so the new parrent node will become the root
                    this.node = splitLeafNode(this.node);

                    foreach(Node child in node.childNode)
                    {
                        child.parrentNode = node;
                    }
                }
            }
            else
            {
                Node currentLeafNode = node.insert(node, row); //the leaf node where the row is being inserted

                if (currentLeafNode.isReachMaxDegree())
                {
                    Node parentNode = splitLeafNode(currentLeafNode);

                    while (parentNode.isReachMaxDegree())
                    {
                        parentNode = splitNode(parentNode);
                    }


                    if (parentNode.isRootNode())
                    {
                        this.node = parentNode;
                        //Console.WriteLine(parentNode.rowList[0].id);
                    }

                }
            }
        }

        private Node splitNode(Node currentNode)
        {
            Node newNode = currentNode.Split();

            Node parentNode;

            if (newNode.parrentNode == null)
            {
                parentNode = new Node(maxDegree);

                parentNode.addchildNode(currentNode);

                parentNode.addchildNode(newNode);

                parentNode.insertData(new Row(newNode.getFrist()));

                currentNode.parrentNode = parentNode;

                newNode.parrentNode = parentNode;
            }
            else
            {
                parentNode = currentNode.parrentNode;

                parentNode.addchildNode(newNode);

                parentNode.insertData(new Row(newNode.getFrist()));
            }

            newNode.rowList.RemoveAt(0);



            return parentNode;
        }

        /**
         * this function will split the leafNode then return a pointer to it parrent node
         */
        private Node splitLeafNode(Node leafNode)
        {
            Node newLeafNode = leafNode.splitLeafNode();

            Node parentNode;

            if (leafNode.parrentNode == null) //if there are no parrent node
            {
                parentNode = new Node(maxDegree);

                parentNode.addchildNode(leafNode);

                parentNode.addchildNode(newLeafNode);

                parentNode.insertData(new Row(newLeafNode.getFrist()));

                leafNode.parrentNode = parentNode;

                newLeafNode.parrentNode = parentNode;
            }
            else
            {
                parentNode = leafNode.parrentNode;

                parentNode.addchildNode(newLeafNode);

                parentNode.insertData(new Row(newLeafNode.getFrist()));
            }

            //connect all child node to it right leaf node
            newLeafNode.rightLeafNode = leafNode.rightLeafNode;
            leafNode.rightLeafNode = newLeafNode;

            return parentNode;
        }

        public bool delete(int id)
        {
            if (tryFind(id) == null)
                return false;

            Node nodeP = this.node.Find(this.node, id);



            return true;
        }

        //------------------------------------------------------------------------------------

        public void visualizeVertical(String absolutePath, int space)
        {
            List<Task> taskList = new List<Task>();
            vc = new VisualColumn();

            getVisualLine(this.node);

            foreach (KeyValuePair<int, Column> entry in vc.columns)
            {
                Task task = Task.Run(() =>
                {
                    entry.Value.breakDownToRowList();
                });
                taskList.Add(task);
            }

            foreach (Task task in taskList)
                task.Wait();

            int treeHeight = getTreeHeight(this.node, 0);

            int longestHeight = vc.getLast().Count();

            using (StreamWriter sw = new StreamWriter(absolutePath))
            {
                for (int count = 0; count < longestHeight; count++)
                {
                    foreach (KeyValuePair<int, Column> entry in vc.columns)
                    {
                        String str;
                        if (count == longestHeight - 1)
                            str = entry.Value.getData(count);
                        else
                            str = entry.Value.getData(count, longestHeight);

                        sw.Write(fillString(str, space));
                    }
                    sw.WriteLine();
                }
            }

        }

        public String fillString(String str, int maxLength)
        {
            while (str.Length < maxLength)
                str += " ";

            return str;
        }

        public void getVisualLine(Node currentNode)
        {
            int currentHeight = getCurrentHeight(currentNode, 0);

            vc.add(currentNode.rowList, currentHeight);

            if (!currentNode.isLeafNode())
            {
                foreach (Node child in currentNode.childNode)
                {
                    //Task task = Task.Run(() =>
                    //{
                    //    getVisualLine(child);
                    //});
                    getVisualLine(child);
                }
            }
        }


        public void visualize(String absolutePath)
        {
            lines = new List<string>();

            int height = getTreeHeight(node, 0);

            for (int count = 0; count < height + 1; count++)
            {
                this.lines.Add("");
            }

            getLineList(this.node);

            for (int count = lines.Count - 2; count >= 0; count--)
            {
                String lastLine = lines[count + 1].Trim();
                int lastRowLength = lines[count + 1].Length;
                int lastRowSpace = lastRowLength - lastLine.Length;
                int middle = lastRowSpace + (lastLine.Length / 2);
                int numberOfSpace = middle - (lines[count].Length / 2);

                lines[count] = space(numberOfSpace) + lines[count];
            }

            using (StreamWriter sw = new StreamWriter(absolutePath))
            {
                foreach (String data in lines)
                {
                    sw.WriteLine(data);
                }
            }

        }



        private void getLineList(Node currentNode)
        {
            String line = getLine(currentNode);

            int currentHeight = getCurrentHeight(currentNode, 0);

            lines[currentHeight] += line;

            if(!currentNode.isLeafNode())
            {
                foreach(Node child in currentNode.childNode)
                {
                    getLineList(child);
                }
            }
        }

        private String getLine(Node currentNode)
        {
            int max = currentNode.rowList.Count;
            String line = "";
            for (int count = 0; count < max; count++)
            {
                if (count == max - 1)
                {
                    line += currentNode.rowList[count].id;
                }
                else
                    line += currentNode.rowList[count].id + "|";
            }
            line = "[" + line + "]\t";

            return line;
        }

        private List<String> getList(List<String> list, int currentRow, Node node)
        {

            if (list.Count == currentRow)
                list.Add("");

            if (node.isLeafNode())
            {
                int max = node.rowList.Count;
                for (int count = 0; count < max; count++)
                {
                    if (count == max - 1)
                    {
                        list[currentRow] += node.rowList[count].id;
                    }
                    else
                        list[currentRow] += node.rowList[count].id + "|";
                }
                list[currentRow] += "\t";
            }
            else
            {
                int max = node.rowList.Count;
                for (int count = 0; count < max; count++)
                {
                    if (count == max - 1)
                    {
                        list[currentRow] += node.rowList[count].id;
                    }
                    else
                        list[currentRow] += node.rowList[count].id + "|";
                }
                list[currentRow] += "\t";

                currentRow++;

                foreach (Node nodeT in node.childNode)
                {
                    getList(list, currentRow, nodeT);
                }
            }

            return list;
        }

        private String space(int count)
        {
            String result = "";
            while (count > 0)
            {
                result += " ";
                count--;
            }
            return result;
        }

        private String tab(int count)
        {
            String result = "";
            while (count > 0)
            {
                result += "\t";
                count--;
            }
            return result;
        }
        private int getCurrentHeight(Node node, int count)
        {
            if(!node.isRootNode())
            {
                count++;
                return getCurrentHeight(node.parrentNode, count);
            }
            else
                return count;
        }
        private int getTreeHeight(Node node, int count)
        {
            if (!node.isLeafNode())
            {
                count++;
                return getTreeHeight(node.childNode[0], count);
            }
            else
                return count;
        }
    }
}
