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
            this.components = new System.ComponentModel.Container();
            this.pbMap = new System.Windows.Forms.PictureBox();
            this.panCombat = new System.Windows.Forms.Panel();
            this.pbDHealth = new System.Windows.Forms.PictureBox();
            this.pbPHealth = new System.Windows.Forms.PictureBox();
            this.pbDefender = new System.Windows.Forms.PictureBox();
            this.pbPlayer = new System.Windows.Forms.PictureBox();
            this.btnFLEE = new System.Windows.Forms.Label();
            this.btnITEM = new System.Windows.Forms.Label();
            this.btnATK = new System.Windows.Forms.Label();
            this.lblCombatOutput = new System.Windows.Forms.RichTextBox();
            this.wait = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pbMap)).BeginInit();
            this.panCombat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDHealth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPHealth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDefender)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPlayer)).BeginInit();
            this.SuspendLayout();
            // 
            // pbMap
            // 
            this.pbMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbMap.Image = global::CsharpRPG.Properties.Resources._void;
            this.pbMap.Location = new System.Drawing.Point(0, 0);
            this.pbMap.Name = "pbMap";
            this.pbMap.Size = new System.Drawing.Size(878, 588);
            this.pbMap.TabIndex = 15;
            this.pbMap.TabStop = false;
            this.pbMap.Click += new System.EventHandler(this.pbMap_Click);
            this.pbMap.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbMap_MouseClick);
            this.pbMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbMap_MouseMove);
            // 
            // panCombat
            // 
            this.panCombat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
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
            this.panCombat.Size = new System.Drawing.Size(877, 587);
            this.panCombat.TabIndex = 30;
            this.panCombat.Visible = false;
            // 
            // pbDHealth
            // 
            this.pbDHealth.Location = new System.Drawing.Point(685, 195);
            this.pbDHealth.Name = "pbDHealth";
            this.pbDHealth.Size = new System.Drawing.Size(160, 16);
            this.pbDHealth.TabIndex = 34;
            this.pbDHealth.TabStop = false;
            // 
            // pbPHealth
            // 
            this.pbPHealth.Location = new System.Drawing.Point(16, 196);
            this.pbPHealth.Name = "pbPHealth";
            this.pbPHealth.Size = new System.Drawing.Size(160, 16);
            this.pbPHealth.TabIndex = 33;
            this.pbPHealth.TabStop = false;
            // 
            // pbDefender
            // 
            this.pbDefender.Location = new System.Drawing.Point(679, 7);
            this.pbDefender.Name = "pbDefender";
            this.pbDefender.Size = new System.Drawing.Size(180, 184);
            this.pbDefender.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbDefender.TabIndex = 32;
            this.pbDefender.TabStop = false;
            // 
            // pbPlayer
            // 
            this.pbPlayer.Location = new System.Drawing.Point(7, 7);
            this.pbPlayer.Name = "pbPlayer";
            this.pbPlayer.Size = new System.Drawing.Size(180, 184);
            this.pbPlayer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbPlayer.TabIndex = 31;
            this.pbPlayer.TabStop = false;
            // 
            // btnFLEE
            // 
            this.btnFLEE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFLEE.BackColor = System.Drawing.Color.DarkGray;
            this.btnFLEE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnFLEE.Location = new System.Drawing.Point(777, 471);
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
            this.btnITEM.Location = new System.Drawing.Point(777, 429);
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
            this.btnATK.Location = new System.Drawing.Point(777, 386);
            this.btnATK.Name = "btnATK";
            this.btnATK.Size = new System.Drawing.Size(90, 23);
            this.btnATK.TabIndex = 28;
            this.btnATK.Text = "ATK";
            this.btnATK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCombatOutput
            // 
            this.lblCombatOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCombatOutput.Location = new System.Drawing.Point(7, 388);
            this.lblCombatOutput.Name = "lblCombatOutput";
            this.lblCombatOutput.ReadOnly = true;
            this.lblCombatOutput.Size = new System.Drawing.Size(764, 188);
            this.lblCombatOutput.TabIndex = 27;
            this.lblCombatOutput.Text = " ";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(876, 587);
            this.ControlBox = false;
            this.Controls.Add(this.panCombat);
            this.Controls.Add(this.pbMap);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.Text = "Window";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pbMap)).EndInit();
            this.panCombat.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbDHealth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPHealth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDefender)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPlayer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbMap;
        private System.Windows.Forms.Panel panCombat;
        private System.Windows.Forms.PictureBox pbDHealth;
        private System.Windows.Forms.PictureBox pbPHealth;
        private System.Windows.Forms.PictureBox pbDefender;
        private System.Windows.Forms.PictureBox pbPlayer;
        private System.Windows.Forms.Label btnFLEE;
        private System.Windows.Forms.Label btnITEM;
        private System.Windows.Forms.Label btnATK;
        private System.Windows.Forms.RichTextBox lblCombatOutput;
        private System.Windows.Forms.Timer wait;
    }
}