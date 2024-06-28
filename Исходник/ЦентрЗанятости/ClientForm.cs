using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ЦентрЗанятости
{
    public partial class ClientForm : Form
    {
        public ClientForm()
        {
            InitializeComponent();
            LoadData();
            LoadDataInListBox();

            ОписаниеКнопкиСозданиеРезюме.SetToolTip(Кнопка_созданияРезюме, "Создать резюме");
            label_время.Text = DateTime.Now.ToString("HH:mm");
            timer.Interval = 1000;
            timer.Tick += (sender, e) => {
                label_время.Text = DateTime.Now.ToString("HH:mm");
            };
            timer.Start();
        }

        private void Кнопка_подачиЗаявления_Click(object sender, EventArgs e)
        {
            try
            {
                string ChangeService = comboBox_выборУслуги.SelectedValue.ToString();
                string FIO = textBox_ФИО.Text;
                string Number = maskedTextBox_НомерТелефона.Text;
                string Mail = textBox_Почта.Text;
                if (textBox_ФИО.Text != "" && maskedTextBox_НомерТелефона.Text != "" || textBox_Почта.Text != "")
                {
                    string connectionString = GetConnectionString();
                    SQLiteConnection con = new SQLiteConnection(connectionString);
                    string query = $"insert into ЗаявленияНаУслугу(НаименованиеУслуги, ДатаПодачиЗаявления, ФИОКлиента, Телефон, Почта) values('{ChangeService}', date('now'), '{FIO}', '{Number}', '{Mail}'); select НаименованиеУслуги, ДатаПодачиЗаявления, ФИОКлиента from ЗаявленияНаУслугу where ФИОКлиента = '{FIO}'";
                    SQLiteCommand com = new SQLiteCommand(query, con);
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(com);
                    DataTable tab = new DataTable();
                    adapter.Fill(tab);
                    ОтображениеДанных.DataSource = tab;
                    DialogResult result = MessageBox.Show("Заявление успешно оформлено", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void LoadData()
        {
            string dbPath = GetConnectionString();
            string sql = "select НаименованиеУслуги from Услуги";

            using (SQLiteConnection connection = new SQLiteConnection(dbPath))
            {
                connection.Open();
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    comboBox_выборУслуги.DisplayMember = "НаименованиеУслуги";
                    comboBox_выборУслуги.ValueMember = "НаименованиеУслуги";
                    comboBox_выборУслуги.DataSource = table;
                }
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

        public string GetConnectionString()
        {
            return $@"Data Source = emplcenter.db; Version = 3";
        }

        private void textBox_найти_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string search = textBox_найти.Text.Trim();
                string connectionString = GetConnectionString();
                SQLiteConnection con = new SQLiteConnection(connectionString);
                string query = $"select * from ЗаявленияНаУслугу where ФИОКлиента like '%{search}%'";
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

        

        private void Кнопка_отображенияЗаявлений_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = GetConnectionString();
                SQLiteConnection con = new SQLiteConnection(connectionString);
                string query = $"select * from ЗаявленияНаУслугу";
                SQLiteCommand com = new SQLiteCommand(query, con);
                SQLiteDataAdapter ad = new SQLiteDataAdapter(com);
                DataTable dtshow = new DataTable();
                ad.Fill(dtshow);
                ОтображениеДанных.DataSource = dtshow;
            }
            catch(Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void LoadDataInListBox()
        {
            var connectionString = GetConnectionString();
            var tableName = "Услуги";
            var columnName = "НаименованиеУслуги";

            LoadDataIntoListBox(connectionString, tableName, columnName, listBox_услуги);
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
            catch(Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Кнопка_отменыЗаявления_Click(object sender, EventArgs e)
        {
            try
            {
                string code = textBox_отозватьЗаявление.Text;
                if (textBox_отозватьЗаявление.Text != "")
                {
                    string connectionString = GetConnectionString();
                    SQLiteConnection con = new SQLiteConnection(connectionString);
                    string query = $"delete from ЗаявленияНаУслугу where КодЗаявления = '{code}'; select * from ЗаявленияНаУслугу";
                    SQLiteCommand com = new SQLiteCommand(query, con);
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(com);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    ОтображениеДанных.DataSource = dt;

                    textBox_отозватьЗаявление.Clear();
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

        private void textBox_поискВакансии_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string find = textBox_поискВакансии.Text.Trim();
                string connectionString = GetConnectionString();
                SQLiteConnection con = new SQLiteConnection(connectionString);
                string query = $"select Наименование, Работодатель, Зарплата, УровеньОбразования, СтатусВакансии from Вакансии where Наименование like '%{find}%'";
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

        private void Кнопка_просмотраВакансий_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = GetConnectionString();
                SQLiteConnection con = new SQLiteConnection(connectionString);
                string query = $"select Наименование, Работодатель, Зарплата, УровеньОбразования, СтатусВакансии from Вакансии";
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

        private void comboBox_сортировкаПоЗарплате_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBox_сортировкаПоЗарплате.SelectedIndex == 0)
                {
                    string connectionString = GetConnectionString();
                    SQLiteConnection con = new SQLiteConnection(connectionString);
                    string query = $"select Наименование, Работодатель, Зарплата, УровеньОбразования, СтатусВакансии from Вакансии where Зарплата < 40000";
                    SQLiteCommand command = new SQLiteCommand(query, con);
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    ОтображениеДанных.DataSource = dt;
                }
                else if (comboBox_сортировкаПоЗарплате.SelectedIndex == 1)
                {
                    string connectionString = GetConnectionString();
                    SQLiteConnection con = new SQLiteConnection(connectionString);
                    string query = $"select Наименование, Работодатель, Зарплата, УровеньОбразования, СтатусВакансии from Вакансии where Зарплата between 40000 and 60000";
                    SQLiteCommand command = new SQLiteCommand(query, con);
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    ОтображениеДанных.DataSource = dt;
                }
                else if (comboBox_сортировкаПоЗарплате.SelectedIndex == 2)
                {
                    string connectionString = GetConnectionString();
                    SQLiteConnection con = new SQLiteConnection(connectionString);
                    string query = $"select Наименование, Работодатель, Зарплата, УровеньОбразования, СтатусВакансии from Вакансии where Зарплата > 60000";
                    SQLiteCommand command = new SQLiteCommand(query, con);
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    ОтображениеДанных.DataSource = dt;
                }
                else if (comboBox_сортировкаПоЗарплате.SelectedIndex == 3)
                {
                    string connectionString = GetConnectionString();
                    SQLiteConnection con = new SQLiteConnection(connectionString);
                    string query = $"select Наименование, Работодатель, Зарплата, УровеньОбразования, СтатусВакансии from Вакансии";
                    SQLiteCommand command = new SQLiteCommand(query, con);
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    ОтображениеДанных.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox_сортировкаПоСтатусу_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBox_сортировкаПоСтатусу.SelectedIndex == 0)
                {
                    string connectionString = GetConnectionString();
                    SQLiteConnection con = new SQLiteConnection(connectionString);
                    string query = $"select Наименование, Работодатель, Зарплата, УровеньОбразования, СтатусВакансии from Вакансии where СтатусВакансии = 'Активна'";
                    SQLiteCommand command = new SQLiteCommand(query, con);
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    ОтображениеДанных.DataSource = dt;
                }
                else if (comboBox_сортировкаПоСтатусу.SelectedIndex == 1)
                {
                    string connectionString = GetConnectionString();
                    SQLiteConnection con = new SQLiteConnection(connectionString);
                    string query = $"select Наименование, Работодатель, Зарплата, УровеньОбразования, СтатусВакансии from Вакансии where СтатусВакансии = 'Неактивна'";
                    SQLiteCommand command = new SQLiteCommand(query, con);
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    ОтображениеДанных.DataSource = dt;
                }
                else if (comboBox_сортировкаПоСтатусу.SelectedIndex == 2)
                {
                    string connectionString = GetConnectionString();
                    SQLiteConnection con = new SQLiteConnection(connectionString);
                    string query = $"select Наименование, Работодатель, Зарплата, УровеньОбразования, СтатусВакансии from Вакансии";
                    SQLiteCommand command = new SQLiteCommand(query, con);
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
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

        private void comboBox_сортировкаПоУровнюОбразования_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBox_сортировкаПоУровнюОбразования.SelectedIndex == 0)
                {
                    string connectionString = GetConnectionString();
                    SQLiteConnection con = new SQLiteConnection(connectionString);
                    string query = $"select Наименование, Работодатель, Зарплата, УровеньОбразования, СтатусВакансии from Вакансии where УровеньОбразования = 'Среднее профессиональное'";
                    SQLiteCommand command = new SQLiteCommand(query, con);
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    ОтображениеДанных.DataSource = dt;
                }
                else if (comboBox_сортировкаПоУровнюОбразования.SelectedIndex == 1)
                {
                    string connectionString = GetConnectionString();
                    SQLiteConnection con = new SQLiteConnection(connectionString);
                    string query = $"select Наименование, Работодатель, Зарплата, УровеньОбразования, СтатусВакансии from Вакансии where УровеньОбразования = 'Высшее'";
                    SQLiteCommand command = new SQLiteCommand(query, con);
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    ОтображениеДанных.DataSource = dt;
                }
                else if (comboBox_сортировкаПоУровнюОбразования.SelectedIndex == 2)
                {
                    string connectionString = GetConnectionString();
                    SQLiteConnection con = new SQLiteConnection(connectionString);
                    string query = $"select Наименование, Работодатель, Зарплата, УровеньОбразования, СтатусВакансии from Вакансии where УровеньОбразования = 'Не имеет значения'";
                    SQLiteCommand command = new SQLiteCommand(query, con);
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    ОтображениеДанных.DataSource = dt;
                }
                else if (comboBox_сортировкаПоУровнюОбразования.SelectedIndex == 3) 
                {
                    string connectionString = GetConnectionString();
                    SQLiteConnection con = new SQLiteConnection(connectionString);
                    string query = $"select Наименование, Работодатель, Зарплата, УровеньОбразования, СтатусВакансии from Вакансии";
                    SQLiteCommand command = new SQLiteCommand(query, con);
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    ОтображениеДанных.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
        }

        private void radioButton_сортПоВозр_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_сортПоВозр.Checked) 
            {
                radioButton_сортПоУбыв.Checked = false;
            }
        }

        private void radioButton_сортПоУбыв_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton_сортПоУбыв.Checked)
            {
                radioButton_сортПоВозр.Checked = false;
            }
        }

        private void comboBox_Сорт_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string orderColumn = "";
                string orderDirection = "";

                if (comboBox_Сорт.SelectedIndex == 0)
                {
                    orderColumn = "Зарплата";
                }
                else if (comboBox_Сорт.SelectedIndex == 1)
                {
                    orderColumn = "УровеньОбразования";
                }
                else if (comboBox_Сорт.SelectedIndex == 2)
                {
                    orderColumn = "СтатусВакансии";
                }

                if (radioButton_сортПоВозр.Checked)
                {
                    orderDirection = "";
                }
                else if (radioButton_сортПоУбыв.Checked)
                {
                    orderDirection = "desc";
                }

                string connectionString = GetConnectionString();
                SQLiteConnection con = new SQLiteConnection(connectionString);
                string query = $"select Наименование, Работодатель, Зарплата, УровеньОбразования, СтатусВакансии from Вакансии order by {orderColumn} {orderDirection}";
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

        private void Кнопка_созданияРезюме_Click(object sender, EventArgs e)
        {
            try
            {
                string FIO = textBox_резюмеФИО.Text;
                string Dolzh = textBox_РезюмеДолжность.Text;
                string Profession = textBox_РезюмеПрофессия.Text;
                string Sfera = comboBox_РезюмеСфера.SelectedItem.ToString();
                string Salary = textBox_РезюмеЗарплата.Text;
                string Date = dateTimePicker_РезюмеДата.Value.ToString();
                string Grazhd = comboBox_РезюмеГражданство.SelectedItem.ToString();
                if (textBox_резюмеФИО.Text != "" && textBox_РезюмеДолжность.Text != "" && textBox_РезюмеПрофессия.Text != "" && textBox_РезюмеЗарплата.Text != "" && comboBox_РезюмеСфера.SelectedIndex != -1 && comboBox_РезюмеГражданство.SelectedIndex != -1)
                {
                    string connectionString = GetConnectionString();
                    SQLiteConnection con = new SQLiteConnection(connectionString);
                    string query = $"insert into Резюме(ФИОКлиента, ЖелаемаяДолжность, Профессия, СфераДеятельности, ЗаработнаяПлата, ДатаНачалаРаботы, Гражданство) values('{FIO}', '{Dolzh}', '{Profession}', '{Sfera}', '{Salary}', '{Date}', '{Grazhd}'); select * from Резюме";
                    SQLiteCommand com = new SQLiteCommand(query, con);
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(com);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    ОтображениеДанных.DataSource = dt;
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

        private void textBox_РезюмеПоискПоФИО_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string find = textBox_РезюмеПоискПоФИО.Text.Trim();
                string connectionString = GetConnectionString();
                SQLiteConnection con = new SQLiteConnection(connectionString);
                string query = $"select * from Резюме where ФИОКлиента like '%{find}%'";
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

        private void Кнопка_удаленияРезюме_Click(object sender, EventArgs e)
        {
            try
            {
                string DeleteCode = textBox_РезюмеУдалениеПоКоду.Text;
                if(textBox_РезюмеУдалениеПоКоду.Text != "")
                {
                    string connectionString = GetConnectionString();
                    SQLiteConnection con = new SQLiteConnection(connectionString);
                    string query = $"delete from Резюме where КодРезюме = '{DeleteCode}'";
                    SQLiteCommand command = new SQLiteCommand(query, con);
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    ОтображениеДанных.DataSource = dt;
                }
                else
                {
                    DialogResult result = MessageBox.Show("Заполните поле!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex) 
            {
                DialogResult result = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
