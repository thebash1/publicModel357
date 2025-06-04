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
    public partial class DeleteRegister : Form
    {
        public DeleteRegister()
        {
            InitializeComponent();

            int x = 800; int y = 500;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(x, y);
            this.MaximizeBox = false;

            Label labelTitle = DinamicControls.CreateLabel("labelTitle", "Eliminar registro", (x - 750) / 2, (y / 2 - 230), 300, 40);
            labelTitle.Font = new Font("Consolas", 20, FontStyle.Bold);
            DataGridView dataGridView = DinamicControls.CreateDataGridView("dataGridViewDelete", 40, 80, x - 100, y - 150);

            Controls.Add(dataGridView);
            Controls.Add(labelTitle);
        }

        private void DeleteRegister_Load(object sender, EventArgs e)
        {
            RegisterCrud registerCrud = new RegisterCrud();
            registerCrud.ListRegisters((DataGridView)Controls["dataGridViewDelete"]);
        }
    }
}
