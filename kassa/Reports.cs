using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Newtonsoft.Json;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;

namespace kassa
{
    public partial class Reports : Form
    {
        List<Product> list = new List<Product>();

        public enum ReportsType
        {
            selledProducts,
            recipiesPerHour
        }

        public ReportsType currentReportType;

        public Reports()
        {
            InitializeComponent();
            GetWorkers();
            GetChanges();
            GetProducts();
            radioButtonAllPersonal.Checked = true;
        }

        private void Reports_FormClosing(object sender, FormClosingEventArgs e)
        {
            AdminForm af = (AdminForm)this.Owner;
            af.Enabled = true;

        }

        private void radioButtonAllPersonal_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonAllPersonal.Checked)
            {
                checkedListBox1.Enabled = false;
            }
            if(radioButtonSpecificPersonal.Checked)
            {
                checkedListBox1.Enabled = true;
            }
        }
       
        private void GetWorkers ()
        {
            string connectToWorkers = @"Data Source = DESKTOP-NLAJBQI; Initial Catalog = Kass; Integrated Security = True";
            string sqlCommand = "SELECT Name, Surname, IsWorking FROM dbo.Personal";

            using (SqlConnection connection = new SqlConnection(connectToWorkers))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlCommand, connection);
                var reader = command.ExecuteReader();
                
                while (reader.Read())
                {
                    if (reader.GetBoolean(2))
                        checkedListBox1.Items.Add(reader.GetString(0) + " " + reader.GetString(1));
                }               

            }
        }

        private void GetChanges()
        {
            List<string> list = new List<string>();

            HelperClass hc = new HelperClass();
            list = hc.GetChangesList();

            for(int i = 0; i < list.Count; i++)
            {
                checkedListBox2.Items.Add(list[i]);
            }

        }

        private void GetProducts()
        {
            HelperClass hc = new HelperClass();
            List<Product> listProducts = new List<Product>();
            listProducts = hc.FillInfo();

            for(int i = 0; i < listProducts.Count; i++)
            {
                checkedListBox4.Items.Add(listProducts[i].Name);
            }

        }

       

        private void radioButtonAllProducts_MouseClick(object sender, MouseEventArgs e)
        {       
            if (radioButtonAllProducts.Checked && checkedListBox4.Items.Count != 0)
            {
                for (int i = 0; i < checkedListBox4.Items.Count; i++)
                {
                    checkedListBox4.SetItemChecked(i, true);
                }
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonAcceptFilters_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();

            dataGridView1.ColumnCount = 2;
            dataGridView1.Columns[0].Name = "Товар";
            dataGridView1.Columns[1].Name = "Количество";

            HelperClass hc = new HelperClass();


            List<string> personal = new List<string>();
            List<int> changesId = new List<int>();
            List<string> products = new List<string>();

            for(int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if(checkedListBox1.GetItemChecked(i))
                {
                    personal.Add(checkedListBox1.Items[i].ToString());
                }
            }
            
            for(int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                if(checkedListBox2.GetItemChecked(i))
                {
                    changesId.Add(i);
                }
            }

            for(int i = 0; i < checkedListBox4.Items.Count; i++)
            {
                if(checkedListBox4.GetItemChecked(i))
                {
                    products.Add(checkedListBox4.Items[i].ToString());
                }
            }
            
            List<Product> list = hc.GetRecipiesFiltratedByChanges(personal, changesId, products);

            for(int i = 0; i < list.Count; i++)
            {
                dataGridView1.Rows.Add(list[i].Name, list[i].Amount);
            }

            hc.PrintReport(dataGridView1, personal, changesId, products);

            MessageBox.Show("Отчет сохранен");
        }


    }
}
