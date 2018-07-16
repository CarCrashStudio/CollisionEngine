using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;

namespace LinkEngine
{
    public partial class GUI : Form
    {
        StreamWriter writer;
        StreamReader reader;

        public string projectName = "NewProject";
        public string projectTemplate = "None";
        string path;

        public GUI(string name, string template)
        {
            InitializeComponent();

            projectName = name;
            projectTemplate = template;

            PopulateComponents();
            LoadProject();
            LoadAssests();
            PopulateComponents();
            PopulateTemplateComponents(projectTemplate);
        }

        void LoadProject ()
        {
            path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/LinkEngine";

            Directory.CreateDirectory(path + "/Projects/" + projectName);
            fsWatcher.Path = path;

            LoadAssests();
            ListDirectory(treFiles, path + "/Projects/" + projectName);

        }
        void LoadAssests ()
        {
            Directory.CreateDirectory(path + "/Projects/" + projectName + "/Assets");
        }
        void PopulateTemplateComponents(string template)
        {
            if (projectTemplate != "None")
            {
                treComponents.Nodes.Add(template, template);
                int index = treComponents.Nodes.IndexOf(treComponents.Nodes[template]);

                var dll = Assembly.LoadFile(path + "/Libraries/LinkEngine." + template + ".dll");
                foreach (Type type in dll.GetExportedTypes())
                {
                    if (type.Namespace == ("LinkEngine." + template) && !type.Name.Contains("<>"))
                    {
                        treComponents.Nodes[index].Nodes.Add(type.Name);
                    }
                }
            }
        }

        void NewProject ()
        {
            NewProjectWindow npw = new NewProjectWindow();
            npw.ShowDialog();

            if (npw.txtProjName.Text != "")
            {
                projectName = npw.txtProjName.Text;
                projectTemplate = npw.cmbTemplates.Items[npw.cmbTemplates.SelectedIndex].ToString();
                LoadProject();
                LoadAssests();
                PopulateComponents();
                PopulateTemplateComponents(projectTemplate);
            }
        }

        void OpenFile (string file)
        {
            reader = new StreamReader(File.OpenRead(file));
            rtbEditor.Clear();
            while (!reader.EndOfStream)
            {
                rtbEditor.Text += reader.ReadLine() + "\n";
            }

            reader.Close();
        }
        void SaveFile (string file)
        {
            File.Delete(file);
            writer = new StreamWriter(File.OpenWrite(file));
            foreach (string str in rtbEditor.Lines)
            {
                writer.WriteLine(str);
            }
            writer.Close();
        }

        int Compile ()
        {
            bool DoneBuilding = false;
            while (!DoneBuilding)
            {
                // execute code
                var compiler = new CSharpCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", "v1.0" } });
                var parameters = new CompilerParameters(new[] { "mscorlib.dll", "System.Core.dll" }, "projectname.exe", true);
                parameters.GenerateExecutable = true;

                CompilerResults results = compiler.CompileAssemblyFromFile(parameters, new[] { "" });

                // increase the progress bar
                prgBar.PerformStep();
                if (prgBar.Value == prgBar.Maximum)
                {
                    DoneBuilding = true;
                }
            }
            // code compiled with no errors, return 0 to let the code continue as normal
            return 0;
        }
        void ListDirectory(TreeView treeView, string path)
        {
            treeView.Nodes.Clear();

            var stack = new Stack<TreeNode>();
            var rootDirectory = new DirectoryInfo(path);
            var node = new TreeNode(rootDirectory.Name) { Tag = rootDirectory };
            stack.Push(node);

            while (stack.Count > 0)
            {
                var currentNode = stack.Pop();
                var directoryInfo = (DirectoryInfo)currentNode.Tag;

                foreach (var directory in directoryInfo.GetDirectories())
                {
                    var childDirectoryNode = new TreeNode(directory.Name) { Tag = directory };
                    currentNode.Nodes.Add(childDirectoryNode);
                    stack.Push(childDirectoryNode);
                }
                foreach (var file in directoryInfo.GetFiles())
                    currentNode.Nodes.Add(new TreeNode(file.Name));

            }

            treeView.Nodes.Add(node);
        }
        void ListComponents (TreeView treeView, int nodeIndex, string nameSpace)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type[] ary = assembly.GetTypes();
            foreach (Type type in ary)
            {
                if (type.Namespace == nameSpace && !type.Name.Contains("<>"))
                {
                    treeView.Nodes[nodeIndex].Nodes.Add(type.Name);
                }
            }
        }
        void PopulateComponents ()
        {
            treComponents.Nodes.Clear();

            treComponents.Nodes.Add("Components", "Components");
            int index = treComponents.Nodes.IndexOf(treComponents.Nodes["Components"]);
            ListComponents(treComponents, index, "LinkEngine.Components");

            treComponents.Nodes.Add("Entities","Entities");
            index = treComponents.Nodes.IndexOf(treComponents.Nodes["Entities"]);
            ListComponents(treComponents, index, "LinkEngine.Entities");

            treComponents.Nodes.Add("Rendering", "Rendering");
            index = treComponents.Nodes.IndexOf(treComponents.Nodes["Rendering"]);
            ListComponents(treComponents, index, "LinkEngine.Rendering");

            treComponents.Nodes.Add("WorldGen", "WorldGen");
            index = treComponents.Nodes.IndexOf(treComponents.Nodes["WorldGen"]);
            ListComponents(treComponents, index, "LinkEngine.WorldGen");
        }

        #region EventHandlers
        #region ToolStripMenuItems
        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // change status message to Building...
            lblStatus.Text = "Building...";

            // Start compiling code
            // Start progress bar
            if (Compile() == 0)
            {
                lblStatus.Text = "Done";
            }
        }
        private void fileToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }
        private void newItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sfdSave = new SaveFileDialog();
            sfdSave.InitialDirectory = path + "/Projects/" + projectName + "/";
            sfdSave.ShowDialog();

            if (sfdSave.FileName.Contains(".cs"))
            {
                if (projectTemplate != "None")
                {
                    rtbEditor.Text = "using LinkEngine." + projectTemplate + ";\nusing LinkEngine;\nnamespace " + projectName + "\n{ \n\tclass Class1\n\t{\n\t}\n}";
                }
                else
                {
                    rtbEditor.Text = "using LinkEngine;\nnamespace " + projectName + "\n{ \n\tclass Class1\n\t{\n\t}\n}";
                }
                SaveFile(sfdSave.FileName);
                rtbEditor.Clear();
            }
            else
            {
                File.Create(sfdSave.FileName);
            }
            ListDirectory(treFiles, fsWatcher.Path + "/Projects/" + projectName);
            OpenFile(sfdSave.FileName);
        }
        private void newFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treFiles.SelectedNode.Index != -1)
            {
                if (treFiles.SelectedNode.Text != projectName)
                {
                    Directory.CreateDirectory(path + "/" + treFiles.SelectedNode.Text + "/NewDirectory");
                }
                else
                {
                    File.Create(path + "/NewDirectory");
                }
            }
        }
        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewProject();
        }
        private void cScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void saveAsToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }
        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveFile(path + "/Projects/" + projectName + "/Assets/class.cs");
        }
        #endregion
        #region treFiles
        private void treFiles_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }
        private void treFiles_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            string parent = "";
            string file = "";
            TreeNode node = e.Node;

            file = node.Text;
            if (file.Contains("."))
            {
                do
                {
                    parent = node.Parent.Text;
                    node = node.Parent;
                    file = file.Insert(0, parent + "/");
                } while (node.Parent != null);

                OpenFile(path + "/Projects/" + file);
            }
        }
        #endregion
        #region treComponents
        private void treComponents_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {

        }
        #endregion
        private void fsWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            ListDirectory(treFiles, fsWatcher.Path + "/Projects/" + projectName);
        }
        #endregion
    }
}
