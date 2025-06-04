using System;
using System.Drawing;
using System.Windows.Forms;

namespace Model357App
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen; // centrar formulario
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // evitar redimensionamiento
            this.MaximizeBox = false;
            this.Size = new Size(1067, 600); // establecer tamaño del formulario

            // añadir titulo al formulario
            int x = this.Width; int y = this.Height;
            Label title = DinamicControls.CreateLabel("label1", "Registro", x / 2 - 80, 30, 300, 40);
            title.Font = new Font("Consolas", 20, FontStyle.Bold);

            #region formulario registro

            Label labelName = DinamicControls.CreateLabel("labelName", "Nombre: ", x / 4 - 100, y / 5, 100, 40);
            labelName.TextAlign = ContentAlignment.TopRight;
            TextBox name = DinamicControls.CreateTextBox("textboxName", x / 4, y / 5, 200, 40);

            Label labelLastName = DinamicControls.CreateLabel("labelLastName", "Apellido: ", x / 4 + 200, y / 5, 100, 40);
            labelLastName.TextAlign = ContentAlignment.TopRight;
            TextBox lastName = DinamicControls.CreateTextBox("textboxLastName", x / 4 + 300, y / 5, 200, 40);

            //// campo de usuario
            //Label labelUser = ControlCreator.CreateLabel("labelUser", "Usuario: ", x / 4 - 100, y / 3 - 20, 100, 40);
            //labelUser.TextAlign = ContentAlignment.TopRight;
            //TextBox user = ControlCreator.CreateTextBox("textboxUser", x / 4, y / 3 - 20, 200, 40);
            //this.Controls.Add(labelUser);
            //this.Controls.Add(user);

            //// campo de contraseña
            //Label labelPassword = ControlCreator.CreateLabel("labelPassword", "Contraseña: ", x / 4 + 200, y / 3 - 20, 100, 40);
            //labelPassword.TextAlign = ContentAlignment.TopRight;
            //TextBox password = ControlCreator.CreateTextBox("textboxPassword", x / 4 + 300, y / 3 - 20, 200, 40);
            //this.Controls.Add(labelPassword);
            //this.Controls.Add(password);

            int userY = y / 3 - 20; // ajustar la posición de los campos de usuario
            int passwdY = y / 3 - 20; // y contraseña
            int emailY = y / 2 - 60; // ajustar la posición del campo de correo
            int phoneY = y / 2 - 60; // ajustar la posición del campo de teléfono
            int sexY = y / 2; // ajustar la posición del campo de sexo
            int ageY = y / 2; // ajustar la posición del campo de edad
            int programY = y / 2 + 40; // ajustar la posición del campo de programa
            int initDateY = y / 2 + 40; // ajustar la posición del campo de fecha de inicio
            int endDateY = y / 2 + 100; // ajustar la posición del campo de fecha de finalización


            // campo de correo
            Label labelEmail = DinamicControls.CreateLabel("labelEmail", "Correo: ", x / 4 - 100, userY, 100, 40);
            labelEmail.TextAlign = ContentAlignment.TopRight;
            TextBox email = DinamicControls.CreateTextBox("textboxEmail", x / 4, userY, 200, 40);

            // campo de teléfono
            Label labelPhone = DinamicControls.CreateLabel("labelPhone", "Teléfono: ", x / 4 + 200, userY, 100, 40);
            labelPhone.TextAlign = ContentAlignment.TopRight;
            TextBox phone = DinamicControls.CreateTextBox("textboxPhone", x / 4 + 300, userY, 200, 40);

            // campo de sexo
            Label labelSex = DinamicControls.CreateLabel("labelSex", "Sexo: ", x / 4 - 100, emailY, 100, 40);
            labelSex.TextAlign = ContentAlignment.TopRight;
            ComboBox sex = DinamicControls.CrearComboBox("comboboxSex", x / 4, emailY, 200, 40);
            sex.Items.Add("Masculino");
            sex.Items.Add("Femenino");
            sex.DropDownStyle = ComboBoxStyle.DropDownList; // establece solo lectura en el combo box

            // campo de edad
            Label labelAge = DinamicControls.CreateLabel("labelAge", "Edad: ", x / 4 + 200, emailY, 100, 40);
            labelAge.TextAlign = ContentAlignment.TopRight;
            ComboBox age = DinamicControls.CrearComboBox("comboboxAge", x / 4 + 300, emailY, 200, 40);
            addItemsComboBox(age, returnArrayStrNumbers());
            age.DropDownStyle = ComboBoxStyle.DropDownList; // establece solo lectura en el combo box
            
            // campo de correo
            Label labelUser = DinamicControls.CreateLabel("labelUser", "Usuario: ", x / 4 - 100, sexY, 100, 40);
            labelUser.TextAlign = ContentAlignment.TopRight;
            TextBox user = DinamicControls.CreateTextBox("textboxUser", x / 4, sexY, 200, 40);

            // campo de teléfono
            Label labelPassword = DinamicControls.CreateLabel("labelPassword", "Contraseña: ", x / 4 + 200, sexY, 100, 40);
            labelPassword.TextAlign = ContentAlignment.TopRight;
            TextBox password = DinamicControls.CreateTextBox("textboxPassword", x / 4 + 300, sexY, 200, 40);

            // campo de programa de estudio
            //Label labelProgram = DinamicControls.CreateLabel("labelProgram", "Programa: ", x / 4 - 100, sexY, 100, 40);
            //labelProgram.TextAlign = ContentAlignment.TopRight;
            //ComboBox program = DinamicControls.CrearComboBox("comboboxProgram", x / 4, sexY, 200, 40);
            //program.Items.Add("Ingeniería de Sistemas");
            //program.Items.Add("Ingeniería Agropecuaria");
            //program.Items.Add("Ingeniería Ambiental y Sanitaria");
            //program.Items.Add("Ingeniería Agroindustrial");
            //program.Items.Add("Administración de empresas");
            //program.Items.Add("Contaduría Pública");
            //program.Items.Add("Economía");
            //program.DropDownStyle = ComboBoxStyle.DropDownList; // establece solo lectura en el combo box
            //this.Controls.Add(labelProgram);
            //this.Controls.Add(program);

            //// campo fecha de inicio
            //Label labelInitDate = DinamicControls.CreateLabel("labelInitDate", "Fecha de inicio: ", x / 4 + 200, sexY, 150, 40);
            //labelInitDate.TextAlign = ContentAlignment.TopRight;
            //DateTimePicker programInitDate = new DateTimePicker
            //{
            //    Name = "datepickerInitDate",
            //    Location = new Point(x / 4 + 350, sexY),
            //    Size = new Size(150, 40),
            //    Format = DateTimePickerFormat.Short // formato corto para la fecha
            //};
            //programInitDate.MaxDate = DateTime.Now; // no permitir fechas futuras
            //this.Controls.Add(labelInitDate);
            //this.Controls.Add(programInitDate);

            //// campo fecha de finalización
            //Label labelEndDate = DinamicControls.CreateLabel("labelEndDate", "Fecha de finalización: ", x / 4 + 200, programY, 150, 40);
            //labelEndDate.TextAlign = ContentAlignment.TopRight;
            //DateTimePicker programEndDate = new DateTimePicker
            //{
            //    Name = "datepickerGraduationDate",
            //    Location = new Point(x / 4 + 350, programY),
            //    Size = new Size(150, 40),
            //    Format = DateTimePickerFormat.Short // formato corto para la fecha
            //};

            //this.Controls.Add(labelEndDate);
            //this.Controls.Add(programEndDate);

            // botón de registro
            Button btnRegister = DinamicControls.CreateButton("btnRegister", "Registrarme", x / 2 - 120, y / 2 + 160, 200, 40);
            
            //LinkLabel linkLogin = ControlCreator.CreateLinkLabel(
            //    "Iniciar sesión",
            //    new Point(x / 2 - 170 + 50, y / 2 + 200),
            //    new Size(200, 40),
            //    (s, args) => this.Close());
            //linkLogin.TextAlign = ContentAlignment.MiddleCenter;
            
            btnRegister.Click += new EventHandler(btnRegister_Click);

            Controls.AddRange(new Control[]
            {
                title,
                labelName,
                name,
                labelLastName,
                lastName,
                labelEmail,
                email,
                labelPhone,
                phone,
                labelSex,
                sex,
                labelAge,
                age,
                labelUser,
                user,
                labelPassword,
                password,
                btnRegister
            });

            //this.Controls.Add(linkLogin);

            #endregion 

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

            RegisterCrud person = new RegisterCrud();
            person.RegisterUser(nombres.ToLower(), apellidos.ToLower(), telefono, correo.ToLower(), sexo, edad, usuario, contraseña, this);
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }
        

    }
}
