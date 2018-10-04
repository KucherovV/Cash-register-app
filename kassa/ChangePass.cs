using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace kassa
{
    public partial class ChangePass : Form
    {
        public ChangePass()
        {
            InitializeComponent();
        }

        private void ChangePass_FormClosing(object sender, FormClosingEventArgs e)
        {
            AdminForm af = (AdminForm)this.Owner;
            af.Enabled = true;
        }

      

        public void VipeFields()
        {
            textBoxCurrentPassword.Clear();
            textBoxNewPasword.Clear();
            textBoxConfirm.Clear();
        }

        private async void buttonConfirmChangePassword_Click(object sender, EventArgs e)
        {
            string connectToPasswords = @"Data Source = DESKTOP-NLAJBQI; Initial Catalog = Kass; Integrated Security = True";
            string sqlCommand = "Select * FROM dbo.Passwords";

            string newPassword = textBoxNewPasword.Text;
            string newPasswordConfirm = textBoxConfirm.Text;

            //Если одно из полей не заполнено
            if (newPassword != "" && newPasswordConfirm != "" && textBoxCurrentPassword.Text != "")
            {
                using (SqlConnection connection = new SqlConnection(connectToPasswords))
                {
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand(sqlCommand, connection);

                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (newPassword == newPasswordConfirm)
                    {
                        if (radioButtonNewCashierPassword.Checked)
                        {
                            await reader.ReadAsync();
                            string currPassword = reader.GetString(1);
                            reader.Close();

                            sqlCommand = "UPDATE Passwords SET Password = @newPassword WHERE Owner = 'Kashier'";

                            if (textBoxCurrentPassword.Text == currPassword)
                            {
                                command.CommandText = sqlCommand;

                                SqlParameter param = new SqlParameter("@newPassword", newPassword);
                                command.Parameters.Add(param);
                                await command.ExecuteNonQueryAsync();

                                MessageBox.Show("Пароль изменен");
                                VipeFields();
                            }
                            else
                            {
                                MessageBox.Show("Неверный текущий пароль");
                                VipeFields();
                            }

                        }

                        if (radioButtonNewAdminPassword.Checked)
                        {
                            await reader.ReadAsync();
                            await reader.ReadAsync();
                            string currPassword = reader.GetString(1);
                            reader.Close();

                            sqlCommand = "UPDATE Passwords SET Password = @newPassword WHERE Owner = 'Admin'";

                            if (textBoxCurrentPassword.Text == currPassword)
                            {
                                command.CommandText = sqlCommand;

                                SqlParameter param = new SqlParameter("@newPassword", newPassword);
                                command.Parameters.Add(param);
                                await command.ExecuteNonQueryAsync();
                                MessageBox.Show("Пароль изменен");
                                VipeFields();
                            }
                            else
                            {
                                MessageBox.Show("Неверный текущий пароль");
                                VipeFields();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Новые пароли не совпадают");
                        VipeFields();
                    }

                }
            }
            else
            {
                MessageBox.Show("Не все поля заполнены.");
                VipeFields();
            }
        }

        
    }
}
