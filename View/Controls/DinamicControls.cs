using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;

namespace Model357App
{
    internal class DinamicControls
    {
        // Método para crear un Label
        public static Label CreateLabel(string nombre, string texto, int x, int y, int width, int height)
        {
            Label label = new Label();
            label.Name = nombre;
            label.Text = texto;
            label.Font = new Font("Consolas", 10, FontStyle.Regular);
            label.Location = new Point(x, y);
            label.Size = new Size(width, height);
            return label;
        }

        // Método para crear un Button
        public static Button CreateButton(string nombre, string texto, int x, int y, int width, int height)
        {
            Button boton = new Button();
            boton.Name = nombre;
            boton.Text = texto;
            boton.Location = new Point(x, y);
            boton.Size = new Size(width, height);
            return boton;
        }

        public static void CustomButton(Button boton)
        {
            boton.BackColor = Color.FromArgb(0, Color.White); ;
            boton.ForeColor = Color.Black;
            boton.Font = new Font("Consolas", 12, FontStyle.Bold);
            boton.Cursor = Cursors.Hand;
            boton.FlatStyle = FlatStyle.Popup;
        }

        // Método para crear un TextBox
        public static TextBox CreateTextBox(string nombre, int x, int y, int width, int height)
        {
            return new TextBox
            {
                Name = nombre,
                Text = string.Empty,
                Location = new Point(x, y),
                Size = new Size(width, height),
                Font = new Font("Consolas", 10)
            };
        }

        // Método para crear un RadioButton
        public static RadioButton CreateRadioButton(string nombre, string texto, int x, int y, int width, int height)
        {
            RadioButton radioButton = new RadioButton();
            radioButton.Name = nombre;
            radioButton.Text = texto;
            radioButton.Location = new Point(x, y);
            radioButton.Size = new Size(width, height);
            return radioButton;
        }

        public static PictureBox CreatePictureBox(string nombre, string rutaImagen, int x, int y, int width, int height)
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.Name = nombre;
            pictureBox.Image = Image.FromFile(rutaImagen);
            pictureBox.Location = new Point(x, y);
            pictureBox.Size = new Size(width, height);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            return pictureBox;
        }

        // Método para crear un ComboBox
        public static ComboBox CreateComboBox(string nombre, List<string> items, int x, int y, int width, int height)
        {
            ComboBox comboBox = new ComboBox();
            comboBox.Name = nombre;
            comboBox.Location = new Point(x, y);
            comboBox.Size = new Size(width, height);

            // Agregar los items si se proporcionaron
            if (items != null && items.Count > 0)
            {
                comboBox.Items.AddRange(items.ToArray());
            }

            return comboBox;
        }

        // Método sobrecargado para crear un ComboBox sin items iniciales
        public static ComboBox CrearComboBox(string nombre, int x, int y, int width, int height)
        {
            return CreateComboBox(nombre, new List<string>(), x, y, width, height);
        }

        // Método para agregar items a un ComboBox existente
        public static void AddItemsComboBox(ComboBox comboBox, List<string> items)
        {
            try
            {
                if (items != null && items.Count > 0)
                {
                    comboBox.Items.AddRange(items.ToArray());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar elementos al combo box: {ex.Message}");
                throw;
            }
        }

        // crear control dinamico para texto de tipo link
        public static LinkLabel CreateLinkLabel(string texto, Point ubicacion, Size tamaño, EventHandler onClick)
        {
            LinkLabel linkLabel = new LinkLabel
            {
                Text = texto,
                Location = ubicacion,
                Size = tamaño,
                LinkColor = Color.Blue,
                VisitedLinkColor = Color.Purple,
                ActiveLinkColor = Color.Red,
                LinkBehavior = LinkBehavior.HoverUnderline,
                Cursor = Cursors.Hand
            };

            // Agregar el evento click
            linkLabel.Click += onClick;

            return linkLabel;
        }

        public static MenuStrip CreateMenuStrip(string[] menuOptions)
        {
            // Crear nuevo MenuStrip  
            MenuStrip menuStrip = new MenuStrip();
            menuStrip.Name = "menuStrip1";
            // Agregar cada opción como un ítem del menú  
            foreach (string opcion in menuOptions)
            {
                ToolStripMenuItem menuItem = new ToolStripMenuItem(opcion);
                menuItem.Name = $"toolstrip{opcion.ToLower().Trim()}"; // Asignar un nombre único al menú
                menuStrip.Items.Add(menuItem);
            }

            // Devolver el MenuStrip creado  
            return menuStrip;
        }

        public static ToolStrip CreateToolStrip(string[] toolStripOptions)
        {
            // Crear nuevo ToolStrip  
            ToolStrip toolStrip = new ToolStrip();
            toolStrip.Name = "toolStrip1";
            // Agregar cada opción como un ítem del ToolStrip  
            foreach (string opcion in toolStripOptions)
            {
                ToolStripButton toolStripButton = new ToolStripButton(opcion);
                toolStripButton.Name = $"toolstrip{opcion.ToLower().Trim()}"; // Asignar un nombre único al botón
                toolStrip.Items.Add(toolStripButton);
            }
            // Devolver el ToolStrip creado  
            return toolStrip;
        }

        public static void FormBackground(Form form, string path)
        {
            try
            {
                // Cargar y establecer la imagen  
                form.BackgroundImage = Image.FromFile(path);
                form.BackgroundImageLayout = ImageLayout.Stretch; // Ajustar la imagen al tamaño del formulario  
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la imagen: " + ex.Message);
            }
        }

        public static Form CreateForm(string nameForm, string tittleForm, int sizeX, int sizeY, bool resizeForm)
        {
            Form form = new Form
            {
                Name = nameForm,
                Text = tittleForm,
                Size = new Size(sizeX, sizeY),
                StartPosition = FormStartPosition.CenterScreen, // Centrar el formulario
                MaximizeBox = resizeForm, // Permitir maximizar si resizeForm es true
                FormBorderStyle = FormBorderStyle.FixedSingle
            };
            return form;
        }

        // crear y retornar elemento ToolStripMenuItem
        public static ToolStripMenuItem CreateToolStripItem(string text)
        {
            ToolStripMenuItem newItem = new ToolStripMenuItem(text);
            newItem.Name = $"toolstrip{text.ToLower().Trim()}";
            return newItem;
        }

        // crea y retorna un label con un icono de texto unicode
        public static Label returnLabelIcon(string name, string textUnicodeIcon, int size, int x, int y, Cursor cursorEvent)
        {
            Label labelIcon = new Label();
            labelIcon.Name = name;
            labelIcon.Text = textUnicodeIcon;
            labelIcon.Size = new Size(size, size);
            labelIcon.Font = new Font("Segoe UI emoji", 14F);
            labelIcon.Location = new Point(x, y);
            labelIcon.Cursor = cursorEvent;

            return labelIcon;
        }

        public static DataGridView CreateDataGridView(string name, int x, int y, int width, int height)
        {
            // crear y configurar un data grid view solo para mostrar datos
            DataGridView dataGridView = new DataGridView
            {
                Name = name,
                Location = new Point(x, y),
                Size = new Size(width, height),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                AllowUserToResizeColumns = false,
                AllowUserToOrderColumns = false,
                ReadOnly = true,
                EditMode = DataGridViewEditMode.EditProgrammatically,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            dataGridView.CurrentCell = null;
            dataGridView.ClearSelection();

            return dataGridView;
        }

    }

    public partial class CustomTexBox : TextBox
    {
        public CustomTexBox()
        {
            //SetStyle(ControlStyles.SupportsTransparentBackColor |
            //    ControlStyles.OptimizedDoubleBuffer |
            //    ControlStyles.AllPaintingInWmPaint |
            //    ControlStyles.ResizeRedraw |
            //    ControlStyles.UserPaint, true);

            SetStyle(ControlStyles.UserPaint, true);
            Font = new Font("Segoe UI", 10, FontStyle.Regular, GraphicsUnit.Point);
            BorderStyle = BorderStyle.None;
            ForeColor = Color.Black;

        }

        public TextBox ReturnCustomTextBox()
        {
            return this;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Dibuja una línea en la parte inferior
            using (Pen pen = new Pen(Color.Gray, 1))
            {
                e.Graphics.DrawLine(pen,
                    0, this.Height - 1,           // punto inicial
                    this.Width, this.Height - 1); // punto final
            }
        }

        //protected override void OnPaintBackground(PaintEventArgs e)
        //{
        //    using (SolidBrush brush = new SolidBrush(BackColor))
        //    {
        //        e.Graphics.FillRectangle(brush, ClientRectangle);
        //    }
        //}
    }
}
