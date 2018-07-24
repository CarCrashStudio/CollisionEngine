using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinkEngine
{
    public partial class MainMenu : Form
    {
        StreamReader reader;

        GUI gui;

        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/LinkEngine";

        public MainMenu(GUI gui)
        {
            InitializeComponent();

            this.gui = gui;
            cmbRecent.Items.Add("None");
            cmbRecent.SelectedIndex = 0;

            reader = new StreamReader(File.OpenRead(path + "/temp/recents.file"));
            while (!reader.EndOfStream)
            {
                cmbRecent.Items.Add(reader.ReadLine());
            }
            reader.Close();
        }

        void NewProject()
        {
            gui.menuAction = "new";
            
        }

        void LoadProject (string name)
        {
            gui.menuAction = "load";
            gui.projectName = name;
            
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            NewProject();
            gui.Show();
            Close();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (cmbRecent.SelectedIndex == 0)
            {
                openFileDialog1.ShowDialog();
                if (openFileDialog1.FileName != null)
                {
                    LoadProject(openFileDialog1.FileName);
                }
            }
            else
            {
                LoadProject(cmbRecent.SelectedItem.ToString());
            }
            gui.Show();
            Close();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
