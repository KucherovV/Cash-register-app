using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data.SqlClient;
using Newtonsoft.Json;


namespace kassa
{
    public partial class CashierForm : Form
    {      
        public string finalRecipelist;     
        
        public bool finalPaymentTypeCash;
        public bool finalTakeAway;

        public string finalComment;
        public bool finalClosed = false;

        public DateTime finalDatetime;

        public string user;

        public int totalSum = 0;

        public CashierForm(string user)
        {
            InitializeComponent();

            AddToDGV();

            //dgv списка чеков
            dataGridView2.ColumnCount = 8;
            dataGridView2.Columns[0].Name = "Id";
            dataGridView2.Columns[1].Name = "Дата/Время";
            dataGridView2.Columns[2].Name = "Список продуктов";
            dataGridView2.Columns[3].Name = "Цена";
            dataGridView2.Columns[4].Name = "Тип оплаты";
            dataGridView2.Columns[5].Name = "Метсо";
            dataGridView2.Columns[6].Name = "Комментарий";
            dataGridView2.Columns[7].Name = "Кассир";

            dataGridView2.Columns[0].Width = 40;
            dataGridView2.Columns[1].Width = 110;
            dataGridView2.Columns[2].Width = 500;
            dataGridView2.Columns[3].Width = 40;
            dataGridView2.Columns[4].Width = 40;
            dataGridView2.Columns[5].Width = 70;
            dataGridView2.Columns[6].Width = 230;
            dataGridView2.Columns[7].Width = 150;

            this.user = user;

        }     

        public async void AddToDGV()
        {
            HelperClass hc = new HelperClass();
            if(!hc.CheckIfChangeOpen())
            {
                MessageBox.Show("Смена не открыта. Откройте ее в меню администратора и повторите попытку.");
                Application.Exit();
            }

            string connectToRecipies = @"Data Source = DESKTOP-NLAJBQI; Initial Catalog = Kass; Integrated Security = True";
            string sqlCommand = "SELECT * FROM dbo.Recipes";
            string place;
            List<Product> DBList = new List<Product>();

            using (SqlConnection connection = new SqlConnection(connectToRecipies))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlCommand, connection);
                var reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        string str = reader.GetString(2);
                        DBList = JsonConvert.DeserializeObject<List<Product>>(str);
                        str = "";
                        foreach(var i in DBList)
                        {
                            str += i.Name + " (x" + i.Amount + "), "; 
                        }
                        str = str.TrimEnd(',', ' ');


                        if (reader.GetBoolean(5))
                            place = "На вынос";
                        else
                            place = "В кафе";
                     
                        dataGridView2.Rows.Add(reader.GetInt32(0), reader.GetDateTime(1), str, reader.GetInt32(3),
                            reader.GetString(4), place, reader.GetString(6), reader.GetString(7));

                    }
                }
                
            }
        }

        private void новыйЧекToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewRecipie cn = new CreateNewRecipie(user);
            cn.Owner = this;
            this.Enabled = false;
            cn.ShowDialog();
            dataGridView2.Rows.Clear();
            AddToDGV();
        }
      
        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                новыйЧекToolStripMenuItem_Click(sender, e);
            }
        }
    }
}
