namespace CsharpRPG
{
    partial class CharacterForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.dgvQuests = new System.Windows.Forms.DataGridView();
            this.lblQuestDesc = new System.Windows.Forms.Label();
            this.pbHeadPiece = new System.Windows.Forms.PictureBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblClassLevel = new System.Windows.Forms.Label();
            this.lblStr = new System.Windows.Forms.Label();
            this.lblDefense = new System.Windows.Forms.Label();
            this.pbCharImg = new System.Windows.Forms.PictureBox();
            this.pbHead = new System.Windows.Forms.PictureBox();
            this.pbTorso = new System.Windows.Forms.PictureBox();
            this.pbRightHand = new System.Windows.Forms.PictureBox();
            this.pbBoots = new System.Windows.Forms.PictureBox();
            this.pbLeftHand = new System.Windows.Forms.PictureBox();
            this.pbBracers = new System.Windows.Forms.PictureBox();
            this.pbLegs = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuests)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHeadPiece)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCharImg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHead)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTorso)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRightHand)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBoots)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLeftHand)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBracers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLegs)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Constantia", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(382, -3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 42);
            this.label1.TabIndex = 20;
            this.label1.Text = "Quests";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Font = new System.Drawing.Font("Constantia", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(347, 348);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(201, 44);
            this.btnClose.TabIndex = 21;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dgvQuests
            // 
            this.dgvQuests.AllowUserToAddRows = false;
            this.dgvQuests.AllowUserToDeleteRows = false;
            this.dgvQuests.AllowUserToResizeRows = false;
            this.dgvQuests.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvQuests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQuests.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvQuests.Enabled = false;
            this.dgvQuests.Location = new System.Drawing.Point(347, 42);
            this.dgvQuests.MultiSelect = false;
            this.dgvQuests.Name = "dgvQuests";
            this.dgvQuests.RowHeadersVisible = false;
            this.dgvQuests.Size = new System.Drawing.Size(201, 149);
            this.dgvQuests.TabIndex = 22;
            this.dgvQuests.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvQuests_CellMouseClick);
            // 
            // lblQuestDesc
            // 
            this.lblQuestDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblQuestDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblQuestDesc.Location = new System.Drawing.Point(347, 194);
            this.lblQuestDesc.Name = "lblQuestDesc";
            this.lblQuestDesc.Size = new System.Drawing.Size(201, 151);
            this.lblQuestDesc.TabIndex = 23;
            // 
            // pbHeadPiece
            // 
            this.pbHeadPiece.BackgroundImage = global::CsharpRPG.Properties.Resources.CharImgBox;
            this.pbHeadPiece.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbHeadPiece.Location = new System.Drawing.Point(15, 12);
            this.pbHeadPiece.Name = "pbHeadPiece";
            this.pbHeadPiece.Size = new System.Drawing.Size(60, 60);
            this.pbHeadPiece.TabIndex = 24;
            this.pbHeadPiece.TabStop = false;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(78, 350);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(176, 13);
            this.lblName.TabIndex = 25;
            this.lblName.Text = "Name: abcdefghijklmnopqrstuvwxyz";
            // 
            // lblClassLevel
            // 
            this.lblClassLevel.AutoSize = true;
            this.lblClassLevel.Location = new System.Drawing.Point(78, 364);
            this.lblClassLevel.Name = "lblClassLevel";
            this.lblClassLevel.Size = new System.Drawing.Size(134, 13);
            this.lblClassLevel.TabIndex = 26;
            this.lblClassLevel.Text = "Class (Level): Warrior (100)";
            // 
            // lblStr
            // 
            this.lblStr.AutoSize = true;
            this.lblStr.Location = new System.Drawing.Point(78, 376);
            this.lblStr.Name = "lblStr";
            this.lblStr.Size = new System.Drawing.Size(59, 13);
            this.lblStr.TabIndex = 27;
            this.lblStr.Text = "Strength: 0";
            // 
            // lblDefense
            // 
            this.lblDefense.AutoSize = true;
            this.lblDefense.Location = new System.Drawing.Point(153, 376);
            this.lblDefense.Name = "lblDefense";
            this.lblDefense.Size = new System.Drawing.Size(59, 13);
            this.lblDefense.TabIndex = 28;
            this.lblDefense.Text = "Defense: 0";
            // 
            // pbCharImg
            // 
            this.pbCharImg.BackColor = System.Drawing.Color.Transparent;
            this.pbCharImg.BackgroundImage = global::CsharpRPG.Properties.Resources.bagbox;
            this.pbCharImg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbCharImg.Location = new System.Drawing.Point(81, 12);
            this.pbCharImg.Name = "pbCharImg";
            this.pbCharImg.Size = new System.Drawing.Size(194, 335);
            this.pbCharImg.TabIndex = 29;
            this.pbCharImg.TabStop = false;
            // 
            // pbHead
            // 
            this.pbHead.BackgroundImage = global::CsharpRPG.Properties.Resources.CharImgBox;
            this.pbHead.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbHead.Location = new System.Drawing.Point(15, 101);
            this.pbHead.Name = "pbHead";
            this.pbHead.Size = new System.Drawing.Size(60, 60);
            this.pbHead.TabIndex = 30;
            this.pbHead.TabStop = false;
            // 
            // pbTorso
            // 
            this.pbTorso.BackgroundImage = global::CsharpRPG.Properties.Resources.CharImgBox;
            this.pbTorso.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbTorso.Location = new System.Drawing.Point(15, 287);
            this.pbTorso.Name = "pbTorso";
            this.pbTorso.Size = new System.Drawing.Size(60, 60);
            this.pbTorso.TabIndex = 32;
            this.pbTorso.TabStop = false;
            // 
            // pbRightHand
            // 
            this.pbRightHand.BackgroundImage = global::CsharpRPG.Properties.Resources.CharImgBox;
            this.pbRightHand.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbRightHand.Location = new System.Drawing.Point(15, 194);
            this.pbRightHand.Name = "pbRightHand";
            this.pbRightHand.Size = new System.Drawing.Size(60, 60);
            this.pbRightHand.TabIndex = 31;
            this.pbRightHand.TabStop = false;
            // 
            // pbBoots
            // 
            this.pbBoots.BackgroundImage = global::CsharpRPG.Properties.Resources.CharImgBox;
            this.pbBoots.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbBoots.Location = new System.Drawing.Point(281, 285);
            this.pbBoots.Name = "pbBoots";
            this.pbBoots.Size = new System.Drawing.Size(60, 60);
            this.pbBoots.TabIndex = 36;
            this.pbBoots.TabStop = false;
            // 
            // pbLeftHand
            // 
            this.pbLeftHand.BackgroundImage = global::CsharpRPG.Properties.Resources.CharImgBox;
            this.pbLeftHand.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbLeftHand.Location = new System.Drawing.Point(281, 194);
            this.pbLeftHand.Name = "pbLeftHand";
            this.pbLeftHand.Size = new System.Drawing.Size(60, 60);
            this.pbLeftHand.TabIndex = 35;
            this.pbLeftHand.TabStop = false;
            // 
            // pbBracers
            // 
            this.pbBracers.BackgroundImage = global::CsharpRPG.Properties.Resources.CharImgBox;
            this.pbBracers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbBracers.Location = new System.Drawing.Point(281, 12);
            this.pbBracers.Name = "pbBracers";
            this.pbBracers.Size = new System.Drawing.Size(60, 60);
            this.pbBracers.TabIndex = 34;
            this.pbBracers.TabStop = false;
            // 
            // pbLegs
            // 
            this.pbLegs.BackgroundImage = global::CsharpRPG.Properties.Resources.CharImgBox;
            this.pbLegs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbLegs.Location = new System.Drawing.Point(281, 101);
            this.pbLegs.Name = "pbLegs";
            this.pbLegs.Size = new System.Drawing.Size(60, 60);
            this.pbLegs.TabIndex = 33;
            this.pbLegs.TabStop = false;
            // 
            // CharacterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(554, 395);
            this.ControlBox = false;
            this.Controls.Add(this.pbBoots);
            this.Controls.Add(this.pbLeftHand);
            this.Controls.Add(this.pbBracers);
            this.Controls.Add(this.pbLegs);
            this.Controls.Add(this.pbTorso);
            this.Controls.Add(this.pbRightHand);
            this.Controls.Add(this.pbHead);
            this.Controls.Add(this.lblDefense);
            this.Controls.Add(this.lblStr);
            this.Controls.Add(this.lblClassLevel);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.pbHeadPiece);
            this.Controls.Add(this.lblQuestDesc);
            this.Controls.Add(this.dgvQuests);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbCharImg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CharacterForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Stats";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuests)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHeadPiece)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCharImg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHead)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTorso)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRightHand)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBoots)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLeftHand)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBracers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLegs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;
        public System.Windows.Forms.DataGridView dgvQuests;
        private System.Windows.Forms.Label lblQuestDesc;
        public System.Windows.Forms.PictureBox pbHeadPiece;
        public System.Windows.Forms.Label lblName;
        public System.Windows.Forms.Label lblClassLevel;
        public System.Windows.Forms.Label lblStr;
        public System.Windows.Forms.Label lblDefense;
        public System.Windows.Forms.PictureBox pbCharImg;
        public System.Windows.Forms.PictureBox pbHead;
        public System.Windows.Forms.PictureBox pbTorso;
        public System.Windows.Forms.PictureBox pbRightHand;
        public System.Windows.Forms.PictureBox pbBoots;
        public System.Windows.Forms.PictureBox pbLeftHand;
        public System.Windows.Forms.PictureBox pbBracers;
        public System.Windows.Forms.PictureBox pbLegs;
    }
}