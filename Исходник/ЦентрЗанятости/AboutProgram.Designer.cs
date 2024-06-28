namespace ЦентрЗанятости
{
    partial class AboutProgram
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutProgram));
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel_ссылка = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 104);
            this.label1.TabIndex = 1;
            this.label1.Text = "Программа \"Career Compass\"\r\n\r\nВерсия: 1.0\r\n\r\nРазработчик: Никита Хохрин\r\n\r\nОбратн" +
    "ая связь:\r\n\r\n";
            // 
            // linkLabel_ссылка
            // 
            this.linkLabel_ссылка.AutoSize = true;
            this.linkLabel_ссылка.Location = new System.Drawing.Point(97, 194);
            this.linkLabel_ссылка.Name = "linkLabel_ссылка";
            this.linkLabel_ссылка.Size = new System.Drawing.Size(143, 13);
            this.linkLabel_ссылка.TabIndex = 3;
            this.linkLabel_ссылка.TabStop = true;
            this.linkLabel_ссылка.Text = "https://vk.com/unknown_ini";
            this.linkLabel_ссылка.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_ссылка_LinkClicked);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ЦентрЗанятости.Properties.Resources.free_icon_compass_998887;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(103, 101);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // AboutProgram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(269, 229);
            this.Controls.Add(this.linkLabel_ссылка);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(285, 268);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(285, 268);
            this.Name = "AboutProgram";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "О программе";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabel_ссылка;
    }
}