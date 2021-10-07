using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    class Pair
    {
        public String line;
        public int id;

        public Pair(String line)
        {
            this.line = line;
            this.id = Int32.Parse(line.Substring(0, line.IndexOf(',')));
        }
    }
}
