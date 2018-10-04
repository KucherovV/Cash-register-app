using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace kassa
{
    public partial class AdminForm : Form
    {
        List<string> list = new List<string>();

        public AdminForm()
        {
            InitializeComponent();

            richTextBoxPass.Text = "Возможности окна 'Смена пароля' позволяют пользователю защитить кассовую программу, " +
                "установив желаемый пароль для кассира или администратора.";

            richTextBoxPersonal.Text = "В окне 'Работа с сотрудниками' пользователь использует базу данных чтобы добавлять и удалять сотрудников." +
                "Все сотрудники отображаются в таблице. Красным выделены сотрудники, которые уволены." +
                "Для добавления нового сотрудника необходимо указать его имя, фамилию, должность и номер телефона. " +
                "Новому сотруднику будет автоматическ иприсвоен номер.";

            richTextBoxProduct.Text = "Используя окно 'Работа с товарами', пользователь может добавлять и удалять товары с базы данных." +
                "Для добавления товара необходимо ввести его название и цену. Чтобы удалить товар, достаточно выделить его " +
                "в таблице и нажать клавишу Удалить.";

            richTextBoxChanges.Text = "С помощью окна 'Работа со сменами', можно управлять работой точки. В окне есть возможность открыть и закрыть смену. " +
                "Пользователю показана таблица, в которой находится информация о всех закрытых сменах. Также можно получить подробную информацию " +
                "о каждой смене, нажав соответствующую кнопку. В открывшемся окне будет показан отчет о количестве проданых штук каждого товара. " +
                "Этот отчет можно сохранить в PDF файл. Во второй таблице будут отображены все чеки, сделаные в данную смену";

            richTextBoxSettings.Text = "В данном окне можно задать основную ифнормацию о точке, а также установить основные настройки принтера: " +
                "ширина летны, путь к папке для сохранения, само устройство для печати.";
        }

        private void сменаПароляToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            ChangePass cp = new ChangePass();
            cp.Owner = this;
            cp.ShowDialog();
        }

        private void работаССотрудникамиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            WorkingWithPersonal wp = new WorkingWithPersonal();
            wp.Owner = this;
            wp.ShowDialog();
        }

        private void раьотаСТоварамиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            WorkingWithProducts working = new WorkingWithProducts();
            working.Owner = this;
            working.ShowDialog();
        }

        private void работаСоСменамиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            Changes ch = new Changes();
            ch.Owner = this;
            ch.ShowDialog();
        }
        
        private void информацияОТочкеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PointInfo pi = new PointInfo();
            pi.Owner = this;
            pi.Show();
            this.Enabled = false;
        }

        private void печатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintSettings ps = new PrintSettings();
            ps.Owner = this;
            ps.Show();
            this.Enabled = false;
        }      

        private void выборПринераToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChoosePrinter cp = new ChoosePrinter();
            cp.Owner = this;
            cp.Show();
            this.Enabled = false;
        }

        private void сменаПароляToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            richTextBoxPass.Visible = true;
            labelDefault.Visible = false;
        }
     
        private void сменаПароляToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            richTextBoxPass.Visible = false;
            labelDefault.Visible = true;
        }

        private void работаССотрудникамиToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            labelDefault.Visible = false;
            richTextBoxPersonal.Visible = true;
        }

        private void работаССотрудникамиToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            labelDefault.Visible = true;
            richTextBoxPersonal.Visible = false;
        }

        private void раьотаСТоварамиToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            labelDefault.Visible = false;
            richTextBoxProduct.Visible = true;
        }

        private void раьотаСТоварамиToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            labelDefault.Visible = true;
            richTextBoxProduct.Visible = false;
        }

        private void работаСоСменамиToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            labelDefault.Visible = false;
            richTextBoxChanges.Visible = true;

        }

        private void работаСоСменамиToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            labelDefault.Visible = true;
            richTextBoxChanges.Visible = false;
        }

        private void настройкаИнформацииОТочкеToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            labelDefault.Visible = false;
            richTextBoxSettings.Visible = true;
        }

        private void настройкаИнформацииОТочкеToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            labelDefault.Visible = true;
            richTextBoxSettings.Visible = false;
        }

        private void сменаПароляToolStripMenuItem_EnabledChanged(object sender, EventArgs e)
        {
            сменаПароляToolStripMenuItem_MouseLeave(sender, e);
        }

        private void работаССотрудникамиToolStripMenuItem_EnabledChanged(object sender, EventArgs e)
        {
            работаССотрудникамиToolStripMenuItem_MouseLeave(sender, e);
        }

        private void раьотаСТоварамиToolStripMenuItem_EnabledChanged(object sender, EventArgs e)
        {
            раьотаСТоварамиToolStripMenuItem_MouseLeave(sender, e);
        }

        private void работаСоСменамиToolStripMenuItem_EnabledChanged(object sender, EventArgs e)
        {
            работаСоСменамиToolStripMenuItem_MouseLeave(sender, e);
        }

        private void настройкаИнформацииОТочкеToolStripMenuItem_EnabledChanged(object sender, EventArgs e)
        {
            настройкаИнформацииОТочкеToolStripMenuItem_MouseLeave(sender, e);
        }

        private void отчетыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reports rp = new Reports();
            rp.Owner = this;
            this.Enabled = false;
            rp.Show();
        }
    }
}
