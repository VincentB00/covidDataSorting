using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace covidDataSorting
{
    public class GridManager
    {
        public int maxColumn; // index of max column
        public int maxRow;  // index of max row
        public List<Row> rowList;
        public GridManager()
        {
            rowList = new List<Row>();
            maxColumn = -1;
            maxRow = -1;
        }

        public bool isEmpty()
        {
            if (maxRow < 0 || rowList.Count <= 0)
                return true;
            else
                return false;
        }

        public void addHeader(String headers)
        {
            if(maxRow < 0)
            {
                List<String> headerList = Row.splitCSVData(headers);

                rowList.Add(new Row());   //because there are no row then each column in column list is new

                rowList[0].addData(headers);
                
                maxRow++;
                maxColumn = headerList.Count - 1;
            }
            else
            {
                int numberOfAddedColumn = 0;

                Row row = new Row();
                row.addData(headers);
                row.columns.RemoveAt(0);

                addRowData(row.ToString(), 0);

                numberOfAddedColumn = row.columns.Count;

                maxColumn += numberOfAddedColumn;
            }
        }

        public void addRow(Row row)
        {
            addRow(row.ToString());
        }

        public void addRow(String line)
        {
            maxRow++;
            rowList.Add(new Row());
            rowList[maxRow].addData(line);
        }

        public void addRowData(String line, int rowIndex)
        {
            rowList[rowIndex].addData(line);
        }

        public void removeRowAt(int index)
        {
            rowList.RemoveAt(index);
            maxRow--;
        }

        public String getHeader()
        {
            String result = "";
            if (maxRow >= 0)
            {
                result = rowList[0].ToString();
            }
            return result;
        }

        public String getData(int column, int row)
        {
            if (column > this.maxColumn && row > this.maxRow)
                return null;
            else
                return rowList[row].columns[column];
        }

        public bool setData(int column, int row, String data)
        {
            if (column > this.maxColumn && row > this.maxRow)
                return false;
            else
            {
                rowList[row].columns[column] = data;
                return true;
            }
        }

        public Row popFirst()
        {
            if (rowList.Count > 0)
            {
                Row row = rowList.First();
                rowList.RemoveAt(0);
                return row;
            }
            else
                return null;
        }

        public List<Row> get(int column, int id) //this function is not working
        {
            List<Row> tempList = new List<Row>();

            foreach(Row row in rowList)
            {
                Console.WriteLine("current String: " + row.columns[column]);

                int currentRowID = Int32.Parse(row.columns[column]);

                Console.WriteLine("current id: " + currentRowID);

                if (currentRowID == id)
                {
                    tempList.Add(row);
                }

                if (currentRowID > id)
                    break;
            }

            return tempList;
        }

        public List<Row> getAndRemove(int column, int id) //this function is not working
        {
            List<Row> tempList = new List<Row>();
            List<int> removeList = new List<int>();

            for(int count = 0; count < rowList.Count; count++)
            {
                
                int currentRowID = Int32.Parse(rowList[count].columns[column]);

                if (currentRowID == id)
                {
                    tempList.Add(rowList[count]);
                    removeList.Add(count);
                }

                if (currentRowID > id)
                    break;
            }

            removeList.Reverse();

            foreach (int rowIndex in removeList)
            {
                rowList.RemoveAt(rowIndex);
                maxRow--;
            }

            return tempList;
        }

        public void filterVax()
        {
            List<Row> newRowList = new List<Row>();
            foreach (Row row in rowList)
            {
                if(row.columns[1].CompareTo("COVID19") == 0)
                {
                    newRowList.Add(row);
                }
            }
            rowList.Clear();
            rowList = newRowList;

            
            filterDupliacate();
            
        }

        public void filterDupliacate()
        {
            Console.WriteLine("before filter dup: " + rowList.Count);

            List<int> indexList = new List<int>();
            List<Row> newRowList = new List<Row>();

            for (int count = 0; count < rowList.Count; count++)
            {
                String currentID = rowList[count].columns[0];

                if(count + 1 < rowList.Count && currentID.CompareTo(rowList[count + 1].columns[0]) != 0)
                {
                    indexList.Add(count);
                }
                else if(count == rowList.Count - 1) //if this is the last index
                {
                    indexList.Add(count);
                }
            }

            
            foreach(int index in indexList)
            {
                newRowList.Add(rowList[index]);
            }
            rowList.Clear();
            rowList = newRowList;

            Console.WriteLine("after filter dup: " + rowList.Count);
        }
    }
}
