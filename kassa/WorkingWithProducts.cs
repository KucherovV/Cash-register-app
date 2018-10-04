using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace kassa
{
    public partial class WorkingWithProducts : Form
    {
        List<string> list = new List<string>();
        string nameSelected;
        
        public WorkingWithProducts()
        {
            InitializeComponent();
            GetProducts();

            dataGridView1.ColumnCount = 2;
            dataGridView1.Columns[0].Name = "Название";
            dataGridView1.Columns[1].Name = "Цена";
        }

        private async void buttonAddProduct_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text.Length != 0 && textBoxPrice.Text.Length != 0)
            {
                string connectToProducts = @"Data Source = DESKTOP-NLAJBQI; Initial Catalog = Kass; Integrated Security = True";
                string sqlProcedure = "SELECT * FROM dbo.Product WHERE Name LIKE @str";
                string name = textBoxName.Text;
                int priceRes;
                int price;

                name = name.ToLower();

                char lastNameChar = name[name.Length - 1];
                if (Char.IsDigit(lastNameChar))
                {
                    MessageBox.Show("Название не может заканчиваться цифрой.");
                    textBoxName.Clear();
                    textBoxPrice.Clear();
                    return;
                }

                bool result = int.TryParse(textBoxPrice.Text, out priceRes);

                if (result)
                {
                    price = priceRes;
                }
                else
                {
                    MessageBox.Show("Некоректное значение.");
                    textBoxName.Clear();
                    textBoxPrice.Clear();
                    return;
                }

                using (SqlConnection connection = new SqlConnection(connectToProducts))
                {
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand(sqlProcedure, connection);

                    SqlParameter param = new SqlParameter("@str", name);
                    command.Parameters.Add(param);

                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        MessageBox.Show("Товар с таким названием уже есть в базе данных.");
                        textBoxName.Clear();
                        textBoxPrice.Clear();
                        return;
                    }
                    reader.Close();

                   
                    if (name.Length < 15 && price <= 9999)
                    {
                        sqlProcedure = "INSERT INTO Product (Name, Price) VALUES(@name, @price)";
                        command.CommandText = sqlProcedure;
                        command.Parameters.Remove(param);

                        SqlParameter nameParam = new SqlParameter("@name", name);
                        command.Parameters.Add(nameParam);

                        SqlParameter priceParam = new SqlParameter("price", price);
                        command.Parameters.Add(priceParam);

                        await command.ExecuteScalarAsync();
                        MessageBox.Show("Товар добавлен.");

                        textBoxName.Clear();
                        textBoxPrice.Clear();

                    }
                    else
                    {
                        MessageBox.Show("Слишком большие значения.");
                        textBoxName.Clear();
                        textBoxPrice.Clear();
                    }
                }
                    
                
                GetProducts();
            }
            else
            {
                MessageBox.Show("Не все поля заполнены.");
                textBoxName.Clear();
                textBoxPrice.Clear();
            }

        }

        private async void buttonDelete_Click(object sender, EventArgs e)
        {
            string connectToProducts = @"Data Source = DESKTOP-NLAJBQI; Initial Catalog = Kass; Integrated Security = True";
            string sqlProcedure = "DELETE FROM dbo.Product WHERE Name = @item";

            using (SqlConnection connection = new SqlConnection(connectToProducts))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlProcedure, connection);

                SqlParameter param = new SqlParameter("@item", nameSelected);
                command.Parameters.Add(param);

                await command.ExecuteNonQueryAsync();
            }

            MessageBox.Show(@"Товар " + nameSelected + " успешно удален");
            GetProducts();
        }

        private void WorkingWithProducts_FormClosing(object sender, FormClosingEventArgs e)
        {
            AdminForm af = (AdminForm)this.Owner;
            af.Enabled = true;
        }

        private void WorkingWithProducts_Load(object sender, EventArgs e)
        {

        }

        public async void GetProducts()
        {
            dataGridView1.Rows.Clear();

            string connectToProducts = @"Data Source = DESKTOP-NLAJBQI; Initial Catalog = Kass; Integrated Security = True";
            string sqlCommand = "SELECT * FROM dbo.Product";

            using (SqlConnection connection = new SqlConnection(connectToProducts))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlCommand, connection);

                var reader = await command.ExecuteReaderAsync();

                if(reader.HasRows)
                {
                    while(await reader.ReadAsync())
                    {
                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetInt32(1));
                    }
                }
                reader.Close();
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            nameSelected = dataGridView1.CurrentRow.Cells[0].Value.ToString();
        }
    }
}
