using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace covidDataSorting
{
    public partial class MainMenu : Form
    {
        GridManager GM;
        public MainMenu()
        {
            InitializeComponent();
        }

        private void browseCSVButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string rawFileName = sender.ToString();

            string absolutePath = rawFileName.Substring(rawFileName.IndexOf("FileName:") + "FileName: ".Length);

            FileLocation FL = new FileLocation(absolutePath);

            CSVFileList.Items.Add(FL);
        }

        private void combineCSVFileButton_Click(object sender, EventArgs e)
        {
            FileLocation[] FL = getAllFileLocation();

            GM = new GridManager();


        }

        private FileLocation[] getAllFileLocation()
        {
            int numberOfFile = CSVFileList.Items.Count;
            FileLocation[] FL = new FileLocation[numberOfFile];

            for(int count = 0; count < numberOfFile; count++)
            {
                FL[count] = (FileLocation)CSVFileList.Items[count];
            }

            return FL;
        }
    }
}
