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
    public partial class DeleteAsk : Form
    {
        private AskCrud askCrud;
        private DataGridView dataGridAsks;

        public DeleteAsk()
        {
            askCrud = new AskCrud();
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.Size = new Size(900, 600);

            Label labelTitle = DinamicControls.CreateLabel("labelTitle", "Eliminar preguntas", 20, 20, 300, 30);
            labelTitle.Font = new Font("Consolas", 16, FontStyle.Bold);

            // Obener el DataGridView dinámico y configurarlo
            dataGridAsks = askCrud.ListAsks();
            if (dataGridAsks != null)
            {
                // Ajustar tamaño y posición según el formulario
                dataGridAsks.Name = "dataGridDelete";
                dataGridAsks.Location = new Point(20, 60);
                dataGridAsks.Size = new Size(Width - 40, Height - 200);

            }

            Button buttonDelete = DinamicControls.CreateButton("buttonDelete", "Eliminar", Width / 2, 500, 150, 40);
            DinamicControls.CustomButton(buttonDelete);
            Button buttonCancel = DinamicControls.CreateButton("buttonCancel", "Cancelar", Width / 2 - 200, 500, 150, 40);
            DinamicControls.CustomButton(buttonCancel);

            buttonDelete.Click += buttonDelete_Click;
            buttonCancel.Click += (s, args) => this.Close();

            Controls.AddRange(new Control[]
            {
                labelTitle,
                dataGridAsks,
                buttonDelete,
                buttonCancel
            });

            this.Resize += (s, args) =>
            {
                dataGridAsks.Size = new Size(this.ClientSize.Width - 40, this.ClientSize.Height - 40);
            };

        }

        private void DeleteAsk_Load(object sender, EventArgs e)
        {
            
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridAsks.SelectedRows.Count > 0)
                {
                    askCrud = new AskCrud();
                    askCrud.DeleteAsk(dataGridAsks.SelectedRows[0].Cells["id"].Value.ToString());

                    // Crear un nuevo DataGridView con las mismas propiedades
                    DataGridView newDataGrid = askCrud.ListAsks();

                    if (newDataGrid != null)
                    {
                        // Configurar el nuevo DataGridView con las propiedades del anterior
                        newDataGrid.Name = "dataGridDelete";
                        newDataGrid.Location = dataGridAsks.Location;
                        newDataGrid.Size = dataGridAsks.Size;
                        newDataGrid.AllowUserToResizeColumns = false;
                        newDataGrid.AllowUserToResizeRows = false;

                        // Remover el DataGridView anterior y añadir el nuevo
                        Controls.Remove(dataGridAsks);
                        Controls.Add(newDataGrid);

                        // Actualizar la referencia
                        dataGridAsks = newDataGrid;

                        // Reconfigurar el evento Resize
                        this.Resize += (s, ev) =>
                        {
                            dataGridAsks.Size = new Size(this.ClientSize.Width - 40, this.ClientSize.Height - 40);
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar la pregunta: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
