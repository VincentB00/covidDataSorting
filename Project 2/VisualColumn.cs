using System;
using System.Collections.Generic;
using System.Text;

namespace Project_2
{
    class VisualColumn
    {
        public Dictionary<int, Column> columns;
        int lastKey = 0;
        int firstKey = 0;
        public VisualColumn()
        {
            columns = new Dictionary<int, Column>();
        }

        public void add(List<Row> rowList, int columnNum)
        {
            if(!columns.ContainsKey(columnNum))
            {
                columns.Add(columnNum, new Column());

                if (columnNum >= lastKey)
                    lastKey = columnNum;

                if (columnNum < firstKey)
                    firstKey = columnNum;
            }

            Column column = columns[columnNum];

            column.addData(rowList);
        }

        public Column getLast()
        {
            return columns[lastKey];
        }

        public Column getFirst()
        {
            return columns[firstKey];
        }

        
    }
}
