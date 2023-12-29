using System;
using System.Windows.Forms;

using System.Configuration;
using System.Data.SqlClient;

using MaterialSkin;
using MaterialSkin.Controls;

namespace TrafficPolice
{
    public partial class OwnerForm : MaterialForm
    {
        // Основные Sql параметры
        SqlConnection SqlConnection;
        public OwnerForm()
        {
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Cyan500, Primary.Cyan50, Primary.Orange50, Accent.Amber700, TextShade.WHITE);
        }

        private async void OwnerForm_Load(object sender, EventArgs e)
        {
            // данная строка кода позволяет загрузить данные в таблицу "policeDataSet.Owners".
            this.ownersTableAdapter.Fill(this.policeDataSet.Owners);
            SqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Police"].ConnectionString); // строка подключения
            await SqlConnection.OpenAsync(); // открыли подключение к БД
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM [Owners] ", SqlConnection);
            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["OwnerId"]) + "   " + Convert.ToString(sqlReader["FIO"]) +
                        "   " + Convert.ToString(sqlReader["Address"]) +  "   " + Convert.ToString(sqlReader["Phone"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                {
                    sqlReader.Close(); // закрыли подключение к БД
                }
            }
        }

        private async void AddBtn_Click(object sender, EventArgs e)
        {
            if (label4.Visible)
                label4.Visible = false;
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text) &&
               !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text) &&
               !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox3.Text))
            {
                SqlCommand command = new SqlCommand("INSERT INTO [Owners] (FIO, Address, Phone) VALUES(@FIO, @Address, @Phone)", SqlConnection);
                command.Parameters.AddWithValue("FIO", textBox1.Text);               
                command.Parameters.AddWithValue("Address", textBox2.Text);
                command.Parameters.AddWithValue("Phone", textBox3.Text);

                await command.ExecuteNonQueryAsync();
            }
            else
            {
                label4.Visible = true;
                label4.Text = "Поля: ФИО, Адрес и Телефон должны быть заполнены!";
            }
        }

        private async void UpdatetoolStripButton1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM [Owners] ", SqlConnection);
            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["OwnerId"]) + "   " + Convert.ToString(sqlReader["FIO"]) +
                         "   " + Convert.ToString(sqlReader["Address"]) + "   " + Convert.ToString(sqlReader["Phone"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                {
                    sqlReader.Close(); // закрыли подключение к БД
                }
            }
        }

        private void BacktoolStripButton2_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();
            mainForm.Show();
            Hide();
        }

        private async  void СhangemetroButton_Click(object sender, EventArgs e)
        {
            if (label11.Visible)
                label11.Visible = false;
            if (!string.IsNullOrEmpty(comboBox2.Text) && !string.IsNullOrWhiteSpace(comboBox2.Text) &&
               !string.IsNullOrEmpty(comboBox1.Text) && !string.IsNullOrWhiteSpace(comboBox1.Text) &&
               !string.IsNullOrEmpty(textBox7.Text) && !string.IsNullOrWhiteSpace(textBox7.Text) &&
               !string.IsNullOrEmpty(textBox8.Text) && !string.IsNullOrWhiteSpace(textBox8.Text))
            {
                SqlCommand command = new SqlCommand("UPDATE [Owners] SET [FIO]=@FIO, [Address]=@Address, [Phone]=@Phone " +
                    "WHERE [OwnerId]=@Id", SqlConnection);
                command.Parameters.AddWithValue("Id", comboBox2.Text);
                command.Parameters.AddWithValue("FIO", comboBox1.Text);
                command.Parameters.AddWithValue("Address", textBox7.Text);
                command.Parameters.AddWithValue("Phone", textBox8.Text);
                

                await command.ExecuteNonQueryAsync();
            }
            else if (!string.IsNullOrEmpty(comboBox2.Text) && !string.IsNullOrWhiteSpace(comboBox2.Text))
            {
                label11.Visible = true;
                label11.Text = "Поля: ФИО, Адрес и Телефон должны быть заполнены!";
            }
            else
            {
                label11.Visible = true;
                label11.Text = "Id должен быть заполнен!";
            }
        }

        private async void DELETEmetroButton1_Click(object sender, EventArgs e)
        {
            if (label10.Visible)
                label10.Visible = false;
            if (!string.IsNullOrEmpty(comboBox3.Text) && !string.IsNullOrWhiteSpace(comboBox3.Text))
            {
                SqlCommand command = new SqlCommand("DELETE FROM [Owners] WHERE [OwnerId]=@Id", SqlConnection); // sql комманда удаление
                command.Parameters.AddWithValue("Id", comboBox3.Text);

                await command.ExecuteNonQueryAsync();
            }
            else
            {
                label10.Visible = true;
                label10.Text = "Id должен быть заполнен!";
            }
        }

        private void ClearingBtn_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            textBox7.Text = "";
            textBox8.Text = "";
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
