namespace ЦентрЗанятости
{
    partial class Форма_входа
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Форма_входа));
            this.Кнопка_входа = new System.Windows.Forms.Button();
            this.label_логин = new System.Windows.Forms.Label();
            this.label_пароль = new System.Windows.Forms.Label();
            this.textBox_логин = new System.Windows.Forms.TextBox();
            this.textBox_пароль = new System.Windows.Forms.TextBox();
            this.checkBox_пароль = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // Кнопка_входа
            // 
            this.Кнопка_входа.BackColor = System.Drawing.Color.Transparent;
            this.Кнопка_входа.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Кнопка_входа.Location = new System.Drawing.Point(82, 137);
            this.Кнопка_входа.Name = "Кнопка_входа";
            this.Кнопка_входа.Size = new System.Drawing.Size(126, 34);
            this.Кнопка_входа.TabIndex = 0;
            this.Кнопка_входа.Text = "Войти";
            this.Кнопка_входа.UseVisualStyleBackColor = false;
            this.Кнопка_входа.Click += new System.EventHandler(this.Кнопка_входа_Click);
            // 
            // label_логин
            // 
            this.label_логин.AutoSize = true;
            this.label_логин.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_логин.Location = new System.Drawing.Point(9, 19);
            this.label_логин.Name = "label_логин";
            this.label_логин.Size = new System.Drawing.Size(47, 17);
            this.label_логин.TabIndex = 1;
            this.label_логин.Text = "Логин";
            // 
            // label_пароль
            // 
            this.label_пароль.AutoSize = true;
            this.label_пароль.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_пароль.Location = new System.Drawing.Point(9, 58);
            this.label_пароль.Name = "label_пароль";
            this.label_пароль.Size = new System.Drawing.Size(57, 17);
            this.label_пароль.TabIndex = 2;
            this.label_пароль.Text = "Пароль";
            // 
            // textBox_логин
            // 
            this.textBox_логин.Location = new System.Drawing.Point(72, 18);
            this.textBox_логин.Name = "textBox_логин";
            this.textBox_логин.Size = new System.Drawing.Size(210, 20);
            this.textBox_логин.TabIndex = 3;
            // 
            // textBox_пароль
            // 
            this.textBox_пароль.Location = new System.Drawing.Point(72, 57);
            this.textBox_пароль.Name = "textBox_пароль";
            this.textBox_пароль.Size = new System.Drawing.Size(210, 20);
            this.textBox_пароль.TabIndex = 4;
            this.textBox_пароль.UseSystemPasswordChar = true;
            // 
            // checkBox_пароль
            // 
            this.checkBox_пароль.AutoSize = true;
            this.checkBox_пароль.Location = new System.Drawing.Point(12, 92);
            this.checkBox_пароль.Name = "checkBox_пароль";
            this.checkBox_пароль.Size = new System.Drawing.Size(127, 17);
            this.checkBox_пароль.TabIndex = 5;
            this.checkBox_пароль.Text = "Отображать пароль";
            this.checkBox_пароль.UseVisualStyleBackColor = true;
            this.checkBox_пароль.CheckedChanged += new System.EventHandler(this.checkBox_пароль_CheckedChanged);
            // 
            // Форма_входа
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(294, 183);
            this.Controls.Add(this.checkBox_пароль);
            this.Controls.Add(this.textBox_пароль);
            this.Controls.Add(this.textBox_логин);
            this.Controls.Add(this.label_пароль);
            this.Controls.Add(this.label_логин);
            this.Controls.Add(this.Кнопка_входа);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(310, 222);
            this.MinimumSize = new System.Drawing.Size(310, 222);
            this.Name = "Форма_входа";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Career Compass";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Кнопка_входа;
        private System.Windows.Forms.Label label_логин;
        private System.Windows.Forms.Label label_пароль;
        private System.Windows.Forms.TextBox textBox_логин;
        private System.Windows.Forms.TextBox textBox_пароль;
        private System.Windows.Forms.CheckBox checkBox_пароль;
    }
}

