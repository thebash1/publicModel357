using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Model357App
{
    internal class MenuEvent
    {
        private Form currentForm;

        public MenuEvent(Form form)
        {
            currentForm = form;
        }

        // abrir un nuevo formulario y ocultar el formulario actual
        public DialogResult OpenForm(Form formToOpen, int sizex, int sizey)
        {
            formToOpen.Size = new System.Drawing.Size(sizex, sizey);
            formToOpen.Owner = currentForm; // Establecer el propietario
            currentForm.Hide();
            formToOpen.FormClosed += (s, args) => currentForm.Show();
            return formToOpen.ShowDialog();
        }

        public void CloseForm(Form formToClose)
        {
            formToClose.Close();
            currentForm.ShowDialog();
        }

        public static void EventSubmenu(MenuStrip menuStrip, string menuPrincipal, string submenuText, EventHandler evento)
        {
            // Buscar el menú principal
            foreach (ToolStripMenuItem item in menuStrip.Items)
            {
                if (item.Text == menuPrincipal)
                {
                    // Buscar el submenú
                    foreach (ToolStripMenuItem subItem in item.DropDownItems)
                    {
                        if (subItem.Text == submenuText)
                        {
                            // Asignar el evento
                            subItem.Click += evento;
                            return;
                        }
                    }
                }
            }
        }

        // Método alternativo que devuelve el submenú si lo encuentra
        public static ToolStripMenuItem SearchSubmenu(MenuStrip menuStrip, string menuPrincipal, string submenu)
        {
            foreach (ToolStripMenuItem item in menuStrip.Items)
            {
                if (item.Text == menuPrincipal)
                {
                    foreach (ToolStripMenuItem subItem in item.DropDownItems)
                    {
                        if (subItem.Text == submenu)
                        {
                            return subItem;
                        }
                    }
                }
            }
            return null;
        }

        public static void AddSubmenu(MenuStrip menuStrip, string menuPrincipal, string submenu)
        {
            foreach (ToolStripMenuItem item in menuStrip.Items)
            {
                if (item.Text == menuPrincipal)
                {
                    item.DropDownItems.Add(new ToolStripMenuItem(submenu));
                    break;
                }
            }
        }

        // método para ocultar la contraseña al hacer click en el icono
        public static void HidePassword(Label labelIcon, System.Windows.Forms.TextBox textboxPassword, bool passwordVisible)
        {
            labelIcon.Click += (s, e) =>
            {
                passwordVisible = !passwordVisible;
                textboxPassword.UseSystemPasswordChar = passwordVisible;
            };
        }

        // método para bloquear o desbloquear opciones dependiendo del rol
        public static void BlockToolStripItems(MenuStrip menuStrip, string menu, bool value)
        {
            try
            {
                // recorrer las opciones principales del menú
                foreach (ToolStripMenuItem item in menuStrip.Items)
                {
                    // buscar el menú a bloquear o desbloquear
                    if (item.Text == menu)
                    {
                        // almacenar los submenúes en una variable y establecer el valor
                        foreach (ToolStripMenuItem subItem in item.DropDownItems)
                        {
                            subItem.Enabled = value;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al bloquear los items del menú {menu}\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static bool IsEnabledToolStripItems(MenuStrip menuStrip, string menu)
        {
            try
            {
                // recorrer las opciones principales del menú
                foreach (ToolStripMenuItem item in menuStrip.Items)
                {
                    // buscar el menú a bloquear o desbloquear
                    if (item.Text == menu)
                    {
                        int count = 0;
                        // almacenar los submenúes en una variable y establecer el valor
                        foreach (ToolStripMenuItem subItem in item.DropDownItems)
                        {
                            if (subItem.Enabled == true)
                                count++;
                        }
                        return count == item.DropDownItems.Count; // devuelve true si todos los submenús están habilitados
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar los items del menú {menu}\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false; // No se encontró el menú
        }

        public static bool AddToolStripItem(MenuStrip menuStrip, string menuPrincipal, ToolStripMenuItem newItem)
        {
            foreach (ToolStripMenuItem item in menuStrip.Items)
            {
                if (item.Text == menuPrincipal)
                {
                    item.Name = $"toolstrip{item.Text.ToLower().Trim()}"; // asignar un nombre único al menú
                    item.DropDownItems.Add(newItem);
                    return true; // Se agregó el nuevo ítem
                }
            }
            return false; // No se encontró el menú principal
        }

        public static void AddToolStripMenuItems(MenuStrip menuStrip, ToolStripMenuItem[] items, string menu)
        {
            foreach (ToolStripMenuItem item in items)
            {
                try
                {
                    if (AddToolStripItem(menuStrip, menu, item)) continue;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al agregar los items al menú usuario\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        public static DataGridViewRow ReturnSelectedRow(Form form, string nombreDataGridView)
        {
            DataGridView dgv = (DataGridView)form.Controls[nombreDataGridView];
            if (dgv.SelectedRows.Count > 0)
            {
                return dgv.SelectedRows[0];
            }
            return null;
        }

        public static DataGridViewRow ReturnCurrentRow(Form form, string nombreDataGridView)
        {
            DataGridView dgv = (DataGridView)form.Controls[nombreDataGridView];
            if (dgv.CurrentRow != null)
            {
                return dgv.CurrentRow;
            }
            return null;
        }

    }
}