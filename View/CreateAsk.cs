using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;

namespace Model357App.View
{
    public partial class CreateAsk : Form
    {
        public static int askType { get; set; } = 0;
        public static int numAsk { get; set; } = 0;

        public CreateAsk()
        {
            InitializeComponent();

            int x = 800; int y = 500;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Size = new Size(x, y);

            // Título
            Label labelTitle = DinamicControls.CreateLabel("labelTitle", "Crear pregunta", (x - 720) / 2, 40, 300, 40);
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
            Button buttonAdd = DinamicControls.CreateButton("buttonAdd", "Agregar", (x/2) + 30, y - 120, 150, 40);
            Button buttonDiscard = DinamicControls.CreateButton("buttonDiscard", "Descartar", (x/2) - 180, y - 120, 150, 40);

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
                // Obtener la pregunta
                string question = ((TextBox)Controls["textBoxQuestion"]).Text;

                // Verificar que se haya ingresado una pregunta
                if (string.IsNullOrWhiteSpace(question))
                {
                    MessageBox.Show("Por favor ingrese una pregunta.", 
                        "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Verificar que se haya seleccionado un tipo de pregunta
                GroupBox groupBox = (GroupBox)Controls.Find("groupBoxOptions", true)[0];
                bool typeSelected = false;
                string selectedType = "";
                int typeNumber = 0;

                foreach (RadioButton rb in groupBox.Controls.OfType<RadioButton>())
                {
                    if (rb.Checked)
                    {
                        typeSelected = true; 
                        typeNumber = int.Parse(rb.Text.Split('.')[0]);
                        selectedType = rb.Text.Substring(rb.Text.IndexOf('.') + 2);
                        break;
                    }
                }

                if (!typeSelected)
                {
                    MessageBox.Show("Por favor seleccione un tipo de pregunta.", 
                        "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                AskCrud askCrud = new AskCrud();
                //AddAskOpt askOpt = new AddAskOpt();
                ListAsk listAsk = new ListAsk();

                if (typeNumber == 1)
                {
                    askType = 1;
                    numAsk++;
                    askCrud.CreateAsk(question, selectedType.Replace(selectedType, "abierta"));
                    //listAsk.UpdateCellValue(numAsk, "Options", AddAskOpt.askText?.ToString() ?? "N/A");
                }

                //if (typeNumber == 2)
                //{
                //    askType = 2;
                //    numAsk++;
                //    askOpt.ShowDialog();
                //    askCrud.CreateAsk(question, selectedType.Replace(selectedType, "selección múltiple"));
                //    listAsk.UpdateCellValue(numAsk, "Options", AddAskOpt.askText.ToString());
                //}

                //if (typeNumber == 3)
                //{
                //    askType = 3;
                //    numAsk++;
                //    askOpt.ShowDialog();
                //    askCrud.CreateAsk(question, selectedType.Replace(selectedType, "opciones predeterminadas"));
                //    listAsk.UpdateCellValue(numAsk, "Options", AddAskOpt.askText.ToString());
                //}
                Controls["textBoxQuestion"].Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar la pregunta: {ex.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateAsk_Load(object sender, EventArgs e)
        {
            
        }
    }
}
