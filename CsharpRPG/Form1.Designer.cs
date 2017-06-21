namespace CsharpRPG
{
    partial class MainForm
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
            this.lblStats = new System.Windows.Forms.Label();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.lblJournal = new System.Windows.Forms.Label();
            this.lblNotes = new System.Windows.Forms.Label();
            this.lblClose = new System.Windows.Forms.Label();
            this.lstInventory = new System.Windows.Forms.ListBox();
            this.lblArmor = new System.Windows.Forms.Label();
            this.lblOutput = new System.Windows.Forms.RichTextBox();
            this.btnUse = new System.Windows.Forms.Label();
            this.pbMap = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbMap)).BeginInit();
            this.SuspendLayout();
            // 
            // lblStats
            // 
            this.lblStats.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStats.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStats.Location = new System.Drawing.Point(677, 5);
            this.lblStats.Name = "lblStats";
            this.lblStats.Size = new System.Drawing.Size(194, 231);
            this.lblStats.TabIndex = 12;
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(5, 557);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(353, 20);
            this.txtInput.TabIndex = 16;
            // 
            // lblJournal
            // 
            this.lblJournal.BackColor = System.Drawing.Color.DarkGray;
            this.lblJournal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblJournal.Location = new System.Drawing.Point(677, 334);
            this.lblJournal.Name = "lblJournal";
            this.lblJournal.Size = new System.Drawing.Size(90, 23);
            this.lblJournal.TabIndex = 21;
            this.lblJournal.Text = "Journal";
            this.lblJournal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblJournal.Click += new System.EventHandler(this.lblJournal_Click);
            // 
            // lblNotes
            // 
            this.lblNotes.BackColor = System.Drawing.Color.DarkGray;
            this.lblNotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNotes.Location = new System.Drawing.Point(781, 334);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(90, 23);
            this.lblNotes.TabIndex = 22;
            this.lblNotes.Text = "Notes";
            this.lblNotes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNotes.Click += new System.EventHandler(this.lblNotes_Click);
            // 
            // lblClose
            // 
            this.lblClose.BackColor = System.Drawing.Color.DarkGray;
            this.lblClose.Location = new System.Drawing.Point(781, 555);
            this.lblClose.Name = "lblClose";
            this.lblClose.Size = new System.Drawing.Size(90, 23);
            this.lblClose.TabIndex = 23;
            this.lblClose.Text = "Close";
            this.lblClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblClose.Click += new System.EventHandler(this.lblClose_Click);
            // 
            // lstInventory
            // 
            this.lstInventory.FormattingEnabled = true;
            this.lstInventory.Location = new System.Drawing.Point(677, 364);
            this.lstInventory.Name = "lstInventory";
            this.lstInventory.Size = new System.Drawing.Size(194, 186);
            this.lstInventory.TabIndex = 25;
            this.lstInventory.SelectedIndexChanged += new System.EventHandler(this.lstInventory_SelectedIndexChanged);
            this.lstInventory.DoubleClick += new System.EventHandler(this.lstInventory_DoubleClick);
            // 
            // lblArmor
            // 
            this.lblArmor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblArmor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblArmor.Location = new System.Drawing.Point(677, 244);
            this.lblArmor.Name = "lblArmor";
            this.lblArmor.Size = new System.Drawing.Size(194, 81);
            this.lblArmor.TabIndex = 26;
            // 
            // lblOutput
            // 
            this.lblOutput.Location = new System.Drawing.Point(5, 364);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.ReadOnly = true;
            this.lblOutput.Size = new System.Drawing.Size(352, 186);
            this.lblOutput.TabIndex = 27;
            this.lblOutput.Text = " ";
            // 
            // btnUse
            // 
            this.btnUse.BackColor = System.Drawing.Color.DarkGray;
            this.btnUse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnUse.Location = new System.Drawing.Point(677, 555);
            this.btnUse.Name = "btnUse";
            this.btnUse.Size = new System.Drawing.Size(90, 23);
            this.btnUse.TabIndex = 29;
            this.btnUse.Text = "Use";
            this.btnUse.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnUse.Click += new System.EventHandler(this.btnUse_Click);
            // 
            // pbMap
            // 
            this.pbMap.Image = global::CsharpRPG.Properties.Resources._void;
            this.pbMap.Location = new System.Drawing.Point(5, 5);
            this.pbMap.Name = "pbMap";
            this.pbMap.Size = new System.Drawing.Size(666, 352);
            this.pbMap.TabIndex = 15;
            this.pbMap.TabStop = false;
            this.pbMap.Click += new System.EventHandler(this.pbMap_Click);
            this.pbMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbMap_MouseMove);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(876, 587);
            this.ControlBox = false;
            this.Controls.Add(this.btnUse);
            this.Controls.Add(this.lblOutput);
            this.Controls.Add(this.lblArmor);
            this.Controls.Add(this.lstInventory);
            this.Controls.Add(this.lblClose);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.lblJournal);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.pbMap);
            this.Controls.Add(this.lblStats);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.Text = "Window";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pbMap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbMap;
        private System.Windows.Forms.Label lblStats;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Label lblJournal;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.Label lblClose;
        private System.Windows.Forms.ListBox lstInventory;
        private System.Windows.Forms.Label lblArmor;
        private System.Windows.Forms.RichTextBox lblOutput;
        private System.Windows.Forms.Label btnUse;
    }
}