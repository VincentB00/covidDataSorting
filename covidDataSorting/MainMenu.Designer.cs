
namespace covidDataSorting
{
    partial class MainMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.browseCSVButton = new System.Windows.Forms.Button();
            this.CSVFileList = new System.Windows.Forms.ListBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.combineCSVFileButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // browseCSVButton
            // 
            this.browseCSVButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.browseCSVButton.Location = new System.Drawing.Point(315, 5);
            this.browseCSVButton.Name = "browseCSVButton";
            this.browseCSVButton.Size = new System.Drawing.Size(187, 65);
            this.browseCSVButton.TabIndex = 0;
            this.browseCSVButton.Text = "Browse CSV file";
            this.browseCSVButton.UseVisualStyleBackColor = true;
            this.browseCSVButton.Click += new System.EventHandler(this.browseCSVButton_Click);
            // 
            // CSVFileList
            // 
            this.CSVFileList.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CSVFileList.FormattingEnabled = true;
            this.CSVFileList.ItemHeight = 24;
            this.CSVFileList.Location = new System.Drawing.Point(12, 5);
            this.CSVFileList.Name = "CSVFileList";
            this.CSVFileList.Size = new System.Drawing.Size(297, 412);
            this.CSVFileList.TabIndex = 1;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // combineCSVFileButton
            // 
            this.combineCSVFileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combineCSVFileButton.Location = new System.Drawing.Point(315, 76);
            this.combineCSVFileButton.Name = "combineCSVFileButton";
            this.combineCSVFileButton.Size = new System.Drawing.Size(187, 65);
            this.combineCSVFileButton.TabIndex = 2;
            this.combineCSVFileButton.Text = "Combine CSV File";
            this.combineCSVFileButton.UseVisualStyleBackColor = true;
            this.combineCSVFileButton.Click += new System.EventHandler(this.combineCSVFileButton_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 561);
            this.Controls.Add(this.combineCSVFileButton);
            this.Controls.Add(this.CSVFileList);
            this.Controls.Add(this.browseCSVButton);
            this.Name = "MainMenu";
            this.Text = "CSV Reader";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button browseCSVButton;
        private System.Windows.Forms.ListBox CSVFileList;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button combineCSVFileButton;
    }
}

