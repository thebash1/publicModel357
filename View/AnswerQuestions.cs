using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Model357App.View
{
    public partial class AnswerQuestions : Form
    {
        private Panel scrollPanel;
        private int currentY = 20; // Posición inicial Y para los controles
        private const int SPACING = 30; // Espacio entre preguntas
        private const int QUESTION_HEIGHT = 40; // Altura para las preguntas
        private const int ANSWER_HEIGHT = 40; // Altura para las respuestas
        private AskCrud askCrud;

        public AnswerQuestions()
        {
            InitializeComponent();
            InitializeForm();
            LoadQuestions();
        }

        private void InitializeForm()
        {
            // Configurar el formulario
            this.Size = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Text = "Responder Preguntas";

            // Crear título
            Label titleLabel = DinamicControls.CreateLabel("labelTitle", "Responder Preguntas", 20, 20, 860, 40);
            titleLabel.Font = new Font("Consolas", 20, FontStyle.Bold);
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;

            // Crear panel de desplazamiento
            scrollPanel = DinamicControls.CreateScrollPanel("scrollPanel", 20, 100, 845, 350);

            // Crear botón para guardar respuestas
            Button saveButton = DinamicControls.CreateButton("buttonSave", "Guardar Respuestas", 350, 500, 200, 40);
            DinamicControls.CustomButton(saveButton);
            saveButton.Click += SaveButton_Click;

            // Agregar controles al formulario
            this.Controls.AddRange(new Control[] { titleLabel, scrollPanel, saveButton });
        }

        private void LoadQuestions()
        {
            askCrud = new AskCrud();
            var questions = askCrud.GetQuestions();

            foreach (var question in questions)
            {
                // Crear label para la pregunta
                Label questionLabel = DinamicControls.CreateLabel(
                    $"question_{question.Id}", // Asume que tienes un Id en tu pregunta
                    question.Text, // Asume que tienes el texto de la pregunta
                    20, 
                    currentY, 
                    780, 
                    QUESTION_HEIGHT
                );
                questionLabel.Font = new Font("Consolas", 12, FontStyle.Bold);

                // Crear TextBox para la respuesta
                TextBox answerBox = DinamicControls.CreateTextBox(
                    $"answer_{question.Id}",
                    20,
                    currentY + QUESTION_HEIGHT + 5,
                    780,
                    ANSWER_HEIGHT
                );
                answerBox.Multiline = true;

                // Agregar controles al panel
                scrollPanel.Controls.Add(questionLabel);
                scrollPanel.Controls.Add(answerBox);

                // Actualizar posición Y para la siguiente pregunta
                currentY += QUESTION_HEIGHT + ANSWER_HEIGHT + SPACING;
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, string> userResponses = new Dictionary<string, string>();
                string questionId = string.Empty;
                string answer = string.Empty;

                // Aquí implementa la lógica para guardar las respuestas
                foreach (Control control in scrollPanel.Controls)
                {
                    if (control is TextBox textBox && textBox.Name.StartsWith("answer_"))
                    {
                        questionId = textBox.Name.Replace("answer_", "");
                        answer = textBox.Text;

                        if (string.IsNullOrWhiteSpace(answer))
                        {
                            MessageBox.Show("Por favor responde todas las preguntas", 
                                "Advertencia", 
                                MessageBoxButtons.OK, 
                                MessageBoxIcon.Warning);
                            return;
                        }
                        
                        userResponses.Add(questionId, answer); 

                        // Guardar la respuesta (implementa este método en tu AskCrud)
                        // askCrud.SaveAnswer(questionId, answer);
                    }
                }
                askCrud = new AskCrud();
                askCrud.SaveUserResponses(ValidateGraduate.dataGraduate[0], ValidateGraduate.dataGraduate[1], userResponses);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar las respuestas: {ex.Message}", 
                    "Error", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // AnswerQuestions
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "AnswerQuestions";
            this.Text = "Formulario";
            this.ResumeLayout(false);

        }
    }
}