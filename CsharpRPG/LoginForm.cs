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
            string pass = obj[0,0].ToString();
            string user = txtUser.Text;

            if(obj.Length != 0)
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

        }
    }
}
