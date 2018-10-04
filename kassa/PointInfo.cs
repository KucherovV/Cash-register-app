using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace kassa
{
    public partial class PointInfo : Form
    {
        public PointInfo()
        {
            InitializeComponent();
        }

        private void PointInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            AdminForm af = (AdminForm)this.Owner;
            af.Enabled = true;
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text.Length != 0 && textBoxAdress.Text.Length != 0 && textBoxPhone.Text.Length != 0)
            {
                if (textBoxName.Text.Length < 30 && textBoxAdress.Text.Length < 30 && textBoxPhone.Text.Length < 30)
                {
                    string name = textBoxName.Text;
                    string adress = textBoxAdress.Text;
                    string phone = textBoxPhone.Text;

                    string connectToPointInfo = @"Data Source = DESKTOP-NLAJBQI; Initial Catalog = Kass; Integrated Security = True";
                    string sqlExpr = "UPDATE dbo.PointInfo SET PointName = @pointName, PointAddress = @pointAddress, PointPhone = @pointPhone WHERE Id = '0'";

                    using (SqlConnection connection = new SqlConnection(connectToPointInfo))
                    {
                        connection.Open();

                        SqlCommand command = new SqlCommand(sqlExpr, connection);
                        command.Parameters.AddWithValue("@pointName", name);
                        command.Parameters.AddWithValue("@pointAddress", adress);
                        command.Parameters.AddWithValue("@pointPhone", phone);
                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Информация обновлена");

                }
                else
                {
                    MessageBox.Show("Слишком длинные данные.");
                }
            }
            else
            {
                MessageBox.Show("Пустые поля");
            }
        }
    }
}
