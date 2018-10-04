using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace kassa
{
    public partial class WorkingWithPersonal : Form
    {
        public bool nameFine, surnameFine, positionFine, phoneNumberFine;

        public WorkingWithPersonal()
        {
            InitializeComponent();

            dataGridView1.ColumnCount = 5;
            dataGridView1.Columns[0].Name = "Id";
            dataGridView1.Columns[1].Name = "Имя";
            dataGridView1.Columns[2].Name = "Фамилия";
            dataGridView1.Columns[3].Name = "Должность";
            dataGridView1.Columns[4].Name = "Номер телефона";

            dataGridView1.Columns[0].Width = 40;
            GetWorkers();
        }

        private void WorkingWithPersonal_FormClosing(object sender, FormClosingEventArgs e)
        {
            AdminForm af = (AdminForm)this.Owner;
            af.Enabled = true;
        }  

        private async void buttonAdd_Click(object sender, EventArgs e)
        {
            int id = 0;
            string name = textBoxName.Text;
            string surname = textBoxSurname.Text;
            string position = textBoxPosition.Text;
            string phone = maskedTextBoxPhone.Text;

            string connectToPersonal = @"Data Source = DESKTOP-NLAJBQI; Initial Catalog = Kass; Integrated Security = True";
            string sqlProcedure = "INSERT INTO dbo.Personal (Id, Name, Surname, Position, PhoneNumber, IsWorking)" +
            " VALUES(@id, @name, @surname, @position, @phoneNumber, @isWorking)";
            string sqlCommand = "SELECT MAX(Id) FROM dbo.Personal";

            using (SqlConnection connection = new SqlConnection(connectToPersonal))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand("SELECT * FROM dbo.Personal", connection);
                var reader = await command.ExecuteReaderAsync();

                await reader.ReadAsync();
                if(reader.HasRows)
                {
                    reader.Close();
                    command.CommandText = sqlCommand;
                    id = Convert.ToInt32(command.ExecuteScalar());
                    id++;
                }
                else
                {
                    id = 0;
                }
                reader.Close();

                command.CommandText = sqlProcedure;

                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@surname", surname);
                command.Parameters.AddWithValue("@position", position);
                command.Parameters.AddWithValue("@phoneNumber", phone);
                command.Parameters.AddWithValue("@isWorking", true);
              
                await command.ExecuteNonQueryAsync();

            }

            MessageBox.Show("Новый сотрудник добавлен.");

            GetWorkers();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id = dataGridView1.CurrentRow.Index;

            string connectToPersonal = @"Data Source = DESKTOP-NLAJBQI; Initial Catalog = Kass; Integrated Security = True";
            string sqlExpr = "UPDATE dbo.Personal SET IsWorkig = '0' WHERE Id = @id";

            using(SqlConnection connection = new SqlConnection(connectToPersonal))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpr, connection);

                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();

            }
            GetWorkers();
        }


        public void CheckAddButton()
        {
            if (nameFine && surnameFine && positionFine && phoneNumberFine)
            {
                buttonAdd.Enabled = true;
                labelName.Visible = false;
                labelSurname.Visible = false;
                labelPosition.Visible = false;
                labelPhone.Visible = false;
            }
            else
            {
                buttonAdd.Enabled = false;

                if(!nameFine && textBoxName.Text.Length != 0)
                {
                    labelName.Visible = true;
                    labelName.Text = "Слишком длинное имя.";
                }
                else 
                {
                    labelName.Visible = false;
                }

                if (!surnameFine && textBoxSurname.Text.Length != 0)
                {
                    labelSurname.Visible = true;
                    labelSurname.Text = "Слишком длинная фамилия.";
                }
                else 
                {
                    labelSurname.Visible = false;
                }

                if(!positionFine && textBoxPosition.Text.Length != 0)
                {
                    labelPosition.Visible = true;
                    labelPosition.Text = "Слишком длинная должность.";
                }
                else
                {
                    labelPosition.Visible = false;
                }

                if(!phoneNumberFine)
                {
                    labelPhone.Visible = true;
                    labelPhone.Text = "Некоректный номер телефона.";
                }
                else
                {
                    labelPhone.Visible = false;
                }
            }
        }

        private void GetWorkers()
        {
            dataGridView1.Rows.Clear();
            string connectToPersonal = @"Data Source = DESKTOP-NLAJBQI; Initial Catalog = Kass; Integrated Security = True";
            string sqlExpr = "SELECT * FROM dbo.Personal";
            int i = 0;

            using (SqlConnection connection = new SqlConnection(connectToPersonal))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpr, connection);
                var reader = command.ExecuteReader();


                while (reader.Read())
                {

                    dataGridView1.Rows.Add(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                            reader.GetString(3), reader.GetString(4));

                    if (!reader.GetBoolean(5))
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LightPink;
                    }

                        i++;
                    
                        
                }

            }

        }

        #region events

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            if (textBoxName.Text.Length != 0)
            {
                if (textBoxName.Text.Length > 15)
                    nameFine = false;

                else               
                    nameFine = true;               
            }
            else
                nameFine = false;           

            CheckAddButton();
        }      

        private void textBoxName_KeyDown(object sender, KeyEventArgs e)
        {
            if(textBoxName.Text.Length != 0 && e.KeyCode == Keys.Back)
            {
                string temp = textBoxName.Text;
                temp = temp.Remove(temp.Length - 1, 0);
                textBoxName.Text = temp;
                textBoxName.SelectionStart = temp.Length;              
            }
        }      

        private void textBoxSurname_TextChanged(object sender, EventArgs e)
        {
            if (textBoxSurname.Text.Length != 0)
            {
                if (textBoxSurname.Text.Length > 15)
                    surnameFine = false;

                else
                    surnameFine = true;  
            }
            else
                surnameFine = false;

            CheckAddButton();
        }
   
        private void textBoxSurname_KeyDown(object sender, KeyEventArgs e)
        {
            if(textBoxSurname.Text.Length != 0 && e.KeyCode == Keys.Back)
            {
                string temp = textBoxSurname.Text;
                temp = temp.Remove(temp.Length - 1, 0);
                textBoxSurname.Text = temp;
                textBoxSurname.SelectionStart = temp.Length;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        

        private void textBoxPosition_TextChanged(object sender, EventArgs e)
        {
            if (textBoxPosition.Text.Length != 0)
            {
                if (textBoxPosition.Text.Length > 15)
                    positionFine = false;

                else
                    positionFine = true;
            }
            else
                positionFine = false;

            CheckAddButton();
        }

        private void textBoxPosition_KeyDown(object sender, KeyEventArgs e)
        {
            if(textBoxPosition.Text.Length != 0 && e.KeyCode == Keys.Back)
            {
                string temp = textBoxPosition.Text;
                temp = temp.Remove(temp.Length - 1, 0);
                textBoxPosition.Text = temp;
                textBoxPosition.SelectionStart = temp.Length;
            }
        }

        private void maskedTextBoxPhone_TextChanged(object sender, EventArgs e)
        {
            if (maskedTextBoxPhone.MaskCompleted)
            {
                phoneNumberFine = true;
            }
            else
                phoneNumberFine = false;

            CheckAddButton();
        }

        #endregion
    }
}
