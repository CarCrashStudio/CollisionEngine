namespace CsharpRPG
{
    partial class LoginForm
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
            this.btnClose = new System.Windows.Forms.Button();
            this.wbUpdates = new System.Windows.Forms.WebBrowser();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSite = new System.Windows.Forms.Button();
            this.pnlLogin = new System.Windows.Forms.Panel();
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlPlayStyle = new System.Windows.Forms.Panel();
            this.btnLocal = new System.Windows.Forms.Button();
            this.btnOnline = new System.Windows.Forms.Button();
            this.pnlLocal = new System.Windows.Forms.Panel();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.lstCharacters = new System.Windows.Forms.ListBox();
            this.pnlLogin.SuspendLayout();
            this.pnlPlayStyle.SuspendLayout();
            this.pnlLocal.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(643, 7);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(30, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // wbUpdates
            // 
            this.wbUpdates.Location = new System.Drawing.Point(6, 36);
            this.wbUpdates.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbUpdates.Name = "wbUpdates";
            this.wbUpdates.Size = new System.Drawing.Size(667, 300);
            this.wbUpdates.TabIndex = 7;
            this.wbUpdates.Url = new System.Uri("http://rogueasp.azurewebsites.net/updates.aspx", System.UriKind.Absolute);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(80, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "v 1.1.0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(2, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 24);
            this.label5.TabIndex = 8;
            this.label5.Text = "Rogue";
            // 
            // btnSite
            // 
            this.btnSite.Location = new System.Drawing.Point(471, 7);
            this.btnSite.Name = "btnSite";
            this.btnSite.Size = new System.Drawing.Size(166, 23);
            this.btnSite.TabIndex = 10;
            this.btnSite.Text = "Visit our Website!";
            this.btnSite.UseVisualStyleBackColor = true;
            this.btnSite.Click += new System.EventHandler(this.btnSite_Click);
            // 
            // pnlLogin
            // 
            this.pnlLogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLogin.Controls.Add(this.btnRegister);
            this.pnlLogin.Controls.Add(this.btnSubmit);
            this.pnlLogin.Controls.Add(this.label3);
            this.pnlLogin.Controls.Add(this.txtPass);
            this.pnlLogin.Controls.Add(this.label2);
            this.pnlLogin.Controls.Add(this.txtUser);
            this.pnlLogin.Controls.Add(this.label1);
            this.pnlLogin.Location = new System.Drawing.Point(6, 366);
            this.pnlLogin.Name = "pnlLogin";
            this.pnlLogin.Size = new System.Drawing.Size(667, 73);
            this.pnlLogin.TabIndex = 11;
            this.pnlLogin.Visible = false;
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(496, 3);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(166, 23);
            this.btnRegister.TabIndex = 12;
            this.btnRegister.Text = "No Account? Sign up here!";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(580, 37);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 11;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(324, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Password:";
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(386, 39);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(188, 20);
            this.txtPass.TabIndex = 9;
            this.txtPass.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Username:";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(81, 39);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(227, 20);
            this.txtUser.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(287, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "Login";
            // 
            // pnlPlayStyle
            // 
            this.pnlPlayStyle.Controls.Add(this.btnLocal);
            this.pnlPlayStyle.Controls.Add(this.btnOnline);
            this.pnlPlayStyle.Location = new System.Drawing.Point(6, 342);
            this.pnlPlayStyle.Name = "pnlPlayStyle";
            this.pnlPlayStyle.Size = new System.Drawing.Size(667, 100);
            this.pnlPlayStyle.TabIndex = 12;
            // 
            // btnLocal
            // 
            this.btnLocal.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLocal.Location = new System.Drawing.Point(3, 4);
            this.btnLocal.Name = "btnLocal";
            this.btnLocal.Size = new System.Drawing.Size(299, 93);
            this.btnLocal.TabIndex = 12;
            this.btnLocal.Text = "Play Locally";
            this.btnLocal.UseVisualStyleBackColor = true;
            this.btnLocal.Click += new System.EventHandler(this.btnLocal_Click);
            // 
            // btnOnline
            // 
            this.btnOnline.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOnline.Location = new System.Drawing.Point(364, 3);
            this.btnOnline.Name = "btnOnline";
            this.btnOnline.Size = new System.Drawing.Size(299, 93);
            this.btnOnline.TabIndex = 11;
            this.btnOnline.Text = "Play Online";
            this.btnOnline.UseVisualStyleBackColor = true;
            this.btnOnline.Click += new System.EventHandler(this.btnOnline_Click);
            // 
            // pnlLocal
            // 
            this.pnlLocal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLocal.Controls.Add(this.lstCharacters);
            this.pnlLocal.Controls.Add(this.btnNew);
            this.pnlLocal.Controls.Add(this.btnLoad);
            this.pnlLocal.Location = new System.Drawing.Point(230, 342);
            this.pnlLocal.Name = "pnlLocal";
            this.pnlLocal.Size = new System.Drawing.Size(227, 73);
            this.pnlLocal.TabIndex = 13;
            this.pnlLocal.Visible = false;
            // 
            // btnNew
            // 
            this.btnNew.AutoSize = true;
            this.btnNew.Location = new System.Drawing.Point(5, 3);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(88, 23);
            this.btnNew.TabIndex = 12;
            this.btnNew.Text = "New Character";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.AutoSize = true;
            this.btnLoad.Location = new System.Drawing.Point(5, 32);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(90, 23);
            this.btnLoad.TabIndex = 11;
            this.btnLoad.Text = "Load Character";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // lstCharacters
            // 
            this.lstCharacters.FormattingEnabled = true;
            this.lstCharacters.Location = new System.Drawing.Point(99, 3);
            this.lstCharacters.Name = "lstCharacters";
            this.lstCharacters.Size = new System.Drawing.Size(120, 56);
            this.lstCharacters.TabIndex = 13;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(215)))), ((int)(((byte)(215)))));
            this.ClientSize = new System.Drawing.Size(679, 451);
            this.Controls.Add(this.pnlLocal);
            this.Controls.Add(this.pnlPlayStyle);
            this.Controls.Add(this.pnlLogin);
            this.Controls.Add(this.btnSite);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.wbUpdates);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LoginForm";
            this.pnlLogin.ResumeLayout(false);
            this.pnlLogin.PerformLayout();
            this.pnlPlayStyle.ResumeLayout(false);
            this.pnlLocal.ResumeLayout(false);
            this.pnlLocal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.WebBrowser wbUpdates;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSite;
        private System.Windows.Forms.Panel pnlLogin;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Panel pnlPlayStyle;
        private System.Windows.Forms.Button btnLocal;
        private System.Windows.Forms.Button btnOnline;
        private System.Windows.Forms.Panel pnlLocal;
        private System.Windows.Forms.ListBox lstCharacters;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnLoad;
    }
}