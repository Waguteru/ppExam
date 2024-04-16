using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ppExam
{
    public partial class FormAplicant : Form
    {

        DataBase dataBase = new DataBase();

        int selectedRow;
        public FormAplicant(string fio,string role)
        {
            InitializeComponent();

           // label3.Text = fio;
          //  label4.Text = role;

            
                                
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

        private void FormAplicant_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshGridView(dataGridView1);
        }

        public void CreateColumns()
        {
            dataGridView1.Columns.Add("level_education", "Уровень образования"); //0
            dataGridView1.Columns.Add("fio", "ФИО");                             //1
            dataGridView1.Columns.Add("score", "Баллы");                         //2
            dataGridView1.Columns.Add("preparation", "Направление");             //3
            dataGridView1.Columns.Add("status", "Статус");                       //4
        }

        public void ReadSingleRows(DataGridView gridView,IDataRecord record)
        {
            gridView.Rows.Add(record.GetString(0),record.GetString(1),record.GetInt64(2),record.GetString(3),record.GetString(4));
        }

        public void RefreshGridView(DataGridView gridView)
        {
            gridView.Rows.Clear();

            string query = $"select level_education,fio,score,preparation,status from users";

            NpgsqlCommand command = new NpgsqlCommand(query,dataBase.GetConnection());

            dataBase.OpenConnecting();

            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRows(gridView,reader);
            }
            reader.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }


        private void Search(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string searchString = $"select level_education,fio,score,preparation,status from users where concat (fio) like '%" + textBox1.Text + "%'";

            NpgsqlCommand comm = new NpgsqlCommand(searchString, dataBase.GetConnection());

            dataBase.OpenConnecting();

            NpgsqlDataReader read = comm.ExecuteReader();

            while (read.Read())
            {
                ReadSingleRows(dgw, read);
            }

            read.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Search(dataGridView1);
        }


        private int clickedRowIndex = -1;

        private void button1_Click(object sender, EventArgs e)
        {
            if (clickedRowIndex >= 0 && clickedRowIndex < dataGridView1.Rows.Count)
            {
                string fioRich = dataGridView1.Rows[clickedRowIndex].Cells[1].Value.ToString();
                string prepa = dataGridView1.Rows[clickedRowIndex].Cells[3].Value.ToString();
               

                NpgsqlConnection npgsqlConnection = new NpgsqlConnection("Server = localhost; port = 5432;Database = applicationvgtu; User Id=postgres; Password = 123");

                FormStatement formOrdersAdmin = new FormStatement(fioRich, prepa, npgsqlConnection);
                formOrdersAdmin.Show();
                this.Hide();

            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Сохраняем индекс строки, на которую нажал пользователь правой кнопкой мыши
                clickedRowIndex = e.RowIndex;

                // Показываем panel1
                button1.Visible = true;

            }
        }
    }
}
