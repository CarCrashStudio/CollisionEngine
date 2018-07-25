/*  Project Name: LinkEngine Editor
    Project Author: Trey Hall
    Project Start Date: 7/16/18
    About the project :
    The LinkEngine Editor should provide better usability of various LinkEngine Libraries, 
    aswell as provide an interface in which all game objects can be seen and edited before runtime.
    The Editor should serve as a compiler for the project and not require the user to use another service to build all the source code
    The Editor should not impact the Libraries use on its own. 
    The LinkEngine Libraries should be usable outside as well as inside the Editor
*/

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;

namespace LinkEngine
{
    public partial class MainForm : Form
    {
        // All variables used by the UI
        #region Variables
        public string menuAction = "";

        StreamWriter writer;
        StreamReader reader;

        public string projectName = "";
        public List<string> projectLibraries;
        List<string> CompiledFiles;

        CSharpCodeProvider compiler;
        CompilerParameters parameters;
        CompilerResults results;

        string EnginePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\LinkEngine";
        string ProjectPath = "";
        #endregion
        public MainForm()
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
                string[] ary = projectName.Split('\\');
                ary[ary.Length - 1] = "";
                foreach (string sr in ary)
                {
                    if (sr == ary[ary.Length - 2])
                        ProjectPath += sr;
                    else
                        if (sr != ary[ary.Length - 1])
                            ProjectPath += sr + "\\";
                }
                LoadProject(projectName);
                
            }
            mm.Close();
            timCompiler.Enabled = false;
        }

        // All Functions in this region handle saving, loading and creating Projects
        #region ProjectHandlers
        void NewProject()
        {
            projectLibraries = new List<string>();
            CompiledFiles = new List<string>();

            NewProjectWindow npw = new NewProjectWindow();
            npw.ShowDialog();

            if (npw.txtProjName.Text != "")
            {
                projectName = npw.txtProjName.Text;

                ProjectPath = npw.txtFile.Text + "\\" + projectName;

                LoadProjectFolder();
                LoadAssests();

                projectLibraries.Add("mscorlib.dll");
                projectLibraries.Add("System.Core.dll");

                foreach (object obj in npw.lstLibraries.CheckedItems)
                {
                    projectLibraries.Add(EnginePath + "\\Libraries\\LinkEngine." + obj.ToString() + ".dll");
                }
                // generate a new world file
                writer = new StreamWriter(File.OpenWrite(ProjectPath + "\\Assets\\World.cs"));
                writer.Write("using LinkEngine;\nnamespace " + projectName + "\n{ \n\tclass World\n\t{\n\t\twhile(true) {\n\t\t\n\t\t}\n\t}\n}");
                writer.Close();

                CompiledFiles.Add(ProjectPath + "\\Assets\\World.cs");

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
        void CloseProject()
        {
            treFiles.Nodes.Clear();
            projectName = "";
        }

        // All Functions in this region handle saving, loading, and creating Project Files to store all the required project information
        // Project files will store all libraries being used and all files that require being compiled
        // 
        #region ProjectFileHandlers
        void PrintProjectFile()
        {
            File.Delete(ProjectPath + "\\" + projectName + ".proj");
            writer = new StreamWriter(File.OpenWrite(ProjectPath + "\\" + projectName + ".proj"));
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

        // All Functions in this region are used to load all Items in the Assets Folder
        // Project assets should be saved as respective type files
        // All information for all assets will be stored inside the file and used when adding assets to the project
        #region ProjectAssets
        void LoadProjectFolder()
        {
            Directory.CreateDirectory(ProjectPath);
            fsWatcher.Path = ProjectPath;

            LoadAssests();
            PopulateCompnents();
            ListDirectory(treFiles, ProjectPath);
        }
        void LoadAssests()
        {
            Directory.CreateDirectory(ProjectPath + "\\Assets");
        }
        void PopulateCompnents()
        {
            var dll = Assembly.LoadFile(EnginePath + "\\Libraries\\LinkEngine.dll");

            foreach(Type type in dll.GetTypes())
            {
                treComponents.Nodes.Add(type.Name);
            }
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

                    foreach (FileInfo file in directory.GetFiles())
                    {
                        var fileNode = new TreeNode(file.Name) { Tag = file };
                        childDirectoryNode.Nodes.Add(fileNode);
                    }
                }
            }

            treeView.Nodes.Add(node);
        }
        void ListComponents(TreeView treeView, int nodeIndex, string nameSpace)
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

        // All Functions in this region handle saving, loading and creating of documents to be used by the user
        #region FileHandlers
        void NewFile()
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
        void OpenFile(string file)
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
        void SaveFile(string file)
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

        // All Functions in this region are used to load all components inside any LinkEngine library
        // Libraries should be loaded and all components should be placed into a components area to be used later
        // Libraries should be one time accessed if possible, 
        // calling to load dlls that are already in the folder is an expensive call to make and reduces the usability of the UI
        // Libraries should be pulled from GitHub to enusre they are always up to date
        #region ProjectLibraries

        #endregion
        #endregion
        #endregion

        // All functions in this region are used to run the build and compile modes of the UI
        // The compiler should always be checking for errors as the user edits code
        // no executable should be built until the user explicitly chooses to build project
        #region ProjectBuilders
        int Compile()
        {
            // execute code
            compiler = new CSharpCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", "v3.5" } });

            if (!Directory.Exists(EnginePath + "\\Projects\\" + projectName + "\\bin\\"))
                Directory.CreateDirectory(EnginePath + "\\Projects\\" + projectName + "\\bin\\");

            parameters = new CompilerParameters(projectLibraries.ToArray(), EnginePath + "\\Projects\\" + projectName + "\\bin\\" + projectName + ".exe", true);
            parameters.GenerateExecutable = true;

            results = compiler.CompileAssemblyFromSource(parameters, CompiledFiles.ToArray());

            if (results.Errors.Count > 0)
                return 1;

            // code compiled with no errors, return 0 to let the code continue as normal
            return 0;
        }
        int Run ()
        {
            return 0;
        }
        #endregion

        // These functions are in charge of putting the ui together at runtime
        #region UIBuilders
        void BuildNewFileEditor (string tabName)
        {
            // this function should build a new file editor window and put it into the main tab control
            RichTextBox rtb = new RichTextBox();
            rtb = rtbEditor;
            rtb.Name = tabName;

            tabMain.TabPages.Add(new TabPage(tabName));
            tabMain.TabPages[tabMain.TabPages.Count - 1].Controls.Add(rtb);
        }
        #endregion

        // All functions in this region handle getting property info of all created objects
        #region PropertyInfo
        object GetPropInfo(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src);
        }
        void ShowSelectedObjectProperties ()
        {

        }
        #endregion

        // These are just event handlers
        // You expected something clever?
        #region EventHandlers
        #region ToolStripMenuItems
        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Compile();
            Run();
        }
        private void compileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Compile();
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
            SaveFile(EnginePath + "\\Assets\\class.cs");
        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void newGameObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        #endregion
        #region treFiles
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
                    if (node.Parent.Text != projectName)
                    {
                        parent = node.Parent.Text;
                        node = node.Parent;
                        file = file.Insert(0, parent + "\\");
                    }
                } while (node.Parent.Text != projectName);

                OpenFile(ProjectPath + "\\" + file);
            }
        }
        #endregion
        private void fsWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            ListDirectory(treFiles, fsWatcher.Path);
        }
        private void GUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }
        private void lstFileView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //BuildNewFileEditor(lstFileView.SelectedItems[0].SubItems[3].Text);
            //OpenFile(lstFileView.SelectedItems[0].SubItems[3].Text);
        }
        #region GameViewHandlers
        #endregion
        private void ddlListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void timCompiler_Tick(object sender, EventArgs e)
        {
            Compile();
        }
        #endregion
    }
}