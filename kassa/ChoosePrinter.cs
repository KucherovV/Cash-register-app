using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace kassa
{
    public partial class ChoosePrinter : Form
    {
        public ChoosePrinter()
        {
            InitializeComponent();
            GetPrinters();
        }

        private void ChoosePrinter_FormClosing(object sender, FormClosingEventArgs e)
        {
            AdminForm af = (AdminForm)this.Owner;
            af.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectToPrinter = @"Data Source = DESKTOP-NLAJBQI; Initial Catalog = Kass; Integrated Security = True";
            string sqlCommand = "SELECT PrinterName FROM dbo.PrinterSettings";

            int index = listBox1.SelectedIndex;
            string choosenPrinter = listBox1.Items[index].ToString();
            string oldValue;
        

            using (SqlConnection connection = new SqlConnection(connectToPrinter))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlCommand, connection);
                var reader = command.ExecuteReader();
                reader.Read();
                oldValue = reader.GetString(0);
                reader.Close();

                command.CommandText = "UPDATE dbo.PrinterSettings SET PrinterName = @name WHERE PrinterName = @oldValue";
                command.Parameters.AddWithValue("@name", choosenPrinter);
                command.Parameters.AddWithValue("@oldValue", oldValue);
                command.ExecuteNonQuery();

            }

            MessageBox.Show("Принтер успешно выбран.");
        }

        private void GetPrinters()
        {          
            foreach(string name in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                listBox1.Items.Add(name);
            }

        }
    }
}
