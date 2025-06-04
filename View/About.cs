using System;
using System.Drawing;
using System.Windows.Forms;

namespace Model357App.View
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();

            // Configuración básica del formulario
            Size = new Size(500, 400);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;

            // Logo
            PictureBox logoBox = DinamicControls.CreatePictureBox
                ("pictureboxLogobox", @"C:\Users\Usuario\Desktop\Git\model357\img\metric.png", 30, 20, 100, 100);

            // Título del Proyecto
            Label titleLabel = DinamicControls.CreateLabel("labelTittle", "Model 357", 150, 30, 200, 40);
            titleLabel.Font = new Font("Segoe UI", 24, FontStyle.Bold);

            Label versionLabel = DinamicControls.CreateLabel("labelVersion", "Versión 1.0.0", 155, 70, 100, 20);
            versionLabel.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            // Descripción del Proyecto
            TextBox descriptionBox = DinamicControls.CreateTextBox("textboxDescription", 30, 130, 420, 60);
            descriptionBox.Text = "Model 357 es un programa estadístico orientado a determinar la probabilidad " +
                      "de desempleo en los egresados del programa Ingeniería en Sistemas de la " +
                      "Universidad Popular del Cesar - Seccional Aguachica.";

            descriptionBox.Multiline = true;
            descriptionBox.ReadOnly = true;
            descriptionBox.BorderStyle = BorderStyle.None;

            Label licenseLabel = DinamicControls.CreateLabel("labelLicense", "Licencia: (Apache 2.0)", 30, 200, 200, 20);
            licenseLabel.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            // licencia del Proyecto
            TextBox licenseBox = DinamicControls.CreateTextBox("textboxLicense", 30, 220, 420, 100);
            licenseBox.Text = "Copyright 2024 Model357 " +
                      "Licensed under the Apache License, Version 2.0 (the \"License\"); " +
                      "you may not use this file except in compliance with the License. " +
                      "You may obtain a copy of the License at " +
                      "http://www.apache.org/licenses/LICENSE-2.0" +
                      " Unless required by applicable law or agreed to in writing, software " +
                      "distributed under the License is distributed on an \"AS IS\" BASIS, " +
                      "WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. " +
                      "See the License for the specific language governing permissions and " +
                      "limitations under the License.";

            licenseBox.Multiline = true;
            licenseBox.ReadOnly = true;
            licenseBox.ScrollBars = ScrollBars.Vertical;

            Button acceptButton = DinamicControls.CreateButton("buttonAccept", "Aceptar", 375, 330, 75, 23);

            acceptButton.Click += (s, e) => this.Close(); // Cerrar el formulario al hacer clic en Aceptar

            // Agregar controles al formulario
            Controls.AddRange(new Control[]
            {
                logoBox,
                titleLabel,
                versionLabel,
                descriptionBox,
                licenseLabel,
                licenseBox,
                acceptButton
            });

        }

        private void AcercaDe_Load(object sender, EventArgs e)
        {

        }
    }
}
