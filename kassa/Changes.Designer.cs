namespace kassa
{
    partial class Changes
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.открытьСменуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.закрытьСменуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.получитьПодробнуюИнформациюОСменеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 27);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView1.Size = new System.Drawing.Size(839, 395);
            this.dataGridView1.TabIndex = 2;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.открытьСменуToolStripMenuItem,
            this.закрытьСменуToolStripMenuItem,
            this.получитьПодробнуюИнформациюОСменеToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(859, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // открытьСменуToolStripMenuItem
            // 
            this.открытьСменуToolStripMenuItem.Name = "открытьСменуToolStripMenuItem";
            this.открытьСменуToolStripMenuItem.Size = new System.Drawing.Size(103, 20);
            this.открытьСменуToolStripMenuItem.Text = "Открыть смену";
            this.открытьСменуToolStripMenuItem.Click += new System.EventHandler(this.открытьСменуToolStripMenuItem_Click);
            // 
            // закрытьСменуToolStripMenuItem
            // 
            this.закрытьСменуToolStripMenuItem.Name = "закрытьСменуToolStripMenuItem";
            this.закрытьСменуToolStripMenuItem.Size = new System.Drawing.Size(102, 20);
            this.закрытьСменуToolStripMenuItem.Text = "Закрыть смену";
            this.закрытьСменуToolStripMenuItem.Click += new System.EventHandler(this.закрытьСменуToolStripMenuItem_Click);
            // 
            // получитьПодробнуюИнформациюОСменеToolStripMenuItem
            // 
            this.получитьПодробнуюИнформациюОСменеToolStripMenuItem.Name = "получитьПодробнуюИнформациюОСменеToolStripMenuItem";
            this.получитьПодробнуюИнформациюОСменеToolStripMenuItem.Size = new System.Drawing.Size(266, 20);
            this.получитьПодробнуюИнформациюОСменеToolStripMenuItem.Text = "Получить подробную информацию о смене";
            this.получитьПодробнуюИнформациюОСменеToolStripMenuItem.Click += new System.EventHandler(this.получитьПодробнуюИнформациюОСменеToolStripMenuItem_Click);
            // 
            // Changes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(859, 430);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Changes";
            this.Text = "Закрыть смену и начать новую";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Changes_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem открытьСменуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem закрытьСменуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem получитьПодробнуюИнформациюОСменеToolStripMenuItem;
    }
}