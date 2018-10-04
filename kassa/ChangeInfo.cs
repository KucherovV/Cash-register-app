using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data.SqlClient;
using Newtonsoft.Json;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace kassa
{
    public partial class ChangeInfo : Form
    {
        List<Product> list = new List<Product>();
        List<OneRecipie> listRecipies = new List<OneRecipie>();

        public ChangeInfo(int id)
        {
            InitializeComponent();
            //list = GetSelledList(id);
            listRecipies = GetFullChangeInfo(id);
        
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
            dataGridView2.Columns[2].Width = 400;
            dataGridView2.Columns[3].Width = 40;
            dataGridView2.Columns[4].Width = 40;
            dataGridView2.Columns[5].Width = 70;
            dataGridView2.Columns[6].Width = 200;
            dataGridView2.Columns[7].Width = 150;

            //FillDGV1();
            FillDGV2();
        }

        private void ChangeInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            Changes ch = (Changes)this.Owner;
            ch.Enabled = true;
        }     

        private List<OneRecipie> GetFullChangeInfo(int id)
        {
            string jsonString = "";
            string connectToChanges = @"Data Source = DESKTOP-NLAJBQI; Initial Catalog = Kass; Integrated Security = True";
            string sqlExpr = "SELECT ChangesInfo FROM dbo.Changes";
            List<OneRecipie> list = new List<OneRecipie>();

            using (SqlConnection connection = new SqlConnection(connectToChanges))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpr, connection);
                var reader = command.ExecuteReader();

                for (int i = 0; i <= id; i++)
                {
                    reader.Read();
                    if (i == id)
                    {
                        jsonString = reader.GetString(0);
                    }
                }
                list = JsonConvert.DeserializeObject<List<OneRecipie>>(jsonString);
            }
            return list;
        }

        private void FillDGV2()
        {
            List<Product> DBList = new List<Product>();
            string place;

            for (int i = 0; i < listRecipies.Count; i++)
            {
                string str = listRecipies[i].ProductList;

                DBList = JsonConvert.DeserializeObject<List<Product>>(str);
                str = "";
                foreach (var j in DBList)
                {
                    str += j.Name + " (x" + j.Amount + "), ";
                }
                str = str.TrimEnd(',', ' ');


                if (listRecipies[i].TakeAway)
                    place = "На вынос";
                else
                    place = "В кафе";

                dataGridView2.Rows.Add(listRecipies[i].Id, listRecipies[i].DateTime, str, listRecipies[i].Price,
                    listRecipies[i].PaymentType, place, listRecipies[i].Comment, listRecipies[i].Cashier);
            }
        }              
    }
}
