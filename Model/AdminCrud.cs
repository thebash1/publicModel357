using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Model357App
{
    public class AdminCrud
    {
        private readonly string dataFolderPath;
        private readonly string registersFilePath;
        private readonly string usersFilePath;

        // Enumeración para los roles del sistema
        public enum UserRole
        {
            Administrator = 1,
            User = 2,
            Graduate = 3
        }

        public AdminCrud()
        {
            try
            {
                string projectPath = Application.StartupPath;
                dataFolderPath = Path.Combine(projectPath, "DataFiles");
                registersFilePath = Path.Combine(dataFolderPath, "registers.txt");
                usersFilePath = Path.Combine(dataFolderPath, "users.txt");

                CreateDataFiles();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al inicializar las rutas: {ex.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateDataFiles()
        {
            try
            {
                if (!Directory.Exists(dataFolderPath))
                    Directory.CreateDirectory(dataFolderPath);

                if (!File.Exists(registersFilePath))
                    File.Create(registersFilePath).Close();

                if (!File.Exists(usersFilePath))
                    File.Create(usersFilePath).Close();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear archivos: {ex.Message}");
            }
        }

        private bool ValidateAdminData(string nombres, string apellidos, string telefono, 
            string correo, string sexo, string edad)
        {
            Except except = new Except();

            try
            {
                if (string.IsNullOrWhiteSpace(nombres) || string.IsNullOrWhiteSpace(apellidos) || 
                    string.IsNullOrWhiteSpace(telefono) || string.IsNullOrWhiteSpace(correo))
                {
                    MessageBox.Show("Todos los campos son obligatorios.", 
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (nombres.Contains(",") || apellidos.Contains(",") || 
                    telefono.Contains(",") || correo.Contains(","))
                {
                    MessageBox.Show("Los campos no deben contener comas.", 
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (!except.validatePhone(telefono))
                {
                    MessageBox.Show("Número de teléfono inválido, debe contener 10 dígitos.", 
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (!except.validateEmail(correo))
                {
                    MessageBox.Show("Correo electrónico inválido.", 
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (string.IsNullOrEmpty(sexo) || string.IsNullOrEmpty(edad))
                {
                    MessageBox.Show("Sexo y edad son campos obligatorios.", 
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en la validación: {ex.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private int GetNextId()
        {
            try
            {
                if (!File.Exists(registersFilePath) || new FileInfo(registersFilePath).Length == 0)
                    return 1;

                var lastLine = File.ReadAllLines(registersFilePath).LastOrDefault();
                if (string.IsNullOrEmpty(lastLine))
                    return 1;

                string[] datos = lastLine.Split(',');
                if (int.TryParse(datos[0], out int ultimoId))
                    return ultimoId + 1;

                return 1;
            }
            catch (Exception)
            {
                return 1;
            }
        }

        // Nuevo método para verificar si existe un administrador con el mismo correo o usuario
        private bool AdminExists(string correo, string usuario)
        {
            try
            {
                if (!File.Exists(registersFilePath) || !File.Exists(usersFilePath))
                    return false;

                // Verificar por correo en registros
                var registers = File.ReadAllLines(registersFilePath);
                foreach (var register in registers)
                {
                    if (!string.IsNullOrEmpty(register))
                    {
                        string[] data = register.Split(',');
                        if (data.Length >= 8 && 
                            data[7] == ((int)UserRole.Administrator).ToString() && 
                            data[4].Equals(correo, StringComparison.OrdinalIgnoreCase))
                        {
                            return true;
                        }
                    }
                }

                // Verificar por usuario en users
                var users = File.ReadAllLines(usersFilePath);
                foreach (var user in users)
                {
                    if (!string.IsNullOrEmpty(user))
                    {
                        string[] data = user.Split(',');
                        if (data.Length >= 4 && 
                            data[3] == ((int)UserRole.Administrator).ToString() && 
                            data[1].Equals(usuario, StringComparison.OrdinalIgnoreCase))
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Método CreateAdmin modificado
        public void CreateAdmin(string nombres, string apellidos, string telefono, 
            string correo, string sexo, string edad, string usuario, string contraseña)
        {
            try
            {
                // Validar datos básicos
                if (!ValidateAdminData(nombres, apellidos, telefono, correo, sexo, edad))
                    return;

                // Verificar si ya existe un administrador con ese correo o usuario
                if (AdminExists(correo, usuario))
                {
                    MessageBox.Show("Ya existe un administrador con ese correo o nombre de usuario.", 
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Confirmar la creación del administrador
                DialogResult confirmacion = MessageBox.Show(
                    "¿Está seguro de que desea crear este administrador?",
                    "Confirmar creación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmacion == DialogResult.Yes)
                {
                    int nuevoId = GetNextId();
                    
                    // Agregar el rol al final del registro (1 = Administrador)
                    string registroPersona = $"{nuevoId},{nombres},{apellidos},{telefono},{correo},{sexo},{edad},{(int)UserRole.Administrator}";
                    string registroUsuario = $"{nuevoId},{usuario},{contraseña},{(int)UserRole.Administrator}";

                    File.AppendAllText(registersFilePath, registroPersona + Environment.NewLine);
                    File.AppendAllText(usersFilePath, registroUsuario + Environment.NewLine);

                    MessageBox.Show($"Administrador registrado exitosamente.\nUsuario: {usuario}", 
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear administrador: {ex.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ListAdmins(DataGridView dataGridView)
        {
            try
            {
                if (!File.Exists(registersFilePath) || !File.Exists(usersFilePath))
                {
                    MessageBox.Show("No hay registros disponibles.", 
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var registers = File.ReadAllLines(registersFilePath);
                var users = File.ReadAllLines(usersFilePath);

                dataGridView.Columns.Clear();
                dataGridView.Rows.Clear();

                // Configurar columnas
                dataGridView.Columns.Add("Id", "ID");
                dataGridView.Columns.Add("Nombres", "Nombres");
                dataGridView.Columns.Add("Apellidos", "Apellidos");
                dataGridView.Columns.Add("Telefono", "Teléfono");
                dataGridView.Columns.Add("Correo", "Correo");
                dataGridView.Columns.Add("Sexo", "Sexo");
                dataGridView.Columns.Add("Edad", "Edad");
                dataGridView.Columns.Add("Usuario", "Usuario");

                foreach (string register in registers)
                {
                    if (!string.IsNullOrEmpty(register))
                    {
                        string[] regData = register.Split(',');
                        if (regData.Length >= 8 && regData[7] == ((int)UserRole.Administrator).ToString())
                        {
                            string username = "";
                            foreach (string user in users)
                            {
                                string[] userData = user.Split(',');
                                if (userData[0] == regData[0])
                                {
                                    username = userData[1];
                                    break;
                                }
                            }

                            dataGridView.Rows.Add(
                                regData[0],    // ID
                                regData[1],    // Nombres
                                regData[2],    // Apellidos
                                regData[3],    // Teléfono
                                regData[4],    // Correo
                                regData[5],    // Sexo
                                regData[6],    // Edad
                                username       // Usuario
                            );
                        }
                    }
                }

                dataGridView.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al listar administradores: {ex.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void UpdateAdmin(string id, string nombres, string apellidos, string telefono, 
            string correo, string sexo, string edad)
        {
            try
            {
                if (!ValidateAdminData(nombres, apellidos, telefono, correo, sexo, edad))
                    return;

                var registers = File.ReadAllLines(registersFilePath).ToList();
                bool found = false;

                for (int i = 0; i < registers.Count; i++)
                {
                    if (!string.IsNullOrEmpty(registers[i]))
                    {
                        string[] data = registers[i].Split(',');
                        if (data[0] == id && data[7] == ((int)UserRole.Administrator).ToString())
                        {
                            registers[i] = $"{id},{nombres},{apellidos},{telefono},{correo},{sexo},{edad},{(int)UserRole.Administrator}";
                            found = true;
                            break;
                        }
                    }
                }

                if (!found)
                {
                    MessageBox.Show("Administrador no encontrado.", 
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                File.WriteAllLines(registersFilePath, registers);
                MessageBox.Show("Administrador actualizado exitosamente.", 
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar administrador: {ex.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DeleteAdmin(string id)
        {
            try
            {
                if (id == "1")
                {
                    MessageBox.Show("No se puede eliminar al administrador principal.", 
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var registers = File.ReadAllLines(registersFilePath).ToList();
                var users = File.ReadAllLines(usersFilePath).ToList();
                bool found = false;

                // Eliminar del registro
                registers.RemoveAll(r =>
                {
                    if (!string.IsNullOrEmpty(r))
                    {
                        string[] data = r.Split(',');
                        if (data[0] == id && data[7] == ((int)UserRole.Administrator).ToString())
                        {
                            found = true;
                            return true;
                        }
                    }
                    return false;
                });

                if (!found)
                {
                    MessageBox.Show("Administrador no encontrado.", 
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Eliminar usuario asociado
                users.RemoveAll(u =>
                {
                    if (!string.IsNullOrEmpty(u))
                    {
                        string[] data = u.Split(',');
                        return data[0] == id && data[3] == ((int)UserRole.Administrator).ToString();
                    }
                    return false;
                });

                File.WriteAllLines(registersFilePath, registers);
                File.WriteAllLines(usersFilePath, users);

                MessageBox.Show("Administrador eliminado exitosamente.", 
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar administrador: {ex.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool IsUserAdmin(string userId)
        {
            try
            {
                if (!File.Exists(registersFilePath))
                    return false;

                var line = File.ReadAllLines(registersFilePath)
                    .FirstOrDefault(l => !string.IsNullOrEmpty(l) && 
                                       l.Split(',')[0] == userId && 
                                       l.Split(',')[7] == ((int)UserRole.Administrator).ToString());

                return line != null;
            }
            catch
            {
                return false;
            }
        }


        public void ClearRegisterAdmin(Form form)
        {
            form.Controls["textboxName"].Text = "";
            form.Controls["textboxLastName"].Text = "";
            form.Controls["textboxPhone"].Text = "";
            form.Controls["textboxEmail"].Text = "";
            ((ComboBox)form.Controls["comboboxSex"]).SelectedIndex = -1;
            ((ComboBox)form.Controls["comboboxAge"]).SelectedIndex = -1;
            ((TextBox)form.Controls["textboxUser"]).Text = "";
            ((TextBox)form.Controls["textboxPassword"]).Text = "";
        }
    }
}