using System;
using System.Collections.Generic;
using System.Text;

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
            if(node.isLeafNode())
            {
                node.insertData(row);
                if(node.isReachMaxDegree())
                {
                    //because this node right now is only a leaf node so the new parrent node will become the root
                    this.node = splitLeafNode(this.node);
                }
            }
            else
            {

            }
        }

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

                parentNode.insertData(new Row(newLeafNode.getFrist()));
            }

            leafNode.parrentNode = parentNode;

            newLeafNode.parrentNode = parentNode;

            return parentNode;
        }

        private void splitNode(Node splitOnNode)
        {
            
            

        }
    }
}
