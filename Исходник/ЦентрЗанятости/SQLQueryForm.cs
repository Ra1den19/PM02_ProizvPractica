using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ЦентрЗанятости
{
    public partial class SQLQueryForm : Form
    {
        public SQLQueryForm()
        {
            InitializeComponent();
        }

        public string GetConnectionString()
        {
            return $@"Data Source = emplcenter.db; Version = 3";
        }

        private void Кнопка_выполненияЗапроса_Click(object sender, EventArgs e)
        {
            try
            {
                if (richTextBox_написатьЗапрос.Text != "")
                {
                    string connectionString = GetConnectionString();
                    SQLiteConnection con = new SQLiteConnection(connectionString);
                    string query = richTextBox_написатьЗапрос.Text;
                    SQLiteCommand com = new SQLiteCommand(query, con);
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(com);
                    DataTable dtshow = new DataTable();
                    ad.Fill(dtshow);
                    ОтображениеДанных.DataSource = dtshow;
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

        private void Кнопка_очисткиЗапроса_Click(object sender, EventArgs e)
        {
            richTextBox_написатьЗапрос.Clear();
        }
    }
}
