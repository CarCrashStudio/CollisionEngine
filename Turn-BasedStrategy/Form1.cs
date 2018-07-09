using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinkEngine.Strategy.TurnBased
{
    public partial class Form1 : Form
    {
        public List<Player> Players { get; set; }
        public World World;

        public Form1()
        {
            InitializeComponent();

            World = new World(100);
        }
    }
}
