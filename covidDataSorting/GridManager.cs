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

        public void addHeader(String header)
        {
            if(maxRow < 0)
            {
                List<String> headerList = header.Split(',').ToList<String>();
                
                rowList.Add(new Row());   //because there are no row then each column in column list is new

                rowList[0].addData(header);
                
                maxRow++;
                maxColumn = headerList.Count - 1;
            }
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

        public void addRow(String line)
        {
            maxRow++;
            rowList.Add(new Row());
            rowList[maxRow].addData(line);
        }

        public void addRow(String line, int IDColumn) //need more work
        {
            maxRow++;
            rowList.Add(new Row());
            rowList[maxRow].addData(line);
        }
    }
}
