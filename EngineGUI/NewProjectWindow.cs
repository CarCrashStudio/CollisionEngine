using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinkEngine
{
    public partial class NewProjectWindow : Form
    {
        public NewProjectWindow()
        {
            InitializeComponent();
            txtFile.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\LinkEngine\\Projects";
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Hide();
        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = txtFile.Text;
            folderBrowserDialog1.ShowDialog();
        }
    }
}
