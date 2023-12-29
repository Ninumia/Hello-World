using System;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using MaterialSkin;
using MaterialSkin.Controls;

namespace TrafficPolice
{
    public partial class PenaltiesForm : MaterialForm
    {
        SqlConnection SqlConnection;
        public PenaltiesForm()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Cyan500, Primary.Cyan50, Primary.Orange50, Accent.Amber700, TextShade.WHITE);
        }

        private async void PenaltiesForm_Load(object sender, EventArgs e)
        {
            //  данная строка кода позволяет загрузить данные в таблицу "policeDataSet.Owners". 
            this.ownersTableAdapter.Fill(this.policeDataSet.Owners);
            SqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Police"].ConnectionString); // строка подключения
            await SqlConnection.OpenAsync(); // открыли подключение к БД
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM [Penalties] ", SqlConnection);
            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["FineId"]) + "   " + Convert.ToString(sqlReader["Date"]) +
                        "  " + Convert.ToString(sqlReader["Violation"]) + "   " + Convert.ToString(sqlReader["Violator"]) +
                        "   " + Convert.ToString(sqlReader["Quantity"]) + "   " + Convert.ToString(sqlReader["AmountFine"]) +
                      "   " + Convert.ToString(sqlReader["TotalAmount"]));
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

        private async void ADDBtn_Click(object sender, EventArgs e)
        {
            if (label7.Visible)
                label7.Visible = false;
            if (!string.IsNullOrEmpty(dateTimePicker1.Text) && !string.IsNullOrWhiteSpace(dateTimePicker1.Text) &&
               !string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text) &&
               !string.IsNullOrEmpty(comboBox1.Text) && !string.IsNullOrWhiteSpace(comboBox1.Text) && 
               !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text) && 
               !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox3.Text) && 
               !string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text))
            {
                SqlCommand command = new SqlCommand("INSERT INTO [Penalties] (Date, Violation, Violator, Quantity, " +
                    "AmountFine, TotalAmount) VALUES(@Date, @Violation, @Violator, @Quantity, @AmountFine, @TotalAmount)", SqlConnection);
                command.Parameters.AddWithValue("Date", dateTimePicker1.Text);
                command.Parameters.AddWithValue("Violation", textBox1.Text);
                command.Parameters.AddWithValue("Violator", comboBox1.Text);
                command.Parameters.AddWithValue("Quantity", textBox2.Text);
                command.Parameters.AddWithValue("AmountFine", textBox3.Text);
                command.Parameters.AddWithValue("TotalAmount", textBox4.Text);


                await command.ExecuteNonQueryAsync();
            }
            else
            {
                label7.Visible = true;
                label7.Text = "Все поля должны быть заполнены!";
            }
        }

        private async void UpdatetoolStripButton_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM [Penalties] ", SqlConnection);
            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["FineId"]) + "   " + Convert.ToString(sqlReader["Date"]) +
                        "  " + Convert.ToString(sqlReader["Violation"]) + "   " + Convert.ToString(sqlReader["Violator"]) +
                        "   " + Convert.ToString(sqlReader["Quantity"]) + "   " + Convert.ToString(sqlReader["AmountFine"]) +
                      "   " + Convert.ToString(sqlReader["TotalAmount"]));
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

        private void BacktoolStripButton_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();
            mainForm.Show();
            Hide();
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }

        private void CalcBtn_Click(object sender, EventArgs e)
        {
            double quantity;
            double amountFine;

            quantity = Convert.ToDouble(textBox2.Text);
            amountFine = Convert.ToDouble(textBox3.Text);

            switch (comboBox2.Text)
            {
                case "*":

                    textBox4.Text = Convert.ToString(quantity * amountFine);
                    break;
            }
        }

        private void CalculateBtn_Click(object sender, EventArgs e)
        {
            double quantity;
            double amountFine;

            quantity = Convert.ToDouble(textBox7.Text);
            amountFine = Convert.ToDouble(textBox8.Text);

            switch (comboBox4.Text)
            {
                case "*":

                    textBox9.Text = Convert.ToString(quantity * amountFine);
                    break;
            }
        }

        private async void EditBtn_Click(object sender, EventArgs e)
        {
            if (label15.Visible)
            {
                label15.Visible = false;
            }

            if (!string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox5.Text) &&
                !string.IsNullOrEmpty(dateTimePicker2.Text) && !string.IsNullOrWhiteSpace(dateTimePicker2.Text) &&
               !string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox6.Text) &&
               !string.IsNullOrEmpty(comboBox3.Text) && !string.IsNullOrWhiteSpace(comboBox3.Text) &&
               !string.IsNullOrEmpty(textBox7.Text) && !string.IsNullOrWhiteSpace(textBox7.Text) &&
               !string.IsNullOrEmpty(textBox8.Text) && !string.IsNullOrWhiteSpace(textBox8.Text) && 
               !string.IsNullOrEmpty(textBox9.Text) && !string.IsNullOrWhiteSpace(textBox9.Text))
            {
                SqlCommand command = new SqlCommand("UPDATE [Penalties] SET [Date]=@Date, " +
                    "[Violation]=@Violation, [Violator]=@Violator, [Quantity]=@Quantity, [AmountFine]=@AmountFine, [TotalAmount]=@TotalAmount " +
                    "WHERE [FineId]=@Id", SqlConnection);
                command.Parameters.AddWithValue("Id", textBox5.Text);
                command.Parameters.AddWithValue("Date", dateTimePicker2.Text);
                command.Parameters.AddWithValue("Violation", textBox6.Text);
                command.Parameters.AddWithValue("Violator", comboBox3.Text);
                command.Parameters.AddWithValue("Quantity", textBox7.Text);
                command.Parameters.AddWithValue("AmountFine", textBox8.Text);
                command.Parameters.AddWithValue("TotalAmount", textBox9.Text);

                await command.ExecuteNonQueryAsync();
            }
            else if (!string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox5.Text))
            {
                label15.Visible = true;
                label15.Text = "Все поля должны быть заполнены!";
            }
            else
            {
                label15.Visible = true;
                label15.Text = "Id должен быть заполнен!";
            }
        }

        private async void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (label17.Visible)
                label17.Visible = false;
            if (!string.IsNullOrEmpty(textBox10.Text) && !string.IsNullOrWhiteSpace(textBox10.Text))
            {
                SqlCommand command = new SqlCommand("DELETE FROM [Penalties] WHERE [FineId]=@Id", SqlConnection); // sql комманда удаление
                command.Parameters.AddWithValue("Id", textBox10.Text);

                await command.ExecuteNonQueryAsync();
            }
            else
            {
                label17.Visible = true;
                label17.Text = "Id должен быть заполнен!";
            }
        }
    }
}
