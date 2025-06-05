using System;
using System.Drawing;
using System.Windows.Forms;

namespace Model357App.View
{
    public partial class ListAsk : Form
    {
        private AskCrud askCrud;
        private DataGridView dataGridAsks;
        public ListAsk()
        {
            InitializeComponent();

            askCrud = new AskCrud();
            int x = 900; int y = 600;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(x, y);
            this.MaximizeBox = false;

            Label labelTitle = DinamicControls.CreateLabel("labelTitle", "Lista de preguntas", 20, 20, 300, 30);
            labelTitle.Font = new Font("Consolas", 16, FontStyle.Bold);
            Controls.Add(labelTitle);

            // Obener el DataGridView dinámico y configurarlo
            dataGridAsks = askCrud.ListAsks();
            if (dataGridAsks != null)
            {
                // Ajustar tamaño y posición según el formulario
                dataGridAsks.Name = "dataGridList";
                dataGridAsks.Location = new Point(20, 60);
                dataGridAsks.Size = new Size(x - 40, y - 40);

                this.Controls.Add(dataGridAsks);

                // Manejar el evento Resize del formulario
                this.Resize += (s, e) =>
                {
                    dataGridAsks.Size = new Size(this.ClientSize.Width - 40, this.ClientSize.Height - 40);
                };
            }
        }

        private void ListAsk_Load(object sender, EventArgs e)
        {

        }

        public void UpdateCellValue(int row, string column, object newValue)
        {
            if (dataGridAsks != null && row <= dataGridAsks.Rows.Count)
            {
                try
                {
                    dataGridAsks.Rows[row].Cells[column].Value = newValue;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al modificar la celda: {ex.Message}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
