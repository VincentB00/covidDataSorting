using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace covidDataSorting
{
    class Column
    {
        public List<String> rows;
        public Column()
        {
            rows = new List<string>();
        }
        public String getHeader()
        {
            if (rows.Count > 1)
                return rows[0];
            else 
                return null;
        }
        public void addRow(String data)
        {
            rows.Add(data);
        }

        public String getRow(int row)
        {
            return rows[row];
        }

        public bool setRow(int row, String data)
        {
            if (row > rows.Count)
                return false;
            else
            {
                rows[row] = data;
                return true;
            }
        }

    }
}
