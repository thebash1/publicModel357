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
    public partial class FormUpdateAsk : Form
    {
        private AskCrud askCrud;
        private DataGridView dataGridAsks;

        public FormUpdateAsk(DataGridView parentDatGrid)
        {
            InitializeComponent();
            askCrud = new AskCrud();
            dataGridAsks = parentDatGrid;

            int x = 800; int y = 500;
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Size = new Size(x, y);
            
            // Título
            Label labelTitle = DinamicControls.CreateLabel("labelTitle", "Actualizar pregunta", (x - 720) / 2, 40, 300, 40);
            labelTitle.Font = new Font("Consolas", 20, FontStyle.Bold);

            // Campo de pregunta
            Label labelQuestion = DinamicControls.CreateLabel("labelQuestion", "Pregunta:", 50, 100, 100, 30);
            TextBox textBoxQuestion = DinamicControls.CreateTextBox("textBoxQuestion", 50, 130, x - 100, 50);

            // Radio Buttons para opciones
            Label labelOptions = DinamicControls.CreateLabel("labelOptions", "Tipo de pregunta:", 50, 190, 200, 30);
            labelOptions.Font = new Font("Consolas", 12, FontStyle.Bold);

            // Grupo de radio buttons
            GroupBox groupBoxOptions = new GroupBox();
            groupBoxOptions.Name = "groupBoxOptions";
            groupBoxOptions.Location = new Point(50, 220);
            groupBoxOptions.Size = new Size(x - 100, 120);

            // Radio buttons
            RadioButton radioOpen = DinamicControls.CreateRadioButton("radioOpen", "1. ¿Es abierta?", 30, 30, 150, 30);
            RadioButton radioMultiple = DinamicControls.CreateRadioButton("radioMultiple", "2. ¿Es de selección múltiple?", 200, 30, 200, 30);
            RadioButton radioPredef = DinamicControls.CreateRadioButton("radioPredef", "3. ¿Hay opciones predeterminadas?", 430, 30, 250, 30);

            radioMultiple.Enabled = false;
            radioPredef.Enabled = false;

            // Agregar radio buttons al group box
            groupBoxOptions.Controls.AddRange(new Control[] { radioOpen, radioMultiple, radioPredef });

            // Botones
            Button buttonAdd = DinamicControls.CreateButton("buttonAdd", "Actualizar", (x / 2) + 30, y - 120, 150, 40);
            Button buttonDiscard = DinamicControls.CreateButton("buttonDiscard", "Descartar", (x / 2) - 180, y - 120, 150, 40);

            // Personalizar botones
            DinamicControls.CustomButton(buttonAdd);
            DinamicControls.CustomButton(buttonDiscard);

            // Agregar eventos
            buttonAdd.Click += ButtonAdd_Click;
            buttonDiscard.Click += (s, args) => this.Close();

            // Agregar controles al formulario
            Controls.AddRange(new Control[]
            {
                labelTitle,
                labelQuestion,
                textBoxQuestion,
                labelOptions,
                groupBoxOptions,
                buttonAdd,
                buttonDiscard
            });
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener el TextBox y su valor correctamente
                TextBox questionTextBox = (TextBox)Controls["textBoxQuestion"];
                if (questionTextBox == null)
                {
                    MessageBox.Show("Error al obtener el control de la pregunta.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string question = questionTextBox.Text;

                if (string.IsNullOrWhiteSpace(question))
                {
                    MessageBox.Show("La pregunta no puede estar vacía.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                askCrud.UpdateAsk(
                    dataGridAsks.SelectedRows[0].Cells[0].Value.ToString(),
                    question,
                    "abierta"
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar la pregunta: {ex.Message}",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormUpdateAsk_Load(object sender, EventArgs e)
        {

        }
    }
}
