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
    public partial class FormWorkers : Form
    {

        DataBase dataBase = new DataBase();

        enum RowState
        {
            Existed,
            New,
            Modfied,
            ModfiedNew,
            Deleted
        }

        int selectedRow;
        public FormWorkers(string fio, string role)
        {
            InitializeComponent();

            label4.Text = fio;
            label5.Text = role;
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

        private void FormWorkers_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshGridView(dataGridView1);
        }

        public void CreateColumns()
        {
            dataGridView1.Columns.Add("id", "Номер");                               //0
            dataGridView1.Columns.Add("level_education", "Уровень образования");   //1
            dataGridView1.Columns.Add("fio", "ФИО");                               //2 
            dataGridView1.Columns.Add("pasport", "Паспортные данные");             //3  
            dataGridView1.Columns.Add("email", "Почта");  //4 
            dataGridView1.Columns.Add("phone", "Номер телефона"); //5 
            dataGridView1.Columns.Add("fio_parent", "ФИО представителя");          //6 
            dataGridView1.Columns.Add("educati_institu", "Учебное пособие, которое было закончено");    //7
            dataGridView1.Columns.Add("score", "Баллы");  //8
            dataGridView1.Columns.Add("preparation", "Направление");                //9
            dataGridView1.Columns.Add("status", "Статус");                          //10    
            dataGridView1.Columns.Add("comment_worker", "Комментарий");             //11
            dataGridView1.Columns.Add("snils", "СНИЛС");  //12
            dataGridView1.Columns.Add("isNew", String.Empty);                       //13
            dataGridView1.Columns["isNew"].Visible = false;
        }

        public void ReadSingleRow(DataGridView gridView, IDataRecord record)
        {
            gridView.Rows.Add(record.GetInt64(0), record.GetString(1), record.GetString(2), record.GetString(3), record.GetString(4), record.GetString(5), record.GetString(6), record.GetString(7), record.GetInt64(8), record.GetString(9), record.GetString(10), record.GetString(11), record.GetInt64(12), RowState.ModfiedNew);
        }

        public void RefreshGridView(DataGridView gridView)
        {
            gridView.Rows.Clear();

            string query = $"select * from users";

            NpgsqlCommand command = new NpgsqlCommand(query, dataBase.GetConnection());

            dataBase.OpenConnecting();

            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(gridView, reader);
            }
            reader.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];

                comboBox1.Text = row.Cells[10].Value.ToString();
                textBox1.Text = row.Cells[11].Value.ToString();
                textBoxID.Text = row.Cells[0].Value.ToString();
            }
        }

        private void ClearFields()
        {
            comboBox1.Text = "";
            textBox1.Text = "";
            textBoxID.Text = "";
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataBase.OpenConnecting();


            var status = comboBox1.SelectedItem.ToString();
          
            var id = Convert.ToInt64(textBoxID.Text);

            var comment = textBox1.Text;

            string query = $"UPDATE users SET status = '{status}', comment_worker = '{comment}' WHERE id = " + id;
            NpgsqlCommand command = new NpgsqlCommand(@query, dataBase.GetConnection());
            command.ExecuteNonQuery();
            dataBase.CloseConnecting();

            MessageBox.Show("Статус заявки обновлён");

            ClearFields();

            RefreshGridView(dataGridView1);
        }

        private void buttonСПО_Click(object sender, EventArgs e)
        {
            FormSPO formSPO = new FormSPO();
            formSPO.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormБАКАВ form = new FormБАКАВ();
            form.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FormСпециа formСпециа = new FormСпециа();
            formСпециа.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FormМагистр formМагистр = new FormМагистр();
            formМагистр.Show();
            this.Hide();
        }
    }
}
