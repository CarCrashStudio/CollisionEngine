using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RPG_Engine;

namespace CsharpRPG
{
    public partial class Journal : Form
    {
        Character player;
        public Journal()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void dgvQuests_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }
    }
}
