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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace ppExam
{
    public partial class FormAuten : Form
    {
        DataBase dataBase = new DataBase();
        private bool closed = false;
        private NpgsqlConnection conn;

        public FormAuten()
        {
            InitializeComponent();
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

        private void button1_Click(object sender, EventArgs e)
        {
            string fio = textBox1.Text;
            string password = textBox2.Text;

            if (closed)
            {
                return;
            }
            else if (CheckLogin(fio, password))
            {
                ShowUserRoleForm(fio);
            }
        }

        string connectingString = "Server = localhost; port = 5432; Database = applicationvgtu; User Id = postgres; Password = 123";

        private bool CheckLogin(string fio, string password)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectingString))
            {
                string query = "SELECT COUNT(*) FROM users2 WHERE fio = @fio AND password_user = @password";
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("fio", fio);
                    command.Parameters.AddWithValue("password", password);

                    connection.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        private void ShowUserRoleForm(string fio)
        {
            string role = GetUserRole(fio);

            if (role == "Работник")
            {
                FormWorkers formApplication = new FormWorkers(fio, role);
                formApplication.Show();
            }
            else if (role == "Абитуриент")
            {
                FormAplicant formWorkingWithApplications = new FormAplicant(fio, role);
                formWorkingWithApplications.Show();
            }
            else
            {
                MessageBox.Show("ошибка: неизвестная роль пользователя", "ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Hide();
        }

        private string GetUserRole(string fio)
        {
            string role = "";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectingString))
            {
                string query = "SELECT role_user FROM users2 WHERE fio = @fio";
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@fio", fio);
                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            role = result.ToString();
                        }
                        else
                        {
                            MessageBox.Show("не удалось получить роль пользователя", "ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"ошибка при получении роли пользователя: {ex.Message}", "ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            return role;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormRegister form1 = new FormRegister();
            form1.Show();
            this.Hide();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
