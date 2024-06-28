using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ЦентрЗанятости
{
    public partial class AdminForm : Form
    {

        private DataTable currentDataTable = new DataTable();
        public AdminForm()
        {
            InitializeComponent();

            label_время.Text = DateTime.Now.ToString("HH:mm");
            timer.Interval = 1000;
            timer.Tick += (sender, e) => {
                label_время.Text = DateTime.Now.ToString("HH:mm");
            };
            timer.Start();

        }

        private void Кнопка_добавления_Click(object sender, EventArgs e)
        {
            try
            {
                string login = textBox_логин.Text;
                string password = textBox_пароль.Text;
                string role = comboBox_выборРоли.SelectedItem.ToString();
                if (textBox_логин.Text != "" && textBox_пароль.Text != "" && comboBox_выборРоли.SelectedIndex != -1)
                {
                    string connectionString = GetConnectionString();
                    SQLiteConnection con = new SQLiteConnection(connectionString);
                    string query = $"insert into Пользователи(Логин, Пароль, Роль) values('{login}', '{password}', '{role}'); select * from Пользователи";
                    SQLiteCommand com = new SQLiteCommand(query, con);
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(com);
                    DataTable dtadd = new DataTable();
                    currentDataTable = dtadd;
                    ad.Fill(dtadd);
                    ОтображениеДанных.DataSource = dtadd;

                    textBox_логин.Clear();
                    textBox_пароль.Clear();

                }
                else
                {
                    DialogResult result = MessageBox.Show("Заполните поля!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex) 
            {
                DialogResult result = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }

        private void Кнопка_просмотра_Click(object sender, EventArgs e)
        {
            try
            {
                string show = comboBox_выборТаблицы.SelectedItem.ToString();
                string connectionString = GetConnectionString();
                SQLiteConnection con = new SQLiteConnection(connectionString);
                string query = $"select * from {show}";
                SQLiteCommand com = new SQLiteCommand(query, con);
                SQLiteDataAdapter ad = new SQLiteDataAdapter(com);
                DataTable dtshow = new DataTable();
                currentDataTable = dtshow;
                ad.Fill(dtshow);
                ОтображениеДанных.DataSource = dtshow;
            }
            catch
            {
                DialogResult result = MessageBox.Show("Выберите таблицу", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBox_поиск_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string table = comboBox_выборТаблицы.SelectedItem.ToString();
                string column = comboBox_выборСтолбца.SelectedValue.ToString();
                string search = textBox_поиск.Text.Trim();
                string connectionString = GetConnectionString();
                SQLiteConnection con = new SQLiteConnection(connectionString);
                string query = $"select * from {table} where {column} like '%{search}%'";
                SQLiteCommand com = new SQLiteCommand(query, con);
                SQLiteDataAdapter ad = new SQLiteDataAdapter(com);
                DataTable dtfind = new DataTable();
                ad.Fill(dtfind);
                ОтображениеДанных.DataSource = dtfind;    
            }
            catch (Exception ex) 
            {
                DialogResult result = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        public string GetConnectionString()
        {
            return $@"Data Source = emplcenter.db; Version = 3";
        }

        private void Кнопка_изменения_Click(object sender, EventArgs e)
        {
            try
            {
                string code = textBox_код.Text;
                string login = textBox_изменитьЛогин.Text;
                string password = textBox_изменитьПароль.Text;
                string role = comboBox_изменениеРоли.SelectedItem.ToString();
                if (textBox_код.Text != "" || textBox_изменитьЛогин.Text != "" || textBox_изменитьПароль.Text != "" && comboBox_изменениеРоли.SelectedIndex != -1)
                {
                    string connectionString = GetConnectionString();
                    SQLiteConnection con = new SQLiteConnection(connectionString);
                    string query = $"update Пользователи set Логин = '{login}', Пароль = '{password}', Роль = '{role}' where КодПользователя = '{code}'; select * from Пользователи";
                    SQLiteCommand com = new SQLiteCommand(query, con);
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(com);
                    DataTable dtalter = new DataTable();
                    currentDataTable = dtalter;
                    ad.Fill(dtalter);
                    ОтображениеДанных.DataSource = dtalter;

                    textBox_код.Clear();
                    textBox_изменитьЛогин.Clear();
                    textBox_изменитьПароль.Clear();

                }
                else
                {
                    DialogResult result = MessageBox.Show("Заполните поля", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Кнопка_удаления_Click(object sender, EventArgs e)
        {
            try
            {
                string code = textBox_удалитьПоКоду.Text;
                if (textBox_удалитьПоКоду.Text != "")
                {
                    string connectionString = GetConnectionString();
                    SQLiteConnection con = new SQLiteConnection(connectionString);
                    string query = $"delete from Пользователи where КодПользователя = '{code}'; select * from Пользователи";
                    SQLiteCommand com = new SQLiteCommand(query, con);
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(com);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    ОтображениеДанных.DataSource = dt;

                    textBox_удалитьПоКоду.Clear();
                }
                else
                {
                    DialogResult result = MessageBox.Show("Заполните поле", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex) 
            {
                DialogResult result = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void мелкийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeFontSize(this, 8);
        }

        private void обычныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeFontSize(this, 10);
        }

        private void крупныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeFontSize(this, 12);
        }

        public static void ChangeFontSize(Control control, float fontSize)
        {
            control.Font = new Font(control.Font.FontFamily, fontSize);
            foreach (Control childControl in control.Controls)
            {
                ChangeFontSize(childControl, fontSize);
            }
        }

        public void LoadTables()
        {
            string connectionString = GetConnectionString();
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                SQLiteCommand command = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table';", connection);
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string tableName = reader.GetString(0);
                    comboBox_выборТаблицы.Items.Add(tableName);
                }

                reader.Close();
            }
        }

        private void LoadColumnNames(string tableName)
        {
            string connectionString = GetConnectionString();
            string sql = $"PRAGMA table_info({tableName})";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    comboBox_выборСтолбца.DisplayMember = "name";
                    comboBox_выборСтолбца.ValueMember = "name";
                    comboBox_выборСтолбца.DataSource = table;
                }
            }
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            LoadTables();
        }

        private void textBox_удалитьПоКоду_TextChanged(object sender, EventArgs e)
        {
            try
            {
               string code = textBox_удалитьПоКоду.Text.Trim();
               string connectionString = GetConnectionString();
               SQLiteConnection con = new SQLiteConnection(connectionString);
               string query = $"select * from Пользователи where КодПользователя like '%{code}%'";
               SQLiteCommand com = new SQLiteCommand(query, con);
               SQLiteDataAdapter ad = new SQLiteDataAdapter(com);
               DataTable dtdel = new DataTable();
                currentDataTable = dtdel;
                ad.Fill(dtdel);
               ОтображениеДанных.DataSource = dtdel;
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void закрытьПрограммуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Закрыть программу?", "Выход из программы", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void выходИзУчЗаписиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы уверены, что хотите выйти из учетной записи?", "Выход из учетной записи", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void sQLзапросToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SQLQueryForm sql = new SQLQueryForm();
                sql.ShowDialog();
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AboutProgram ap = new AboutProgram();
                ap.ShowDialog();
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox_выборТаблицы_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadColumnNames(comboBox_выборТаблицы.SelectedItem.ToString());
        }

        private void светлаяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
            foreach (Control control in this.Controls)
            {
                control.BackColor = Color.FromKnownColor(KnownColor.Control);
            }
        }

        private void темнаяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.FromKnownColor(KnownColor.ControlDark);
            foreach (Control control in this.Controls)
            {
                control.BackColor = Color.FromKnownColor(KnownColor.ControlDark);
            }
        }

        private void включитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string appPath = Application.ExecutablePath;
                string startUpCommand = $"\"{appPath}\" /minimize";

                RegistryKey runOnceKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\RunOnce", true);
                runOnceKey.SetValue("CareerCompass", startUpCommand);
                runOnceKey.Close();

                DialogResult result = MessageBox.Show("Программа будет запускаться вместе с системой", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void выключитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string appPath = Application.ExecutablePath;
                string startUpCommand = $"\"{appPath}\" /minimize";

                RegistryKey runOnceKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\RunOnce", true);
                runOnceKey.DeleteValue("CareerCompass");
                runOnceKey.Close();

                DialogResult result = MessageBox.Show("Программа больше не будет запускаться вместе с системой", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void включитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            label_время.Visible = true;
        }

        private void отключитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label_время.Visible = false;
        }

        private void заголовкиТаблицыToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (заголовкиТаблицыToolStripMenuItem.Checked == true)
            {
                ОтображениеДанных.ColumnHeadersVisible = true;
            }
            else if (заголовкиТаблицыToolStripMenuItem.Checked == false)
            {
                ОтображениеДанных.ColumnHeadersVisible = false;
            }
        }

        private void помощьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string pdf = "Справка-по-программе-Career-Compass.pdf";
            try
            {
                Process.Start(pdf);
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
