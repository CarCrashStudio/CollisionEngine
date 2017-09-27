namespace CsharpRPG
{
    partial class CraftingForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnClose = new System.Windows.Forms.Button();
            this.lstCraftable = new System.Windows.Forms.ListBox();
            this.pbItem = new System.Windows.Forms.PictureBox();
            this.rtbItemDesc = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbItem)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(210, 417);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // lstCraftable
            // 
            this.lstCraftable.FormattingEnabled = true;
            this.lstCraftable.Location = new System.Drawing.Point(12, 264);
            this.lstCraftable.Name = "lstCraftable";
            this.lstCraftable.Size = new System.Drawing.Size(260, 147);
            this.lstCraftable.TabIndex = 1;
            this.lstCraftable.SelectedIndexChanged += new System.EventHandler(this.lstCraftable_SelectedIndexChanged);
            this.lstCraftable.DoubleClick += new System.EventHandler(this.lstCraftable_DoubleClick);
            // 
            // pbItem
            // 
            this.pbItem.Location = new System.Drawing.Point(12, 12);
            this.pbItem.Name = "pbItem";
            this.pbItem.Size = new System.Drawing.Size(139, 142);
            this.pbItem.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbItem.TabIndex = 2;
            this.pbItem.TabStop = false;
            // 
            // rtbItemDesc
            // 
            this.rtbItemDesc.BackColor = System.Drawing.SystemColors.Control;
            this.rtbItemDesc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbItemDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbItemDesc.Location = new System.Drawing.Point(12, 162);
            this.rtbItemDesc.Name = "rtbItemDesc";
            this.rtbItemDesc.Size = new System.Drawing.Size(260, 96);
            this.rtbItemDesc.TabIndex = 3;
            this.rtbItemDesc.Text = "";
            // 
            // CraftingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 440);
            this.Controls.Add(this.rtbItemDesc);
            this.Controls.Add(this.pbItem);
            this.Controls.Add(this.lstCraftable);
            this.Controls.Add(this.btnClose);
            this.Name = "CraftingForm";
            this.Text = "CraftingForm";
            ((System.ComponentModel.ISupportInitialize)(this.pbItem)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ListBox lstCraftable;
        private System.Windows.Forms.PictureBox pbItem;
        private System.Windows.Forms.RichTextBox rtbItemDesc;
    }
}