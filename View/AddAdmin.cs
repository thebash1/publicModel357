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
    public partial class AddAdmin : Form
    {
        public AddAdmin()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen; // centrar formulario
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // evitar redimensionamiento
            this.MaximizeBox = false;
            this.Size = new Size(1067, 600); // establecer tamaño del formulario

            int x = this.Width; int y = this.Height;
            Label title = DinamicControls.CreateLabel("label1", "Registro administradores", x / 2 - 200, 30, 400, 40);
            title.Font = new Font("Consolas", 20, FontStyle.Bold);
            #region formulario registro

            Label labelName = DinamicControls.CreateLabel("labelName", "Nombre: ", x / 4 - 100, y / 5, 100, 40);
            labelName.TextAlign = ContentAlignment.TopRight;
            TextBox name = DinamicControls.CreateTextBox("textboxName", x / 4, y / 5, 200, 50);

            Label labelLastName = DinamicControls.CreateLabel("labelLastName", "Apellido: ", x / 4 + 250, y / 5, 100, 40);
            labelLastName.TextAlign = ContentAlignment.TopRight;
            TextBox lastName = DinamicControls.CreateTextBox("textboxLastName", x / 4 + 350, y / 5, 200, 50);

            int userY = y / 3 - 20;
            int emailY = y / 2 - 60;
            int sexY = y / 2;
            int programY = y / 2 + 40; 

            // campo de correo
            Label labelEmail = DinamicControls.CreateLabel("labelEmail", "Correo: ", x / 4 - 100, userY, 100, 40);
            labelEmail.TextAlign = ContentAlignment.TopRight;
            TextBox email = DinamicControls.CreateTextBox("textboxEmail", x / 4, userY, 200, 50);

            // campo de teléfono
            Label labelPhone = DinamicControls.CreateLabel("labelPhone", "Teléfono: ", x / 4 + 250, userY, 100, 40);
            labelPhone.TextAlign = ContentAlignment.TopRight;
            TextBox phone = DinamicControls.CreateTextBox("textboxPhone", x / 4 + 350, userY, 200, 50);

            // campo de sexo
            Label labelSex = DinamicControls.CreateLabel("labelSex", "Sexo: ", x / 4 - 100, emailY, 100, 40);
            labelSex.TextAlign = ContentAlignment.TopRight;
            ComboBox sex = DinamicControls.CrearComboBox("comboboxSex", x / 4, emailY, 200, 40);
            sex.Items.Add("Masculino");
            sex.Items.Add("Femenino");
            sex.DropDownStyle = ComboBoxStyle.DropDownList; // establece solo lectura en el combo box

            // campo de edad
            Label labelAge = DinamicControls.CreateLabel("labelAge", "Edad: ", x / 4 + 250, emailY, 100, 40);
            labelAge.TextAlign = ContentAlignment.TopRight;
            ComboBox age = DinamicControls.CrearComboBox("comboboxAge", x / 4 + 350, emailY, 200, 40);
            addItemsComboBox(age, returnArrayStrNumbers());
            age.DropDownStyle = ComboBoxStyle.DropDownList; // establece solo lectura en el combo box

            // campo de correo
            Label labelUser = DinamicControls.CreateLabel("labelUser", "Usuario: ", x / 4 - 100, sexY, 100, 40);
            labelUser.TextAlign = ContentAlignment.TopRight;
            TextBox user = DinamicControls.CreateTextBox("textboxUser", x / 4, sexY, 200, 50);

            // campo de teléfono
            Label labelPassword = DinamicControls.CreateLabel("labelPassword", "Contraseña: ", x / 4 + 250, sexY, 100, 40);
            labelPassword.TextAlign = ContentAlignment.TopRight;
            TextBox password = DinamicControls.CreateTextBox("textboxPassword", x / 4 + 350, sexY, 200, 50);

            // En el constructor después de los otros controles
            Label labelRole = DinamicControls.CreateLabel("labelRole", "Rol:", x / 4 - 100, programY + 25, 100, 40);
            labelRole.TextAlign = ContentAlignment.TopRight;
            ComboBox comboRole = DinamicControls.CrearComboBox("comboboxRole", x / 4, programY + 25, 200, 40);

            Button buttonRegister = DinamicControls.CreateButton("buttonRegister", "Registrar", x / 2, y / 2 + 160, 200, 40);
            DinamicControls.CustomButton(buttonRegister);

            Button buttonBack = DinamicControls.CreateButton("buttonBack", "Regresar", x / 2 - 250, y / 2 + 160, 200, 40);
            DinamicControls.CustomButton(buttonBack);

            buttonRegister.Click += new EventHandler(btnRegister_Click);
            buttonBack.Click += (s, e) => this.Close();

            Controls.AddRange(new Control[]
            {
                title,
                labelName, name,
                labelLastName, lastName,
                labelEmail, email,
                labelPhone, phone,
                labelSex, sex,
                labelAge, age,
                labelUser, user,
                labelPassword, password,
                buttonBack, buttonRegister
            });

            #endregion

        }

        private void AddAdmin_Load(object sender, EventArgs e)
        {

        }

        private string[] returnArrayStrNumbers()
        {
            string[] numbers = new string[25];
            int j = 0;
            for (int i = 16; i < 41; i++)
            {
                numbers[j] = Convert.ToString(i); // convierte los números en string para agregarlos
                j++;
            }
            return numbers;
        }

        // añadir opciones a cualquier combo box 
        private void addItemsComboBox(ComboBox comboBox, string[] items)
        {
            try
            {
                foreach (string item in items)
                {
                    // verificar si es nulo o vacio el elemento antes de agregar
                    if (!string.IsNullOrEmpty(item)) comboBox.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error, al cargar opciones de combobox: {ex.Message}");
                throw;
            }
        }

        public void btnRegister_Click(object sender, EventArgs e)
        {
            // obtener los valores de los campos
            string nombres = ((TextBox)this.Controls["textboxName"]).Text;
            string apellidos = ((TextBox)this.Controls["textboxLastName"]).Text;
            string telefono = ((TextBox)this.Controls["textboxPhone"]).Text;
            string correo = ((TextBox)this.Controls["textboxEmail"]).Text;
            string sexo = ((ComboBox)this.Controls["comboboxSex"]).SelectedItem?.ToString();
            string edad = ((ComboBox)this.Controls["comboboxAge"]).SelectedItem?.ToString();
            string usuario = ((TextBox)this.Controls["textboxUser"]).Text;
            string contraseña = ((TextBox)this.Controls["textboxPassword"]).Text;

            AdminCrud person = new AdminCrud();
            person.CreateAdmin(nombres.ToLower(), apellidos.ToLower(), telefono, correo.ToLower(), sexo, edad, usuario, contraseña);
            person.ClearRegisterAdmin(this);
        }

    }
}
