using System;
using System.Windows.Forms;

using System.Configuration;
using System.Data.SqlClient;

using MaterialSkin;
using MaterialSkin.Controls;

namespace TrafficPolice
{
    public partial class CarsForm : MaterialForm
    {
        // Основные Sql параметры
        SqlConnection SqlConnection;
        public CarsForm()
        {
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Cyan500, Primary.Cyan50, Primary.Orange50, Accent.Amber700, TextShade.WHITE);
        }

        private async void CarsForm_Load(object sender, EventArgs e)
        {
            //  данная строка кода позволяет загрузить данные в таблицу "policeDataSet.Owners". 
            this.ownersTableAdapter.Fill(this.policeDataSet.Owners);
            //  данная строка кода позволяет загрузить данные в таблицу "policeDataSet.Cars". 
            this.carsTableAdapter.Fill(this.policeDataSet.Cars);

            SqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Police"].ConnectionString); // строка подключения
            await SqlConnection.OpenAsync(); // открыли подключение к БД
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM [Cars] ", SqlConnection);
            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["CarId"]) + "   " + Convert.ToString(sqlReader["Owner"]) +
                        "   " + Convert.ToString(sqlReader["Brand"]) + "   " + Convert.ToString(sqlReader["Model"]));
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
            if (!string.IsNullOrEmpty(comboBox1.Text) && !string.IsNullOrWhiteSpace(comboBox1.Text) &&
               !string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text) &&
               !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text))
            {
                SqlCommand command = new SqlCommand("INSERT INTO [Cars] (Owner, Brand, Model) VALUES(@Owner, @Brand, @Model)", SqlConnection);
                command.Parameters.AddWithValue("Owner", comboBox1.Text);
                command.Parameters.AddWithValue("Brand", textBox1.Text);
                command.Parameters.AddWithValue("Model", textBox2.Text);

                await command.ExecuteNonQueryAsync();
            }
            else
            {
                label4.Visible = true;
                label4.Text = "Поля: Владелец, Марка и Модель должны быть заполнены!";
            }
        }

        private async void UpdatetoolStripButton1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM [Cars] ", SqlConnection);
            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["CarId"]) + "   " + Convert.ToString(sqlReader["Owner"]) +
                        "   " + Convert.ToString(sqlReader["Brand"]) + "   " + Convert.ToString(sqlReader["Model"]));
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

        private async void EditBtn_Click(object sender, EventArgs e)
        {
            if (label9.Visible)
                label9.Visible = false;
            if (!string.IsNullOrEmpty(comboBox2.Text) && !string.IsNullOrWhiteSpace(comboBox2.Text) &&
               !string.IsNullOrEmpty(comboBox3.Text) && !string.IsNullOrWhiteSpace(comboBox3.Text) &&
               !string.IsNullOrEmpty(comboBox4.Text) && !string.IsNullOrWhiteSpace(comboBox4.Text) &&
               !string.IsNullOrEmpty(comboBox5.Text) && !string.IsNullOrWhiteSpace(comboBox5.Text))
            {
                SqlCommand command = new SqlCommand("UPDATE [Cars] SET [Owner]=@Owner, [Brand]=@Brand, [Model]=@Model " +
                    "WHERE [CarId]=@Id", SqlConnection);
                command.Parameters.AddWithValue("Id", comboBox2.Text);
                command.Parameters.AddWithValue("Owner", comboBox3.Text);
                command.Parameters.AddWithValue("Brand", comboBox4.Text);
                command.Parameters.AddWithValue("Model", comboBox5.Text);


                await command.ExecuteNonQueryAsync();
            }
            else if (!string.IsNullOrEmpty(comboBox2.Text) && !string.IsNullOrWhiteSpace(comboBox2.Text))
            {
                label9.Visible = true;
                label9.Text = "Все поля должны быть заполнены!";
            }
            else
            {
                label9.Visible = true;
                label9.Text = "Id должен быть заполнен!";
            }
        }        

        private void BacktoolStripButton2_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();
            mainForm.Show();
            Hide();
        }

        private async void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (label11.Visible)
                label11.Visible = false;
            if (!string.IsNullOrEmpty(comboBox3.Text) && !string.IsNullOrWhiteSpace(comboBox3.Text))
            {
                SqlCommand command = new SqlCommand("DELETE FROM [Cars] WHERE [CarId]=@Id", SqlConnection); // sql комманда удаление
                command.Parameters.AddWithValue("Id", comboBox3.Text);

                await command.ExecuteNonQueryAsync();
            }
            else
            {
                label11.Visible = true;
                label11.Text = "Id должен быть заполнен!";
            }
        }

        private void ClearingBtn_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }
    }
}
