using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace covidDataSorting
{
    class Column
    {
        List<String> rows;
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
    }
}
