using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Model357App.View
{
    public partial class AddAskOpt : Form
    {
        public static string askText { get; set; }

        public AddAskOpt()
        {
            InitializeComponent();
            
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.Size = new Size(550, 400);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            Label labelTitle = DinamicControls.CreateLabel("labelTitle", "Agregar opciones a la pregunta", 45, 30, 400, 40);
            labelTitle.Font = new Font("Consolas", 14, FontStyle.Bold);
            
            Label labelText = DinamicControls.CreateLabel("labelText", "Escribe abajo tu pregunta:", 45, 80, 400, 40);
            labelText.TextAlign = ContentAlignment.MiddleLeft;

            TextBox textboxOption = DinamicControls.CreateTextBox("textBoxOption", 50, 120, 430, 100);
            textboxOption.Multiline = true;

            Button buttonCancel = DinamicControls.CreateButton("buttonAdd", "Cancelar", 100, 250, 150, 30);
            Button buttonAdd = DinamicControls.CreateButton("buttonAdd", "Añadir", 300, 250, 150, 30);
            DinamicControls.CustomButton(buttonAdd);
            DinamicControls.CustomButton(buttonCancel);
            buttonAdd.Font = new Font("Consolas", 10);
            buttonCancel.Font = new Font("Consolas", 10);

            buttonCancel.Click += (s, e) => this.Close();
            buttonAdd.Click += buttonAdd_Click;

            Controls.AddRange(new Control[]
            {
                labelTitle,
                labelText,
                textboxOption,
                buttonAdd,
                buttonCancel
            });
        }

        private void AddAskOpt_Load(object sender, EventArgs e)
        {

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            int type = CreateAsk.askType;
            if (type > 0 && type < 4)
                AddOption(type);

            MessageBox.Show("Opción añadida correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Controls["textBoxOption"].Text = string.Empty;

        }

        private void AddOption(int type)
        {
            if (type == 2)
                askText = Controls["textboxOption"].Text;
            
            if (type == 3)
                askText = Controls["textboxOption"].Text;
            
            else 
                return;
        }

    }
}
