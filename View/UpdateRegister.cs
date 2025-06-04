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
    public partial class UpdateRegister : Form
    {
        private string id;
        private string name;

        public UpdateRegister()
        {
            InitializeComponent();

            int x = 800;
            int y = 500;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(x, y);
            this.MaximizeBox = false;

            Label labelTittle = DinamicControls.CreateLabel("labelTittle", "Actualizar registro", (x - 750) /2, (y/2) - 230, 300, 40);
            labelTittle.Font = new Font("Consolas", 20, FontStyle.Bold);
            DataGridView dataGridView = DinamicControls.CreateDataGridView("dataGridViewUpdate", 40, 60, x - 100, y - y/2);
            dataGridView.SelectionChanged += DataGridView_SelectionChanged;
            dataGridView.MultiSelect = false;

            Button buttonUpdate = DinamicControls.CreateButton("buttonUpdate", "Actualizar", (x - 150) / 2, (y/2) + 100, 150, 40);
            buttonUpdate.Click += buttonUpdate_Click;

            Controls.Add(labelTittle);
            Controls.Add(dataGridView);
            Controls.Add(buttonUpdate);
        }

        private void UpdateRegister_Load(object sender, EventArgs e)
        {
            RegisterCrud registerCrud = new RegisterCrud();
            DataGridView dvg = ((DataGridView)Controls["dataGridViewUpdate"]);
            registerCrud.ListRegisters(dvg);
            dvg.ClearSelection(); 
            Application.DoEvents();
        }

        private void DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = MenuEvent.ReturnCurrentRow(this, "dataGridViewUpdate");    
            id = selectedRow.Cells[0].Value?.ToString();
            name = selectedRow.Cells[1].Value?.ToString(); 
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"id: {id}, nombre: {name}");
        }

    }
}
