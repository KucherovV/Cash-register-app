using System;
using System.Collections.Generic;
using System.Windows.Forms;
namespace kassa
{
    public partial class CreateNewRecipie : Form
    {
        List<string> list = new List<string>();


        public List<Product> listRecipies = new List<Product>();
        public List<Product> productList = new List<Product>();
        public List<Product> reservList = new List<Product>();


        public string finalRecipelist;

        public bool finalPaymentTypeCash;
        public bool finalTakeAway;

        public string finalComment;
        public bool finalClosed = false;

        public DateTime finalDatetime;

        public string user;

        public int totalSum = 0;

        public CreateNewRecipie(string user)
        {
            HelperClass hc = new HelperClass();
            productList = hc.FillInfo();
            InitializeComponent();

            radioButtonCash.Checked = true;
            radioButtonInCafe.Checked = true;

            dataGridView1.ColumnCount = 3;
            dataGridView1.Columns[0].Name = "Название";
            dataGridView1.Columns[1].Name = "Цена, грн";
            dataGridView1.Columns[2].Name = "К-во";

            dataGridView1.Columns[0].Width = 130;
            dataGridView1.Columns[1].Width = 80;
            dataGridView1.Columns[2].Width = 50;

            this.user = user;
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            listBoxProducts.Items.Clear();
            reservList.Clear();

            label6.Visible = false;

            string find = textBoxSearch.Text.ToLower();

            if (find != "")
            {
                foreach (var str in productList)
                {
                    if (str.Name.Contains(find))
                    {
                        string name = str.Name;
                        string subBegin = name[0].ToString().ToUpper();
                        string subEnd = name.Remove(0, 1);
                        name = subBegin + subEnd;

                        int count = 21 - name.Length - str.Price.ToString().Length;
                        string spaces = new String(' ', count);

                        listBoxProducts.Items.Add(name + spaces + str.Price + " грн.");
                        reservList.Add(str);
                    }
                }
            }

            if (listBoxProducts.Items.Count == 0)
            {
                label6.Visible = true;
            }

        }

        public void AddToRecipeList()
        {
            Product product = reservList[listBoxProducts.SelectedIndex];
            string addstrind = reservList[listBoxProducts.SelectedIndex].Name;
            int index;

            product.Amount += Convert.ToInt32(numericUpDown1.Value);

            if (product.Amount > 100)
            {
                MessageBox.Show("Максимальное количество продукта в чеке - 100");
                product.Amount -= Convert.ToInt32(numericUpDown1.Value);
                return;
            }

            dataGridView1.Rows.Clear();

            if (listRecipies.Count != 0)
            {
                for (int i = 0; i < listRecipies.Count; i++)
                {
                    if (listRecipies[i].Name == addstrind)
                    {
                        index = i;
                        listRecipies[i] = product;

                        goto mark1;
                    }
                }

                listRecipies.Add(product);
            }
            else
            {
                listRecipies.Add(product);
            }

            mark1:

            for (int i = 0; i < listRecipies.Count; i++)
            {
                dataGridView1.Rows.Add(listRecipies[i].Name, listRecipies[i].Price, listRecipies[i].Amount);
            }

            totalSum += product.Price * Convert.ToInt32(numericUpDown1.Value);
            textBoxPrice.Text = totalSum.ToString();
            numericUpDown1.Value = 1;


            listBoxProducts.ClearSelected();
            textBoxSearch.Focus();
            textBoxSearch.SelectAll();
        }

        private void buttonDeleteFromRecipies_Click(object sender, EventArgs e)
        {
            if (listRecipies.Count != 0)
            {
                int index = dataGridView1.CurrentRow.Index;
                string name = dataGridView1.Rows[index].Cells[0].Value.ToString();

                for (int i = 0; i < productList.Count; i++)
                {
                    if (productList[i].Name == name)
                    {
                        totalSum -= productList[i].Price * productList[i].Amount;
                        textBoxPrice.Text = totalSum.ToString();
                        productList[i].Amount = 0;
                    }
                }

                dataGridView1.Rows.RemoveAt(index);
                dataGridView1.Refresh();

                listRecipies.RemoveAt(index);


            }
        }
          
        private void textBoxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back && textBoxSearch.Text != "")
            {
                string str = textBoxSearch.Text;
                str.Remove(str.Length - 1, 1);
                textBoxSearch.Text = str;
            }
            if (listBoxProducts.SelectedItem != null && e.KeyCode == Keys.Enter)
            {
                AddToRecipeList();
            }
            if (e.KeyCode == Keys.Down && listBoxProducts.Items.Count != 0)
            {
                listBoxProducts.Focus();
                listBoxProducts.SetSelected(0, true);
            }

            if(e.KeyCode == Keys.F6)
            {
                button1_Click(sender, e);
            }
        }

        private void listBoxProducts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AddToRecipeList();
            }
            if (e.KeyCode == Keys.Up && listBoxProducts.Items.Count != 0)
            {
                if (listBoxProducts.SelectedIndex == 0)
                {
                    textBoxSearch.Focus();
                }
            }
            if (e.KeyCode == Keys.Subtract && listBoxProducts.Items.Count != 0 && numericUpDown1.Value > 1)
            {
                numericUpDown1.Value--;
                //listBoxProducts.SelectedIndex++;

            }

            if (e.KeyCode == Keys.Add && listBoxProducts.Items.Count != 0 && numericUpDown1.Value < 100)
            {
                numericUpDown1.Value++;
                //listBoxProducts.SelectedIndex--;
            }


        }

        private void listBoxProducts_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBoxProducts.SelectedItem != null)
            {
                AddToRecipeList();
            }
        }

        private void numericUpDown1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && listBoxProducts.Items.Count != 0 && listBoxProducts.Items.Count != 0)
            {
                AddToRecipeList();
            }
            if (e.KeyCode == Keys.Down && listBoxProducts.Items.Count != 0)
            {
                listBoxProducts.Focus();
            }
        }
    
        private void CreateNewRecipie_FormClosing(object sender, FormClosingEventArgs e)
        {
            CashierForm cf = (CashierForm)this.Owner;
            cf.Enabled = true;
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            if (listRecipies.Count != 0)
            {
                //Тип оплаты
                if (radioButtonCard.Checked)
                {
                    finalPaymentTypeCash = false;
                }
                else if (radioButtonCash.Checked)
                {
                    finalPaymentTypeCash = true;
                }
                else
                {
                    MessageBox.Show("Выберите тип оплаты.");
                    return;
                }

                //Где употреблять товар
                if (radioButtonInCafe.Checked)
                {
                    finalTakeAway = false;
                }
                else if (radioButtonTakeAway.Checked)
                {
                    finalTakeAway = true;
                }
                else
                {
                    MessageBox.Show("Выберите где употреблять товар.");
                    return;
                }

                //Дата и время заказа
                finalDatetime = DateTime.Now;
                finalDatetime = new DateTime(finalDatetime.Year, finalDatetime.Month, finalDatetime.Day, finalDatetime.Hour,
                finalDatetime.Minute, finalDatetime.Second, finalDatetime.Kind);
                //finalDatetime = DateTime.Now.ToLocalTime();

                //Комментарий
                finalComment = textBoxComment.Text.ToString();

                this.Enabled = false;
                ConfirmNewRecipe cf = new ConfirmNewRecipe(finalPaymentTypeCash, finalTakeAway, finalDatetime, finalComment, user, listRecipies, totalSum);
                cf.Owner = this;
                cf.ShowDialog();

                if (cf.success)
                {
                    textBoxSearch.Text = "";
                    listRecipies.Clear();
                    dataGridView1.Rows.Clear();
                    textBoxPrice.Text = "";
                    textBoxComment.Text = "";
                    totalSum = 0;
                    radioButtonCash.Checked = true;
                    radioButtonInCafe.Checked = true;

                    productList.Clear();
                    //FillInfo();
                    HelperClass hc = new HelperClass();
                    productList = hc.FillInfo();

                    this.Close();

                }

            }
            else
            {
                MessageBox.Show("Список товаров пуст.");
            }
        }
      
        private void CreateNewRecipie_Shown(object sender, EventArgs e)
        {
            textBoxSearch.Focus();
        }
    }
}
