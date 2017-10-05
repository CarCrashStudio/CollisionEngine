using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsharpRPG.Engine;
using System.Diagnostics;
using Microsoft.VisualBasic;

namespace CsharpRPG
{
    public partial class LoginForm : Form
    {
        Sql SQL;
        MainForm Form;
        public LoginForm(Sql sql, MainForm form)
        {
            InitializeComponent();
            SQL = sql;
            Form = form;
        }

        bool IsValidated(string arg)
        {
            object[,] obj = SQL.ExecuteSELECTWHERE("Password", arg, "UserData");
            string pass = obj[0, 0].ToString();
            string user = txtUser.Text;

            if (obj.Length != 0)
            {
                if (pass == txtPass.Text)
                {
                    return true;
                }
                else { return false; }
            }
            else { return false; }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string arg = String.Format("Username = '{0}'", txtUser.Text);

            SQL.Open();

            if (IsValidated(arg))
            {
                SQL.Close();
                Form.Online = new Online(SQL, Form.world, Form, arg, "CharacterData", "CharacterInventory", "CharacterEquipment", "CharacterSkills", "CharacterQuests");
                Hide();
            }
            else
            {
                MessageBox.Show("Invalid Login.");
                txtPass.Clear();
                txtUser.Clear();
            }
        }

        private void btnSite_Click(object sender, EventArgs e)
        {
            Process.Start("http://rogueasp.azurewebsites.net/");
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Process.Start("http://rogueasp.azurewebsites.net/register.aspx");
        }

        private void btnOnline_Click(object sender, EventArgs e)
        {
            pnlPlayStyle.Visible = false;
            pnlLogin.Visible = true;
        }

        private void btnLocal_Click(object sender, EventArgs e)
        {
            pnlLocal.Visible = true;
            pnlPlayStyle.Visible = false;
            try
            {
                System.IO.StreamReader reader = System.IO.File.OpenText(Application.StartupPath + "\\Saves\\characters.txt");
                while (!reader.EndOfStream)
                {
                    lstCharacters.Items.Add(reader.ReadLine());
                }
                reader.Close();
            }
            catch
            {
                lstCharacters.Items.Add("No saved characters");
            }
            
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (System.IO.Directory.Exists(Application.StartupPath + "\\Saves\\"))
            {
                Form.Local = new Local("nofile", Form.world, Form);
                System.IO.StreamWriter writer = System.IO.File.AppendText(Application.StartupPath + "\\Saves\\characters.txt");
                writer.WriteLine(Form.world.player.Name);
                writer.Close();
                Hide();
            }
            else
            {
                System.IO.Directory.CreateDirectory(Application.StartupPath + "\\Saves\\");
                Form.Local = new Local("nofile", Form.world, Form);
                System.IO.StreamWriter writer = System.IO.File.AppendText(Application.StartupPath + "\\Saves\\characters.txt");
                writer.WriteLine(Form.world.player.Name);
                writer.Close();
                Hide();
            }


        }
        private static DialogResult ShowInputDialog(ref string input)
        {
            System.Drawing.Size size = new System.Drawing.Size(200, 70);
            Form inputBox = new Form();

            inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;
            inputBox.Text = "Name";

            System.Windows.Forms.TextBox textBox = new TextBox();
            textBox.Size = new System.Drawing.Size(size.Width - 10, 23);
            textBox.Location = new System.Drawing.Point(5, 5);
            textBox.Text = input;
            inputBox.Controls.Add(textBox);

            Button okButton = new Button();
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(75, 23);
            okButton.Text = "&OK";
            okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 39);
            inputBox.Controls.Add(okButton);

            Button cancelButton = new Button();
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(75, 23);
            cancelButton.Text = "&Cancel";
            cancelButton.Location = new System.Drawing.Point(size.Width - 80, 39);
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            DialogResult result = inputBox.ShowDialog();
            input = textBox.Text;
            return result;
        } // SAVE FOR LATER

        private void btnLoad_Click(object sender, EventArgs e)
        {
            string name = lstCharacters.SelectedItem.ToString();

            if(name != "No saved characters")
            {
                Form.Local = new Local(Application.StartupPath + "\\Saves\\" + name + ".txt", Form.world, Form);
                Hide();
            }
        }
    }
}
