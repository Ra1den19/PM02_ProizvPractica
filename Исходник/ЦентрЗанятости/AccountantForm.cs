using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Drawing.Printing;
using System.Reflection;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace ЦентрЗанятости
{
    public partial class AccountantForm : Form
    {
        public AccountantForm()
        {
            InitializeComponent();
            LoadData();
            toolTip_кнопкаКомпанииПодсчитать.SetToolTip(Кнопка_подсчетаКомпаний, "Подсчитать количество компаний");
            toolTip_кнопкаВакансииПодсчитать.SetToolTip(Кнопка_подсчетаВакансий, "Подсчитать количество вакансий");
            label_время.Text = DateTime.Now.ToString("HH:mm");
            timer.Interval = 1000;
            timer.Tick += (sender, e) => {
                label_время.Text = DateTime.Now.ToString("HH:mm");
            };
            timer.Start();
        }

        private void Кнопка_просмотраКомпаний_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = GetConnectionString();
                SQLiteConnection con = new SQLiteConnection(connectionString);
                string query = $"select * from Компании";
                SQLiteCommand com = new SQLiteCommand(query, con);
                SQLiteDataAdapter ad = new SQLiteDataAdapter(com);
                System.Data.DataTable dtshow = new System.Data.DataTable();
                ad.Fill(dtshow);
                ОтображениеДанных.DataSource = dtshow;
            }
            catch (Exception ex) 
            {
                DialogResult result = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetConnectionString()
        {
            return $@"Data Source = emplcenter.db; Version = 3";
        }

        private void textBox_КомпанииПоиск_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string find = textBox_КомпанииПоиск.Text.Trim();
                if (comboBox_КомпанииПоиск.SelectedIndex == 0)
                {
                    string connectionString = GetConnectionString();
                    SQLiteConnection con = new SQLiteConnection(connectionString);
                    string query = $"select * from Компании where Наименование like '%{find}%'";
                    SQLiteCommand com = new SQLiteCommand(query, con);
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(com);
                    System.Data.DataTable dtshow = new System.Data.DataTable();
                    ad.Fill(dtshow);
                    ОтображениеДанных.DataSource = dtshow;
                }
                else if (comboBox_КомпанииПоиск.SelectedIndex == 1)
                {
                    string connectionString = GetConnectionString();
                    SQLiteConnection con = new SQLiteConnection(connectionString);
                    string query = $"select * from Компании where АдресКомпании like '%{find}%'";
                    SQLiteCommand com = new SQLiteCommand(query, con);
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(com);
                    System.Data.DataTable dtshow = new System.Data.DataTable();
                    ad.Fill(dtshow);
                    ОтображениеДанных.DataSource = dtshow;
                }
            }
            catch (Exception ex) 
            {
                DialogResult result = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Кнопка_подсчетаКомпаний_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = GetConnectionString();
                SQLiteConnection con = new SQLiteConnection(connectionString);
                string query = $"select count(КодКомпании) as Количество from Компании";
                SQLiteCommand com = new SQLiteCommand(query, con);
                SQLiteDataAdapter ad = new SQLiteDataAdapter(com);
                System.Data.DataTable dtshow = new System.Data.DataTable();
                ad.Fill(dtshow);
                ОтображениеДанных.DataSource = dtshow;
            }
            catch (Exception ex) 
            {
               DialogResult result = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void выйтиИзУчЗаписиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы уверены, что хотите выйти из учетной записи?", "Выход из учетной записи", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void закрытьПрограммуToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Закрыть программу?", "Выход из программы", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                System.Windows.Forms.Application.Exit();
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
            control.Font = new System.Drawing.Font(control.Font.FontFamily, fontSize);
            foreach (Control childControl in control.Controls)
            {
                ChangeFontSize(childControl, fontSize);
            }
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutProgram ap = new AboutProgram();
            ap.ShowDialog();
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
                System.Data.DataTable dt = new System.Data.DataTable();
                ad.Fill(dt);
                ОтображениеДанных.DataSource = dt;
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox_ВакансииПоиск_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string find = textBox_ВакансииПоиск.Text.Trim();
                if (comboBox_ВакансииНайтиПо.SelectedIndex == 0)
                {
                    string connectionString = GetConnectionString();
                    SQLiteConnection con = new SQLiteConnection(connectionString);
                    string query = $"select * from Вакансии where Зарплата like '%{find}%'";
                    SQLiteCommand com = new SQLiteCommand(query, con);
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(com);
                    System.Data.DataTable dtshow = new System.Data.DataTable();
                    ad.Fill(dtshow);
                    ОтображениеДанных.DataSource = dtshow;
                }
                else if(comboBox_ВакансииНайтиПо.SelectedIndex == 1)
                {
                    string connectionString = GetConnectionString();
                    SQLiteConnection con = new SQLiteConnection(connectionString);
                    string query = $"select * from Вакансии where Наименование like '%{find}%'";
                    SQLiteCommand com = new SQLiteCommand(query, con);
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(com);
                    System.Data.DataTable dtshow = new System.Data.DataTable();
                    ad.Fill(dtshow);
                    ОтображениеДанных.DataSource = dtshow;
                }
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
                    System.Data.DataTable dt = new System.Data.DataTable();
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
                    System.Data.DataTable dt = new System.Data.DataTable();
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
                    System.Data.DataTable dt = new System.Data.DataTable();
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
                    System.Data.DataTable dt = new System.Data.DataTable();
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
                    System.Data.DataTable dt = new System.Data.DataTable();
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
                    System.Data.DataTable dt = new System.Data.DataTable();
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
                    System.Data.DataTable dt = new System.Data.DataTable();
                    ad.Fill(dt);
                    ОтображениеДанных.DataSource = dt;
                }
            }
            catch (Exception ex)
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
                    System.Data.DataTable dt = new System.Data.DataTable();
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
                    System.Data.DataTable dt = new System.Data.DataTable();
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
                    System.Data.DataTable dt = new System.Data.DataTable();
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
                    System.Data.DataTable dt = new System.Data.DataTable();
                    ad.Fill(dt);
                    ОтображениеДанных.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void radioButton_вакансииГруппировкаПоНазванию_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_вакансииГруппировкаПоНазванию.Checked)
            {
                radioButton_вакансииГруппировкаПоОбразованию.Checked = false;
            }
        }

        private void radioButton_вакансииГруппировкаПоОбразованию_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_вакансииГруппировкаПоОбразованию.Checked)
            {
                radioButton_вакансииГруппировкаПоНазванию.Checked = false;
            }
        }

        private void Кнопка_группировки_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButton_вакансииГруппировкаПоНазванию.Checked == true)
                {
                    string connectionString = GetConnectionString();
                    SQLiteConnection con = new SQLiteConnection(connectionString);
                    string query = $"select Наименование, count(КодВакансии) as Количество from Вакансии group by Наименование";
                    SQLiteCommand command = new SQLiteCommand(query, con);
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
                    System.Data.DataTable dt = new System.Data.DataTable();
                    ad.Fill(dt);
                    ОтображениеДанных.DataSource = dt;
                }
                else if (radioButton_вакансииГруппировкаПоОбразованию.Checked == true) 
                {
                    string connectionString = GetConnectionString();
                    SQLiteConnection con = new SQLiteConnection(connectionString);
                    string query = $"select УровеньОбразования, count(КодВакансии) as Количество from Вакансии group by УровеньОбразования";
                    SQLiteCommand command = new SQLiteCommand(query, con);
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
                    System.Data.DataTable dt = new System.Data.DataTable();
                    ad.Fill(dt);
                    ОтображениеДанных.DataSource = dt;
                }
            }
            catch (Exception ex) 
            {
                DialogResult result = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Кнопка_просмотраЗаявлений_Click(object sender, EventArgs e)
        {
            try
            {
                if(textBox_ЗаявленияДень.Text != "")
                {
                    if (comboBox_заявленияМесяц.SelectedIndex == 0)
                    {
                        string Year = comboBox_заявленияГод.SelectedItem.ToString();
                        string Day = textBox_ЗаявленияДень.Text;
                        string connectionString = GetConnectionString();
                        SQLiteConnection con = new SQLiteConnection(connectionString);
                        string query = $"select * from ЗаявленияНаУслугу where ДатаПодачиЗаявления = '{Year}-01-{Day}'";
                        SQLiteCommand command = new SQLiteCommand(query, con);
                        SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
                        System.Data.DataTable dt = new System.Data.DataTable();
                        ad.Fill(dt);
                        ОтображениеДанных.DataSource = dt;
                    }
                    else if (comboBox_заявленияМесяц.SelectedIndex == 1)
                    {
                        string Year = comboBox_заявленияГод.SelectedItem.ToString();
                        string Day = textBox_ЗаявленияДень.Text;
                        string connectionString = GetConnectionString();
                        SQLiteConnection con = new SQLiteConnection(connectionString);
                        string query = $"select * from ЗаявленияНаУслугу where ДатаПодачиЗаявления = '{Year}-02-{Day}'";
                        SQLiteCommand command = new SQLiteCommand(query, con);
                        SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
                        System.Data.DataTable dt = new System.Data.DataTable();
                        ad.Fill(dt);
                        ОтображениеДанных.DataSource = dt;
                    }
                    else if (comboBox_заявленияМесяц.SelectedIndex == 2)
                    {
                        string Year = comboBox_заявленияГод.SelectedItem.ToString();
                        string Day = textBox_ЗаявленияДень.Text;
                        string connectionString = GetConnectionString();
                        SQLiteConnection con = new SQLiteConnection(connectionString);
                        string query = $"select * from ЗаявленияНаУслугу where ДатаПодачиЗаявления = '{Year}-03-{Day}'";
                        SQLiteCommand command = new SQLiteCommand(query, con);
                        SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
                        System.Data.DataTable dt = new System.Data.DataTable();
                        ad.Fill(dt);
                        ОтображениеДанных.DataSource = dt;
                    }
                    else if (comboBox_заявленияМесяц.SelectedIndex == 3)
                    {
                        string Year = comboBox_заявленияГод.SelectedItem.ToString();
                        string Day = textBox_ЗаявленияДень.Text;
                        string connectionString = GetConnectionString();
                        SQLiteConnection con = new SQLiteConnection(connectionString);
                        string query = $"select * from ЗаявленияНаУслугу where ДатаПодачиЗаявления = '{Year}-04-{Day}'";
                        SQLiteCommand command = new SQLiteCommand(query, con);
                        SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
                        System.Data.DataTable dt = new System.Data.DataTable();
                        ad.Fill(dt);
                        ОтображениеДанных.DataSource = dt;
                    }
                    else if (comboBox_заявленияМесяц.SelectedIndex == 4)
                    {
                        string Year = comboBox_заявленияГод.SelectedItem.ToString();
                        string Day = textBox_ЗаявленияДень.Text;
                        string connectionString = GetConnectionString();
                        SQLiteConnection con = new SQLiteConnection(connectionString);
                        string query = $"select * from ЗаявленияНаУслугу where ДатаПодачиЗаявления = '{Year}-05-{Day}'";
                        SQLiteCommand command = new SQLiteCommand(query, con);
                        SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
                        System.Data.DataTable dt = new System.Data.DataTable();
                        ad.Fill(dt);
                        ОтображениеДанных.DataSource = dt;
                    }
                    else if (comboBox_заявленияМесяц.SelectedIndex == 5)
                    {
                        string Year = comboBox_заявленияГод.SelectedItem.ToString();
                        string Day = textBox_ЗаявленияДень.Text;
                        string connectionString = GetConnectionString();
                        SQLiteConnection con = new SQLiteConnection(connectionString);
                        string query = $"select * from ЗаявленияНаУслугу where ДатаПодачиЗаявления = '{Year}-06-{Day}'";
                        SQLiteCommand command = new SQLiteCommand(query, con);
                        SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
                        System.Data.DataTable dt = new System.Data.DataTable();
                        ad.Fill(dt);
                        ОтображениеДанных.DataSource = dt;
                    }
                    else if (comboBox_заявленияМесяц.SelectedIndex == 6)
                    {
                        string Year = comboBox_заявленияГод.SelectedItem.ToString();
                        string Day = textBox_ЗаявленияДень.Text;
                        string connectionString = GetConnectionString();
                        SQLiteConnection con = new SQLiteConnection(connectionString);
                        string query = $"select * from ЗаявленияНаУслугу where ДатаПодачиЗаявления = '{Year}-07-{Day}'";
                        SQLiteCommand command = new SQLiteCommand(query, con);
                        SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
                        System.Data.DataTable dt = new System.Data.DataTable();
                        ad.Fill(dt);
                        ОтображениеДанных.DataSource = dt;
                    }
                    else if (comboBox_заявленияМесяц.SelectedIndex == 7)
                    {
                        string Year = comboBox_заявленияГод.SelectedItem.ToString();
                        string Day = textBox_ЗаявленияДень.Text;
                        string connectionString = GetConnectionString();
                        SQLiteConnection con = new SQLiteConnection(connectionString);
                        string query = $"select * from ЗаявленияНаУслугу where ДатаПодачиЗаявления = '{Year}-08-{Day}'";
                        SQLiteCommand command = new SQLiteCommand(query, con);
                        SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
                        System.Data.DataTable dt = new System.Data.DataTable();
                        ad.Fill(dt);
                        ОтображениеДанных.DataSource = dt;
                    }
                    else if (comboBox_заявленияМесяц.SelectedIndex == 8)
                    {
                        string Year = comboBox_заявленияГод.SelectedItem.ToString();
                        string Day = textBox_ЗаявленияДень.Text;
                        string connectionString = GetConnectionString();
                        SQLiteConnection con = new SQLiteConnection(connectionString);
                        string query = $"select * from ЗаявленияНаУслугу where ДатаПодачиЗаявления = '{Year}-09-{Day}'";
                        SQLiteCommand command = new SQLiteCommand(query, con);
                        SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
                        System.Data.DataTable dt = new System.Data.DataTable();
                        ad.Fill(dt);
                        ОтображениеДанных.DataSource = dt;
                    }
                    else if (comboBox_заявленияМесяц.SelectedIndex == 9)
                    {
                        string Year = comboBox_заявленияГод.SelectedItem.ToString();
                        string Day = textBox_ЗаявленияДень.Text;
                        string connectionString = GetConnectionString();
                        SQLiteConnection con = new SQLiteConnection(connectionString);
                        string query = $"select * from ЗаявленияНаУслугу where ДатаПодачиЗаявления = '{Year}-10-{Day}'";
                        SQLiteCommand command = new SQLiteCommand(query, con);
                        SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
                        System.Data.DataTable dt = new System.Data.DataTable();
                        ad.Fill(dt);
                        ОтображениеДанных.DataSource = dt;
                    }
                    else if (comboBox_заявленияМесяц.SelectedIndex == 10)
                    {
                        string Year = comboBox_заявленияГод.SelectedItem.ToString();
                        string Day = textBox_ЗаявленияДень.Text;
                        string connectionString = GetConnectionString();
                        SQLiteConnection con = new SQLiteConnection(connectionString);
                        string query = $"select * from ЗаявленияНаУслугу where ДатаПодачиЗаявления = '{Year}-11-{Day}'";
                        SQLiteCommand command = new SQLiteCommand(query, con);
                        SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
                        System.Data.DataTable dt = new System.Data.DataTable();
                        ad.Fill(dt);
                        ОтображениеДанных.DataSource = dt;
                    }
                    else if (comboBox_заявленияМесяц.SelectedIndex == 11)
                    {
                        string Year = comboBox_заявленияГод.SelectedItem.ToString();
                        string Day = textBox_ЗаявленияДень.Text;
                        string connectionString = GetConnectionString();
                        SQLiteConnection con = new SQLiteConnection(connectionString);
                        string query = $"select * from ЗаявленияНаУслугу where ДатаПодачиЗаявления = '{Year}-12-{Day}'";
                        SQLiteCommand command = new SQLiteCommand(query, con);
                        SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
                        System.Data.DataTable dt = new System.Data.DataTable();
                        ad.Fill(dt);
                        ОтображениеДанных.DataSource = dt;
                    }
                }
                else
                {
                    DialogResult result = MessageBox.Show("Заполните поле", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex) 
            {
                DialogResult result = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<int> Years()
        {
            List<int> years = new List<int>();
            int currentYear = DateTime.Now.Year;
            for (int year = 1900; year <= currentYear; year++)
            {
                years.Add(year);
            }
            return years;
        }

        private void AccountantForm_Load(object sender, EventArgs e)
        {
            List<int> years = Years();
            comboBox_заявленияГод.DataSource = years;
        }

        private void comboBox_заявленияФильтр_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filtr = comboBox_заявленияФильтр.SelectedValue.ToString();
            string connectionString = GetConnectionString();
            SQLiteConnection con = new SQLiteConnection(connectionString);
            string query = $"select * from ЗаявленияНаУслугу where НаименованиеУслуги = '{filtr}'";
            SQLiteCommand command = new SQLiteCommand(query, con);
            SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
            System.Data.DataTable dt = new System.Data.DataTable();
            ad.Fill(dt);
            ОтображениеДанных.DataSource = dt;
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
                    System.Data.DataTable table = new System.Data.DataTable();
                    adapter.Fill(table);

                    comboBox_заявленияФильтр.DisplayMember = "НаименованиеУслуги";
                    comboBox_заявленияФильтр.ValueMember = "НаименованиеУслуги";
                    comboBox_заявленияФильтр.DataSource = table;
                }
            }
        }

        private void radioButton_заявленияГруппировкаНазвание_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton_заявленияГруппировкаНазвание.Checked)
            {
                radioButton_заявленияГруппировкаФИО.Checked = false;
            }
        }

        private void radioButton_заявленияГруппировкаФИО_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_заявленияГруппировкаФИО.Checked)
            {
                radioButton_заявленияГруппировкаНазвание.Checked = false;
            }
        }

        private void Кнопка_группировкиЗаявлений_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButton_заявленияГруппировкаНазвание.Checked == true)
                {
                    string connectionString = GetConnectionString();
                    SQLiteConnection con = new SQLiteConnection(connectionString);
                    string query = $"select НаименованиеУслуги, count(КодЗаявления) as Количество from ЗаявленияНаУслугу group by НаименованиеУслуги";
                    SQLiteCommand command = new SQLiteCommand(query, con);
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
                    System.Data.DataTable dt = new System.Data.DataTable();
                    ad.Fill(dt);
                    ОтображениеДанных.DataSource = dt;
                }
                else if (radioButton_заявленияГруппировкаФИО.Checked == true)
                {
                    string connectionString = GetConnectionString();
                    SQLiteConnection con = new SQLiteConnection(connectionString);
                    string query = $"select ФИОКлиента, count(КодЗаявления) as Количество from ЗаявленияНаУслугу group by ФИОКлиента";
                    SQLiteCommand command = new SQLiteCommand(query, con);
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
                    System.Data.DataTable dt = new System.Data.DataTable();
                    ad.Fill(dt);
                    ОтображениеДанных.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox_заявленияПоиск_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string find = textBox_заявленияПоиск.Text.Trim();
                if(textBox_заявленияПоиск.Text != "")
                {
                    string connectionString = GetConnectionString();
                    SQLiteConnection con = new SQLiteConnection(connectionString);
                    string query = $"select * from ЗаявленияНаУслугу where ФИОКлиента like '%{find}%'";
                    SQLiteCommand command = new SQLiteCommand(query, con);
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
                    System.Data.DataTable dt = new System.Data.DataTable();
                    ad.Fill(dt);
                    ОтображениеДанных.DataSource = dt;
                }
            }
            catch (Exception ex) 
            {
                DialogResult result = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Кнопка_просмотраВсехЗаявлений_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = GetConnectionString();
                SQLiteConnection con = new SQLiteConnection(connectionString);
                string query = $"select * from ЗаявленияНаУслугу";
                SQLiteCommand command = new SQLiteCommand(query, con);
                SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
                System.Data.DataTable dt = new System.Data.DataTable();
                ad.Fill(dt);
                ОтображениеДанных.DataSource = dt;
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void светлаяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
            foreach (Control control in this.Controls)
            {
                control.BackColor = Color.FromKnownColor(KnownColor.Control);
            }
        }

        private void Кнопка_просмотраУслуг_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = GetConnectionString();
                SQLiteConnection con = new SQLiteConnection(connectionString);
                string query = $"select НаименованиеУслуги, ОписаниеУслуги from Услуги";
                SQLiteCommand command = new SQLiteCommand(query, con);
                SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
                System.Data.DataTable dt = new System.Data.DataTable();
                ad.Fill(dt);
                ОтображениеДанных.DataSource = dt;
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void включитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string appPath = System.Windows.Forms.Application.ExecutablePath;
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
                string appPath = System.Windows.Forms.Application.ExecutablePath;
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

        private void документExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "CSV Files (*.csv)|*.csv";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter sw = new StreamWriter(sfd.FileName, false, System.Text.Encoding.UTF8))
                    {
                        for (int i = 0; i < ОтображениеДанных.Columns.Count; i++)
                        {
                            sw.Write(ОтображениеДанных.Columns[i].HeaderText);
                            if (i < ОтображениеДанных.Columns.Count - 1)
                            {
                                sw.Write("   ");
                            }
                        }
                        sw.Write(sw.NewLine);

                        foreach (DataGridViewRow row in ОтображениеДанных.Rows)
                        {
                            for (int i = 0; i < ОтображениеДанных.Columns.Count; i++)
                            {
                                sw.Write(row.Cells[i].Value);
                                if (i < ОтображениеДанных.Columns.Count - 1)
                                {
                                    sw.Write("   ");
                                }
                            }
                            sw.Write(sw.NewLine);
                        }
                    }
                    DialogResult result = MessageBox.Show("Файл сохранен", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex) 
            {
                DialogResult result = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ОткрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Текстовые файлы (*.txt)|*.txt",
                Title = "Открыть файл в блокноте"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                System.Diagnostics.Process.Start("notepad.exe", openFileDialog.FileName);
            }
        }

        private void ОтображениеДанных_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Control && e.KeyCode == Keys.S)
                {
                    try
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "CSV Files (*.csv)|*.csv";

                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            using (StreamWriter sw = new StreamWriter(sfd.FileName, false, System.Text.Encoding.UTF8))
                            {
                                for (int i = 0; i < ОтображениеДанных.Columns.Count; i++)
                                {
                                    sw.Write(ОтображениеДанных.Columns[i].HeaderText);
                                    if (i < ОтображениеДанных.Columns.Count - 1)
                                    {
                                        sw.Write("   ");
                                    }
                                }
                                sw.Write(sw.NewLine);

                                foreach (DataGridViewRow row in ОтображениеДанных.Rows)
                                {
                                    for (int i = 0; i < ОтображениеДанных.Columns.Count; i++)
                                    {
                                        sw.Write(row.Cells[i].Value);
                                        if (i < ОтображениеДанных.Columns.Count - 1)
                                        {
                                            sw.Write("   ");
                                        }
                                    }
                                    sw.Write(sw.NewLine);
                                }
                            }
                            DialogResult result = MessageBox.Show("Файл сохранен", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        DialogResult result = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void текстовыйДокументToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                    {
                        for (int i = 0; i < ОтображениеДанных.Rows.Count; i++)
                        {
                            for (int j = 0; j < ОтображениеДанных.Columns.Count; j++)
                            {
                                writer.Write(ОтображениеДанных.Rows[i].Cells[j].Value);
                                if (j < ОтображениеДанных.Columns.Count - 1)
                                    writer.Write("   ");
                            }

                            writer.WriteLine();
                        }
                        DialogResult result = MessageBox.Show("Файл сохранен", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
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

        private void Кнопка_подсчетаВакансий_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = GetConnectionString();
                SQLiteConnection con = new SQLiteConnection(connectionString);
                string query = $"select count(КодВакансии) as Количество from Вакансии";
                SQLiteCommand com = new SQLiteCommand(query, con);
                SQLiteDataAdapter ad = new SQLiteDataAdapter(com);
                System.Data.DataTable dtshow = new System.Data.DataTable();
                ad.Fill(dtshow);
                ОтображениеДанных.DataSource = dtshow;
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Кнопка_подсчетаЗаявленийЗаВсёВремя_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = GetConnectionString();
                SQLiteConnection con = new SQLiteConnection(connectionString);
                string query = $"select count(КодЗаявления) as Количество from ЗаявленияНаУслугу";
                SQLiteCommand com = new SQLiteCommand(query, con);
                SQLiteDataAdapter ad = new SQLiteDataAdapter(com);
                System.Data.DataTable dtshow = new System.Data.DataTable();
                ad.Fill(dtshow);
                ОтображениеДанных.DataSource = dtshow;
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Кнопка_подсчетаЗаОпределённыйПериод_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox_ЗаявленияДень.Text != "")
                {
                    if (comboBox_заявленияМесяц.SelectedIndex == 0)
                    {
                        string Year = comboBox_заявленияГод.SelectedItem.ToString();
                        string Day = textBox_ЗаявленияДень.Text;
                        string connectionString = GetConnectionString();
                        SQLiteConnection con = new SQLiteConnection(connectionString);
                        string query = $"select НаименованиеУслуги, count(КодЗаявления) as Количество from ЗаявленияНаУслугу where ДатаПодачиЗаявления = '{Year}-01-{Day}' group by НаименованиеУслуги";
                        SQLiteCommand command = new SQLiteCommand(query, con);
                        SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
                        System.Data.DataTable dt = new System.Data.DataTable();
                        ad.Fill(dt);
                        ОтображениеДанных.DataSource = dt;
                    }
                    else if (comboBox_заявленияМесяц.SelectedIndex == 1)
                    {
                        string Year = comboBox_заявленияГод.SelectedItem.ToString();
                        string Day = textBox_ЗаявленияДень.Text;
                        string connectionString = GetConnectionString();
                        SQLiteConnection con = new SQLiteConnection(connectionString);
                        string query = $"select НаименованиеУслуги, count(КодЗаявления) as Количество from ЗаявленияНаУслугу where ДатаПодачиЗаявления = '{Year}-02-{Day}' group by НаименованиеУслуги";
                        SQLiteCommand command = new SQLiteCommand(query, con);
                        SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
                        System.Data.DataTable dt = new System.Data.DataTable();
                        ad.Fill(dt);
                        ОтображениеДанных.DataSource = dt;
                    }
                    else if (comboBox_заявленияМесяц.SelectedIndex == 2)
                    {
                        string Year = comboBox_заявленияГод.SelectedItem.ToString();
                        string Day = textBox_ЗаявленияДень.Text;
                        string connectionString = GetConnectionString();
                        SQLiteConnection con = new SQLiteConnection(connectionString);
                        string query = $"select НаименованиеУслуги, count(КодЗаявления) as Количество from ЗаявленияНаУслугу where ДатаПодачиЗаявления = '{Year}-03-{Day}' group by НаименованиеУслуги";
                        SQLiteCommand command = new SQLiteCommand(query, con);
                        SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
                        System.Data.DataTable dt = new System.Data.DataTable();
                        ad.Fill(dt);
                        ОтображениеДанных.DataSource = dt;
                    }
                    else if (comboBox_заявленияМесяц.SelectedIndex == 3)
                    {
                        string Year = comboBox_заявленияГод.SelectedItem.ToString();
                        string Day = textBox_ЗаявленияДень.Text;
                        string connectionString = GetConnectionString();
                        SQLiteConnection con = new SQLiteConnection(connectionString);
                        string query = $"select НаименованиеУслуги, count(КодЗаявления) as Количество from ЗаявленияНаУслугу where ДатаПодачиЗаявления = '{Year}-04-{Day}' group by НаименованиеУслуги";
                        SQLiteCommand command = new SQLiteCommand(query, con);
                        SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
                        System.Data.DataTable dt = new System.Data.DataTable();
                        ad.Fill(dt);
                        ОтображениеДанных.DataSource = dt;
                    }
                    else if (comboBox_заявленияМесяц.SelectedIndex == 4)
                    {
                        string Year = comboBox_заявленияГод.SelectedItem.ToString();
                        string Day = textBox_ЗаявленияДень.Text;
                        string connectionString = GetConnectionString();
                        SQLiteConnection con = new SQLiteConnection(connectionString);
                        string query = $"select НаименованиеУслуги, count(КодЗаявления) as Количество from ЗаявленияНаУслугу where ДатаПодачиЗаявления = '{Year}-05-{Day}' group by НаименованиеУслуги";
                        SQLiteCommand command = new SQLiteCommand(query, con);
                        SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
                        System.Data.DataTable dt = new System.Data.DataTable();
                        ad.Fill(dt);
                        ОтображениеДанных.DataSource = dt;
                    }
                    else if (comboBox_заявленияМесяц.SelectedIndex == 5)
                    {
                        string Year = comboBox_заявленияГод.SelectedItem.ToString();
                        string Day = textBox_ЗаявленияДень.Text;
                        string connectionString = GetConnectionString();
                        SQLiteConnection con = new SQLiteConnection(connectionString);
                        string query = $"select НаименованиеУслуги, count(КодЗаявления) as Количество from ЗаявленияНаУслугу where ДатаПодачиЗаявления = '{Year}-06-{Day}' group by НаименованиеУслуги";
                        SQLiteCommand command = new SQLiteCommand(query, con);
                        SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
                        System.Data.DataTable dt = new System.Data.DataTable();
                        ad.Fill(dt);
                        ОтображениеДанных.DataSource = dt;
                    }
                    else if (comboBox_заявленияМесяц.SelectedIndex == 6)
                    {
                        string Year = comboBox_заявленияГод.SelectedItem.ToString();
                        string Day = textBox_ЗаявленияДень.Text;
                        string connectionString = GetConnectionString();
                        SQLiteConnection con = new SQLiteConnection(connectionString);
                        string query = $"select НаименованиеУслуги, count(КодЗаявления) as Количество from ЗаявленияНаУслугу where ДатаПодачиЗаявления = '{Year}-07-{Day}' group by НаименованиеУслуги";
                        SQLiteCommand command = new SQLiteCommand(query, con);
                        SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
                        System.Data.DataTable dt = new System.Data.DataTable();
                        ad.Fill(dt);
                        ОтображениеДанных.DataSource = dt;
                    }
                    else if (comboBox_заявленияМесяц.SelectedIndex == 7)
                    {
                        string Year = comboBox_заявленияГод.SelectedItem.ToString();
                        string Day = textBox_ЗаявленияДень.Text;
                        string connectionString = GetConnectionString();
                        SQLiteConnection con = new SQLiteConnection(connectionString);
                        string query = $"select НаименованиеУслуги, count(КодЗаявления) as Количество from ЗаявленияНаУслугу where ДатаПодачиЗаявления = '{Year}-08-{Day}' group by НаименованиеУслуги";
                        SQLiteCommand command = new SQLiteCommand(query, con);
                        SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
                        System.Data.DataTable dt = new System.Data.DataTable();
                        ad.Fill(dt);
                        ОтображениеДанных.DataSource = dt;
                    }
                    else if (comboBox_заявленияМесяц.SelectedIndex == 8)
                    {
                        string Year = comboBox_заявленияГод.SelectedItem.ToString();
                        string Day = textBox_ЗаявленияДень.Text;
                        string connectionString = GetConnectionString();
                        SQLiteConnection con = new SQLiteConnection(connectionString);
                        string query = $"select НаименованиеУслуги, count(КодЗаявления) as Количество from ЗаявленияНаУслугу where ДатаПодачиЗаявления = '{Year}-09-{Day}' group by НаименованиеУслуги";
                        SQLiteCommand command = new SQLiteCommand(query, con);
                        SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
                        System.Data.DataTable dt = new System.Data.DataTable();
                        ad.Fill(dt);
                        ОтображениеДанных.DataSource = dt;
                    }
                    else if (comboBox_заявленияМесяц.SelectedIndex == 9)
                    {
                        string Year = comboBox_заявленияГод.SelectedItem.ToString();
                        string Day = textBox_ЗаявленияДень.Text;
                        string connectionString = GetConnectionString();
                        SQLiteConnection con = new SQLiteConnection(connectionString);
                        string query = $"select НаименованиеУслуги, count(КодЗаявления) as Количество from ЗаявленияНаУслугу where ДатаПодачиЗаявления = '{Year}-10-{Day}' group by НаименованиеУслуги";
                        SQLiteCommand command = new SQLiteCommand(query, con);
                        SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
                        System.Data.DataTable dt = new System.Data.DataTable();
                        ad.Fill(dt);
                        ОтображениеДанных.DataSource = dt;
                    }
                    else if (comboBox_заявленияМесяц.SelectedIndex == 10)
                    {
                        string Year = comboBox_заявленияГод.SelectedItem.ToString();
                        string Day = textBox_ЗаявленияДень.Text;
                        string connectionString = GetConnectionString();
                        SQLiteConnection con = new SQLiteConnection(connectionString);
                        string query = $"select НаименованиеУслуги, count(КодЗаявления) as Количество from ЗаявленияНаУслугу where ДатаПодачиЗаявления = '{Year}-11-{Day}' group by НаименованиеУслуги";
                        SQLiteCommand command = new SQLiteCommand(query, con);
                        SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
                        System.Data.DataTable dt = new System.Data.DataTable();
                        ad.Fill(dt);
                        ОтображениеДанных.DataSource = dt;
                    }
                    else if (comboBox_заявленияМесяц.SelectedIndex == 11)
                    {
                        string Year = comboBox_заявленияГод.SelectedItem.ToString();
                        string Day = textBox_ЗаявленияДень.Text;
                        string connectionString = GetConnectionString();
                        SQLiteConnection con = new SQLiteConnection(connectionString);
                        string query = $"select НаименованиеУслуги, count(КодЗаявления) as Количество from ЗаявленияНаУслугу where ДатаПодачиЗаявления = '{Year}-12-{Day}' group by НаименованиеУслуги";
                        SQLiteCommand command = new SQLiteCommand(query, con);
                        SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
                        System.Data.DataTable dt = new System.Data.DataTable();
                        ad.Fill(dt);
                        ОтображениеДанных.DataSource = dt;
                    }
                }
                else
                {
                    DialogResult result = MessageBox.Show("Заполните поле", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
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
            catch(Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
