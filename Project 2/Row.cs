using System;
using System.Collections.Generic;
using System.Text;

namespace Project_2
{
    class Row
    {
        public String line;
        public int id;

        public Row(String line)
        {
            this.line = line;
            this.id = Int32.Parse(line.Substring(0, line.IndexOf(',')));
        }
        public Row(Row row)
        {
            this.line = row.line;
            this.id = Int32.Parse(line.Substring(0, line.IndexOf(',')));
        }
    }
}
