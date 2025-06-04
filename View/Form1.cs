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

            Form login = new Login();
            MenuEvent menuEvent = new MenuEvent(this);

            MenuEvent.AddToolStripItem(menu, opciones[0], new ToolStripMenuItem("Iniciar sesión"));
            MenuEvent.AddToolStripMenuItems(menu, itemsAdmins, opciones[1]);
            MenuEvent.AddToolStripMenuItems(menu, itemsUsuario, opciones[2]);
            MenuEvent.AddToolStripMenuItems(menu, itemsEncuesta, opciones[3]);
            MenuEvent.AddSubmenu(menu, opciones[4], "Color de fondo");
            MenuEvent.AddSubmenu(menu, opciones[4], "Letra y tamaño");
            MenuEvent.AddToolStripItem(menu, opciones[5], new ToolStripMenuItem("Acerca de"));

            MenuEvent.BlockToolStripItems(menu, opciones[1], false);
            MenuEvent.BlockToolStripItems(menu, opciones[2], false);
            MenuEvent.BlockToolStripItems(menu, opciones[3], false);
            MenuEvent.BlockToolStripItems(menu, opciones[4], false);

            MenuEvent.EventSubmenu(menu, opciones[0], "Iniciar sesión", (s, args) => menuEvent.OpenForm(login, 1067, 600));
            MenuEvent.EventSubmenu(menu, opciones[5], "Acerca de", (s, args) =>
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
