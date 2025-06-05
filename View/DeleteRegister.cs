using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Model357App.View
{
    public partial class DeleteRegister : Form
    {
        public static string _id { get; set; } = "0";
        public DeleteRegister()
        {
            InitializeComponent();

            int x = 800; int y = 500;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(x, y);
            this.MaximizeBox = false;

            Label labelTitle = DinamicControls.CreateLabel("labelTitle", "Eliminar registro", (x - 720) / 2, (y / 2 - 230), 300, 40);
            labelTitle.Font = new Font("Consolas", 20, FontStyle.Bold);
            DataGridView dataGridView = DinamicControls.CreateDataGridView("dataGridViewDelete", 40, 60, x - 100, y - 200);
            dataGridView.SelectionChanged += DataGridView_SelectionChanged;
            dataGridView.MultiSelect = false;

            // Crear botones
            Button buttonBack = DinamicControls.CreateButton("buttonBack", "Regresar", (x / 2) - 200, y - 100, 150, 40);
            Button buttonDelete = DinamicControls.CreateButton("buttonDelete", "Eliminar", (x / 2), y - 100, 150, 40);

            // Personalizar botones
            DinamicControls.CustomButton(buttonBack);
            DinamicControls.CustomButton(buttonDelete);

            // Agregar eventos a los botones
            buttonBack.Click += (s, args) => this.Close();
            buttonDelete.Click += ButtonDelete_Click;

            Controls.AddRange(new Control[] 
            { 
                labelTitle, 
                dataGridView, 
                buttonBack, 
                buttonDelete 
            });
        }

        private void DeleteRegister_Load(object sender, EventArgs e)
        {
            RegisterCrud registerCrud = new RegisterCrud();
            if (Login.rol == 1)
                registerCrud.ListRegisters((DataGridView)Controls["dataGridViewDelete"]);

            if (Login.rol == 2)
                registerCrud.ListRegistersUsers((DataGridView)Controls["dataGridViewDelete"]);

        }

        private void DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = MenuEvent.ReturnCurrentRow(this, "dataGridViewDelete");
            _id = selectedRow?.Cells[0].Value?.ToString() ?? "0";
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que se haya seleccionado un registro
                if (string.IsNullOrEmpty(_id) || _id == "0")
                {
                    MessageBox.Show("Por favor, seleccione un registro para eliminar.",
                        "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                else
                {
                    RegisterCrud registerCrud = new RegisterCrud();
                    registerCrud.DeleteRegister(_id);

                    // Recargar el DataGridView
                    ForceRefreshGrid();

                    // Limpiar la selección
                    _id = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar el registro: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ForceRefreshGrid()
        {
            DataGridView dgv = (DataGridView)this.Controls["dataGridViewDelete"];
            dgv.SuspendLayout();

            try
            {
                dgv.DataSource = null;
                dgv.Columns.Clear();
                dgv.Rows.Clear();

                RegisterCrud registerCrud = new RegisterCrud();
                registerCrud.ListRegistersUsers(dgv);

                dgv.Refresh();
            }
            finally
            {
                dgv.ResumeLayout();
            }

            if (dgv.Rows.Count > 0)
            {
                dgv.ClearSelection();
            }
        }

    }
}
