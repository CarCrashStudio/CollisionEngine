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
        string projectName = "";
        string projectTemplate = "";

        public MainMenu()
        {
            InitializeComponent();
        }

        void NewProject()
        {
            NewProjectWindow npw = new NewProjectWindow();
            npw.ShowDialog();

            if (npw.txtProjName.Text != "")
            {
                projectName = npw.txtProjName.Text;
                projectTemplate = npw.cmbTemplates.Items[npw.cmbTemplates.SelectedIndex].ToString();
            }
        }

        void LoadProject (string name)
        {

            reader = new StreamReader(File.OpenRead(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/LinkEngine/Projects/" + name + "/" + name + ".proj"));
            
            while (!reader.EndOfStream)
            {
                projectName = reader.ReadLine();
                projectTemplate = reader.ReadLine();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            NewProject();
            GUI gui = new LinkEngine.GUI(projectName, projectTemplate);
            gui.Show();
            Hide();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadProject();
            GUI gui = new LinkEngine.GUI(projectName, projectTemplate);
            gui.Show();
            Hide();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
