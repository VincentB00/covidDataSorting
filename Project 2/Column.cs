using System;
using System.Collections.Generic;
using System.Text;

namespace Project_2
{
    class Column
    {
        Dictionary<int, VisualNode> rows;
        List<String> rowList;
        public Column()
        {
            rows = new Dictionary<int, VisualNode>();
        }

        public void addData(List<Row> rowList)
        {
            VisualNode visualNode = new VisualNode(rowList);

            rows.Add(visualNode.key, visualNode);
        }

        public int Count()
        {
            return rows.Count;
        }

        public void breakDownToRowList()
        {
            rowList = new List<string>();
            foreach (KeyValuePair<int, VisualNode> entry in rows)
            {
                rowList.Insert(0, entry.Value.ToString());
            }
        }

        public String getData(int index)
        {
            if (index < rowList.Count)
            {
                return rowList[index];
            }
            else
                return "";
        }
        public String getData(int index, int maxHeight)
        {
            int half = maxHeight - Count();
            int quarter = half / 2;

            index -= quarter;

            if (index < 0)
                return "";
            else
                return getData(index);
        }
    }
}
