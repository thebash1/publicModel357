using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Model357App
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            
            FormBorderStyle = FormBorderStyle.FixedSingle; // evitar redimensionamiento
            StartPosition = FormStartPosition.CenterScreen; // centrar formulario
            DinamicControls.FormBackground(this, @"C:\Users\Usuario\Desktop\Git\model357\img\login.png");
            MaximizeBox = false;

            #region formulario login

            int x = Width; int y = Height;
            
            // añadir campo de usuario con posición manual
            Label labelUser = DinamicControls.CreateLabel("labelUser", "Usuario: ", x + 300, y - 60, 100, 40);
            labelUser.TextAlign = ContentAlignment.TopRight;
            labelUser.BackColor = Color.Transparent; 
            TextBox textboxUser = DinamicControls.CreateTextBox("textboxUser", x + 400, y - 60, 200, 40);

            // añadir campo de contraseña con posición manual
            Label labelPassword = DinamicControls.CreateLabel("labelPassword", "Contraseña: ", x + 300, y, 100, 40);
            labelPassword.TextAlign = ContentAlignment.TopRight;
            labelPassword.BackColor = Color.Transparent;
            TextBox textboxPassword = DinamicControls.CreateTextBox("textboxPassword", x + 400, y, 200, 40);
            textboxPassword.UseSystemPasswordChar = true;
            //DinamicControls.CustomizeTextBox(textboxPassword);

            Label iconLabel = DinamicControls.returnLabelIcon("toogglePassword", "\uD83D\uDC41", 40, (x + 400) + (textboxPassword.Width + 10), y, Cursors.Hand);
            MenuEvent.HidePassword(iconLabel, textboxPassword, true);
            iconLabel.BackColor = Color.Transparent; // hacer transparente el icono

            // añadir botón de iniciar sesión
            Button btnLogin = DinamicControls.CreateButton("btnLogin", "Iniciar sesión", x + 400, y + 80, 200, 40);
            btnLogin.FlatStyle = FlatStyle.Popup;
            btnLogin.BackColor = Color.FromArgb(0, Color.White);
            btnLogin.Font = new Font("Consolas", 12, FontStyle.Bold);
            btnLogin.Cursor = Cursors.Hand;

            // link para registro
            //LinkLabel linkRegister = ControlCreator.CreateLinkLabel("Registrarse", new Point(x - 150, y + 150), new Size(200, 40), (s, args) => LoadRegister());
            //linkRegister.TextAlign = ContentAlignment.MiddleCenter;

            Controls.AddRange(new Control[]
            {
                labelUser,
                textboxUser,
                labelPassword,
                textboxPassword,
                iconLabel,
                btnLogin
            });
            
            // this.Controls.Add(linkRegister);

            if (btnLogin != null)
                btnLogin.Click += BtnLogin_Click;

            #endregion

        }

        private void login_Load(object sender, EventArgs e)
        {
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            UserAuth userAuth = new UserAuth();
            string username = Controls["textboxUser"].Text.Trim();
            string password = Controls["textboxPassword"].Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor ingrese usuario y contraseña", "Campos Vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // no funcionó cambiar el texo del menu de inicio de sesión a cerrar sesión
            if (userAuth.ValidateLogin(username, password))
            {
                MessageBox.Show("Inicio de sesión exitoso", "Bienvenido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MenuEvent menuEvent = new MenuEvent(this);
                Form form = new Form2();

                Controls["textboxUser"].Text = string.Empty; // Limpiar el campo de usuario
                Controls["textboxPassword"].Text = string.Empty; // Limpiar el campo de contraseña

                if (UserAuth.ValidateAdmin())
                {
                    MenuEvent.BlockToolStripItems((MenuStrip)form.Controls["menuStrip1"], "Usuarios", true);
                    DialogResult dialog = menuEvent.OpenForm(form, 1067, 600);
                    if (dialog == DialogResult.OK)
                        Close();
                }
                else
                {
                    MenuEvent.BlockToolStripItems((MenuStrip)form.Controls["menuStrip1"], "Administradores", false);
                    MenuEvent.BlockToolStripItems((MenuStrip)form.Controls["menuStrip1"], "Usuarios", false);
                    DialogResult dialog = menuEvent.OpenForm(form, 1067, 600);
                    if (dialog == DialogResult.OK)
                        Close();
                }
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos", "Error de Autenticación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }
    
    }
}
