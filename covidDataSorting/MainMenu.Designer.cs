
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.DebugConsole = new System.Windows.Forms.RichTextBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.absolutePathLabel = new System.Windows.Forms.Label();
            this.openNewFileButton = new System.Windows.Forms.Button();
            this.debugLabel = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // browseCSVButton
            // 
            this.browseCSVButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.browseCSVButton.Location = new System.Drawing.Point(309, 6);
            this.browseCSVButton.Name = "browseCSVButton";
            this.browseCSVButton.Size = new System.Drawing.Size(187, 39);
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
            this.CSVFileList.Location = new System.Drawing.Point(6, 6);
            this.CSVFileList.Name = "CSVFileList";
            this.CSVFileList.Size = new System.Drawing.Size(297, 460);
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
            this.combineCSVFileButton.Location = new System.Drawing.Point(309, 51);
            this.combineCSVFileButton.Name = "combineCSVFileButton";
            this.combineCSVFileButton.Size = new System.Drawing.Size(368, 42);
            this.combineCSVFileButton.TabIndex = 2;
            this.combineCSVFileButton.Text = "Combine CSV File With ID column Index:";
            this.combineCSVFileButton.UseVisualStyleBackColor = true;
            this.combineCSVFileButton.Click += new System.EventHandler(this.combineCSVFileButton_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1160, 537);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.debugLabel);
            this.tabPage1.Controls.Add(this.openNewFileButton);
            this.tabPage1.Controls.Add(this.absolutePathLabel);
            this.tabPage1.Controls.Add(this.CSVFileList);
            this.tabPage1.Controls.Add(this.combineCSVFileButton);
            this.tabPage1.Controls.Add(this.browseCSVButton);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1152, 511);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main Menu";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.DebugConsole);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1152, 511);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Debug Console";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // DebugConsole
            // 
            this.DebugConsole.Location = new System.Drawing.Point(6, 6);
            this.DebugConsole.Name = "DebugConsole";
            this.DebugConsole.Size = new System.Drawing.Size(1140, 499);
            this.DebugConsole.TabIndex = 0;
            this.DebugConsole.Text = "";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "csv";
            this.saveFileDialog1.Filter = "\"CSV Files(*.csv)|*.csv|Text files (*.txt)|*.txt|All files (*.*)|*.*\"";
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // absolutePathLabel
            // 
            this.absolutePathLabel.AutoSize = true;
            this.absolutePathLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.absolutePathLabel.Location = new System.Drawing.Point(471, 106);
            this.absolutePathLabel.Name = "absolutePathLabel";
            this.absolutePathLabel.Size = new System.Drawing.Size(42, 24);
            this.absolutePathLabel.TabIndex = 3;
            this.absolutePathLabel.Text = "N/A";
            // 
            // openNewFileButton
            // 
            this.openNewFileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openNewFileButton.Location = new System.Drawing.Point(309, 99);
            this.openNewFileButton.Name = "openNewFileButton";
            this.openNewFileButton.Size = new System.Drawing.Size(156, 39);
            this.openNewFileButton.TabIndex = 4;
            this.openNewFileButton.Text = "Open New File:";
            this.openNewFileButton.UseVisualStyleBackColor = true;
            this.openNewFileButton.Click += new System.EventHandler(this.openNewFileButton_Click);
            // 
            // debugLabel
            // 
            this.debugLabel.AutoSize = true;
            this.debugLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.debugLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.debugLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.debugLabel.Location = new System.Drawing.Point(6, 482);
            this.debugLabel.Name = "debugLabel";
            this.debugLabel.Size = new System.Drawing.Size(120, 26);
            this.debugLabel.TabIndex = 5;
            this.debugLabel.Text = "Debug Label";
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 561);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainMenu";
            this.Text = "CSV Reader";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button browseCSVButton;
        private System.Windows.Forms.ListBox CSVFileList;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button combineCSVFileButton;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox DebugConsole;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button openNewFileButton;
        private System.Windows.Forms.Label absolutePathLabel;
        private System.Windows.Forms.Label debugLabel;
    }
}

