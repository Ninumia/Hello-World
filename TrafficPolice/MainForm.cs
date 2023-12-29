using System;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace TrafficPolice
{
    public partial class MainForm : MaterialForm
    {
        public MainForm()
        {
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Cyan500, Primary.Cyan50, Primary.Orange50, Accent.Amber700, TextShade.WHITE);
        }

        private void OwnerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OwnerForm owner = new OwnerForm();
            owner.Show();
            Hide();
        }

        private void CarsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CarsForm cars = new CarsForm();
            cars.Show();
            Hide();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void PenaltiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PenaltiesForm penalties = new PenaltiesForm();
            penalties.Show();
            Hide();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
