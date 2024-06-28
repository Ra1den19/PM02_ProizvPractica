using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace ЦентрЗанятости
{
    public partial class Форма_входа : Form
    {
        public Форма_входа()
        {
            InitializeComponent();
        }

        private void Кнопка_входа_Click(object sender, EventArgs e)
        {
            try
            {
                string login = textBox_логин.Text;
                string password = textBox_пароль.Text;
                if (textBox_логин.Text != "" && textBox_пароль.Text != "")
                {
                    string connectionString = GetConnectionString();
                    SQLiteConnection con = new SQLiteConnection(connectionString);
                    string query = "select * from Пользователи where Логин = @login and Пароль = @password";
                    SQLiteCommand com = new SQLiteCommand(query, con);
                    com.Parameters.AddWithValue("@login", login);
                    com.Parameters.AddWithValue("@password", password);
                    con.Open();
                    SQLiteDataReader read = com.ExecuteReader();
                    if (read.HasRows)
                    {
                        while (read.Read())
                        {
                            string role = read["Роль"].ToString();
                            if (role == "Клиент")
                            {
                                DialogResult result = MessageBox.Show("Авторизация прошла успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ClientForm clientform = new ClientForm();
                                clientform.ShowDialog();
                                textBox_логин.Clear();
                                textBox_пароль.Clear();
                            }
                            else if (role == "Бухгалтер")
                            {
                                DialogResult result = MessageBox.Show("Авторизация прошла успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                AccountantForm accountantform = new AccountantForm();
                                accountantform.ShowDialog();
                                textBox_логин.Clear();
                                textBox_пароль.Clear();
                            }
                            else if (role == "Администратор")
                            {
                                DialogResult result = MessageBox.Show("Авторизация прошла успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                AdminForm adminForm = new AdminForm();
                                adminForm.ShowDialog();
                                textBox_логин.Clear();
                                textBox_пароль.Clear();
                            }
                            else if (role == "Работодатель")
                            {
                                DialogResult result = MessageBox.Show("Авторизация прошла успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                RabotodatelForm rf = new RabotodatelForm();
                                rf.ShowDialog();
                                textBox_логин.Clear();
                                textBox_пароль.Clear();
                            }
                        }
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("Неверный логин или пароль", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBox_логин.Clear();
                        textBox_пароль.Clear();
                    }
                    con.Close();
                }
                else
                {
                    DialogResult result = MessageBox.Show("Введите свои логин и пароль", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex) 
            {
                DialogResult result = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void checkBox_пароль_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBox_пароль.Checked == true)
                {
                    textBox_пароль.UseSystemPasswordChar = false;
                }
                else
                {
                    textBox_пароль.UseSystemPasswordChar = true;
                }
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
    }
}
