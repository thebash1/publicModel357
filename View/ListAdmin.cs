using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Model357App.View
{
    public partial class ListAdmin : Form
    {
        public ListAdmin()
        {
            InitializeComponent();

            Size = new Size(1067, 600);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;

            int x = Width; int y = Height;
            Label tittle = DinamicControls.CreateLabel("labelTittle", "Lista de administradores", x / 2 - 500, 20, 400, 40);
            tittle.Font = new Font("Consolas", 20, FontStyle.Bold);
            Controls.Add(tittle);

            DataGridView dataGridView = DinamicControls.CreateDataGridView("dataGridViewList", 40, 80, x - 100, y - 150);
            Controls.Add(dataGridView);
        }

        private void ListAdmin_Load(object sender, EventArgs e)
        {
            AdminCrud adminCrud = new AdminCrud();
            adminCrud.ListAdmins((DataGridView)Controls["dataGridViewList"]);

        }
    }
}
