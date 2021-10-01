
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
            this.combineRunTimeLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.selectionSortButton = new System.Windows.Forms.Button();
            this.selectionSortTimerLabel = new System.Windows.Forms.Label();
            this.insertionSortTimerLabel = new System.Windows.Forms.Label();
            this.quickSortTimerLabel = new System.Windows.Forms.Label();
            this.suffleCheckBox = new System.Windows.Forms.CheckBox();
            this.insertionSortButton = new System.Windows.Forms.Button();
            this.quickSortButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.browseTask2FileButton = new System.Windows.Forms.Button();
            this.filterTask1Button = new System.Windows.Forms.Button();
            this.browseTask1Button = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.exportToCSVFileButton = new System.Windows.Forms.Button();
            this.groupFileButton = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.browseTask2FileButton2 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.DebugConsole = new System.Windows.Forms.RichTextBox();
            this.absolutePathLabel = new System.Windows.Forms.Label();
            this.openNewFileButton = new System.Windows.Forms.Button();
            this.debugLabel = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
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
            this.CSVFileList.Size = new System.Drawing.Size(297, 412);
            this.CSVFileList.TabIndex = 1;
            this.CSVFileList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CSVFileList_KeyDown);
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
            this.combineCSVFileButton.Size = new System.Drawing.Size(314, 42);
            this.combineCSVFileButton.TabIndex = 2;
            this.combineCSVFileButton.Text = "Read, Combine and write CSV File";
            this.combineCSVFileButton.UseVisualStyleBackColor = true;
            this.combineCSVFileButton.Click += new System.EventHandler(this.combineCSVFileButton_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1160, 466);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.combineRunTimeLabel);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.CSVFileList);
            this.tabPage1.Controls.Add(this.combineCSVFileButton);
            this.tabPage1.Controls.Add(this.browseCSVButton);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1152, 440);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Task 1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // combineRunTimeLabel
            // 
            this.combineRunTimeLabel.AutoSize = true;
            this.combineRunTimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combineRunTimeLabel.Location = new System.Drawing.Point(309, 105);
            this.combineRunTimeLabel.Name = "combineRunTimeLabel";
            this.combineRunTimeLabel.Size = new System.Drawing.Size(127, 25);
            this.combineRunTimeLabel.TabIndex = 8;
            this.combineRunTimeLabel.Text = "Run time: ...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(803, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(343, 120);
            this.label1.TabIndex = 6;
            this.label1.Text = "Note:\r\nplease input file in order with group of 3:\r\n1. Data file\r\n2. Symtom file\r" +
    "\n3. Vax file";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.selectionSortButton);
            this.tabPage3.Controls.Add(this.selectionSortTimerLabel);
            this.tabPage3.Controls.Add(this.insertionSortTimerLabel);
            this.tabPage3.Controls.Add(this.quickSortTimerLabel);
            this.tabPage3.Controls.Add(this.suffleCheckBox);
            this.tabPage3.Controls.Add(this.insertionSortButton);
            this.tabPage3.Controls.Add(this.quickSortButton);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.browseTask2FileButton);
            this.tabPage3.Controls.Add(this.filterTask1Button);
            this.tabPage3.Controls.Add(this.browseTask1Button);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1152, 440);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Task 2";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // selectionSortButton
            // 
            this.selectionSortButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectionSortButton.Location = new System.Drawing.Point(7, 159);
            this.selectionSortButton.Name = "selectionSortButton";
            this.selectionSortButton.Size = new System.Drawing.Size(249, 35);
            this.selectionSortButton.TabIndex = 13;
            this.selectionSortButton.Text = "Selection Sort";
            this.selectionSortButton.UseVisualStyleBackColor = true;
            this.selectionSortButton.Click += new System.EventHandler(this.selectionSortButton_Click);
            // 
            // selectionSortTimerLabel
            // 
            this.selectionSortTimerLabel.AutoSize = true;
            this.selectionSortTimerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectionSortTimerLabel.Location = new System.Drawing.Point(3, 197);
            this.selectionSortTimerLabel.Name = "selectionSortTimerLabel";
            this.selectionSortTimerLabel.Size = new System.Drawing.Size(42, 24);
            this.selectionSortTimerLabel.TabIndex = 12;
            this.selectionSortTimerLabel.Text = "N/A";
            // 
            // insertionSortTimerLabel
            // 
            this.insertionSortTimerLabel.AutoSize = true;
            this.insertionSortTimerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.insertionSortTimerLabel.Location = new System.Drawing.Point(3, 327);
            this.insertionSortTimerLabel.Name = "insertionSortTimerLabel";
            this.insertionSortTimerLabel.Size = new System.Drawing.Size(42, 24);
            this.insertionSortTimerLabel.TabIndex = 10;
            this.insertionSortTimerLabel.Text = "N/A";
            // 
            // quickSortTimerLabel
            // 
            this.quickSortTimerLabel.AutoSize = true;
            this.quickSortTimerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quickSortTimerLabel.Location = new System.Drawing.Point(3, 262);
            this.quickSortTimerLabel.Name = "quickSortTimerLabel";
            this.quickSortTimerLabel.Size = new System.Drawing.Size(42, 24);
            this.quickSortTimerLabel.TabIndex = 9;
            this.quickSortTimerLabel.Text = "N/A";
            // 
            // suffleCheckBox
            // 
            this.suffleCheckBox.AutoSize = true;
            this.suffleCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.suffleCheckBox.Location = new System.Drawing.Point(266, 57);
            this.suffleCheckBox.Name = "suffleCheckBox";
            this.suffleCheckBox.Size = new System.Drawing.Size(187, 28);
            this.suffleCheckBox.TabIndex = 8;
            this.suffleCheckBox.Text = "Suffle for debuging";
            this.suffleCheckBox.UseVisualStyleBackColor = true;
            // 
            // insertionSortButton
            // 
            this.insertionSortButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.insertionSortButton.Location = new System.Drawing.Point(7, 289);
            this.insertionSortButton.Name = "insertionSortButton";
            this.insertionSortButton.Size = new System.Drawing.Size(249, 35);
            this.insertionSortButton.TabIndex = 7;
            this.insertionSortButton.Text = "Insertion Sort";
            this.insertionSortButton.UseVisualStyleBackColor = true;
            this.insertionSortButton.Click += new System.EventHandler(this.insertionSortButton_Click);
            // 
            // quickSortButton
            // 
            this.quickSortButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quickSortButton.Location = new System.Drawing.Point(7, 224);
            this.quickSortButton.Name = "quickSortButton";
            this.quickSortButton.Size = new System.Drawing.Size(249, 35);
            this.quickSortButton.TabIndex = 6;
            this.quickSortButton.Text = "Quick Sort";
            this.quickSortButton.UseVisualStyleBackColor = true;
            this.quickSortButton.Click += new System.EventHandler(this.quickSortButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 24);
            this.label4.TabIndex = 5;
            this.label4.Text = "N/A";
            // 
            // browseTask2FileButton
            // 
            this.browseTask2FileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.browseTask2FileButton.Location = new System.Drawing.Point(7, 94);
            this.browseTask2FileButton.Name = "browseTask2FileButton";
            this.browseTask2FileButton.Size = new System.Drawing.Size(249, 35);
            this.browseTask2FileButton.TabIndex = 4;
            this.browseTask2FileButton.Text = "Browse and read task 2 file";
            this.browseTask2FileButton.UseVisualStyleBackColor = true;
            this.browseTask2FileButton.Click += new System.EventHandler(this.browseTask2FileButton_Click);
            // 
            // filterTask1Button
            // 
            this.filterTask1Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterTask1Button.Location = new System.Drawing.Point(7, 53);
            this.filterTask1Button.Name = "filterTask1Button";
            this.filterTask1Button.Size = new System.Drawing.Size(249, 35);
            this.filterTask1Button.TabIndex = 2;
            this.filterTask1Button.Text = "filter task 1 file";
            this.filterTask1Button.UseVisualStyleBackColor = true;
            this.filterTask1Button.Click += new System.EventHandler(this.filterTask1Button_Click);
            // 
            // browseTask1Button
            // 
            this.browseTask1Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.browseTask1Button.Location = new System.Drawing.Point(7, 12);
            this.browseTask1Button.Name = "browseTask1Button";
            this.browseTask1Button.Size = new System.Drawing.Size(249, 35);
            this.browseTask1Button.TabIndex = 1;
            this.browseTask1Button.Text = "Browse and read task 1 file location";
            this.browseTask1Button.UseVisualStyleBackColor = true;
            this.browseTask1Button.Click += new System.EventHandler(this.browseTask1Button_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.label5);
            this.tabPage4.Controls.Add(this.label3);
            this.tabPage4.Controls.Add(this.label2);
            this.tabPage4.Controls.Add(this.button1);
            this.tabPage4.Controls.Add(this.exportToCSVFileButton);
            this.tabPage4.Controls.Add(this.groupFileButton);
            this.tabPage4.Controls.Add(this.treeView1);
            this.tabPage4.Controls.Add(this.browseTask2FileButton2);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(1152, 440);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Task 3";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 24);
            this.label2.TabIndex = 11;
            this.label2.Text = "N/A";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(8, 174);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(249, 35);
            this.button1.TabIndex = 10;
            this.button1.Text = "Test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // exportToCSVFileButton
            // 
            this.exportToCSVFileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exportToCSVFileButton.Location = new System.Drawing.Point(8, 133);
            this.exportToCSVFileButton.Name = "exportToCSVFileButton";
            this.exportToCSVFileButton.Size = new System.Drawing.Size(249, 35);
            this.exportToCSVFileButton.TabIndex = 9;
            this.exportToCSVFileButton.Text = "Export To CSV File";
            this.exportToCSVFileButton.UseVisualStyleBackColor = true;
            this.exportToCSVFileButton.Click += new System.EventHandler(this.exportToCSVFileButton_Click);
            // 
            // groupFileButton
            // 
            this.groupFileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupFileButton.Location = new System.Drawing.Point(7, 68);
            this.groupFileButton.Name = "groupFileButton";
            this.groupFileButton.Size = new System.Drawing.Size(249, 35);
            this.groupFileButton.TabIndex = 8;
            this.groupFileButton.Text = "Group File";
            this.groupFileButton.UseVisualStyleBackColor = true;
            this.groupFileButton.Click += new System.EventHandler(this.groupFileButton_Click);
            // 
            // treeView1
            // 
            this.treeView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView1.Location = new System.Drawing.Point(367, 3);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(782, 434);
            this.treeView1.TabIndex = 7;
            // 
            // browseTask2FileButton2
            // 
            this.browseTask2FileButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.browseTask2FileButton2.Location = new System.Drawing.Point(6, 3);
            this.browseTask2FileButton2.Name = "browseTask2FileButton2";
            this.browseTask2FileButton2.Size = new System.Drawing.Size(249, 35);
            this.browseTask2FileButton2.TabIndex = 6;
            this.browseTask2FileButton2.Text = "Browse and read task 2 file";
            this.browseTask2FileButton2.UseVisualStyleBackColor = true;
            this.browseTask2FileButton2.Click += new System.EventHandler(this.browseTask2FileButton_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.DebugConsole);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1152, 440);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Debug Console";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // DebugConsole
            // 
            this.DebugConsole.Location = new System.Drawing.Point(6, 6);
            this.DebugConsole.Name = "DebugConsole";
            this.DebugConsole.Size = new System.Drawing.Size(1140, 428);
            this.DebugConsole.TabIndex = 0;
            this.DebugConsole.Text = "";
            // 
            // absolutePathLabel
            // 
            this.absolutePathLabel.AutoSize = true;
            this.absolutePathLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.absolutePathLabel.Location = new System.Drawing.Point(259, 491);
            this.absolutePathLabel.Name = "absolutePathLabel";
            this.absolutePathLabel.Size = new System.Drawing.Size(42, 24);
            this.absolutePathLabel.TabIndex = 3;
            this.absolutePathLabel.Text = "N/A";
            // 
            // openNewFileButton
            // 
            this.openNewFileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openNewFileButton.Location = new System.Drawing.Point(22, 484);
            this.openNewFileButton.Name = "openNewFileButton";
            this.openNewFileButton.Size = new System.Drawing.Size(231, 39);
            this.openNewFileButton.TabIndex = 4;
            this.openNewFileButton.Text = "Open Current Pointer file";
            this.openNewFileButton.UseVisualStyleBackColor = true;
            this.openNewFileButton.Click += new System.EventHandler(this.openNewFileButton_Click);
            // 
            // debugLabel
            // 
            this.debugLabel.AutoSize = true;
            this.debugLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.debugLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.debugLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.debugLabel.Location = new System.Drawing.Point(22, 526);
            this.debugLabel.Name = "debugLabel";
            this.debugLabel.Size = new System.Drawing.Size(120, 26);
            this.debugLabel.TabIndex = 5;
            this.debugLabel.Text = "Debug Label";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "csv";
            this.saveFileDialog1.Filter = "\"CSV Files(*.csv)|*.csv|Text files (*.txt)|*.txt|All files (*.*)|*.*\"";
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 24);
            this.label3.TabIndex = 12;
            this.label3.Text = "Grouping timer";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(4, 413);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(230, 24);
            this.label5.TabIndex = 13;
            this.label5.Text = "Number of death cases: ...";
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 561);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.debugLabel);
            this.Controls.Add(this.absolutePathLabel);
            this.Controls.Add(this.openNewFileButton);
            this.Name = "MainMenu";
            this.Text = "CSV Reader";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label combineRunTimeLabel;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button filterTask1Button;
        private System.Windows.Forms.Button browseTask1Button;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button browseTask2FileButton;
        private System.Windows.Forms.Button insertionSortButton;
        private System.Windows.Forms.Button quickSortButton;
        private System.Windows.Forms.CheckBox suffleCheckBox;
        private System.Windows.Forms.Label quickSortTimerLabel;
        private System.Windows.Forms.Label insertionSortTimerLabel;
        private System.Windows.Forms.Label selectionSortTimerLabel;
        private System.Windows.Forms.Button selectionSortButton;
        private System.Windows.Forms.Button browseTask2FileButton2;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button groupFileButton;
        private System.Windows.Forms.Button exportToCSVFileButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
    }
}

