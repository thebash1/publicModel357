using Model357App.View;
using System;
using System.Windows.Forms;

namespace Model357App
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            DinamicControls.FormBackground(this, @"C:\Users\Usuario\Desktop\Git\model357\img\adminMenu.png");
            StartPosition = FormStartPosition.CenterScreen; // centrar formulario  
            FormBorderStyle = FormBorderStyle.FixedSingle; // evitar redimensionamiento
            MaximizeBox = false;

            string[] opciones = { "Archivo", "Administradores", "Usuarios", "Encuesta", "Configuración", "Ayuda" };
            MenuStrip menu = DinamicControls.CreateMenuStrip(opciones);

            ToolStripMenuItem[] itemsAdmins =
            {
                DinamicControls.CreateToolStripItem("Registrar administrador"),
                DinamicControls.CreateToolStripItem("Actualizar administrador"),
                DinamicControls.CreateToolStripItem("Eliminar administrador"),
                DinamicControls.CreateToolStripItem("Listar administrador"),
            };

            ToolStripMenuItem[] itemsUsuario =
            {
                DinamicControls.CreateToolStripItem("Registrar usuarios"),
                DinamicControls.CreateToolStripItem("Actualizar usuarios"),
                DinamicControls.CreateToolStripItem("Eliminar usuarios"),
                DinamicControls.CreateToolStripItem("Listar usuarios"),
            };

            ToolStripMenuItem[] itemsEncuesta =
            {
                DinamicControls.CreateToolStripItem("Crear preguntas"),
                DinamicControls.CreateToolStripItem("Actualizar preguntas"),
                DinamicControls.CreateToolStripItem("Eliminar preguntas"),
                DinamicControls.CreateToolStripItem("Listar preguntas"),
            };

            MenuEvent menuEvent = new MenuEvent(this);
            int x = Width; int y = Height;

            // Agregar submenú a un menú específico
            MenuEvent.AddToolStripItem(menu, opciones[0], new ToolStripMenuItem("Cerrar sesión"));
            MenuEvent.AddToolStripItem(menu, opciones[opciones.Length - 1], new ToolStripMenuItem("Acerca de"));

            // Agregar varios submenús a un menú especificado
            MenuEvent.AddToolStripMenuItems(menu, itemsAdmins, opciones[1]);
            MenuEvent.AddToolStripMenuItems(menu, itemsUsuario, opciones[2]);
            MenuEvent.AddToolStripMenuItems(menu, itemsEncuesta, opciones[3]);
            MenuEvent.AddSubmenu(menu, opciones[4], "Color de fondo");
            MenuEvent.AddSubmenu(menu, opciones[4], "Letra y tamaño");

            // Agregar eventos a los submenús
            MenuEvent.EventSubmenu(menu, opciones[0], "Cerrar sesión", (sender, e) => this.Close());
            MenuEvent.EventSubmenu(menu, opciones[2], "Registrar usuarios", (sender, e) => menuEvent.OpenForm(new Register(), 1067, 600));
            MenuEvent.EventSubmenu(menu, opciones[2], "Actualizar usuarios", (sender, e) => 
            {
                MessageBox.Show("Por favor selecciona un id para actualizar", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });

            MenuEvent.EventSubmenu(menu, opciones[2], "Eliminar usuarios", (sender, e) => 
            {
                MessageBox.Show("Por favor selecciona un id para eliminar", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                menuEvent.OpenForm(new DeleteRegister(), 1067, 600);
            });

            MenuEvent.EventSubmenu(menu, opciones[2], "Listar usuarios", (sender, e) => menuEvent.OpenForm(new ListRegisters(), 1067, 600));
            MenuEvent.EventSubmenu(menu, opciones[5], "Acerca de", (s, e) =>
            {
                Form about = new About();
                about.Show();
            });

            Button buttonAdmins = DinamicControls.CreateButton("buttonAdmins", "Administradores", x / 2 - 40, y - 25, 200, 50);
            Button buttonRegisters = DinamicControls.CreateButton("buttonRegisters", "Registros", x / 2 + 270, y - 25, 200, 50);
            Button buttonQuestions = DinamicControls.CreateButton("buttonQuestions", "Encuesta", x / 2 + 590, y - 25, 200, 50);
            
            // Agregar los manejadores de eventos
            buttonAdmins.Click += buttonAdmins_Click;
            buttonRegisters.Click += buttonRegisters_Click;
            buttonQuestions.Click += buttonQuestions_Click;

            DinamicControls.CustomButton(buttonAdmins);
            DinamicControls.CustomButton(buttonRegisters);
            DinamicControls.CustomButton(buttonQuestions);

            Controls.AddRange(new Control[]
            {
                menu,
                buttonAdmins,
                buttonRegisters,
                buttonQuestions
            });

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void buttonAdmins_Click(object sender, EventArgs e)
        {
            if (MenuEvent.IsEnabledToolStripItems((MenuStrip)this.Controls["menuStrip1"], "Administradores"))
                MessageBox.Show("Aquí se gestionarán los administradores.", "Administradores", MessageBoxButtons.OK, MessageBoxIcon.Information);

            else
                MessageBox.Show("No tienes permisos para acceder a esta sección.", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void buttonRegisters_Click(object sender, EventArgs e)
        {
            if (MenuEvent.IsEnabledToolStripItems((MenuStrip)this.Controls["menuStrip1"], "Usuarios"))
            {
                MenuEvent menuEvent = new MenuEvent(this);
                menuEvent.OpenForm(new ListRegisters(), 1067, 600);
            }

            else
                MessageBox.Show("No tienes permisos para acceder a esta sección.", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void buttonQuestions_Click(object sender, EventArgs e)
        {
            if (MenuEvent.IsEnabledToolStripItems((MenuStrip)this.Controls["menuStrip1"], "Encuesta"))
                MessageBox.Show("Aquí se gestionarán las preguntas de la encuesta.", "Encuesta", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
    }
}
