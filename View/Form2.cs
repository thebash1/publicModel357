using Model357App.Model;
using Model357App.View;
using System;
using System.Windows.Forms;

namespace Model357App
{
    public partial class Form2 : Form
    {
        private MenuEvent menuEvent;
        public Form2()
        {
            InitializeComponent();

            DinamicControls.FormBackground(this, @"C:\Users\Usuario\Desktop\Git\model357\img\adminMenu.png");
            StartPosition = FormStartPosition.CenterScreen; // centrar formulario  
            FormBorderStyle = FormBorderStyle.FixedSingle; // evitar redimensionamiento
            MaximizeBox = false;

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
            int x = Width; int y = Height;

            // Agregar submenú a un menú específico
            MenuEvent.AddToolStripItem(menu, opciones[0], new ToolStripMenuItem("Cerrar sesión"));
            MenuEvent.AddToolStripItem(menu, opciones[6], new ToolStripMenuItem("Acerca de"));

            // Agregar varios submenús a un menú especificado
            MenuEvent.AddToolStripMenuItems(menu, itemsAdmins, opciones[1]);
            MenuEvent.AddToolStripMenuItems(menu, itemsUsuario, opciones[2]);
            MenuEvent.AddToolStripMenuItems(menu, itemsEncuesta, opciones[3]);
            MenuEvent.AddSubmenu(menu, opciones[4], "Responder formulario");
            MenuEvent.AddSubmenu(menu, opciones[5], "Color de menu strip");

            // Agregar eventos a los submenús
            MenuEvent.EventSubmenu(menu, opciones[0], "Cerrar sesión", (sender, e) => this.Close());
            MenuEvent.EventSubmenu(menu, opciones[1], "Registrar administradores", (sender, e) => menuEvent.OpenForm(new AddAdmin(), 1067, 600));
            MenuEvent.EventSubmenu(menu, opciones[1], "Actualizar administradores", (sender, e) => menuEvent.OpenForm(new UpdateAdmin(), 800, 500));
            MenuEvent.EventSubmenu(menu, opciones[1], "Listar administradores", (sender, e) => menuEvent.OpenForm(new ListAdmin(), 1067, 600));
            MenuEvent.EventSubmenu(menu, opciones[1], "Eliminar administradores", (sender, e) => menuEvent.OpenForm(new DeleteAdmin(), 800, 500));

            MenuEvent.EventSubmenu(menu, opciones[2], "Registrar usuarios", (sender, e) => menuEvent.OpenForm(new Register(), 1067, 600));
            MenuEvent.EventSubmenu(menu, opciones[2], "Actualizar usuarios", (sender, e) => 
            {
                UpdateRegister updateRegister = new UpdateRegister();
                updateRegister.Owner = this; // Establecer el propietario explícitamente
                menuEvent.OpenForm(updateRegister, 800, 500); // Mostrar el formulario de actualización
            });

            MenuEvent.EventSubmenu(menu, opciones[2], "Eliminar usuarios", (sender, e) => 
            {
                MessageBox.Show("Por favor selecciona un id para eliminar", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                menuEvent.OpenForm(new DeleteRegister(), 800, 500);
            });

            MenuEvent.EventSubmenu(menu, opciones[2], "Listar usuarios", (sender, e) => menuEvent.OpenForm(new ListRegisters(), 1067, 600));

            MenuEvent.EventSubmenu(menu, opciones[3], "Crear preguntas", (sender, e) => menuEvent.OpenForm(new CreateAsk(), 800, 500));
            MenuEvent.EventSubmenu(menu, opciones[3], "Actualizar preguntas", (sender, e) => menuEvent.OpenForm(new UpdateAsk(), 900, 600));
            MenuEvent.EventSubmenu(menu, opciones[3], "Eliminar preguntas", (sender, e) => menuEvent.OpenForm(new DeleteAsk(), 900, 600));
            MenuEvent.EventSubmenu(menu, opciones[3], "Listar preguntas", (sender, e) => menuEvent.OpenForm(new ListAsk(), 900, 600));
            MenuEvent.EventSubmenu(menu, opciones[4], "Responder formulario", (sender, e) =>
            {
                DialogResult result = MessageBox.Show("¿El egresado está registrado?", "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    menuEvent.OpenForm(new ValidateGraduate(), 300, 450);
                }
                else
                {
                    menuEvent.OpenForm(new Graduate(), 1067, 600);
                }
            });
            MenuEvent.EventSubmenu(menu, opciones[5], "Color de menu strip", (sender, e) =>
            {
                MessageBox.Show("Esta opción cambiará el color del menú.", "Color de menú", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });

            MenuEvent.EventSubmenu(menu, opciones[6], "Acerca de", (s, e) =>
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
            {
                menuEvent = new MenuEvent(this);
                menuEvent.OpenForm(new ListAdmin(), 1067, 600);
            }

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
            {
                DialogResult result = MessageBox.Show("¿El egresado está registrado?", "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    menuEvent = new MenuEvent(this);    
                    menuEvent.OpenForm(new ValidateGraduate(), 300, 450);
                }
                else
                {
                    MenuEvent menuEvent = new MenuEvent(this);
                    menuEvent.OpenForm(new Graduate(), 1067, 600);
                }
            }
        }
        
    }
}
