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
    public partial class Form1 : Form
    {

        DataBase dataBase = new DataBase();
        private string fio;
        private string role;

        public Form1()
        {
            InitializeComponent();

            NpgsqlConnection connection = new NpgsqlConnection("Server = localhost; port = 5432; Database = applicationvgtu;  User Id=postgres; Password= 123");

            conn = connection;

            panelСПО.Visible = false;
            panelБАКАЛАВР.Visible = false;
            panelСПЕЦИ.Visible = false;
            panelМАГИСТР.Visible = false;
        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            FormAplicant formAplicant = new FormAplicant(fio, role);
            formAplicant.Show();
            this.Hide();

        }

        private void ClearFields()
        {
            comboBoxLevelEduc.Text = "";
            textBoxFIO.Text = "";
            textBoxPasport.Text = "";
            textBoxEmail.Text = "";
            maskedTextBoxPhone.Text = "";
            textBoxParent.Text = "";
            textBoxInstitu.Text = "";
            textBoxScore.Text = "";
           

            textBoxSnils.Text = "";
        }


        private NpgsqlConnection conn;

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBoxFIO.Text == string.Empty || textBoxPasport.Text == string.Empty || comboBoxLevelEduc.Text == string.Empty || textBoxEmail.Text == string.Empty || maskedTextBoxPhone.Text == string.Empty || textBoxParent.Text == string.Empty || textBoxInstitu.Text == string.Empty || textBoxScore.Text == string.Empty || comboBox1.Text == string.Empty || textBoxSnils.Text == string.Empty || comboBox2.Text == string.Empty || comboBox4.Text == string.Empty || comboBox3.Text == string.Empty)
            {
                label13.Visible = true;
                label15.Visible = true;
                label14.Visible = true;
                label16.Visible = true;
                label17.Visible = true;
                // label18.Visible = true;
                label19.Visible = true;
                label20.Visible = true;
                label21.Visible = true;
                label22.Visible = true;
              


                textBoxFIO.BackColor = Color.LightCoral;
                textBoxPasport.BackColor = Color.LightCoral;
                comboBoxLevelEduc.BackColor = Color.LightCoral;
                textBoxEmail.BackColor = Color.LightCoral;
                maskedTextBoxPhone.BackColor = Color.LightCoral;
                textBoxParent.BackColor = Color.LightCoral;
                textBoxInstitu.BackColor = Color.LightCoral;
                textBoxScore.BackColor = Color.LightCoral;
             
                // textBoxPassword.BackColor = Color.LightCoral;
                textBoxSnils.BackColor = Color.LightCoral;
            }
            else
            {
                dataBase.OpenConnecting();

                object specialValue = DBNull.Value; // Значение по умолчанию
                

                switch (comboBoxLevelEduc.SelectedItem.ToString())
                {
                    case "СПО":
                        specialValue = comboBox1.SelectedItem ?? DBNull.Value;
                        break;
                    case "Магистратура":
                        specialValue = comboBox2.SelectedItem ?? DBNull.Value;
                        break;
                    case "Специалитет":
                        specialValue = comboBox4.SelectedItem ?? DBNull.Value;
                        break;
                    case "Бакалавриат":
                        specialValue = comboBox3.SelectedItem ?? DBNull.Value;
                        break;
                    default:
                        break;
                }


                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@Special", specialValue); // Передача значения параметра


                    var levelEdu = comboBoxLevelEduc.Text;
                    var fio = textBoxFIO.Text;
                    var pasport = textBoxPasport.Text;
                    var email = textBoxEmail.Text;
                    var phone = maskedTextBoxPhone.Text;
                    var fioParent = textBoxParent.Text;
                    var educationInstitu = textBoxInstitu.Text;
                    var score = textBoxScore.Text;
                   // var preparation = comboBoxPreparation.Text;
                   // var preparation2 = comboBox4.Text;
                    //  var password = textBoxPassword.Text;
                    var status = "Подано";
                    var comment = "-";

                    var snils = textBoxSnils.Text;

                    string query = $"insert into users (level_education,fio,pasport,email,phone,fio_parent,educati_institu,score,preparation,status,comment_worker,snils) values ('{levelEdu}','{fio}','{pasport}','{email}','{phone}','{fioParent}','{educationInstitu}','{score}','{specialValue}','{status}','{comment}','{snils}')";

                    NpgsqlCommand command = new NpgsqlCommand(query, dataBase.GetConnection());

                    command.ExecuteNonQuery();

                    MessageBox.Show("Заявка отправлена", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    dataBase.CloseConnecting();

                    FormAplicant formAuten = new FormAplicant(fio, role);
                    formAuten.Show();
                    this.Hide();
                }
                  
            }

        }

        private void textBoxFIO_Enter(object sender, EventArgs e)
        {
            label13.Visible = false;
            textBoxFIO.BackColor = Color.White;
        }

        private void textBoxPasport_Enter(object sender, EventArgs e)
        {
            label15.Visible = false;
            textBoxPasport.BackColor = Color.White;
        }

        private void comboBoxLevelEduc_Enter(object sender, EventArgs e)
        {
            label14.Visible = false;
            comboBoxLevelEduc.BackColor = Color.White;
        }

        private void textBoxSnils_Enter(object sender, EventArgs e)
        {
            label16.Visible = false;
            textBoxSnils.BackColor = Color.White;
        }

        private void textBoxEmail_Enter(object sender, EventArgs e)
        {
            label17.Visible = false;
            textBoxEmail.BackColor = Color.White;
        }

        private void textBoxPassword_Enter(object sender, EventArgs e)
        {
            /*label18.Visible = false;
            textBoxPassword.BackColor = Color.White;*/

        }

        private void maskedTextBoxPhone_Enter(object sender, EventArgs e)
        {
            label19.Visible = false;
            maskedTextBoxPhone.BackColor = Color.White;
        }

        private void textBoxParent_Enter(object sender, EventArgs e)
        {
            label20.Visible = false;
            textBoxParent.BackColor = Color.White;
        }

        private void textBoxInstitu_Enter(object sender, EventArgs e)
        {
            label21.Visible = false;
            textBoxInstitu.BackColor = Color.White;
        }

        private void textBoxScore_Enter(object sender, EventArgs e)
        {
            label22.Visible = false;
            textBoxScore.BackColor = Color.White;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
           /* comboBox1.Items.Insert(1, "СПО");
            comboBox1.Items.Insert(2, "Бакалавриат");
            comboBox1.Items.Insert(3, "Специалитет");
            comboBox1.Items.Insert(4, "Магистратура");*/

        }





        private void comboBoxLevelEduc_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = comboBoxLevelEduc.SelectedItem.ToString();

            // Показываем или скрываем четыре других комбобокса в зависимости от выбранного элемента
            switch (selectedItem)
            {
                case "СПО":
                    panelСПО.Visible = true;
                    panelБАКАЛАВР.Visible = false;
                    panelСПЕЦИ.Visible = false;
                    panelМАГИСТР.Visible = false;
                    break;
                case "Бакалавриат":

                    panelСПО.Visible = false;
                    panelБАКАЛАВР.Visible = true;
                    panelСПЕЦИ.Visible = false;
                    panelМАГИСТР.Visible = false;
                    break;
                case "Специалитет":
                    panelСПО.Visible = false;
                    panelБАКАЛАВР.Visible = false;
                    panelСПЕЦИ.Visible = true;
                    panelМАГИСТР.Visible = false;
                    break;
                case "Магистратура":
                    panelСПО.Visible = false;
                    panelБАКАЛАВР.Visible = false;
                    panelСПЕЦИ.Visible = false;
                    panelМАГИСТР.Visible = true;
                    break;
                default:
                    // Если выбрано неопределенное значение, скрываем все комбобоксы
                    break;
            }

            /*  private void comboBoxLevelEduc_SelectedIndexChanged(object sender, EventArgs e)
              {
                  if (comboBoxLevelEduc.SelectedIndex == 0)// СПО
                  {
                      panelСПО.Visible = true;
                      panelБАКАЛАВР.Visible = false;
                      panelСПЕЦИ.Visible = false;
                      panelМАГИСТР.Visible = false;
                    //  panelEge.Visible = false;
                  }
                  if (comboBoxLevelEduc.SelectedIndex == 1)//БАКАЛАВР
                  {
                      panelСПО.Visible = false;
                      panelБАКАЛАВР.Visible = true;
                      panelСПЕЦИ.Visible = false;
                      panelМАГИСТР.Visible = false;
                      //  panelEge.Visible = false;
                  }
                  if (comboBoxLevelEduc.SelectedIndex == 2)//СПЕЦИ
                  {
                      panelСПО.Visible = false;
                      panelБАКАЛАВР.Visible = false;
                      panelСПЕЦИ.Visible = true;
                      panelМАГИСТР.Visible = false;
                      //  panelEge.Visible = false;
                  }
                  if (comboBoxLevelEduc.SelectedIndex == 3)//СПЕЦИ
                  {
                      panelСПО.Visible = false;
                      panelБАКАЛАВР.Visible = false;
                      panelСПЕЦИ.Visible = false;
                      panelМАГИСТР.Visible = true;
                      //  panelEge.Visible = false;
                  }
              }*/
        }
    }
}
