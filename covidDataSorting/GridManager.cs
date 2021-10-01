using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace covidDataSorting
{
    public class GridManager : IDisposable
    {
        public int maxColumnIndex; // index of max column
        public int maxRowIndex;  // index of max row
        public List<Row> rowList;
        private bool disposedValue;

        public GridManager()
        {
            rowList = new List<Row>();
            maxColumnIndex = -1;
            maxRowIndex = -1;
        }

        public bool isEmpty()
        {
            if (maxRowIndex < 0 || rowList.Count <= 0)
                return true;
            else
                return false;
        }

        public void addHeader(String headers)
        {
            if(maxRowIndex < 0)
            {
                List<String> headerList = Row.splitCSVData(headers);

                rowList.Add(new Row());   //because there are no row then each column in column list is new

                rowList[0].addData(headers);
                
                maxRowIndex++;
                maxColumnIndex = headerList.Count - 1;
            }
            else
            {
                int numberOfAddedColumn = 0;

                Row row = new Row();
                row.addData(headers);
                row.columns.RemoveAt(0);

                addRowData(row.ToString(), 0);

                numberOfAddedColumn = row.columns.Count;

                maxColumnIndex += numberOfAddedColumn;
            }
        }

        public void addRow(Row row)
        {
            addRow(row.ToString());
        }

        public void addRow(String line)
        {
            maxRowIndex++;
            rowList.Add(new Row());
            rowList[maxRowIndex].addData(line);
        }

        public void addRowData(String line, int rowIndex)
        {
            rowList[rowIndex].addData(line);
        }

        public void removeRowAt(int index)
        {
            rowList.RemoveAt(index);
            maxRowIndex--;
        }

        public String getHeader()
        {
            String result = "";
            if (maxRowIndex >= 0)
            {
                result = rowList[0].ToString();
            }
            return result;
        }

        public String getData(int column, int row)
        {
            if (column > this.maxColumnIndex && row > this.maxRowIndex)
                return null;
            else
                return rowList[row].columns[column];
        }

        public bool setData(int column, int row, String data)
        {
            if (column > this.maxColumnIndex && row > this.maxRowIndex)
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
                maxRowIndex--;
            }

            return tempList;
        }

        //---------------------------------------task 1 function-------------------------------
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
            //Console.WriteLine("before filter dup: " + rowList.Count);

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

            //Console.WriteLine("after filter dup: " + rowList.Count);
        }

        //--------------------------------task 2 function----------------------------------------
        public void suffleRowList()
        {
            Random random = new Random();
            RowDataSet rds = new RowDataSet(rowList);

            for(int count = 0; count < rowList.Count; count++)
            {
                int ranNum1 = random.Next(0, rowList.Count);
                int ranNum2 = random.Next(0, rowList.Count);

                if(ranNum1 != ranNum2 && ranNum1 != 0 && ranNum2 != 0)
                {
                    rds.swap(ranNum1, ranNum2);
                }
            }

            List<Row> newRowList = rds.getCopyOfCurrentRowListOrder();
            rowList.Clear();
            rowList = newRowList;
        }
        public void filterColumn(List<int> columns)
        {
            List<int> errorIndex = new List<int>();
            for(int count = 0; count < rowList.Count; count++)
            {
                try
                {
                    Row rowT = new Row();
                    foreach (int index in columns)
                    {
                        rowT.columns.Add(rowList[count].columns[index]);
                    }
                    rowList[count].columns = rowT.columns;
                }
                catch(Exception ex)
                {
                    errorIndex.Add(count);
                }
            }

            errorIndex.Reverse();

            foreach (int index in errorIndex)
            {
                //Console.WriteLine("removing row: " + rowList[index].columns[0]);
                rowList.RemoveAt(index);
            }

            maxColumnIndex = rowList[0].columns.Count - 1;
        }

        public void splitColumn(List<int> columns)
        {
            columns.Reverse();
            int currentIndex = 0;
            List<Row> newRowList = new List<Row>();
            Row tempRow = rowList[currentIndex];

            //Modify Header
            tempRow.columns[columns.Last()] = "SYMPTOM";

            foreach(int column in columns)
            if(column != columns.Last()) //discount the last index
            {
                tempRow.columns.RemoveAt(column);
            }

            newRowList.Add(tempRow);

            //modify data //need fix here
            currentIndex++;
            while(currentIndex < rowList.Count)
            {
                List<Row> splitedRowList = splitColumnToRow(currentIndex, columns);
                foreach (Row row in splitedRowList)
                    newRowList.Add(row);
                
                currentIndex++;
            }

            rowList.Clear();
            rowList = newRowList;

            maxColumnIndex = rowList[0].columns.Count - 1;
            maxRowIndex = rowList.Count - 1;
        }

        public List<Row> splitColumnToRow(int index, List<int> columns)
        {
            //this will work if columns is already Reverse

            List<Row> tempRowList = new List<Row>();
            //Row pointerRow = rowList[index];
            Row pointerRow = new Row();
            copyRow(rowList[index], pointerRow);

            Row tempRow = new Row();
            copyRow(pointerRow, tempRow);
            //Row tempRow = pointerRow;
            foreach (int column in columns)
            {
                if(column != columns.Last()) //skip first column
                {
                    tempRow.columns.RemoveAt(column);
                }
            }
            foreach(int column in columns)//here is the problem
            {
                String data = pointerRow.columns[column].ToString();
                if (data.CompareTo("") != 0 && data != null)
                {
                    Row row = new Row();
                    tempRow.columns[columns.Last()] = data;
                    copyRow(tempRow, row);
                    tempRowList.Add(row);
                }
            }

            return tempRowList;
        }

        private void copyRow(Row row1, Row row2)
        {
            foreach (String data in row1.columns)
                row2.columns.Add(data);
        }

        public static void QuickSort(RowDataSet rds, int l, int r)
        {
            if (l < r)
            {
                int p = Partition(rds, l, r);

                QuickSort(rds, l, p - 1);
                QuickSort(rds, p + 1, r);
            }
        }

        public static int Partition(RowDataSet rds, int l, int r)
        {
            int pivot = rds.getData(r).getIDColumn();

            int i = l - 1;

            for (int j = l; j <= r - 1; j++)
            {
                if (rds.getData(j).getIDColumn() < pivot)
                {
                    i++;
                    rds.swap(i, j);
                }
            }

            rds.swap(i + 1, r);

            return i + 1;
        }

        public static void InsertionSort(RowDataSet rds)
        {
            for (int i = 1; i < rds.orderList.Count; i++)
            {
                int j = i;
                
                while (j > 0 && rds.getData(j - 1).getIDColumn() > rds.getData(j).getIDColumn())
                {
                    rds.swap(j, j - 1);
                    j = j - 1;
                }
            }
        }

        public static void SelectionSort(RowDataSet rds)
        {
            int max = rds.orderList.Count;
            for(int j = 0; j < max - 1; j++)
            {
                int iMin = j;
                for(int i = j + 1; i < max; i++)
                {
                    if(rds.getData(i).getIDColumn() < rds.getData(iMin).getIDColumn())
                    {
                        iMin = i;
                    }
                }
                if (iMin != j)
                {
                    rds.swap(j, iMin);
                }
            }
        }

        //--------------------------------task 3 function----------------------------------------
        public List<int> groupByAge(int minAge, int maxAge)
        {
            List<int> rowIndexs = new List<int>();
            for(int count = 0; count < rowList.Count; count++)
            {
                String ageStr = rowList[count].columns[1];
                if(ageStr != null && ageStr.CompareTo("") != 0 && ageStr.CompareTo("AGE_YRS") != 0)
                {
                    float currentAge = float.Parse(ageStr);
                    if(currentAge >= minAge && currentAge <= maxAge)
                    {
                        rowIndexs.Add(count);
                    }
                }
                else if((ageStr == null || ageStr.CompareTo("") == 0) && (minAge == -1 && maxAge == -1))
                {
                    rowIndexs.Add(count);
                }
            }

            return rowIndexs;
        }

        public List<int> groupByColumn(List<int> range, int column, String compareWith)
        {
            List<int> rowIndexs = new List<int>();

            foreach(int index in range)
            {
                if (rowList[index].columns[column].CompareTo(compareWith) == 0)
                    rowIndexs.Add(index);
            }

            return rowIndexs;
        }

        public int calculateNumberOfDeathCases()
        {
            int result = 0;
            RowDataSet rds = new RowDataSet(rowList);
            List<int> allDeathRow = groupByColumn(rds.orderList, 6, "Y");

            using(var tempGM = new GridManager())
            {
                foreach (int index in allDeathRow)
                {
                    tempGM.addRow(rowList[index].ToString());
                }

                tempGM.filterDupliacate();

                result = tempGM.rowList.Count;
            }

            return result;
        }


        //-----------------------------------------------------------------------------------------------------

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    rowList.Clear();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
