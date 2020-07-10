using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditorApplication
{
    public partial class Form1 : Form
    {
        const uint cellSize = 32;
        private bool ViewStatusBar 
        {
            get { return Properties.Settings.Default.ViewStatusBar; }
            set 
            {
                Properties.Settings.Default.ViewStatusBar = value;
                Properties.Settings.Default.Save();
            }
        }
        private string Namespace { get; set; }
        public Form1()
        {
            InitializeComponent();

            toolViewStatusBar.Checked = ViewStatusBar;

            editor1.CellHeight = cellSize;
            editor1.CellWidth = cellSize;

            editor2.CellHeight = cellSize;
            editor2.CellWidth = cellSize;
        }
        private void toolViewStatusBar_CheckedChanged(object sender, EventArgs e)
        {
            ViewStatusBar = toolViewStatusBar.Checked;
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
