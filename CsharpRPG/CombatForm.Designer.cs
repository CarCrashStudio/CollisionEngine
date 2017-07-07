namespace CsharpRPG
{
    partial class CombatForm
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
            this.components = new System.ComponentModel.Container();
            this.panCombat = new System.Windows.Forms.Panel();
            this.lstSkills = new System.Windows.Forms.ListBox();
            this.btnFLEE = new System.Windows.Forms.Label();
            this.btnITEM = new System.Windows.Forms.Label();
            this.btnATK = new System.Windows.Forms.Label();
            this.lblCombatOutput = new System.Windows.Forms.RichTextBox();
            this.pbDHealth = new System.Windows.Forms.PictureBox();
            this.pbPHealth = new System.Windows.Forms.PictureBox();
            this.pbDefender = new System.Windows.Forms.PictureBox();
            this.pbPlayer = new System.Windows.Forms.PictureBox();
            this.wait = new System.Windows.Forms.Timer(this.components);
            this.panCombat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDHealth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPHealth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDefender)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPlayer)).BeginInit();
            this.SuspendLayout();
            // 
            // panCombat
            // 
            this.panCombat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panCombat.Controls.Add(this.lstSkills);
            this.panCombat.Controls.Add(this.pbDHealth);
            this.panCombat.Controls.Add(this.pbPHealth);
            this.panCombat.Controls.Add(this.pbDefender);
            this.panCombat.Controls.Add(this.pbPlayer);
            this.panCombat.Controls.Add(this.btnFLEE);
            this.panCombat.Controls.Add(this.btnITEM);
            this.panCombat.Controls.Add(this.btnATK);
            this.panCombat.Controls.Add(this.lblCombatOutput);
            this.panCombat.Location = new System.Drawing.Point(0, 0);
            this.panCombat.Name = "panCombat";
            this.panCombat.Size = new System.Drawing.Size(860, 549);
            this.panCombat.TabIndex = 31;
            this.panCombat.Visible = false;
            // 
            // lstSkills
            // 
            this.lstSkills.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lstSkills.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstSkills.FormattingEnabled = true;
            this.lstSkills.ItemHeight = 25;
            this.lstSkills.Location = new System.Drawing.Point(590, 180);
            this.lstSkills.Name = "lstSkills";
            this.lstSkills.Size = new System.Drawing.Size(257, 154);
            this.lstSkills.TabIndex = 35;
            this.lstSkills.Visible = false;
            // 
            // btnFLEE
            // 
            this.btnFLEE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFLEE.BackColor = System.Drawing.Color.DarkGray;
            this.btnFLEE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnFLEE.Location = new System.Drawing.Point(760, 433);
            this.btnFLEE.Name = "btnFLEE";
            this.btnFLEE.Size = new System.Drawing.Size(90, 23);
            this.btnFLEE.TabIndex = 30;
            this.btnFLEE.Text = "FLEE";
            this.btnFLEE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnITEM
            // 
            this.btnITEM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnITEM.BackColor = System.Drawing.Color.DarkGray;
            this.btnITEM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnITEM.Location = new System.Drawing.Point(760, 391);
            this.btnITEM.Name = "btnITEM";
            this.btnITEM.Size = new System.Drawing.Size(90, 23);
            this.btnITEM.TabIndex = 29;
            this.btnITEM.Text = "ITEM";
            this.btnITEM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnATK
            // 
            this.btnATK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnATK.BackColor = System.Drawing.Color.DarkGray;
            this.btnATK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnATK.Location = new System.Drawing.Point(760, 348);
            this.btnATK.Name = "btnATK";
            this.btnATK.Size = new System.Drawing.Size(90, 23);
            this.btnATK.TabIndex = 28;
            this.btnATK.Text = "SKILL";
            this.btnATK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCombatOutput
            // 
            this.lblCombatOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCombatOutput.Location = new System.Drawing.Point(7, 350);
            this.lblCombatOutput.Name = "lblCombatOutput";
            this.lblCombatOutput.ReadOnly = true;
            this.lblCombatOutput.Size = new System.Drawing.Size(747, 188);
            this.lblCombatOutput.TabIndex = 27;
            this.lblCombatOutput.Text = " ";
            // 
            // pbDHealth
            // 
            this.pbDHealth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbDHealth.Location = new System.Drawing.Point(691, 189);
            this.pbDHealth.Name = "pbDHealth";
            this.pbDHealth.Size = new System.Drawing.Size(160, 16);
            this.pbDHealth.TabIndex = 34;
            this.pbDHealth.TabStop = false;
            // 
            // pbPHealth
            // 
            this.pbPHealth.Location = new System.Drawing.Point(7, 189);
            this.pbPHealth.Name = "pbPHealth";
            this.pbPHealth.Size = new System.Drawing.Size(160, 16);
            this.pbPHealth.TabIndex = 33;
            this.pbPHealth.TabStop = false;
            // 
            // pbDefender
            // 
            this.pbDefender.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbDefender.Location = new System.Drawing.Point(677, 3);
            this.pbDefender.Name = "pbDefender";
            this.pbDefender.Size = new System.Drawing.Size(180, 184);
            this.pbDefender.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbDefender.TabIndex = 32;
            this.pbDefender.TabStop = false;
            // 
            // pbPlayer
            // 
            this.pbPlayer.Location = new System.Drawing.Point(3, 3);
            this.pbPlayer.Name = "pbPlayer";
            this.pbPlayer.Size = new System.Drawing.Size(180, 184);
            this.pbPlayer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbPlayer.TabIndex = 31;
            this.pbPlayer.TabStop = false;
            // 
            // wait
            // 
            this.wait.Interval = 350;
            // 
            // CombatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 549);
            this.Controls.Add(this.panCombat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CombatForm";
            this.ShowInTaskbar = false;
            this.Text = "CombatForm";
            this.panCombat.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbDHealth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPHealth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDefender)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPlayer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panCombat;
        public System.Windows.Forms.ListBox lstSkills;
        public System.Windows.Forms.PictureBox pbDHealth;
        public System.Windows.Forms.PictureBox pbPHealth;
        public System.Windows.Forms.PictureBox pbDefender;
        public System.Windows.Forms.PictureBox pbPlayer;
        public System.Windows.Forms.Label btnFLEE;
        public System.Windows.Forms.Label btnITEM;
        public System.Windows.Forms.Label btnATK;
        public System.Windows.Forms.RichTextBox lblCombatOutput;
        public System.Windows.Forms.Timer wait;
    }
}