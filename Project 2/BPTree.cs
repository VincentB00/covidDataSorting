using System;
using System.Collections.Generic;
using System.IO;

namespace Project_2
{
    class BPTree
    {
        public int maxDegree;
        Node node;

        public BPTree()
        {
            this.node = new Node();
        }
        public BPTree(int maxDegree)
        {
            this.maxDegree = maxDegree;
            this.node = new Node(maxDegree);
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
                        parentNode = split(parentNode);
                    }
                        

                    if (parentNode.isRootNode())
                    {
                        this.node = parentNode;
                        Console.WriteLine(parentNode.rowList[0].id);
                    }
                        
                }
            }
        }


        private Node split(Node currentNode)
        {
            Node newNode = currentNode.Split();

            Node parentNode;

            if(newNode.parrentNode == null)
            {
                parentNode = new Node(maxDegree);

                parentNode.addchildNode(currentNode);

                parentNode.addchildNode(newNode);

                parentNode.insertData(new Row(newNode.getFrist()));
            }
            else
            {
                parentNode = currentNode.parrentNode;

                parentNode.addchildNode(newNode);

                parentNode.insertData(new Row(newNode.getFrist()));
            }

            newNode.rowList.RemoveAt(0);

            currentNode.parrentNode = parentNode;

            newNode.parrentNode = parentNode;

            return parentNode;
        }

        /**
         * this function will split the leafNode then return a pointer to it parrent node
         */
        private Node splitLeafNode(Node leafNode)
        {
            Node newLeafNode = leafNode.splitLeafNode();

            leafNode.rightLeafNode = newLeafNode;

            Node parentNode;

            if (leafNode.parrentNode == null) //if there are no parrent node
            {
                parentNode = new Node(maxDegree);

                parentNode.addchildNode(leafNode);

                parentNode.addchildNode(newLeafNode);

                parentNode.insertData(new Row(newLeafNode.getFrist()));
            }
            else
            {
                parentNode = leafNode.parrentNode;

                parentNode.addchildNode(newLeafNode);

                parentNode.insertData(new Row(newLeafNode.getFrist()));
            }

            leafNode.parrentNode = parentNode;

            newLeafNode.parrentNode = parentNode;

            return parentNode;
        }

        private void splitNode(Node splitOnNode)
        {



        }

        public void visualize(String absolutePath)
        {
            //int height = getTreeHeight(node, 0);

            List<String> list = getList(new List<string>(), 0, node);

            list.Reverse();

            //int longestRowLength = list[0].Length;

            for (int count = 1; count < list.Count; count++)
            {
                int lastRowLength = list[count - 1].Length;
                int lastRowSpace = lastRowLength - list[count - 1].Trim().Length;
                int middle = lastRowSpace + (list[count - 1].Trim().Length / 2);
                int numberOfSpace = middle - (list[count].Length / 2);

                list[count] = space(numberOfSpace) + list[count];
            }

            list.Reverse();

            using (StreamWriter sw = new StreamWriter(absolutePath))
            {
                foreach (String data in list)
                {
                    sw.WriteLine(data);
                }
            }

        }

        private List<String> getList(List<String> list, int currentRow, Node node)
        {
            if (list.Count == currentRow)
                list.Add("");

            if(node.isLeafNode())
            {
                foreach (Row row in node.rowList)
                {
                    list[currentRow] += row.id + "|";
                }
                list[currentRow] = list[currentRow].Substring(0, list[currentRow].Length - 1) + "\t";
            }
            else
            {
                foreach (Row row in node.rowList)
                {
                    list[currentRow] += row.id + "|";
                }
                list[currentRow] = list[currentRow].Substring(0, list[currentRow].Length - 1) + "\t";

                currentRow++;

                foreach(Node nodeT in node.childNode)
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
            while(count > 0)
            {
                result += "\t";
                count--;
            }
            return result;
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
