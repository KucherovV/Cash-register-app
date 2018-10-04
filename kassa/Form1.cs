using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace kassa
{
    public partial class StartForm : Form
    {
        string connectToPasswords = @"Data Source = DESKTOP-NLAJBQI; Initial Catalog = Kass; Integrated Security = True";
       
        public StartForm()
        {
            InitializeComponent();
            FillWorkers();
        }

        //Обраьотка авторизации
        private async void buttonLogin_Click(object sender, EventArgs e)
        {
            string sqlExpr = "SELECT * FROM dbo.Passwords";
            string inputPassword = textBoxPassword.Text;
            string user;

            using (SqlConnection connection = new SqlConnection(connectToPasswords))
            {
                await connection.OpenAsync();

                if(connection.State == ConnectionState.Broken || connection.State == ConnectionState.Closed)
                {
                    MessageBox.Show("Отсутствует подключение к БД.");
                    Application.Exit();
                }

                SqlCommand command = new SqlCommand(sqlExpr, connection);

                var reader = await command.ExecuteReaderAsync();

                if (radioButtonCashier.Checked && comboBoxWorkers.SelectedIndex != -1)
                {
                    await reader.ReadAsync();
                    string cashierPassword = reader.GetString(1);

                    if (inputPassword == cashierPassword)
                    {
                        user = comboBoxWorkers.SelectedItem.ToString();
                        Hide();
                        CashierForm cf = new CashierForm(user);
                        cf.Show();
                    }
                    else
                    {
                        MessageBox.Show("Wrong password");
                    }
                }
                else
                    label2.Visible = true;

                //Авторизация как Админ
                if (radioButtonAdmin.Checked)
                {
                    label2.Visible = false;
                    await reader.ReadAsync(); await reader.ReadAsync();
                    string adminPassword = reader.GetString(1);

                    if (inputPassword == adminPassword)
                    {
                        //MessageBox.Show("Logined as admin");
                        Hide();
                        AdminForm af = new AdminForm();
                        af.Show();
                    }
                    else
                    {
                        MessageBox.Show("Wrong password");
                    }
                }
            }
        
        }

        //Обработка клавиш поля ввода пароля
        private void textBoxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                textBoxPassword.Text = "";
            }

            if(e.KeyCode == Keys.Back)
            {
                if (textBoxPassword.Text.Length != 0)
                {
                    string reducedPassword = textBoxPassword.Text;
                    reducedPassword.Remove(reducedPassword.Length - 1, 1);
                    textBoxPassword.Text = reducedPassword;
                }
            }

            if(e.KeyCode == Keys.Enter)
            {
                buttonLogin_Click(sender, e);
            }
        }

       private async void FillWorkers()
        {
            string connectToWorkers = @"Data Source = DESKTOP-NLAJBQI; Initial Catalog = Kass; Integrated Security = True";
            string sqlCommand = "SELECT Name, Surname, IsWorking FROM dbo.Personal";

            using (SqlConnection connection = new SqlConnection(connectToWorkers))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlCommand, connection);
                var reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while(await reader.ReadAsync())
                    {
                        if(reader.GetBoolean(2))
                        comboBoxWorkers.Items.Add(reader.GetString(0) + " " + reader.GetString(1));
                    }
                }
                
            }
        }

       private void radioButtonCashier_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButtonCashier.Checked)
            {
                comboBoxWorkers.Enabled = true;
            }
            if(radioButtonAdmin.Checked)
            {
                comboBoxWorkers.Enabled = false;
                label2.Visible = false;
            }
        }

        private void StartForm_Load(object sender, EventArgs e)
        {

        }
    }
}
