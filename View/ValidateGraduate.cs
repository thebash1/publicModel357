using System;
using System.Drawing;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;

namespace Model357App.View
{
    public partial class ValidateGraduate : Form
    {
        private AnswerQuestions answerQuestions;
        private MenuEvent menuEvent;
        public static string[] dataGraduate = new string[2];

        public ValidateGraduate()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Size = new Size(300, 450);

            Label title = DinamicControls.CreateLabel("labelTitle", "Validar Egresado", 40, 30, 300, 40);
            title.Font = new Font("Consolas", 16, FontStyle.Bold);

            Label labelEmail = DinamicControls.CreateLabel("labelEmail", "Correo:", 40, 80, 200, 40);
            TextBox textboxEmail = DinamicControls.CreateTextBox("textboxEmail", 40, 120, 200, 40);

            Label labelPhone = DinamicControls.CreateLabel("labelPhone", "Teléfono:", 40, 160, 200, 40);
            TextBox textboxPhone = DinamicControls.CreateTextBox("textboxPhone", 40, 200, 200, 40);

            Button validateButton = DinamicControls.CreateButton("buttonValidate", "Validar", 40, 270, 200, 35);
            DinamicControls.CustomButton(validateButton);
            
            Button cancelButton = DinamicControls.CreateButton("buttonCancel", "Regresar", 40, 320, 200, 35);
            DinamicControls.CustomButton(cancelButton);

            validateButton.Click += buttonValidate_Click;
            cancelButton.Click += (sender, e) => this.Close();

            Controls.AddRange(new Control[]
            {
                title,
                labelEmail,
                textboxEmail,
                labelPhone,
                textboxPhone,
                validateButton,
                cancelButton
            });


        }

        private void buttonValidate_Click(object sender, EventArgs e)
        {
            RegisterCrud registerCrud = new RegisterCrud();
            Except except = new Except();

            string phone = ((TextBox)this.Controls["textboxPhone"]).Text.Trim();
            string email = ((TextBox)this.Controls["textboxEmail"]).Text.Trim();
            fillData(phone, email);

            if (!except.validateEmail(email))
            {
                MessageBox.Show("Correo electrónico inválido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!except.validatePhone(phone)) 
            {
                MessageBox.Show("Número de teléfono inválido, debe contener 10 dígitos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!registerCrud.IsGraduateRegistered(phone, email))
                return;
            
            ((TextBox)this.Controls["textboxEmail"]).Text = string.Empty;
            ((TextBox)this.Controls["textboxPhone"]).Text = string.Empty;

            MessageBox.Show("Validación realizada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            answerQuestions = new AnswerQuestions();
            menuEvent = new MenuEvent(this);
            menuEvent.OpenForm(answerQuestions, 900, 600);
        }

        private void ValidateGraduate_Load(object sender, EventArgs e)
        {

        }

        public static void fillData(string phone, string email)
        {
            try
            {
                // Verificar si el archivo existe
                if (!File.Exists(RegisterCrud.pathGraduate))
                {
                    MessageBox.Show("No hay egresados registrados en el sistema.",
                        "Advertencia",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Leer todas las líneas del archivo
                string[] graduates = File.ReadAllLines(RegisterCrud.pathGraduate);

                // Buscar coincidencia de teléfono y correo
                foreach (string graduate in graduates)
                {
                    if (!string.IsNullOrEmpty(graduate))
                    {
                        string[] data = graduate.Split(',');
                        // El teléfono está en el índice 3 y el correo en el índice 4
                        if (data.Length >= 5 &&
                            data[3].Equals(phone, StringComparison.OrdinalIgnoreCase) &&
                            data[4].Equals(email, StringComparison.OrdinalIgnoreCase))
                        {
                            dataGraduate[0] = data[1] + " " + data[2]; // Nombre completo
                            dataGraduate[1] = data[8]; // programa
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al verificar el registro: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        
    }
}
