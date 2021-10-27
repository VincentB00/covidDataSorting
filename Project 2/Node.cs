using System;
using System.Collections.Generic;
using System.Text;

namespace Project_2
{
    class Node
    {
        public int maxDegree = 3;

        public Node parrentNode;
        public List<Row> rowList;
        public List<Node> childNode;
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

        public bool isRootNode()
        {
            if (parrentNode == null)
                return true;
            else
                return false;
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

        /**
         * this function will return a pointer to the leaf node where the row is being inserted
         */
        public Node insert(Node node, Row row)
        {
            if(node.isLeafNode())
            {
                node.insertData(row);
                return node;
            }
            else if(node.rowList.Count == 1) //if there only 1 row in current node
            {
                Row rowP = node.rowList[0];
                if(row.id < rowP.id)
                {
                    return insert(node.childNode[0], row);
                }
                else
                {
                    return insert(node.childNode[1], row);
                }
            }
            else //else if there are more than 1 row in current node
            {

                for(int count = 0; count < node.rowList.Count; count++)
                {
                    Row rowP = node.rowList[count];
                    if (count == 0 && row.id < rowP.id) //if this is the first element
                    {
                        return insert(node.childNode[count], row);
                    }
                    else if(count == node.rowList.Count - 1 && row.id >= rowP.id) //if this is the last element
                    {
                        return insert(node.childNode[count + 1], row);
                    }
                    else if(count > 0) //if this is either both
                    {
                        Row rowT = node.rowList[count - 1];
                        if (row.id >= rowT.id && row.id < rowP.id)
                        {
                            return insert(node.childNode[count], row);
                        }
                    }
                }
            }

            return null;
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

        public Node Split()
        {
            List<Row> newRowList = new List<Row>();
            int halfMaxDegree = maxDegree / 2;

            for (int count = halfMaxDegree; count < maxDegree; count++)
            {
                newRowList.Add(new Row(rowList[count]));
            }

            rowList.RemoveRange(halfMaxDegree, maxDegree - halfMaxDegree);

            Node newNode = new Node(maxDegree);

            newNode.rowList = newRowList;

            //connect both to the same parrent if there is one
            if (parrentNode != null)
                newNode.parrentNode = parrentNode;

            //split it child node ref
            int maxRef = maxDegree + 1;
            int splitAt = maxDegree % 2 != 0 ? maxRef / 2 : (maxRef / 2) + 1;

            for (int count = splitAt; count < maxRef; count++)
            {
                newNode.addchildNode(childNode[count]);
                childNode[count] = new Node(maxDegree);
            }

            childNode.RemoveRange(splitAt, maxRef - splitAt);

            return newNode;
        }
        /**
         * this function will assume that current node is leaf node and split base on leaf node
         * this function will connect both split node with it parrent if there is one
         * this function will return the new leaf node after split
         */
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
