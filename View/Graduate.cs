using System;
using System.Drawing;
using System.Windows.Forms;

namespace Model357App.View
{
    public partial class Graduate : Form
    {
        public Graduate()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen; // centrar formulario
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // evitar redimensionamiento
            this.MaximizeBox = false;
            this.Size = new Size(1067, 600); // establecer tamaño del formulario

            // añadir titulo al formulario
            int x = this.Width; int y = this.Height;
            Label title = DinamicControls.CreateLabel("label1", "Registro egresados", x / 2 - 150, 30, 300, 40);
            title.Font = new Font("Consolas", 20, FontStyle.Bold);

            #region formulario egresado

            Label labelName = DinamicControls.CreateLabel("labelName", "Nombre: ", x / 4 - 100, y / 5, 100, 40);
            labelName.TextAlign = ContentAlignment.TopRight;
            TextBox name = DinamicControls.CreateTextBox("textboxName", x / 4, y / 5, 200, 40);

            Label labelLastName = DinamicControls.CreateLabel("labelLastName", "Apellido: ", x / 4 + 250, y / 5, 100, 40);
            labelLastName.TextAlign = ContentAlignment.TopRight;
            TextBox lastName = DinamicControls.CreateTextBox("textboxLastName", x / 4 + 350, y / 5, 200, 40);

            int emailPhone = y / 3 - 20; // ajustar la posición de los campos de usuario
            int sexAge = y / 2 - 60; // ajustar la posición del campo de correo
            int programPosition = y / 2; // ajustar la posición del campo de sexo
            int combobox = y / 2 + 40;

            // campo de correo
            Label labelEmail = DinamicControls.CreateLabel("labelEmail", "Correo: ", x / 4 - 100, emailPhone, 100, 40);
            labelEmail.TextAlign = ContentAlignment.TopRight;
            TextBox email = DinamicControls.CreateTextBox("textboxEmail", x / 4, emailPhone, 200, 40);

            // campo de teléfono
            Label labelPhone = DinamicControls.CreateLabel("labelPhone", "Teléfono: ", x / 4 + 250, emailPhone, 100, 40);
            labelPhone.TextAlign = ContentAlignment.TopRight;
            TextBox phone = DinamicControls.CreateTextBox("textboxPhone", x / 4 + 350, emailPhone, 200, 40);

            // campo de sexo
            Label labelSex = DinamicControls.CreateLabel("labelSex", "Sexo: ", x / 4 - 100, sexAge, 100, 40);
            labelSex.TextAlign = ContentAlignment.TopRight;
            ComboBox sex = DinamicControls.CrearComboBox("comboboxSex", x / 4, sexAge, 200, 40);
            sex.Items.Add("Masculino");
            sex.Items.Add("Femenino");
            sex.DropDownStyle = ComboBoxStyle.DropDownList; // establece solo lectura en el combo box

            // campo de edad
            Label labelAge = DinamicControls.CreateLabel("labelAge", "Edad: ", x / 4 + 250, sexAge, 100, 40);
            labelAge.TextAlign = ContentAlignment.TopRight;
            ComboBox age = DinamicControls.CrearComboBox("comboboxAge", x / 4 + 350, sexAge, 200, 40);
            addItemsComboBox(age, returnArrayStrNumbers());
            age.DropDownStyle = ComboBoxStyle.DropDownList; // establece solo lectura en el combo box

            Label labelProgram = DinamicControls.CreateLabel("labelProgram", "Programa", x / 4 - 100, programPosition, 100, 40);
            labelProgram.TextAlign = ContentAlignment.TopRight;
            ComboBox program = DinamicControls.CrearComboBox("comboboxProgram", x / 4, programPosition, 200, 40);
            program.Items.AddRange(new string[]
            {
                "Administración de empresas",
                "Contaduría pública",
                "Economía",
                "Ingeniería agroindustrial",
                "Ingeniería ambiental y sanitaria",
                "Ingeniería de sistemas",
                "Ingeniería en Tecnología Agropecuaría"
            });
            program.DropDownStyle = ComboBoxStyle.DropDownList;
            
            Label labelInit = DinamicControls.CreateLabel("labelInit", "Fecha inicio:", x / 2 - 100, programPosition, 200, 40);
            labelInit.TextAlign = ContentAlignment.TopRight;
            DateTimePicker programInitDate = new DateTimePicker
            {
                Name = "datepickerInitDate",
                Location = new Point(x / 4 + 400, programPosition),
                Size = new Size(150, 40),
                Format = DateTimePickerFormat.Short // formato corto para la fecha
            };

            // En el constructor después de los otros controles
            Label labelRole = DinamicControls.CreateLabel("labelRole", "Rol:", x / 4 - 100, combobox + 25, 100, 40);
            labelRole.TextAlign = ContentAlignment.TopRight;
            ComboBox comboRole = DinamicControls.CrearComboBox("comboboxRole", x / 4, combobox + 25, 200, 40);

            // Agregar rol
            comboRole.Items.Add(new { Text = "Egresado", Value = RegisterCrud.UserType.Egresado });
            comboRole.DisplayMember = "Text";
            comboRole.ValueMember = "Value";
            comboRole.DropDownStyle = ComboBoxStyle.DropDownList;
            comboRole.SelectedIndex = 0;

            Label labelEnd = DinamicControls.CreateLabel("labelEnd", "Fecha grado:", x / 2 - 100, combobox + 25, 200, 40);
            labelEnd.TextAlign = ContentAlignment.TopRight;
            DateTimePicker programEndDate = new DateTimePicker
            {
                Name = "datepickerEndDate",
                Location = new Point(x / 4 + 400, combobox + 25),
                Size = new Size(150, 40),
                Format = DateTimePickerFormat.Short // formato corto para la fecha
            };

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
                labelProgram, program,
                labelInit, programInitDate,
                labelRole, comboRole,
                labelEnd, programEndDate,
                buttonBack, buttonRegister
            });

            //this.Controls.Add(linkLogin);

            #endregion 

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string nombres = ((TextBox)this.Controls["textboxName"]).Text;
            string apellidos = ((TextBox)this.Controls["textboxLastName"]).Text;
            string correo = ((TextBox)this.Controls["textboxEmail"]).Text;
            string telefono = ((TextBox)this.Controls["textboxPhone"]).Text;
            string sexo = ((ComboBox)this.Controls["comboboxSex"]).SelectedItem?.ToString();
            string edad = ((ComboBox)this.Controls["comboboxAge"]).SelectedItem?.ToString();
            string programa = ((ComboBox)this.Controls["comboboxProgram"]).SelectedItem?.ToString();
            DateTimePicker fechaInicio = this.Controls["datepickerInitDate"] as DateTimePicker;
            DateTimePicker fechaFin = this.Controls["datepickerEndDate"] as DateTimePicker;

            if (fechaInicio != null && fechaFin != null)
            {
                string shortDateInit = fechaInicio.Value.ToShortDateString();
                string shortDateEnd = fechaFin.Value.ToShortDateString();
            }

            RegisterCrud registerCrud = new RegisterCrud();
            registerCrud.SaveGraduate(nombres, apellidos, correo, telefono, sexo, edad, programa, "Egresado", fechaInicio, fechaFin, this);
        }

        // crear vector de números desde el 20 hasta el 60
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

        private void Graduate_Load(object sender, EventArgs e)
        {

        }
    }
}
