using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace covidDataSorting
{
    public class RowDataSet
    {
        public List<Row> rowList; //this will be passby reference
        public List<int> orderList;
        public RowDataSet(List<Row> rowList)
        {
            orderList = new List<int>();
            this.rowList = rowList;
            int index = 0;
            while (index < rowList.Count)
            {
                orderList.Add(index);
                index++;
            }
        }

        public RowDataSet(List<Row> rowList, List<int> orderList)
        {
            this.rowList = rowList;
            this.orderList = orderList;
        }

        public Row getData(int index)
        {
            return rowList[orderList[index]];
        }

        public void setOrderIndexAt(int setIndex, int toIndex)
        {
            orderList[setIndex] = toIndex;
        }

        public void swap(int index1, int index2)
        {
            if(index1 == index2)
                return;

            int temp = orderList[index1];

            orderList[index1] = orderList[index2];
            orderList[index2] = temp;
        }

        public List<Row> getCopyOfCurrentRowListOrder()
        {
            List<Row> tempRowList = new List<Row>();

            for(int count = 0; count < orderList.Count; count++)
            {
                tempRowList.Add(new Row(rowList[orderList[count]]));
            }

            return tempRowList;
        }

        private void copyRow(Row row1, Row row2)
        {
            foreach (String data in row1.columns)
                row2.columns.Add(data);
        }
    }
}
