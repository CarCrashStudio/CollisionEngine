using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.CSharp;
using System.CodeDom.Compiler;

namespace EditorApplication.Controls
{
    public partial class CodeWindow : UserControl
    {
        CSharpCodeProvider _codeProvider = new CSharpCodeProvider();
        ICodeCompiler _compiler;

        /// <summary>
        /// The namespace of the project being worked in
        /// </summary>
        public string Namespace { get; set; }

        public CodeWindow()
        {
#pragma warning disable CS0618 // Type or member is obsolete
            _compiler = _codeProvider.CreateCompiler();
#pragma warning restore CS0618 // Type or member is obsolete
            InitializeComponent();
            rtbCodeEditor.Text += $"using System;{Environment.NewLine}{Environment.NewLine}namespace {Namespace}{Environment.NewLine}{{{Environment.NewLine}{Environment.NewLine}}}";
        }
    }
}
