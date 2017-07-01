﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsharpRPG.Engine;

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
            try
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
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            return false;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string arg = String.Format("Username = '{0}'", txtUser.Text);

            SQL.Open();

            if (IsValidated(arg))
            {
                Form.CheckForProfile(arg);
                SQL.Close();
                Hide();
            }
            else
            {
                MessageBox.Show("Invalid Login.");
                txtPass.Clear();
                txtUser.Clear();
            }            
        }
    }
}
