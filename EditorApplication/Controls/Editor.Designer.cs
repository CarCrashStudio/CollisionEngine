namespace EditorApplication.Controls
{
    partial class Editor
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dvDesigner = new EditorApplication.DesignView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblCurrentCell = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dvDesigner
            // 
            this.dvDesigner.CellHeight = ((uint)(0u));
            this.dvDesigner.CellWidth = ((uint)(0u));
            this.dvDesigner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dvDesigner.Location = new System.Drawing.Point(0, 0);
            this.dvDesigner.MouseHoverUpdatesOnly = false;
            this.dvDesigner.Name = "dvDesigner";
            this.dvDesigner.Size = new System.Drawing.Size(150, 150);
            this.dvDesigner.TabIndex = 0;
            this.dvDesigner.Text = "designView1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblCurrentCell});
            this.statusStrip1.Location = new System.Drawing.Point(0, 128);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(150, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblCurrentCell
            // 
            this.lblCurrentCell.Name = "lblCurrentCell";
            this.lblCurrentCell.Size = new System.Drawing.Size(118, 17);
            this.lblCurrentCell.Text = "toolStripStatusLabel1";
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dvDesigner);
            this.Name = "Editor";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DesignView dvDesigner;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblCurrentCell;
    }
}
