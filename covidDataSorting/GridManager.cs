using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace covidDataSorting
{
    class GridManager
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

                List<String> headerList = Row.splitCSVData(headers);

                List<String> currentHeaderList = rowList[0].columns;

                foreach(String header in headerList)
                {
                    foreach(String currentHeader in currentHeaderList)
                    {
                        if(currentHeader.CompareTo(header) < 0)
                        {
                            addRowData(header, 0);
                            numberOfAddedColumn++;
                        }
                    }
                }

                maxColumn += numberOfAddedColumn;
            }
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
    }
}
