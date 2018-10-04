using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace kassa
{
    public partial class Changes : Form
    {

        public Changes()
        {
            InitializeComponent();

            dataGridView1.ColumnCount = 6;
            dataGridView1.Columns[0].Name = "Id";
            dataGridView1.Columns[1].Name = "Время открытия";
            dataGridView1.Columns[2].Name = "Время закрытия";
            dataGridView1.Columns[3].Name = "Закодированая информация о смене";
            dataGridView1.Columns[4].Name = "Закодированый список проданых товаров";
            dataGridView1.Columns[5].Name = "Выручка";
         
            dataGridView1.Columns[0].Width = 30;
            dataGridView1.Columns[1].Width = 120;
            dataGridView1.Columns[2].Width = 120;
            dataGridView1.Columns[3].Width = 230;
            dataGridView1.Columns[4].Width = 230;

            FillDGV();
        }

        private void Changes_FormClosing(object sender, FormClosingEventArgs e)
        {
            AdminForm af = (AdminForm)this.Owner;
            af.Enabled = true;
        }


        private void OpenChange()
        {
            int id = 0;
            HelperClass hc = new HelperClass();
            if(hc.CheckIfChangeOpen())
            {
                MessageBox.Show("Смена уже создана.");
                return;
            }
            else
            {
                id = hc.GetIdCurrentChange();

                if (id == -1)
                    id = 0;
                else
                    id++;
            }

            string connectToChanges = @"Data Source = DESKTOP-NLAJBQI; Initial Catalog = Kass; Integrated Security = True";
            string sqlCommand = "INSERT INTO dbo.Changes (Id, DateTimeOpen) VALUES (@Id, @DateTimeOpen)";
            DateTime dateTime = DateTime.Now.ToLocalTime();

            using (SqlConnection connection = new SqlConnection(connectToChanges))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlCommand, connection);          

                command.Parameters.AddWithValue("@Id", id);

                command.Parameters.AddWithValue("@DateTimeOpen", dateTime);

                command.ExecuteNonQueryAsync();

                MessageBox.Show("Смена успешно создана.");
            }

        }

        private void CloseChange()
        {
            HelperClass hc = new HelperClass();

            if (hc.CheckIfChangeOpen())
            {

                CreateChangesInfoFinal();
                CountAmountSelled();
                CountProceed();
                SetDateTimeCLose();
                TruncateTable();

                MessageBox.Show("Смена " + hc.GetIdCurrentChange().ToString() + " успешно закрыта.");

                dataGridView1.Rows.Clear();
                FillDGV();
            }
            else
            {
                MessageBox.Show("Сначала откройте смену");
            }
        }

        private void CreateChangesInfoFinal()
        {
            HelperClass hc = new HelperClass();
            if(!hc.CheckIfChangeOpen())
            {
                MessageBox.Show("Сначала откройте смену.");
                return;
            }

            string connectToChanges = @"Data Source = DESKTOP-NLAJBQI; Initial Catalog = Kass; Integrated Security = True";
            string sqlCommand = "UPDATE dbo.Changes SET ChangesInfo = @list WHERE Id = @id";
            int id = hc.GetIdCurrentChange();

            using (SqlConnection connection = new SqlConnection(connectToChanges))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlCommand, connection);

                string str = CreateChangesInfo();

                SqlParameter listParam = new SqlParameter("@list", str);
                command.Parameters.Add(listParam);
                SqlParameter idParam = new SqlParameter("@id", id);
                command.Parameters.Add(idParam);

                command.ExecuteNonQuery();
                    
               
            }
        }

        private string CreateChangesInfo()
        {
            string connectToRecipies = @"Data Source = DESKTOP-NLAJBQI; Initial Catalog = Kass; Integrated Security = True";
            string sqlCommand = "SELECT * FROM dbo.Recipes";

            int id;
            DateTime dateTime;
            string productList;
            int price;
            string paymentType;
            bool takeAway;
            string comment;
            string cashier;

            List<OneRecipie> serialized = new List<OneRecipie>();

            using (SqlConnection conection = new SqlConnection(connectToRecipies))
            {
                conection.Open();
                SqlCommand command = new SqlCommand(sqlCommand, conection);
                var reader = command.ExecuteReader();
                
                if(reader.HasRows)
                {
                    while (reader.Read())
                    {
                        id = reader.GetInt32(0);
                        dateTime = reader.GetDateTime(1);
                        productList = reader.GetString(2);
                        price = reader.GetInt32(3);
                        paymentType = reader.GetString(4);
                        takeAway = reader.GetBoolean(5);
                        comment = reader.GetString(6);
                        cashier = reader.GetString(7);

                        OneRecipie oneRecipie = new OneRecipie(id, dateTime, productList, price, paymentType, takeAway, comment, cashier);
                        serialized.Add(oneRecipie);
                    }
                }

            }
            string test = JsonConvert.SerializeObject(serialized);
            return test;
        }
      
        private void SetDateTimeCLose()
        {
            HelperClass hc = new HelperClass();
            int id = hc.GetIdCurrentChange();
            string connectToRecipies = @"Data Source = DESKTOP-NLAJBQI; Initial Catalog = Kass; Integrated Security = True";
            string sqlExpr = "UPDATE dbo.Changes SET DateTimeClose = @time WHERE Id = @id"; 

            using (SqlConnection connection = new SqlConnection(connectToRecipies))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpr, connection);

                DateTime time = DateTime.Now.ToLocalTime();

                command.Parameters.AddWithValue("@time", time);
                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();
            }
        }

        private void CountAmountSelled()
        {
            HelperClass hc = new HelperClass();
            List<OneRecipie> listObjects = new List<OneRecipie>();
            List<Product> finalproductList = new List<Product>();
            finalproductList = hc.FillInfo();

            string connectToChanges = @"Data Source = DESKTOP-NLAJBQI; Initial Catalog = Kass; Integrated Security = True";

            int id = hc.GetIdCurrentChange();
            string jsonString = "";
            List<Product> temp;
            

            using(SqlConnection connection = new SqlConnection(connectToChanges))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT ChangesInfo FROM dbo.Changes", connection);
                var reader = command.ExecuteReader();

                for(int i = 0; i <= id; i++)
                {
                    reader.Read();
                    if(i == id)
                    {
                        jsonString = reader.GetString(0);
                    }
                }
                reader.Close();
                //список со всеми чеками
                listObjects = JsonConvert.DeserializeObject<List<OneRecipie>>(jsonString);

                for(int i = 0; i < listObjects.Count; i++)
                {                  
                    //список товаров в одном заказе
                    temp = JsonConvert.DeserializeObject<List<Product>>(listObjects[i].ProductList);
                    
                    //Для каждого обьекта в распакованом списке
                    for(int productJson = 0; productJson < temp.Count; productJson++)
                    {
                        //Для каждого обьекта в исходном списке
                        for(int productInList = 0; productInList < finalproductList.Count; productInList++)
                        {
                            //Если названия совпадают
                            if(temp[productJson].Name == finalproductList[productInList].Name)
                            {
                                //То увеличиваем количество данного товара в ихсодном списке
                                finalproductList[productInList].Amount += temp[productJson].Amount;
                                goto mark1;
                            }
                        }
                        mark1:;
                    }
                    
                }

                List<Product> backupList = new List<Product>();

                for(int i = 0; i < finalproductList.Count; i++)
                {
                    if(finalproductList[i].Amount != 0)
                    {
                        backupList.Add(finalproductList[i]);
                    }
                }
                finalproductList.Clear();
                finalproductList = backupList;
                
                finalproductList.Sort(hc.Compare);
              
                string returned =  JsonConvert.SerializeObject(finalproductList);
               
                command.CommandText = "UPDATE dbo.Changes SET AmountSelled = @returned WHERE Id = @id";
                command.Parameters.AddWithValue("@returned", returned);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }


        }

        private void CountProceed()
        {
            string connectToChanges = @"Data Source = DESKTOP-NLAJBQI; Initial Catalog = Kass; Integrated Security = True";
            HelperClass hc = new HelperClass();
            int id = hc.GetIdCurrentChange();
            string jsonString = "";
            int sum = 0;

            List<OneRecipie> listObjects = new List<OneRecipie>();

            using (SqlConnection connection = new SqlConnection(connectToChanges))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT ChangesInfo FROM dbo.Changes", connection);
                var reader = command.ExecuteReader();

                for (int i = 0; i <= id; i++)
                {
                    reader.Read();
                    if (i == id)
                    {
                        jsonString = reader.GetString(0);
                    }
                }
                reader.Close();

                listObjects = JsonConvert.DeserializeObject<List<OneRecipie>>(jsonString);

                for(int i = 0; i < listObjects.Count; i++)
                {
                    sum += listObjects[i].Price;
                }



                command.CommandText = "UPDATE dbo.Changes SET Proceed = @sum WHERE Id = @id";
                command.Parameters.AddWithValue("@sum", sum);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();


            }

        }

        private void TruncateTable()
        {
            string connectToRecipies = @"Data Source = DESKTOP-NLAJBQI; Initial Catalog = Kass; Integrated Security = True";
            string sqlCommand = "TRUNCATE TABLE dbo.Recipes";

            using(SqlConnection connection = new SqlConnection(connectToRecipies))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlCommand, connection);
                command.ExecuteNonQuery();

            }
        }

        private void FillDGV()
        {
            HelperClass hc = new HelperClass();
            int id = hc.GetIdCurrentChange();

            if(hc.CheckIfChangeOpen())
            {
                id--;
            }


            string connectToChanges = @"Data Source = DESKTOP-NLAJBQI; Initial Catalog = Kass; Integrated Security = True";
            string sqlExpr = "SELECT * FROM dbo.Changes";

            using (SqlConnection connection = new SqlConnection(connectToChanges))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpr, connection);
                var reader = command.ExecuteReader();

                for(int i = 0; i <= id; i++)
                {
                    reader.Read();
                    dataGridView1.Rows.Add(reader.GetInt32(0), reader.GetDateTime(1), reader.GetDateTime(2), reader.GetString(3),
                        reader.GetString(4), reader.GetInt32(5));
                }

            }

        }

        private void открытьСменуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChange();
        }

        private void закрытьСменуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseChange();
        }

        private void получитьПодробнуюИнформациюОСменеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int id = dataGridView1.CurrentCell.RowIndex;
                ChangeInfo ci = new ChangeInfo(id);
                ci.Owner = this;
                this.Enabled = false;
                ci.Show();
            }
            catch(NullReferenceException)
            {
                MessageBox.Show("Нет закрытых смен.");
            }
        }    
    }
}
