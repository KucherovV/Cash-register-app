using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace kassa
{
    public partial class PrintSettings : Form
    {
        public PrintSettings()
        {
            InitializeComponent();
        }

        private void PrintSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            AdminForm af = (AdminForm)this.Owner;
            af.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectToChanges = @"Data Source = DESKTOP-NLAJBQI; Initial Catalog = Kass; Integrated Security = True";
            string sqlExpr = "UPDATE dbo.PrinterSettings SET Width = @newValue WHERE Width = @oldValue";
            bool success = int.TryParse(textBox1.Text, out int result);
            int newValue = 0;
            if(success)
            {
                newValue = result;
            }
            else
            {
                MessageBox.Show("Некорректные данные");
                return;
            }

            using(SqlConnection connection = new SqlConnection(connectToChanges))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT Width FROM dbo.PrinterSettings", connection);
                var reader = command.ExecuteReader();
                reader.Read();
                int oldValue = Convert.ToInt32(reader.GetInt32(0));
                reader.Close();

                command.CommandText = sqlExpr;

                command.Parameters.AddWithValue("@newValue", newValue);
                command.Parameters.AddWithValue("@oldValue", oldValue);
                command.ExecuteNonQuery();

                MessageBox.Show("Значение ширины обновлено");
                this.Close();
            }
        }      

        private void button2_Click(object sender, EventArgs e)
        {
            string connectToPrinter = @"Data Source = DESKTOP-NLAJBQI; Initial Catalog = Kass; Integrated Security = True";
            string sqlExpr = "UPDATE dbo.PrinterSettings SET Path = @newValue WHERE Path = @oldValue";

            string newValue = textBox2.Text;
            string oldValue;

            using (SqlConnection connection = new SqlConnection(connectToPrinter))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT Path FROM dbo.PrinterSettings", connection);
                var reader = command.ExecuteReader();
                reader.Read();
                oldValue = reader.GetString(0);
                reader.Close();

                command.CommandText = sqlExpr;
                command.Parameters.AddWithValue("@newValue", newValue);
                command.Parameters.AddWithValue("@oldValue", oldValue);
                command.ExecuteNonQuery();

                MessageBox.Show("Значение пути обновлено.");
                this.Close();
            }
        }
    }
}
