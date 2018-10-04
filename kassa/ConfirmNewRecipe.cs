using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace kassa
{
    public partial class ConfirmNewRecipe : Form
    {
        public bool success = false;
        public List<Product> listRecipies = new List<Product>();
        List<string> listBoxFinalStrings = new List<string>();

        public int id = 0;
        public DateTime datetime;
        public List<Product> list;
        public int price;
        public string paymentType;
        public bool takeAway;
        public string Comment;
        public bool closed;
        public string cashier;
       

        public ConfirmNewRecipe(bool paymentTypeCash,
            bool takeaway, 
            DateTime dateTime, 
            string comment, 
            string user, 
            List<Product> listRecipies,
            int totalSum)
        {
            InitializeComponent();

            maskedTextBox1.Focus();

            //Копируем значения
            cashier = user;
            datetime = dateTime;
            list = listRecipies;
            price = totalSum;
            if (paymentTypeCash)
                paymentType = "Cash";
            else
                paymentType = "Card";

            takeAway = takeaway;
            Comment = comment;

            textBoxTime.Text = dateTime.ToString();
            textBoxPrice.Text = totalSum.ToString() + " грн.";

            if(paymentTypeCash)
            {
                textBoxPaymentType.Text = "Наличные.";
            }
            else
            {
                textBoxPaymentType.Text = "Кредитная карта.";
            }

            if(!takeaway)
            {
                textBoxPlace.Text = "В заведении.";
            }
            else
            {
                textBoxPlace.Text = "С собой.";
            }

            if (comment != null)
            {
                textBoxComment.Text = comment;
            }

            for(int i = 0; i < listRecipies.Count; i++)
            {
                listBoxFinal.Items.Add(listRecipies[i].Name +  " (x" + listRecipies[i].Amount + ") = " + listRecipies[i].Price * listRecipies[i].Amount + " грн");
            }
        }

        private async void buttonConfirm_Click(object sender, EventArgs e)
        {
            HelperClass hc = new HelperClass();
            
            //Преобразование списка продуктов в строку
            string listString = JsonConvert.SerializeObject(list);
           
            closed = true;

            string connectToRecipies = @"Data Source = DESKTOP-NLAJBQI; Initial Catalog = Kass; Integrated Security = True";
            string sqlCommand = "INSERT INTO Recipes(Id, Datetime, ProductList, Price, PaymentType, TakeAway, Comment, Cashier)" +     
            " VALUES(@Id, @Datetime, @ProductList, @Price, @PaymentType, @TakeAway, @Comment, @Cashier)";
            
            using(SqlConnection connection = new SqlConnection(connectToRecipies))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand("SELECT * FROM dbo.Recipes", connection);

                var reader = await command.ExecuteReaderAsync();
                if(reader.HasRows)
                {
                    reader.Close();
                    command.CommandText = "SELECT MAX(Id) FROM dbo.Recipes";
                    id = Convert.ToInt32( await command.ExecuteScalarAsync());
                    id++;
                }
                else
                {
                    id = 0;
                }
                reader.Close();

                command.CommandText = sqlCommand;
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Datetime", datetime);
                command.Parameters.AddWithValue("@ProductList", listString);
                command.Parameters.AddWithValue("@Price", price);
                command.Parameters.AddWithValue("@PaymentType", paymentType);
                command.Parameters.AddWithValue("@TakeAway", takeAway);
                command.Parameters.AddWithValue("@Comment", Comment);
                command.Parameters.AddWithValue("@Cashier", cashier);
              
                await command.ExecuteNonQueryAsync();
            }

            
            foreach (var i in listBoxFinal.Items)
            {
                listBoxFinalStrings.Add(i.ToString());
            }

            //печать чека
            hc.PrintRecipie(datetime, price, paymentType, takeAway, listBoxFinalStrings, cashier);
            hc.SendRecipieToPrinter();


            CreateNewRecipie cr = (CreateNewRecipie)this.Owner;
            cr.Enabled = true;
            this.Hide();
            success = true;
        }

        private void buttonCansel_Click(object sender, EventArgs e)
        {
            CreateNewRecipie cr = (CreateNewRecipie)this.Owner;
            cr.Enabled = true;
            this.Hide();
            success = false;
        }     

        private void ConfirmNewRecipe_FormClosing(object sender, FormClosingEventArgs e)
        {
            CreateNewRecipie cr = (CreateNewRecipie)this.Owner;
            cr.Enabled = true;
            this.Hide();

        }
      
        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (maskedTextBox1.Text.Length != 0)
            {
                int rest = int.Parse(maskedTextBox1.Text) - price;
                if (rest < 0)
                {
                    textBoxRest.Text = "Недостаточно";
                    textBoxRest.ForeColor = Color.Red;
                }
                else
                {
                    textBoxRest.ForeColor = Color.Black;
                    textBoxRest.Text = rest.ToString();
                }
            }
        }

        private void ConfirmNewRecipe_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F7)
            {
                buttonConfirm_Click(sender, e);
            }
        }

        private void buttonConfirm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
            {
                buttonConfirm_Click(sender, e);
            }
        }
    }
}
