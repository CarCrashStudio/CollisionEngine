using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditorApplication.Controls
{
    public partial class Editor : UserControl
    {
        public uint CellWidth { get; set; }
        public uint CellHeight { get; set; }
        public Editor()
        {
            InitializeComponent();

            dvDesigner.CellHeight = CellHeight;
            dvDesigner.CellWidth = CellWidth;
        }


    }
}
