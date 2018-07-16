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
    public partial class NewFileForm : Form
    {
        public NewFileForm()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != null)
            {
                txtLocation.Text = saveFileDialog1.FileName;
            }
        }
    }
}
