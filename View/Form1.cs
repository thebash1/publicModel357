using Model357App.View;
using System;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;

namespace Model357App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            DinamicControls.FormBackground(this, @"C:\Users\Usuario\Desktop\Git\model357\img\main.png");
            StartPosition = FormStartPosition.CenterScreen; // centrar formulario  
            FormBorderStyle = FormBorderStyle.FixedSingle; // evitar redimensionamiento
            MaximizeBox = false;
            Size = new Size(1067, 600);

            string[] opciones = { "Archivo", "Administradores", "Usuarios", "Encuesta", "Egresado", "Configuración", "Ayuda" };
            MenuStrip menu = DinamicControls.CreateMenuStrip(opciones);

            ToolStripMenuItem[] itemsAdmins =
            {
                DinamicControls.CreateToolStripItem("Registrar administradores"),
                DinamicControls.CreateToolStripItem("Actualizar administradores"),
                DinamicControls.CreateToolStripItem("Eliminar administradores"),
                DinamicControls.CreateToolStripItem("Listar administradores"),
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

            MenuEvent.AddToolStripItem(menu, opciones[0], new ToolStripMenuItem("Iniciar sesión"));
            MenuEvent.AddToolStripMenuItems(menu, itemsAdmins, opciones[1]);
            MenuEvent.AddToolStripMenuItems(menu, itemsUsuario, opciones[2]);
            MenuEvent.AddToolStripMenuItems(menu, itemsEncuesta, opciones[3]);
            MenuEvent.AddSubmenu(menu, opciones[4], "Responder formulario");
            MenuEvent.AddSubmenu(menu, opciones[5], "Color de menu strip");
            MenuEvent.AddToolStripItem(menu, opciones[6], new ToolStripMenuItem("Acerca de"));

            MenuEvent.BlockToolStripItems(menu, opciones[1], false);
            MenuEvent.BlockToolStripItems(menu, opciones[2], false);
            MenuEvent.BlockToolStripItems(menu, opciones[3], false);
            MenuEvent.BlockToolStripItems(menu, opciones[4], false);
            MenuEvent.BlockToolStripItems(menu, opciones[5], false);

            MenuEvent.EventSubmenu(menu, opciones[0], "Iniciar sesión", (s, args) =>
            {
                Form login = new Login();
                menuEvent.OpenForm(login, 1067, 600);
                
                // limpiar al cerrar formulario
                login.Controls["textboxUser"].Text = string.Empty; 
                login.Controls["textboxPassword"].Text = string.Empty;
            });

            MenuEvent.EventSubmenu(menu, opciones[5], "Color de menu strip", (sender, e) =>
            {
                MessageBox.Show("Esta opción cambiará el color del menú.", "Color de menú", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });

            MenuEvent.EventSubmenu(menu, opciones[6], "Acerca de", (s, args) =>
            {
                Form about = new About();
                about.Show();
            });

            Controls.Add(menu);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        
    }
}
