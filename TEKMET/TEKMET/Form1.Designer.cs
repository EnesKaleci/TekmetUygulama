namespace TEKMET
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mÜŞTERİToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.müşteriİşlemleriToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.müsşteriHesapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.raporlarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.teklifToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mÜŞTERİToolStripMenuItem,
            this.raporlarToolStripMenuItem,
            this.teklifToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(913, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mÜŞTERİToolStripMenuItem
            // 
            this.mÜŞTERİToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.müşteriİşlemleriToolStripMenuItem,
            this.müsşteriHesapToolStripMenuItem});
            this.mÜŞTERİToolStripMenuItem.Name = "mÜŞTERİToolStripMenuItem";
            this.mÜŞTERİToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.mÜŞTERİToolStripMenuItem.Text = "Müşteri";
            this.mÜŞTERİToolStripMenuItem.Click += new System.EventHandler(this.mÜŞTERİToolStripMenuItem_Click);
            // 
            // müşteriİşlemleriToolStripMenuItem
            // 
            this.müşteriİşlemleriToolStripMenuItem.Name = "müşteriİşlemleriToolStripMenuItem";
            this.müşteriİşlemleriToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.müşteriİşlemleriToolStripMenuItem.Text = "Müşteri İşlemleri";
            this.müşteriİşlemleriToolStripMenuItem.Click += new System.EventHandler(this.müşteriİşlemleriToolStripMenuItem_Click);
            // 
            // müsşteriHesapToolStripMenuItem
            // 
            this.müsşteriHesapToolStripMenuItem.Name = "müsşteriHesapToolStripMenuItem";
            this.müsşteriHesapToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.müsşteriHesapToolStripMenuItem.Text = "Müşteri Hesap";
            this.müsşteriHesapToolStripMenuItem.Click += new System.EventHandler(this.müsşteriHesapToolStripMenuItem_Click);
            // 
            // raporlarToolStripMenuItem
            // 
            this.raporlarToolStripMenuItem.Name = "raporlarToolStripMenuItem";
            this.raporlarToolStripMenuItem.Size = new System.Drawing.Size(102, 20);
            this.raporlarToolStripMenuItem.Text = "Hesap Makinesi";
            this.raporlarToolStripMenuItem.Click += new System.EventHandler(this.raporlarToolStripMenuItem_Click);
            // 
            // teklifToolStripMenuItem
            // 
            this.teklifToolStripMenuItem.Name = "teklifToolStripMenuItem";
            this.teklifToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.teklifToolStripMenuItem.Text = "Teklif";
            this.teklifToolStripMenuItem.Click += new System.EventHandler(this.teklifToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::TEKMET.Properties.Resources.logo;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(913, 499);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "TEKMET";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mÜŞTERİToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem müşteriİşlemleriToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem müsşteriHesapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem raporlarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem teklifToolStripMenuItem;
    }
}

