namespace ЦентрЗанятости
{
    partial class SQLQueryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SQLQueryForm));
            this.ОтображениеДанных = new System.Windows.Forms.DataGridView();
            this.richTextBox_написатьЗапрос = new System.Windows.Forms.RichTextBox();
            this.Панель_инструментов = new System.Windows.Forms.ToolStrip();
            this.Кнопка_выполненияЗапроса = new System.Windows.Forms.ToolStripButton();
            this.Кнопка_очисткиЗапроса = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.ОтображениеДанных)).BeginInit();
            this.Панель_инструментов.SuspendLayout();
            this.SuspendLayout();
            // 
            // ОтображениеДанных
            // 
            this.ОтображениеДанных.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.ОтображениеДанных.BackgroundColor = System.Drawing.Color.White;
            this.ОтображениеДанных.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ОтображениеДанных.Location = new System.Drawing.Point(16, 15);
            this.ОтображениеДанных.Margin = new System.Windows.Forms.Padding(4);
            this.ОтображениеДанных.Name = "ОтображениеДанных";
            this.ОтображениеДанных.Size = new System.Drawing.Size(965, 251);
            this.ОтображениеДанных.TabIndex = 0;
            // 
            // richTextBox_написатьЗапрос
            // 
            this.richTextBox_написатьЗапрос.Location = new System.Drawing.Point(16, 273);
            this.richTextBox_написатьЗапрос.Margin = new System.Windows.Forms.Padding(4);
            this.richTextBox_написатьЗапрос.Name = "richTextBox_написатьЗапрос";
            this.richTextBox_написатьЗапрос.Size = new System.Drawing.Size(964, 250);
            this.richTextBox_написатьЗапрос.TabIndex = 1;
            this.richTextBox_написатьЗапрос.Text = "";
            // 
            // Панель_инструментов
            // 
            this.Панель_инструментов.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Панель_инструментов.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Панель_инструментов.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Кнопка_выполненияЗапроса,
            this.Кнопка_очисткиЗапроса});
            this.Панель_инструментов.Location = new System.Drawing.Point(0, 534);
            this.Панель_инструментов.Name = "Панель_инструментов";
            this.Панель_инструментов.Size = new System.Drawing.Size(997, 25);
            this.Панель_инструментов.TabIndex = 5;
            this.Панель_инструментов.Text = "toolStrip1";
            // 
            // Кнопка_выполненияЗапроса
            // 
            this.Кнопка_выполненияЗапроса.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Кнопка_выполненияЗапроса.Image = global::ЦентрЗанятости.Properties.Resources.free_icon_play_button_6385977;
            this.Кнопка_выполненияЗапроса.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Кнопка_выполненияЗапроса.Name = "Кнопка_выполненияЗапроса";
            this.Кнопка_выполненияЗапроса.Size = new System.Drawing.Size(23, 22);
            this.Кнопка_выполненияЗапроса.Text = "Выполнить";
            this.Кнопка_выполненияЗапроса.Click += new System.EventHandler(this.Кнопка_выполненияЗапроса_Click);
            // 
            // Кнопка_очисткиЗапроса
            // 
            this.Кнопка_очисткиЗапроса.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Кнопка_очисткиЗапроса.Image = global::ЦентрЗанятости.Properties.Resources.free_icon_clean_up_11623802;
            this.Кнопка_очисткиЗапроса.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Кнопка_очисткиЗапроса.Name = "Кнопка_очисткиЗапроса";
            this.Кнопка_очисткиЗапроса.Size = new System.Drawing.Size(23, 22);
            this.Кнопка_очисткиЗапроса.Text = "Очистить";
            this.Кнопка_очисткиЗапроса.Click += new System.EventHandler(this.Кнопка_очисткиЗапроса_Click);
            // 
            // SQLQueryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(997, 559);
            this.Controls.Add(this.Панель_инструментов);
            this.Controls.Add(this.richTextBox_написатьЗапрос);
            this.Controls.Add(this.ОтображениеДанных);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1013, 598);
            this.MinimumSize = new System.Drawing.Size(1013, 598);
            this.Name = "SQLQueryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Career Compass";
            ((System.ComponentModel.ISupportInitialize)(this.ОтображениеДанных)).EndInit();
            this.Панель_инструментов.ResumeLayout(false);
            this.Панель_инструментов.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ОтображениеДанных;
        private System.Windows.Forms.RichTextBox richTextBox_написатьЗапрос;
        private System.Windows.Forms.ToolStrip Панель_инструментов;
        private System.Windows.Forms.ToolStripButton Кнопка_выполненияЗапроса;
        private System.Windows.Forms.ToolStripButton Кнопка_очисткиЗапроса;
    }
}