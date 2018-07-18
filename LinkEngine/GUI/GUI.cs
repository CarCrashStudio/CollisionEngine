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
        TabPage EditorPage;
        RichTextBox Editor;

        public string menuAction = "";

        bool MouseDragging = false;
        int deltaX = 0;
        int deltaY = 0;

        StreamWriter writer;
        StreamReader reader;

        CSharpCodeProvider compiler;
        CompilerParameters parameters;
        List<string> CompiledFiles;

        public string projectName = "NewProject";
        public List<string> projectLibraries;
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/LinkEngine";

        public GUI()
        {
            InitializeComponent();
            EditorPage = tabMain.TabPages[1];
            Editor = rtbEditor;
            EditorPage.Controls.Add(Editor);

            MainMenu mm = new LinkEngine.MainMenu(this);
            mm.ShowDialog();
            Hide();

            if (menuAction == "new")
            {
                NewProject();
            }
            if (menuAction == "load")
            {
                LoadProject(projectName);
            }

            PopulateComponents();
            LoadProjectFolder();
            LoadAssests();
        }

        // Game View window functions
        private void pbScreen_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                MouseDragging = true;
                deltaX = Cursor.Position.X - e.X;
                deltaY = Cursor.Position.Y - e.Y;
            }
        }
        private void pbScreen_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right && MouseDragging)
            {
                pbScreen.Location = new System.Drawing.Point(Cursor.Position.X, Cursor.Position.Y);
            }
        }
        private void pbScreen_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && MouseDragging)
            {
                MouseDragging = false;
            }
        }

        // Misc functions
        void LoadProjectFolder ()
        {
            CompiledFiles = new List<string>();
            Directory.CreateDirectory(path + "/Projects/" + projectName);
            fsWatcher.Path = path;

            LoadAssests();
            ListDirectory(treFiles, path + "/Projects/" + projectName);
        }
        void LoadAssests ()
        {
            Directory.CreateDirectory(path + "/Projects/" + projectName + "/Assets");
        }
        void PopulateTemplateComponents()
        {
            if (projectLibraries.Count != 0)
            {
                foreach(string library in projectLibraries)
                {
                    treComponents.Nodes.Add(library, library);
                    int index = treComponents.Nodes.IndexOf(treComponents.Nodes[library]);

                    var dll = Assembly.LoadFile(path + "/Libraries/LinkEngine." + library + ".dll");
                    foreach (Type type in dll.GetExportedTypes())
                    {
                        if (type.Namespace == ("LinkEngine." + library) && !type.Name.Contains("<>"))
                        {
                            treComponents.Nodes[index].Nodes.Add(type.Name);
                        }
                    }
                }
            }
        }

        void NewProject ()
        {
            projectLibraries = new List<string>();
            CompiledFiles = new List<string>();

            NewProjectWindow npw = new NewProjectWindow();
            npw.ShowDialog();

            if (npw.txtProjName.Text != "")
            {
                projectName = npw.txtProjName.Text;
                foreach (object obj in npw.lstLibraries.CheckedItems)
                {
                    projectLibraries.Add(obj.ToString());
                }

                // generate a new world file
                writer = new StreamWriter(File.OpenWrite(path + "/Projects/" + projectName + "/Assets/World.cs"));
                foreach (string str in projectLibraries)
                {
                    writer.Write("using LinkEngine." + str + "; \n");
                }
                writer.Write("using LinkEngine;\nnamespace " + projectName + "\n{ \n\tclass World\n\t{\n\t}\n}");
                writer.Close();

                CompiledFiles.Add(path + "/Projects/" + projectName + "/World.cs");

                PrintProjectFile();

                bool found = false;
                reader = new StreamReader(File.OpenRead(path + "/temp/recents.file"));
                while (!reader.EndOfStream)
                {
                    if (reader.ReadLine() == projectName)
                    {
                        // if this project already exists in recents
                        found = true;
                    }
                }
                reader.Close();
                if (!found)
                {
                    writer = new StreamWriter(File.OpenWrite(path + "/temp/recents.file"));
                    writer.WriteLine(projectName);
                    writer.Close();
                }

                LoadProjectFolder();
                LoadAssests();
                PopulateComponents();
            }
        }
        void LoadProject(string name)
        {
            CompiledFiles = new List<string>();
            projectLibraries = new List<string>();

            LoadProjectFile(name);

            bool found = false;
            reader = new StreamReader(File.OpenRead(path + "/temp/recents.file"));
            while (!reader.EndOfStream)
            {
                if (reader.ReadLine() == projectName)
                {
                    // if this project already exists in recents
                    found = true;
                }
            }
            reader.Close();
            if (!found)
            {
                writer = new StreamWriter(File.OpenWrite(path + "/temp/recents.file"));
                writer.WriteLine(projectName);
                writer.Close();
            }
        }
        void CloseProject ()
        {
            treFiles.Nodes.Clear();
            projectName = "";
            PopulateComponents();
        }

        void NewFile ()
        {
            sfdSave = new SaveFileDialog();
            sfdSave.InitialDirectory = path + "/Projects/" + projectName + "/";
            sfdSave.ShowDialog();

            if (sfdSave.FileName.Contains(".cs"))
            {
                foreach (string str in projectLibraries)
                {
                    rtbEditor.Text += "using LinkEngine." + str + "; \n";
                }

                rtbEditor.Text = "using LinkEngine;\nnamespace " + projectName + "\n{ \n\tclass Class1\n\t{\n\t}\n}";
                SaveFile(sfdSave.FileName);
                rtbEditor.Clear();
            }
            else
            {
                File.Create(sfdSave.FileName);
            }

            CompiledFiles.Add(sfdSave.FileName);

            PrintProjectFile();

            ListDirectory(treFiles, fsWatcher.Path + "/Projects/" + projectName);
            OpenFile(sfdSave.FileName);
        }
        void OpenFile (string file)
        {
            rtbEditor.Clear();
            reader = new StreamReader(File.OpenRead(file));

            tabMain.TabPages.Add(EditorPage);
            tabMain.TabPages[tabMain.TabCount - 1].Text = "Editor";
            while (!reader.EndOfStream)
            {
                tabMain.TabPages[tabMain.TabCount - 1].Controls[0].Text += reader.ReadLine() + "\n";
            }
            tabMain.TabPages[tabMain.TabCount - 1].Controls[0].Visible = true;
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

        void PrintProjectFile()
        {
            File.Delete(path + "/Projects/" + projectName + "/" + projectName + ".proj");
            writer = new StreamWriter(File.OpenWrite(path + "/Projects/" + projectName + "/" + projectName + ".proj"));
            writer.WriteLine(projectName);
            writer.WriteLine("-LIBRARIES-");
            foreach (string str in projectLibraries)
            {
                writer.WriteLine(str);
            }
            writer.WriteLine("-ENDLIBRARIES-");
            writer.WriteLine("-COMPILEDFILES-");
            foreach (string str in CompiledFiles)
            {
                writer.WriteLine(str);
            }
            writer.WriteLine("-ENDCOMPILED-");
            writer.WriteLine("-OBJECTS-");
            writer.WriteLine("-ENDOBJECTS-");
            writer.Close();
        }
        void LoadProjectFile (string projFile)
        {
            reader = new StreamReader(File.OpenRead(projFile));
            int i = 0;
            while (!reader.EndOfStream)
            {
                if (i == 0)
                {
                    projectName = reader.ReadLine();
                    i++;
                }

                string str = reader.ReadLine();
                if (str == "-LIBRARIES-")
                {
                    do
                    {
                        str = reader.ReadLine();
                        if (str != "-ENDLIBRARIES-")
                            projectLibraries.Add(str);
                    } while (str != "-ENDLIBRARIES-");
                }
                if (str == "-COMPILEDFILES-")
                {
                    do
                    {
                        str = reader.ReadLine();
                        if (str != "-ENDCOMPILED")
                            CompiledFiles.Add(str);
                    } while (str != "-ENDCOMPILED-");
                }
            }
            reader.Close();
        }

        int Compile ()
        {
            bool DoneBuilding = false;
            while (!DoneBuilding)
            {
                // execute code
                compiler = new CSharpCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", "v3.5" } });
                string[] libs = new[] { "mscorlib.dll", "System.Core.dll" };
                parameters = new CompilerParameters(libs, path + "/Projects/" + projectName + "/bin/" +  projectName + ".exe", true);
                parameters.GenerateExecutable = true;

                CompilerResults results = compiler.CompileAssemblyFromFile(parameters, CompiledFiles.ToArray());

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

            // check for template components
            PopulateTemplateComponents();
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
            NewFile();
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
        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ofdOpen.ShowDialog();
            if (ofdOpen.FileName != null)
            {
                LoadProject(ofdOpen.FileName);
                LoadProjectFolder();
            }
            else
            {
                MessageBox.Show("File not found!");
            }
        }
        private void closeProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseProject();
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
            // when a node is double clicked, an object of the class should be created and added to the 
            // the project's designer class
            treScene.Nodes["World"].Nodes.Add((TreeNode)e.Node.Clone());
        }
        #endregion
        private void fsWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            ListDirectory(treFiles, fsWatcher.Path + "/Projects/" + projectName);
        }
        private void GUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        #endregion
    }
}