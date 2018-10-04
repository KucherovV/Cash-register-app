namespace kassa
{
    partial class Reports
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.проданыеТоварыЗаСменуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonSpecificPersonal = new System.Windows.Forms.RadioButton();
            this.radioButtonAllPersonal = new System.Windows.Forms.RadioButton();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButtonAllProducts = new System.Windows.Forms.RadioButton();
            this.checkedListBox4 = new System.Windows.Forms.CheckedListBox();
            this.checkedListBox2 = new System.Windows.Forms.CheckedListBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.buttonAcceptFilters = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.проданыеТоварыЗаСменуToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(711, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // проданыеТоварыЗаСменуToolStripMenuItem
            // 
            this.проданыеТоварыЗаСменуToolStripMenuItem.Name = "проданыеТоварыЗаСменуToolStripMenuItem";
            this.проданыеТоварыЗаСменуToolStripMenuItem.Size = new System.Drawing.Size(170, 20);
            this.проданыеТоварыЗаСменуToolStripMenuItem.Text = "Проданые товары за смену";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonSpecificPersonal);
            this.groupBox1.Controls.Add(this.radioButtonAllPersonal);
            this.groupBox1.Location = new System.Drawing.Point(12, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(159, 72);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Выбор обьекта отчета";
            // 
            // radioButtonSpecificPersonal
            // 
            this.radioButtonSpecificPersonal.AutoSize = true;
            this.radioButtonSpecificPersonal.Location = new System.Drawing.Point(6, 42);
            this.radioButtonSpecificPersonal.Name = "radioButtonSpecificPersonal";
            this.radioButtonSpecificPersonal.Size = new System.Drawing.Size(143, 17);
            this.radioButtonSpecificPersonal.TabIndex = 2;
            this.radioButtonSpecificPersonal.TabStop = true;
            this.radioButtonSpecificPersonal.Text = "Конкретные работники";
            this.radioButtonSpecificPersonal.UseVisualStyleBackColor = true;
            // 
            // radioButtonAllPersonal
            // 
            this.radioButtonAllPersonal.AutoSize = true;
            this.radioButtonAllPersonal.Location = new System.Drawing.Point(6, 19);
            this.radioButtonAllPersonal.Name = "radioButtonAllPersonal";
            this.radioButtonAllPersonal.Size = new System.Drawing.Size(100, 17);
            this.radioButtonAllPersonal.TabIndex = 1;
            this.radioButtonAllPersonal.TabStop = true;
            this.radioButtonAllPersonal.Text = "Все работники";
            this.radioButtonAllPersonal.UseVisualStyleBackColor = true;
            this.radioButtonAllPersonal.CheckedChanged += new System.EventHandler(this.radioButtonAllPersonal_CheckedChanged);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.Enabled = false;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(12, 118);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(159, 199);
            this.checkedListBox1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(177, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Выбор смены";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButtonAllProducts);
            this.groupBox2.Location = new System.Drawing.Point(333, 40);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(120, 50);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Фильтр товаров";
            // 
            // radioButtonAllProducts
            // 
            this.radioButtonAllProducts.AutoSize = true;
            this.radioButtonAllProducts.Location = new System.Drawing.Point(6, 19);
            this.radioButtonAllProducts.Name = "radioButtonAllProducts";
            this.radioButtonAllProducts.Size = new System.Drawing.Size(84, 17);
            this.radioButtonAllProducts.TabIndex = 9;
            this.radioButtonAllProducts.TabStop = true;
            this.radioButtonAllProducts.Text = "Все товары";
            this.radioButtonAllProducts.UseVisualStyleBackColor = true;
            this.radioButtonAllProducts.MouseClick += new System.Windows.Forms.MouseEventHandler(this.radioButtonAllProducts_MouseClick);
            // 
            // checkedListBox4
            // 
            this.checkedListBox4.FormattingEnabled = true;
            this.checkedListBox4.Location = new System.Drawing.Point(333, 96);
            this.checkedListBox4.Name = "checkedListBox4";
            this.checkedListBox4.Size = new System.Drawing.Size(120, 214);
            this.checkedListBox4.TabIndex = 9;
            // 
            // checkedListBox2
            // 
            this.checkedListBox2.FormattingEnabled = true;
            this.checkedListBox2.Location = new System.Drawing.Point(177, 59);
            this.checkedListBox2.Name = "checkedListBox2";
            this.checkedListBox2.Size = new System.Drawing.Size(150, 259);
            this.checkedListBox2.TabIndex = 6;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(459, 40);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(247, 270);
            this.dataGridView1.TabIndex = 10;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // buttonAcceptFilters
            // 
            this.buttonAcceptFilters.Location = new System.Drawing.Point(515, 330);
            this.buttonAcceptFilters.Name = "buttonAcceptFilters";
            this.buttonAcceptFilters.Size = new System.Drawing.Size(75, 23);
            this.buttonAcceptFilters.TabIndex = 11;
            this.buttonAcceptFilters.Text = "Обработать";
            this.buttonAcceptFilters.UseVisualStyleBackColor = true;
            this.buttonAcceptFilters.Click += new System.EventHandler(this.buttonAcceptFilters_Click);
            // 
            // Reports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 363);
            this.Controls.Add(this.buttonAcceptFilters);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.checkedListBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkedListBox2);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Reports";
            this.Text = " ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Reports_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem проданыеТоварыЗаСменуToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonSpecificPersonal;
        private System.Windows.Forms.RadioButton radioButtonAllPersonal;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButtonAllProducts;
        private System.Windows.Forms.CheckedListBox checkedListBox4;
        private System.Windows.Forms.CheckedListBox checkedListBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button buttonAcceptFilters;
    }
}