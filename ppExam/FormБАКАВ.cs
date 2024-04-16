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

namespace ppExam
{
    public partial class FormБАКАВ : Form
    {

        DataBase dataBase = new DataBase();

        enum RowState
        {
            New,
            Deleted,
            ModfienNew,
            Modfien,
            Existed
        }

        int selectedRow;
        private string fio;
        private string role;

        public FormБАКАВ()
        {
            InitializeComponent();
        }

        private void FormБАКАВ_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshGridView(dataGridView1);
        }

        public void CreateColumns()
        {
            dataGridView1.Columns.Add("id_prepa", "Номер");
            dataGridView1.Columns.Add("name_prepa", "Название направления");
            dataGridView1.Columns.Add("isNew", String.Empty); //2
            dataGridView1.Columns["isNew"].Visible = false;
        }

        public void ReadSingleRow(DataGridView gridView, IDataRecord record)
        {
            gridView.Rows.Add(record.GetInt64(0), record.GetString(1), RowState.ModfienNew);
        }

        public void RefreshGridView(DataGridView gridView)
        {
            gridView.Rows.Clear();

            gridView.Rows.Clear();
            string queryString = $"select * from bachelor";

            NpgsqlCommand command = new NpgsqlCommand(queryString, dataBase.GetConnection());

            dataBase.OpenConnecting();

            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(gridView, reader);
            }
            reader.Close();
        }

        private void ClearFieldsAdd()
        {
            textBox2.Text = "";

        }

        private void ClearFieldsDeleUp()
        {
            textBox1.Text = "";
            textBox3.Text = "";

        }

        private void button6_Click(object sender, EventArgs e)
        {
            dataBase.OpenConnecting();

            var name_prepa = textBox2.Text;


            string query = $"insert into bachelor (name_prepa) values ('{name_prepa}')";

            NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, dataBase.GetConnection());
            npgsqlCommand.ExecuteNonQuery();

            MessageBox.Show("данные успешно добавлены!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ClearFieldsAdd();

            RefreshGridView(dataGridView1);

            dataBase.CloseConnecting();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataBase.OpenConnecting();

            var name_prepa = textBox1.Text;

            var id = Convert.ToInt64(textBox3.Text);

            string query = $"DELETE FROM bachelor WHERE id_prepa = " + id;
            NpgsqlCommand command = new NpgsqlCommand(@query, dataBase.GetConnection());
            command.ExecuteNonQuery();


            RefreshGridView(dataGridView1);

            ClearFieldsDeleUp();

            dataBase.CloseConnecting();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataBase.OpenConnecting();

            var name_prepa = textBox1.Text;

            var id = Convert.ToInt32(textBox3.Text);

            string query = $"UPDATE bachelor SET name_prepa = '{name_prepa}' WHERE id_prepa = " + id;
            NpgsqlCommand command = new NpgsqlCommand(@query, dataBase.GetConnection());
            command.ExecuteNonQuery();

            RefreshGridView(dataGridView1);

            ClearFieldsDeleUp();

            dataBase.CloseConnecting();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];

                textBox1.Text = row.Cells[1].Value.ToString();
                textBox3.Text = row.Cells[0].Value.ToString();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormWorkers formWorkers = new FormWorkers(fio,role);
            formWorkers.Show();
            this.Hide();
        }
    }
}
