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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ppExam
{
    public partial class FormRegister : Form
    {

        DataBase dataBase = new DataBase();

        public FormRegister()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataBase.OpenConnecting();
            var fio = textBoxFIO.Text;
            var password = textBoxPassword.Text;
            var role = "Абитуриент";

            string query = $"insert into users2 (fio,password_user,role_user) values ('{fio}','{password}','{role}')";
            NpgsqlCommand command = new NpgsqlCommand(query, dataBase.GetConnection());
            command.ExecuteNonQuery();

            MessageBox.Show("Регистрация прошла успешно", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);


            dataBase.CloseConnecting();

            FormAuten formAuten = new FormAuten();
            formAuten.Show();
            this.Hide();
        }
    }
}
