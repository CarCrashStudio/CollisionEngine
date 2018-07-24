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
        public string menuAction = "";

        bool MouseDragging = false;
        int deltaX = 0;
        int deltaY = 0;

        StreamWriter writer;
        StreamReader reader;

        public string projectName = "";
        public List<string> projectLibraries;
        List<string> CompiledFiles;

        CSharpCodeProvider compiler;
        CompilerParameters parameters;
        CompilerResults results;

        string EnginePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\LinkEngine";
        string path = "";

        public GUI()
        {
            InitializeComponent();
            MainMenu mm = new MainMenu(this);
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
            mm.Close();
        }

        //
        #region ProjectAssets
        void LoadProjectFolder ()
        {
            Directory.CreateDirectory(path + "\\" + projectName);
            fsWatcher.Path = EnginePath;

            LoadAssests();
            ListDirectory(treFiles, path + "\\" + projectName);
        }
        void LoadAssests ()
        {
            Directory.CreateDirectory(path + "\\" + projectName + "\\Assets");
        }
        void PopulateTemplateComponents()
        {
            if (projectLibraries.Count != 0)
            {
                foreach(string library in projectLibraries)
                {
                    if (library.Contains("LinkEngine"))
                    {
                        var dll = Assembly.LoadFile(library);

                        treComponents.Nodes.Add(library, dll.GetName().Name);
                        int index = treComponents.Nodes.IndexOf(treComponents.Nodes[library]);

                        foreach (Type type in dll.GetExportedTypes())
                        {
                            if (!type.Name.Contains("<>"))
                            {
                                treComponents.Nodes[index].Nodes.Add(type.Name);
                            }
                        }
                    }
                }
            }
        }
        void PopulateComponents()
        {
            treComponents.Nodes.Clear();

            treComponents.Nodes.Add("Components", "Components");
            int index = treComponents.Nodes.IndexOf(treComponents.Nodes["Components"]);
            ListComponents(treComponents, index, "LinkEngine.Components");

            treComponents.Nodes.Add("Entities", "Entities");
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
        #endregion

        //
        #region ProjectBuilders
        int Compile()
        {
            // execute code
            compiler = new CSharpCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", "v3.5" } });
            prgBar.PerformStep();

            if (!Directory.Exists(EnginePath + "\\Projects\\" + projectName + "\\bin\\"))
                Directory.CreateDirectory(EnginePath + "\\Projects\\" + projectName + "\\bin\\");
            prgBar.PerformStep();

            parameters = new CompilerParameters(projectLibraries.ToArray(), EnginePath + "\\Projects\\" + projectName + "\\bin\\" + projectName + ".exe", true);
            prgBar.PerformStep();
            parameters.GenerateExecutable = true;
            prgBar.PerformStep();

            results = compiler.CompileAssemblyFromSource(parameters, CompiledFiles.ToArray());
            prgBar.PerformStep();

            if (results.Errors.Count > 0)
                return 1;

            // code compiled with no errors, return 0 to let the code continue as normal
            return 0;
        }
        #endregion

        //
        #region ProjectFileHandlers
        void PrintProjectFile()
        {
            File.Delete(EnginePath + "\\Projects\\" + projectName + "\\" + projectName + ".proj");
            writer = new StreamWriter(File.OpenWrite(EnginePath + "\\Projects\\" + projectName + "\\" + projectName + ".proj"));
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
        void LoadProjectFile(string projFile)
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
                        if (str != "-ENDCOMPILED-")
                            CompiledFiles.Add(str);
                    } while (str != "-ENDCOMPILED-");
                }
            }
            reader.Close();
        }
        #endregion

        //
        #region ProjectHandlers
        void NewProject ()
        {
            projectLibraries = new List<string>();
            CompiledFiles = new List<string>();

            NewProjectWindow npw = new NewProjectWindow();
            npw.ShowDialog();

            if (npw.txtProjName.Text != "")
            {
                projectName = npw.txtProjName.Text;

                path = npw.txtFile.Text;

                projectLibraries.Add("mscorlib.dll");
                projectLibraries.Add("System.Core.dll");

                foreach (object obj in npw.lstLibraries.CheckedItems)
                {
                    projectLibraries.Add(EnginePath + "\\Libraries\\LinkEngine."+ obj.ToString() + ".dll");
                }
                // generate a new world file
                writer = new StreamWriter(File.OpenWrite(path + "\\" + projectName + "\\Assets\\World.cs"));
                foreach (string str in projectLibraries)
                {
                    writer.Write("using LinkEngine." + str + "; \n");
                }
                writer.Write("using LinkEngine;\nnamespace " + projectName + "\n{ \n\tclass World\n\t{\n\t}\n}");
                writer.Close();

                CompiledFiles.Add(path + "\\" + projectName + "\\Assets\\World.cs");

                PrintProjectFile();

                bool found = false;
                reader = new StreamReader(File.OpenRead(EnginePath + "\\temp\\recents.file"));
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
                    writer = new StreamWriter(File.OpenWrite(EnginePath + "\\temp\\recents.file"));
                    writer.WriteLine(projectName);
                    writer.Close();
                }

                LoadProjectFolder();
                LoadAssests();
                PopulateComponents();
                npw.Close();
            }
        }
        void LoadProject(string name)
        {
            CompiledFiles = new List<string>();
            projectLibraries = new List<string>();

            LoadProjectFile(name);

            bool found = false;
            reader = new StreamReader(File.OpenRead(EnginePath + "\\temp\\recents.file"));
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
                writer = new StreamWriter(File.OpenWrite(EnginePath + "\\temp\\recents.file"));
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
        #endregion

        //
        #region FileHandlers
        void NewFile ()
        {
            NewFileForm nff = new NewFileForm();
            nff.ShowDialog();

            if (nff.txtLocation.Text != "")
            {
                if (nff.txtLocation.Text.Contains(".cs"))
                {
                    foreach (string str in projectLibraries)
                    {
                        rtbEditor.Text += "using " + str + "; \n";
                    }

                    rtbEditor.Text = "using LinkEngine;\nnamespace " + projectName + "\n{ \n\tclass Class1\n\t{\n\t}\n}";
                    SaveFile(nff.txtLocation.Text);
                    rtbEditor.Clear();
                }
                else
                {
                    File.Create(nff.txtLocation.Text);
                }

                CompiledFiles.Add(nff.txtLocation.Text);

                PrintProjectFile();

                ListDirectory(treFiles, fsWatcher.Path + "\\" + projectName);
                OpenFile(nff.txtLocation.Text);
            }
            
            nff.Close();
        }
        void OpenFile (string file)
        {
            rtbEditor.Clear();
            reader = new StreamReader(File.OpenRead(file));

            //tabMain.TabPages.Add(EditorPage);
            //tabMain.TabPages[tabMain.TabCount - 1].Text = "Editor";
            while (!reader.EndOfStream)
            {
                //tabMain.TabPages[tabMain.TabCount - 1].Controls[0].Text += reader.ReadLine() + "\n";
                rtbEditor.Text += reader.ReadLine() + "\n";
            }
            //tabMain.TabPages[tabMain.TabCount - 1].Controls[0].Visible = true;
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
        #endregion

        //
        #region TreeNodeFillers
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
        #endregion

        //
        #region PropertyInfo
        object GetPropInfo(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src);
        }
        void ShowSelectedObjectProperties ()
        {

        }
        #endregion

        //
        #region EventHandlers
        #region ToolStripMenuItems
        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // change status message to Building...
            lblStatus.Text = "Building...";

            // Start compiling code
            // Start progress bar
            int status = Compile();
            if (status == 0)
            {
                lblStatus.Text = "Done";
            }
            else if (status == 1)
            {
                lblStatus.Text = "Error Compiling." + results.Errors[0].ToString();
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
                    Directory.CreateDirectory(EnginePath + "\\" + treFiles.SelectedNode.Text + "\\NewDirectory");
                }
                else
                {
                    File.Create(EnginePath + "\\NewDirectory");
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
            SaveFile(EnginePath + "\\Projects\\" + projectName + "\\Assets\\class.cs");
        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void newGameObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {

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
                    file = file.Insert(0, parent + "\\");
                } while (node.Parent != null);

                OpenFile(EnginePath + "\\Projects\\" + file);
            }
        }
        void treFiles_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode newSelected = e.Node;
            listView1.Items.Clear();
            DirectoryInfo nodeDirInfo = (DirectoryInfo)newSelected.Tag;
            ListViewItem.ListViewSubItem[] subItems;
            ListViewItem item = null;

            foreach (DirectoryInfo dir in nodeDirInfo.GetDirectories())
            {
                item = new ListViewItem(dir.Name, 0);
                subItems = new ListViewItem.ListViewSubItem[]
                          {new ListViewItem.ListViewSubItem(item, "Directory"),
                   new ListViewItem.ListViewSubItem(item,
                dir.LastAccessTime.ToShortDateString())};
                item.SubItems.AddRange(subItems);
                listView1.Items.Add(item);
            }
            foreach (FileInfo file in nodeDirInfo.GetFiles())
            {
                item = new ListViewItem(file.Name, 1);
                subItems = new ListViewItem.ListViewSubItem[]
                          { new ListViewItem.ListViewSubItem(item, "File"),
                   new ListViewItem.ListViewSubItem(item,
                file.LastAccessTime.ToShortDateString()),
                              new ListViewItem.ListViewSubItem(item, file.FullName),
                          new ListViewItem.ListViewSubItem(item, file.Extension)};

                item.SubItems.AddRange(subItems);
                listView1.Items.Add(item);
            }
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
        #endregion
        #region treComponents
        private void treComponents_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {

        }
        #endregion
        #region treScene
        private void treScene_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {

        }
        #endregion
        private void fsWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            ListDirectory(treFiles, fsWatcher.Path + "\\Projects\\" + projectName);
        }
        private void GUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }
        #region GameViewHandlers
        private void pbScreen_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                MouseDragging = true;
                deltaX = Cursor.Position.X - e.X;
                deltaY = Cursor.Position.Y - e.Y;
            }
            else if (e.Button == MouseButtons.Left)
            {

            }
        }
        private void pbScreen_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && MouseDragging)
            {

            }
            if (e.Button == MouseButtons.Right && MouseDragging)
            {
                pbScreen.Location = new System.Drawing.Point(Cursor.Position.X, Cursor.Position.Y);
            }
        }
        private void pbScreen_MouseUp(object sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Right || e.Button == MouseButtons.Left) && MouseDragging)
            {
                MouseDragging = false;
            }
        }
        #endregion
        private void ddlListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion
    }
}