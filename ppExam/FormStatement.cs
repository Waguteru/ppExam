using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ppExam
{
    public partial class FormStatement : Form
    {

        DataBase dataBase = new DataBase();

        private string fioRich;
        private string prepa;

        public FormStatement(string fioRich,string prepa, Npgsql.NpgsqlConnection npgsqlConnection)
        {
            InitializeComponent();

            this.fioRich = fioRich;
            this.prepa = prepa;
        }

        private void FormStatement_Load(object sender, EventArgs e)
        {
            richTextBox1.Text =
                                $"              Заявление\n\n"+
                                $"Я, {this.fioRich} подал документы в ВУЗ\n\n" +
                                $"На направления подготовки: {this.prepa}\n\n" +
                                $"Дата заявления: {DateTime.Now:dd/MM/yyyy} \n\n";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(richTextBox1.Text, new Font("Times New Roman", 16, FontStyle.Regular), Brushes.Black, new Point(10, 10));

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var result = new System.Windows.Forms.DialogResult();
            result = MessageBox.Show("Точно хотите выйти?", "Внимание",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }

        }
    }
}
