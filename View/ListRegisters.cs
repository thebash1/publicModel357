using System;
using System.Drawing;
using System.Windows.Forms;

namespace Model357App.View
{
    public partial class ListRegisters : Form
    {
        public ListRegisters()
        {
            InitializeComponent();

            Size = new Size(1067, 600);
            FormBorderStyle = FormBorderStyle.FixedSingle; 
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;

            int x = Width; int y = Height;
            Label tittle = DinamicControls.CreateLabel("labelTittle", "Lista de registros", x/2 - 500, 20, 300, 40);
            tittle.Font = new Font("Consolas", 20, FontStyle.Bold);
            Controls.Add(tittle);

            DataGridView dataGridView = DinamicControls.CreateDataGridView("dataGridViewList", 40, 80, x - 100, y - 150);
            Controls.Add(dataGridView);

        }

        private void ListUsers_Load(object sender, EventArgs e)
        {
            RegisterCrud registerCrud = new RegisterCrud();
            registerCrud.ListRegisters((DataGridView)Controls["dataGridViewList"]);
        }
    }
}
