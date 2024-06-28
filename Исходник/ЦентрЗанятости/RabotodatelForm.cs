using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ЦентрЗанятости
{
    public partial class RabotodatelForm : Form
    {
        public RabotodatelForm()
        {
            InitializeComponent();
            LoadDataInListBox();

            label_время.Text = DateTime.Now.ToString("HH:mm");
            timer.Interval = 1000;
            timer.Tick += (sender, e) => {
                label_время.Text = DateTime.Now.ToString("HH:mm");
            };
            timer.Start();
        }

        private void Кнопка_добавленияВакансии_Click(object sender, EventArgs e)
        {
            try
            {
                string FIO = textBox_ВакансииФИО.Text;
                string Name = textBox_ВакансииНазвание.Text;
                string Salary = textBox_ВакансииЗарплата.Text;
                string Obrazov = comboBox_ВакансииОбразование.SelectedItem.ToString();
                if (textBox_ВакансииФИО.Text != "" && textBox_ВакансииНазвание.Text != "" && textBox_ВакансииЗарплата.Text != "")
                {
                    string connectionString = GetConnectionString();
                    SQLiteConnection con = new SQLiteConnection(connectionString);
                    string query = $"insert into Вакансии(Наименование, Работодатель, Зарплата, УровеньОбразования, СтатусВакансии) values('{Name}', '{FIO}', '{Salary}', '{Obrazov}', 'Активна'); select * from Вакансии where Наименование = '{Name}'";
                    SQLiteCommand com = new SQLiteCommand(query, con);
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(com);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    ОтображениеДанных.DataSource = dt;
                    textBox_ВакансииФИО.Clear();
                    textBox_ВакансииНазвание.Clear();
                    textBox_ВакансииЗарплата.Clear();
                }
                else
                {
                    DialogResult result = MessageBox.Show("Заполните поля", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch(Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string GetConnectionString()
        {
            return $@"Data Source = emplcenter.db; Version = 3";
        }

        private void Кнопка_просмотраВакансий_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = GetConnectionString();
                SQLiteConnection con = new SQLiteConnection(connectionString);
                string query = $"select * from Вакансии";
                SQLiteCommand com = new SQLiteCommand(query, con);
                SQLiteDataAdapter ad = new SQLiteDataAdapter(com);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                ОтображениеДанных.DataSource = dt;
            }
            catch(Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox_вакансииПоискПо_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string ChangeFind = comboBox_вакансииПоискПо.SelectedItem.ToString();
                string Find = textBox_вакансииПоискПо.Text.Trim();
                if(comboBox_вакансииПоискПо.SelectedIndex == 0)
                {
                    string connectionString = GetConnectionString();
                    SQLiteConnection con = new SQLiteConnection(connectionString);
                    string query = $"select * from Вакансии where Работодатель like '%{Find}%'";
                    SQLiteCommand com = new SQLiteCommand(query, con);
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(com);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    ОтображениеДанных.DataSource = dt;
                }
                else if(comboBox_вакансииПоискПо.SelectedIndex == 1)
                {
                    string connectionString = GetConnectionString();
                    SQLiteConnection con = new SQLiteConnection(connectionString);
                    string query = $"select * from Вакансии where Наименование like '%{Find}%'";
                    SQLiteCommand com = new SQLiteCommand(query, con);
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(com);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    ОтображениеДанных.DataSource = dt;
                }
            }
            catch(Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Кнопка_удаленияВакансии_Click(object sender, EventArgs e)
        {
            try
            {
                string DeleteCode = textBox_вакансииУдалениеПоКоду.Text;
                if(textBox_вакансииУдалениеПоКоду.Text != "")
                {
                    string connectionString = GetConnectionString();
                    SQLiteConnection con = new SQLiteConnection(connectionString);
                    string query = $"delete from Вакансии where КодВакансии = '{DeleteCode}'; select * from Вакансии";
                    SQLiteCommand com = new SQLiteCommand(query, con);
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(com);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    ОтображениеДанных.DataSource = dt;
                    textBox_вакансииУдалениеПоКоду.Clear();
                }
                else
                {
                   DialogResult result = MessageBox.Show("Заполните поле", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch(Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void закрытьПрограммуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Закрыть программу?", "Выход из программы", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutProgram ap = new AboutProgram();
            ap.ShowDialog();
        }

        public static void ChangeFontSize(Control control, float fontSize)
        {
            control.Font = new Font(control.Font.FontFamily, fontSize);
            foreach (Control childControl in control.Controls)
            {
                ChangeFontSize(childControl, fontSize);
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

        private void Кнопка_просмотраРезюме_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = GetConnectionString();
                SQLiteConnection con = new SQLiteConnection(connectionString);
                string query = $"select * from Резюме";
                SQLiteCommand command = new SQLiteCommand(query, con);
                SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                ОтображениеДанных.DataSource = dt;
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listBox_услуги_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listBox_услуги.SelectedItem != null)
                {
                    string selectedItem = listBox_услуги.SelectedItem.ToString();
                    string connectionString = GetConnectionString();
                    SQLiteConnection con = new SQLiteConnection(connectionString);
                    string query = $"select ОписаниеУслуги from Услуги where НаименованиеУслуги = '{selectedItem}'";
                    SQLiteCommand com = new SQLiteCommand(query, con);
                    com.Connection.Open();
                    label_описание.Text = com.ExecuteScalar().ToString();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDataInListBox()
        {
            var connectionString = GetConnectionString();
            var tableName = "Услуги";
            var columnName = "НаименованиеУслуги";

            LoadDataIntoListBox(connectionString, tableName, columnName, listBox_услуги);
        }

        public void LoadDataIntoListBox(string connectionString, string tableName, string columnName, ListBox listBox)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                var command = new SQLiteCommand($"SELECT {columnName} FROM {tableName}", connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listBox.Items.Add(reader.GetString(0));
                    }
                }
            }
        }

        private void Кнопка_измененияСтатусаВакансии_Click(object sender, EventArgs e)
        {
            try
            {
                string Code = textBox_вакансииИзмКод.Text;
                string Status = comboBox_вакансииИзм.SelectedItem.ToString();
                if (textBox_вакансииИзмКод.Text != "")
                {
                    string connectionString = GetConnectionString();
                    SQLiteConnection con = new SQLiteConnection(connectionString);
                    string query = $"update Вакансии set СтатусВакансии = '{Status}' where КодВакансии = '{Code}'; select * from Вакансии";
                    SQLiteCommand command = new SQLiteCommand(query, con);
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    ОтображениеДанных.DataSource = dt;
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
            else if(заголовкиТаблицыToolStripMenuItem.Checked == false)
            {
                ОтображениеДанных.ColumnHeadersVisible = false;
            }
        }

        private void textBox_РезюмеПоискПоДолжности_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string find = textBox_РезюмеПоискПоДолжности.Text.Trim();
                string connectionString = GetConnectionString();
                SQLiteConnection con = new SQLiteConnection(connectionString);
                string query = $"select * from Резюме where ЖелаемаяДолжность like '%{find}%'";
                SQLiteCommand comm = new SQLiteCommand(query, con);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(comm);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                ОтображениеДанных.DataSource = dt;
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox_РезюмеПоискПоПрофессии_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string find = textBox_РезюмеПоискПоПрофессии.Text.Trim();
                string connectionString = GetConnectionString();
                SQLiteConnection con = new SQLiteConnection(connectionString);
                string query = $"select * from Резюме where Профессия like '%{find}%'";
                SQLiteCommand comm = new SQLiteCommand(query, con);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(comm);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                ОтображениеДанных.DataSource = dt;
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
