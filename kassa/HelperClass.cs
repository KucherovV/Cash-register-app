using System;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using RawPrint;
using Newtonsoft.Json;

namespace kassa
{
    class HelperClass : IComparer<Product>
    {
        public async Task<int> GetIdCurrentChangeAsync()
        {
            string connectToChanges = @"Data Source = DESKTOP-NLAJBQI; Initial Catalog = Kass; Integrated Security = True";
            string sqlCommand = "SELECT MAX(Id) FROM dbo.Changes";

            using (SqlConnection connection = new SqlConnection(connectToChanges))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(sqlCommand, connection);

                return Convert.ToInt32(await command.ExecuteScalarAsync());
            }
        }

        public int GetIdCurrentChange()
        {
            string connectToChanges = @"Data Source = DESKTOP-NLAJBQI; Initial Catalog = Kass; Integrated Security = True";
            string sqlCommand = "SELECT MAX(Id) FROM dbo.Changes";
            bool isExeption = false;

            using (SqlConnection connection = new SqlConnection(connectToChanges))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlCommand, connection);

                object temp = command.ExecuteScalar();
                int id = 0;

                try
                {
                    id = Convert.ToInt32(temp);
                }
                catch (Exception)
                {
                    isExeption = true;
                }

                if (isExeption)
                    return -1;
                else return id;

            }   
        }

        public bool CheckIfChangeOpen()
        {
            string connectToChanges = @"Data Source = DESKTOP-NLAJBQI; Initial Catalog = Kass; Integrated Security = True";
            string sqlCommand = "SELECT Id, DateTimeClose FROM dbo.Changes";
            bool isExeption = false;
            DateTime dateTimeClose;
            int id = 0;

            using (SqlConnection connection = new SqlConnection(connectToChanges))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlCommand, connection);

                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Close();
                    command.CommandText = "SELECT MAX(Id) FROM dbo.Changes";

                    id = Convert.ToInt32(command.ExecuteScalar());

                    command.CommandText = sqlCommand;
                    command.ExecuteNonQuery();
                    reader = command.ExecuteReader();

                    for (int i = 0; i <= id; i++)
                    {
                        reader.Read();
                        if (i == id)
                        {
                            try
                            {
                                dateTimeClose = reader.GetDateTime(1);
                            }
                            catch (SqlNullValueException)
                            {
                                isExeption = true;
                            }

                            if (isExeption)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }

                    }

                    return false;
                }
                else
                {
                    return false;
                }

            }
        }

        public async Task<bool> CheckIfChangeOpenAsync()
        {
            string connectToChanges = @"Data Source = DESKTOP-NLAJBQI; Initial Catalog = kass_AllChanges; Integrated Security = True";
            string sqlCommand = "SELECT Id, DateTimeClose FROM dbo.Changes";
            bool isExeption = false;
            DateTime dateTimeClose;
            int id = 0;

            using (SqlConnection connection = new SqlConnection(connectToChanges))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(sqlCommand, connection);

                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Close();
                    command.CommandText = "SELECT MAX(Id) FROM dbo.Changes";

                    id = Convert.ToInt32(await command.ExecuteScalarAsync());

                    command.CommandText = sqlCommand;
                    await command.ExecuteNonQueryAsync();
                    reader = await command.ExecuteReaderAsync();

                    for (int i = 0; i <= id; i++)
                    {
                        await reader.ReadAsync();
                        if (i == id)
                        {
                            try
                            {
                                dateTimeClose = reader.GetDateTime(1);
                            }
                            catch (SqlNullValueException)
                            {
                                isExeption = true;
                            }

                            if (isExeption)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }

                    }

                    return false;
                }
                else
                {
                    return false;
                }

            }
        }

        public List<Product> FillInfo()
        {
            List<Product> productList = new List<Product>();
            string connectToProducts = @"Data Source = DESKTOP-NLAJBQI; Initial Catalog = Kass; Integrated Security = True";
            string sqlCommand = "SELECT * FROM dbo.Product";

            using (SqlConnection connection = new SqlConnection(connectToProducts))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlCommand, connection);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    int i = 0;

                    while (reader.Read())
                    {
                        Product product = new Product(reader.GetString(0), reader.GetInt32(1), 0, i);
                        productList.Add(product);

                        i++;
                    }

                }
                else
                {
                    MessageBox.Show("База данных продуктов пуста.");
                }
                return productList;
            }
        }

        public int Compare(Product first, Product second)
        {
            int i = String.Compare(first.Name, second.Name);
            return i;

        }

        public int GetPrinterWidth()
        {
            string connectToPrinter = @"Data Source = DESKTOP-NLAJBQI; Initial Catalog = Kass; Integrated Security = True";

            using (SqlConnection connection = new SqlConnection(connectToPrinter))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT Width FROM dbo.PrinterSettings", connection);
                var reader = command.ExecuteReader();
                reader.Read();

                double width = Convert.ToDouble(reader.GetInt32(0));
                double widthFloat = width / 0.381;
                return Convert.ToInt32(widthFloat);

            }
        }

        public string GetPath()
        {
            string connectToPrinter = @"Data Source = DESKTOP-NLAJBQI; Initial Catalog = Kass; Integrated Security = True";

            using (SqlConnection connection = new SqlConnection(connectToPrinter))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("Select Path FROM dbo.PrinterSettings", connection);
                var reader = command.ExecuteReader();
                reader.Read();

                return Convert.ToString(reader.GetString(0));
            }
        }

        public bool ifRecipieOpen()
        {
            string path = this.GetPath();
            bool isExeption = false;

            Document doc = new Document();
            try
            {
                doc.Open();
                PdfWriter.GetInstance(doc, new FileStream(path + "/Recipie.pdf", FileMode.Create));
            }
            catch(IOException)
            {
                isExeption = true;
            }
            finally
            {
                doc.Close();
            }
            if(isExeption)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void PrintRecipie(DateTime dateTime, int sum, string paymentType, bool takeAway, List<string> list, string cashier)
        {
            string pointName;
            string pointAdress;
            string pointPhone;
            string place = "";
            if (takeAway)
                place = "С собой";
            else
                place = "В заведении";

            double value = Convert.ToDouble(this.GetPrinterWidth());
            double widthFloat = value / 0.381;
            int width = Convert.ToInt32(widthFloat);
            int height = 180 + list.Count * 13;
            string path = this.GetPath();

            string connectToPoint = @"Data Source = DESKTOP-NLAJBQI; Initial Catalog = Kass; Integrated Security = True";
            using (SqlConnection connection = new SqlConnection(connectToPoint))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM dbo.PointInfo", connection);
                command.ExecuteNonQuery();
                var reader = command.ExecuteReader();

                reader.Read();
                pointName = Convert.ToString(reader.GetString(1));
                pointAdress = Convert.ToString(reader.GetString(2));
                pointPhone = Convert.ToString(reader.GetString(3));
            }


            try
            {
                var pageSize = new Rectangle(width, height);   //check
                Document doc = new Document(pageSize, 10, 10, 10, 10);

                BaseFont bf = BaseFont.CreateFont(@"consola.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font f = new iTextSharp.text.Font(bf, 8);

                PdfWriter.GetInstance(doc, new FileStream(path + "/Recipie.pdf", FileMode.Create));
                doc.Open();

                doc.Add(new Phrase(dateTime.ToString() + "\n", f));
                doc.Add(new Phrase(pointName + "\n", f));
                doc.Add(new Phrase(pointAdress + "\n", f));
                doc.Add(new Phrase(pointPhone + "\n", f));
                doc.Add(new Phrase("Кассир: " + cashier, f));

                doc.Add(new Phrase("_____________ \n"));

                foreach (var i in list)
                {
                    doc.Add(new Phrase(i + "\n", f));
                }

                doc.Add(new Phrase("_____________ \n"));
                doc.Add(new Phrase("Тип оплаты: " + paymentType + "\n", f));
                doc.Add(new Phrase(place + "\n", f));
                doc.Add(new Phrase("Сумма заказа: " + sum.ToString() + "\n", f));
                doc.Add(new Phrase("Спасибо за покупку!", f));

                doc.Close();
            }
            catch(IOException ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        public void PrintReport(DataGridView dgv, List<string> cashiers, List<int> changes, List<string> products)
        {
            HelperClass hc = new HelperClass();
            string path = hc.GetPath();

            Document doc = new Document(PageSize.A4);
            PdfWriter.GetInstance(doc, new FileStream(path + "/Report.pdf", FileMode.Create));
            doc.Open();

            BaseFont bf = BaseFont.CreateFont(@"consola.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font f = new iTextSharp.text.Font(bf, 8);
          

            DataTable dt = new DataTable();

            foreach (DataGridViewColumn col in dgv.Columns)
            {
                dt.Columns.Add(col.Name);
            }

            foreach (DataGridViewRow row in dgv.Rows)
            {
                DataRow dtr = dt.NewRow();

                foreach (DataGridViewCell cell in row.Cells)
                {
                    dtr[cell.ColumnIndex] = cell.Value;
                }
                dt.Rows.Add(dtr);
            }

            PdfPTable pdfPTable = new PdfPTable(dt.Columns.Count);

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                PdfPCell cell = new PdfPCell();
                cell.BackgroundColor = BaseColor.GRAY;
                cell.AddElement(new Chunk(dgv.Columns[i].Name, f));
                pdfPTable.AddCell(cell);
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    Phrase p = new Phrase(dt.Rows[i][j].ToString(), f);
                    pdfPTable.AddCell(p);
                }
            }


            string workers = "Сотрудники: ";
            foreach(var i in cashiers)
            {
                workers += i + ", ";
            }
            workers = workers.TrimEnd(',');
            doc.Add(new Paragraph(workers, f));
            doc.Add(new Paragraph("   ", f));

            string change = "Смены: ";
            foreach (var i in changes)
            {
                change += i + ", ";
            }
            change = change.TrimEnd(',');
            doc.Add(new Paragraph(change, f));
            doc.Add(new Paragraph("   ", f));


            string product = "Товары: ";
            foreach (var i in products)
            {
                product += i + ", ";
            }
            product = product.TrimEnd(',');
            doc.Add(new Paragraph(product, f));
            doc.Add(new Paragraph("   ", f));


            doc.Add(pdfPTable);
            doc.Close();


        }

        public void SendRecipieToPrinter()
        {
            string filePath;
            string fileName = "Recipie.pdf";
            string printerName;

            string connectToPrinter = @"Data Source = DESKTOP-NLAJBQI; Initial Catalog = Kass; Integrated Security = True";
            using (SqlConnection connection = new SqlConnection(connectToPrinter))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT Path, PrinterName FROM dbo.PrinterSettings", connection);
                var reader = command.ExecuteReader();

                reader.Read();
                filePath = Convert.ToString(reader.GetString(0));
                printerName = Convert.ToString(reader.GetString(1));
                filePath += @"\Recipie.pdf";
            }          

            IPrinter printer = new Printer();
            printer.PrintRawFile(printerName, filePath, fileName);
        }

        public List<string> GetChangesList()
        {
            int changeCount = 0;
            List<string> list = new List<string>();

            HelperClass hc = new HelperClass();

            changeCount = hc.GetIdCurrentChange();
            if (hc.CheckIfChangeOpen())
            {
                changeCount--;
            }

            string connectionString = @"Data Source = DESKTOP-NLAJBQI; Initial Catalog = Kass; Integrated Security = True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT Id, DateTimeOpen FROM dbo.Changes", connection);

                var reader = command.ExecuteReader();

                for (int i = 0; i <= changeCount; i++)
                {
                    reader.Read();
                    list.Add("Смена №" + reader.GetInt32(0).ToString() + " от " + reader.GetDateTime(1).ToShortDateString().ToString());
                }

                return list;
            }


        }

        public List<Product> GetRecipiesFiltratedByChanges(List<string> cashiers, List<int> changes, List<string> products)
        {
            List<OneRecipie> recipies = new List<OneRecipie>();
            List<OneRecipie> recipiesFiltratedByCashier = new List<OneRecipie>();
            List<OneRecipie> recipiesFiltratedByProducts = new List<OneRecipie>();
            List<Product> finalProductList = this.FillInfo();

            string connectionString = @"Data Source = DESKTOP-NLAJBQI; Initial Catalog = Kass; Integrated Security = True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();

                //Для каждой смены
                foreach (var i in changes)
                {
                    string sqlCommand = "SELECT ChangesInfo FROM dbo.Changes WHERE Id = " + i;

                    command.CommandText = sqlCommand;
                    command.Connection = connection;

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        recipies.AddRange(JsonConvert.DeserializeObject<List<OneRecipie>>(reader.GetString(0)));
                    }
                    reader.Close();
                }
            }

            foreach(var i in recipies)
            {              
                if (cashiers.Contains(i.Cashier))
                {
                    recipiesFiltratedByCashier.Add(i);
                }
            }

            foreach(var i in recipiesFiltratedByCashier)
            {
                List<Product> listProduct = JsonConvert.DeserializeObject<List<Product>>(i.ProductList);

                foreach(var productsSelled in listProduct)
                {
                    if(products.Contains(productsSelled.Name))
                    {
                        for(int z = 0; z < finalProductList.Count; z++)
                        {
                            if (finalProductList[z].Name == productsSelled.Name)
                            {
                                finalProductList[z].Amount += productsSelled.Amount;
                            }

                        }
                    }
                }


            }
            List<Product> returnedList = new List<Product>();
            foreach(var i in finalProductList)
            {
                if(i.Amount != 0)
                {
                    returnedList.Add(i);
                }
            }

            return returnedList;
        }

 
    }
}
