using System;
using System.Drawing;
using System.Windows.Forms;

namespace Model357App.View
{
    public partial class UpdateAsk : Form
    {
        private AskCrud askCrud;
        private DataGridView dataGridAsks;
        private FormUpdateAsk formUpdateAsk;

        public UpdateAsk()
        {
            askCrud = new AskCrud();
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Size = new Size(900, 600);

            Label labelTitle = DinamicControls.CreateLabel("labelTitle", "Actualizar preguntas", 20, 20, 300, 30);
            labelTitle.Font = new Font("Consolas", 16, FontStyle.Bold);

            // Obener el DataGridView dinámico y configurarlo
            dataGridAsks = askCrud.ListAsks();
            if (dataGridAsks != null)
            {
                // Ajustar tamaño y posición según el formulario
                dataGridAsks.Name = "dataGridUpdate";
                dataGridAsks.Location = new Point(20, 60);
                dataGridAsks.Size = new Size(Width - 40, Height - 200);

            }

            Button buttonUpdate = DinamicControls.CreateButton("buttonUpdate", "Actualizar", Width / 2, 500, 150, 40);
            DinamicControls.CustomButton(buttonUpdate);
            Button buttonCancel = DinamicControls.CreateButton("buttonCancel", "Cancelar", Width / 2 - 200, 500, 150, 40);
            DinamicControls.CustomButton(buttonCancel);

            buttonUpdate.Click += buttonUpdate_Click;
            buttonCancel.Click += (s, args) => this.Close();

            Controls.AddRange(new Control[]
            {
                labelTitle,
                dataGridAsks,
                buttonUpdate,
                buttonCancel
            });

            this.Resize += (s, args) =>
            {
                dataGridAsks.Size = new Size(this.ClientSize.Width - 40, this.ClientSize.Height - 40);
            };

        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridAsks.SelectedRows.Count > 0)
                {
                    formUpdateAsk = new FormUpdateAsk(dataGridAsks);
                    askCrud = new AskCrud();

                    formUpdateAsk.ShowDialog(); // Mostrar el formulario de actualización

                    // Crear un nuevo DataGridView con las mismas propiedades
                    DataGridView newDataGrid = askCrud.ListAsks();

                    if (newDataGrid != null)
                    {
                        // Configurar el nuevo DataGridView con las propiedades del anterior
                        newDataGrid.Name = "dataGridUpdate";
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
                MessageBox.Show($"Error al actualizar la pregunta: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateAsk_Load(object sender, EventArgs e)
        {

        }
        

    }
}
