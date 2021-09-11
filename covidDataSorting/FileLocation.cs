using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace covidDataSorting
{
    class FileLocation
    {
        public string fileName;
        public string absolutePath;

        public FileLocation(String absolutePath)
        {
            this.fileName = subFileName(absolutePath);
            this.absolutePath = absolutePath;
        }

        public String subFileName(String absolutePath)
        {
            while(absolutePath.IndexOf("\\") > -1)
            {
                absolutePath = absolutePath.Substring(absolutePath.IndexOf("\\") + 1);
            }

            return absolutePath;
        }

        public override string ToString()
        {
            return fileName;
        }
    }
}
