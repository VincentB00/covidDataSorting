using System;
using System.Collections.Generic;
using System.Text;

namespace Project_2
{
    class Node
    {
        public int maxDegree = 3;

        public Node parrentNode;
        List<Row> rowList;
        List<Node> childNode;
        public Node rightLeafNode;

        public Node()
        {
            defaultSetUp();
        }
        public Node(int maxDegree)
        {
            this.maxDegree = maxDegree;
            defaultSetUp();
        }
        public Row getFrist()
        {
            return rowList[0];
        }
        private void defaultSetUp()
        {
            parrentNode = null;
            childNode = new List<Node>();
            rightLeafNode = null;
            rowList = new List<Row>();
        }

        public bool isLeafNode()
        {
            if (childNode.Count == 0)
                return true;
            else
                return false;
        }

        public bool isReachMaxDegree()
        {
            if (rowList.Count == maxDegree)
                return true;
            else
                return false;
        }

        public void addchildNode(Node node)
        {
            childNode.Add(node);
        }

        public int insertData(Row row)
        {
            int count = 0;
            if(rowList.Count == 0)
            {
                rowList.Add(row);
                count = rowList.Count - 1;
            }
            else if (row.id >= rowList[rowList.Count - 1].id)
            {
                rowList.Add(row);
                count = rowList.Count - 1;
            }
            else
            {
                while(count < rowList.Count)
                {
                    if (row.id <= rowList[count].id)
                    {
                        rowList.Insert(count, row);
                        break;
                    }
                    else
                        count++;
                }
            }

            return count;
        }



        public Node splitLeafNode()
        {
            List<Row> newRowList = new List<Row>();
            int halfMaxDegree = maxDegree / 2;

            for (int count = halfMaxDegree; count < maxDegree; count++)
            {
                newRowList.Add(new Row(rowList[count]));
            }

            rowList.RemoveRange(halfMaxDegree, maxDegree - halfMaxDegree);

            Node newLeafNode = new Node(maxDegree);

            newLeafNode.rowList = newRowList;

            if (parrentNode != null)
                newLeafNode.parrentNode = parrentNode;

            return newLeafNode;
        }
    }
}
