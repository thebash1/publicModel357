using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Forms;

namespace Model357App
{
    internal class RegisterCrud
    {
        private readonly string dataFolderPath;
        private readonly string registersFilePath;
        private readonly string usersFilePath;

        public RegisterCrud()
        {
            // crear carpeta en el ejecutable del programa y archivos en caso de no estar creados
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
                MessageBox.Show($"Error al inicializar las rutas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateDataFiles()
        {
            try
            {
                if (!Directory.Exists(dataFolderPath))
                    Directory.CreateDirectory(dataFolderPath);
                
                // Crear archivos si no existen
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

        private bool ValidateRegister(string nombres, string apellidos, string telefono, string correo, string sexo, string edad)
        {
            Except except = new Except();

            try
            {
                // Validaciones
                if (string.IsNullOrWhiteSpace(nombres) || string.IsNullOrWhiteSpace(apellidos) || string.IsNullOrWhiteSpace(telefono) || string.IsNullOrWhiteSpace(correo))
                {
                    MessageBox.Show("Todos los campos son obligatorios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                
                if (nombres.Contains(",") || apellidos.Contains(",") || telefono.Contains(",") || correo.Contains(","))
                {
                    MessageBox.Show("Los campos no deben contener comas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (!except.validatePhone(telefono))
                {
                    MessageBox.Show("Número de teléfono inválido, debe contener 10 dígitos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (!except.validateEmail(correo))
                { 
                    MessageBox.Show("Correo electrónico inválido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (string.IsNullOrEmpty(sexo) || string.IsNullOrEmpty(edad))
                {
                    MessageBox.Show("Sexo, edad y programa son campos obligatorios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar persona: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // validar usuario y contraseña
        private bool ValidateUser(string usuario)
        {
            try
            {
                // comprobar que los campos estén llenos y no contengan coma
                if (!string.IsNullOrEmpty(usuario) && !usuario.Contains(",") && usuario.Length >= 8 && usuario.Length <= 20)
                    return true;

                else return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ValidatePassword(string contraseña)
        {
            try
            {
                // comprobar que la contraseña tenga al menos 8 caracteres y no contenga coma
                if (!string.IsNullOrEmpty(contraseña) && !contraseña.Contains(",") && contraseña.Length >= 8 && contraseña.Length <= 20)
                    return true;

                else return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void InsertAdmin(string nombres, string apellidos, string correo, string telefono, string sexo, string edad)
        {
            if (!ExistAdmin())
            {
                Except except = new Except();
                try
                {
                    if (!string.IsNullOrWhiteSpace(nombres) && !string.IsNullOrWhiteSpace(apellidos) && !string.IsNullOrWhiteSpace(telefono)
                        && !string.IsNullOrWhiteSpace(correo) && !nombres.Contains(",") && !apellidos.Contains(",") && !telefono.Contains(",")
                        && !correo.Contains(",") && except.validatePhone(telefono) && except.validateEmail(correo) && !string.IsNullOrEmpty(sexo)
                        && !string.IsNullOrEmpty(edad))
                    {
                        string username = "admin";
                        string password = "evellyf7339";

                        string registerAdmin = $"{1},{nombres},{apellidos},{telefono},{correo},{sexo},{edad}";
                        string userAdmin = $"{1},{username},{password}";

                        // Usar using para asegurar que los archivos se cierren correctamente
                        using (StreamWriter regWriter = File.AppendText(registersFilePath))
                        {
                            regWriter.WriteLine(registerAdmin);
                        }

                        using (StreamWriter userWriter = File.AppendText(usersFilePath))
                        {
                            userWriter.WriteLine(userAdmin);
                        }

                        MessageBox.Show($"Administrador registrado exitosamente.\n\nUsuario: {username}\nContraseña: {password}", "Registro exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    MessageBox.Show("No se ha podido guardar el administrador");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al insertar administrador: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // validar si existe un administrador
        public bool ExistAdmin()
        {
            try
            {
                if (!File.Exists(registersFilePath) || !File.Exists(usersFilePath))
                    return false;

                string lineReg;
                string lineUser;

                // Usar using para asegurar que los StreamReaders se cierren correctamente
                using (StreamReader adminReg = new StreamReader(registersFilePath))
                using (StreamReader adminUser = new StreamReader(usersFilePath))
                {
                    // leer la primera línea de ambos archivos
                    lineReg = adminReg.ReadLine();
                    lineUser = adminUser.ReadLine();

                    if (lineReg != null && lineUser != null)
                    {
                        string[] reg = lineReg.Split(',');
                        string[] user = lineUser.Split(',');

                        // comprobar que el id sea el mismo en ambos archivos
                        return string.Equals(reg[0], user[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No hay administrador registrado en la plataforma: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        public void RegisterUser(string nombres, string apellidos, string telefono, string correo, string sexo, string edad, string usuario, string contraseña, Form form)
        {

            if (ValidateRegister(nombres, apellidos, telefono, correo, sexo, edad))
            {
                if (ValidateUser(usuario) && ValidatePassword(contraseña))
                {
                    // Obtener siguiente ID
                    int nuevoId = GetNextId();

                    // Guardar registro de persona
                    string registroPersona = $"{nuevoId},{nombres},{apellidos},{telefono},{correo},{sexo},{edad}";

                    // Guardar registro de usuario
                    string registroUsuario = $"{nuevoId},{usuario},{contraseña}";

                    // Escribir en ambos archivos
                    File.AppendAllText(registersFilePath, registroPersona + Environment.NewLine);
                    File.AppendAllText(usersFilePath, registroUsuario + Environment.NewLine);

                    MessageBox.Show($"Credenciales\n\nUsuario: {usuario}\nContraseña: {contraseña}\n", "Registro exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearRegister(form);
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña inválidos.\n\nEl usuario y contraseña deben tener entre 8 y 20 caracteres y no contener comas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
                return;
            
        }

        private void ClearRegister(Form form)
        {
            form.Controls["textboxName"].Text = "";
            form.Controls["textboxLastName"].Text = "";
            form.Controls["textboxPhone"].Text = "";
            form.Controls["textboxEmail"].Text = "";
            ((ComboBox)form.Controls["comboboxSex"]).SelectedIndex = -1;
            ((ComboBox)form.Controls["comboboxAge"]).SelectedIndex = -1;
            form.Controls["textboxUser"].Text = "";
            form.Controls["textboxPassword"].Text = "";

            // DateTimePicker initDateProgram = (DateTimePicker)Controls["datepickerInitDate"];
            // DateTimePicker endDateProgram = (DateTimePicker)Controls["datepickerGraduationDate"];
        }

        private int GetNextId()
        {
            try
            {
                // Verificar si el archivo de registros existe y tiene contenido
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

        private string GenerateUser(string nombres, string apellidos)
        {
            try
            {
                // Limpiar y preparar nombres y apellidos
                string[] nombresParts = nombres.Split(' ');
                string[] apellidosParts = apellidos.Split(' ');

                // Tomar primera letra del primer nombre y el primer apellido completo
                string baseUsername = (nombresParts[0][0] + apellidosParts[0]).ToLower();

                // Asegurar longitud mínima
                while (baseUsername.Length < 8)
                {
                    if (apellidosParts.Length > 1 && !string.IsNullOrEmpty(apellidosParts[1]))
                        baseUsername += apellidosParts[1].ToLower()[0];
                    else
                        baseUsername += new Random().Next(0, 9).ToString();
                }

                // Limitar a 12 caracteres si es necesario
                if (baseUsername.Length > 12)
                    baseUsername = baseUsername.Substring(0, 12);

                // Verificar si el usuario ya existe y agregar número si es necesario
                string finalUsername = baseUsername;
                int counter = 1;
                while (ExistUser(finalUsername))
                {
                    string numberPart = counter.ToString();
                    finalUsername = baseUsername.Substring(0, Math.Min(12 - numberPart.Length, baseUsername.Length)) + numberPart;
                    counter++;
                }

                return finalUsername;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al generar usuario: {ex.Message}");
            }
        }

        private bool ExistUser(string username)
        {
            try
            {
                if (!File.Exists(usersFilePath))
                    return false;

                // retorna true si el usuario ya existe en el archivo de usuarios, si no se cumple retorna false
                return File.ReadAllLines(usersFilePath).Any(line => !string.IsNullOrEmpty(line) && line.Split(',')[1].Equals(username, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception)
            {
                return false;
            }
        }

        private string GeneratePasswd()
        {
            try
            {
                // Definir caracteres permitidos
                const string letras = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                const string numeros = "0123456789";
                const string especiales = "@#$%&";

                var random = new Random();
                var password = new StringBuilder();

                // Agregar al menos una letra minúscula
                password.Append(letras.ToLower()[random.Next(letras.Length / 2)]);

                // Agregar al menos una letra mayúscula
                password.Append(letras.ToUpper()[random.Next(letras.Length / 2)]);

                // Agregar al menos un número
                password.Append(numeros[random.Next(numeros.Length)]);

                // Agregar al menos un carácter especial
                password.Append(especiales[random.Next(especiales.Length)]);

                // Completar hasta 8 caracteres con una mezcla aleatoria
                while (password.Length < 8)
                {
                    int tipo = random.Next(3);
                    switch (tipo)
                    {
                        case 0:
                            password.Append(letras[random.Next(letras.Length)]);
                            break;
                        case 1:
                            password.Append(numeros[random.Next(numeros.Length)]);
                            break;
                        case 2:
                            password.Append(especiales[random.Next(especiales.Length)]);
                            break;
                    }
                }

                // Mezclar los caracteres
                return new string(password.ToString().ToCharArray().OrderBy(x => random.Next()).ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al generar contraseña: {ex.Message}");
            }
        }

        public void ListRegisters(DataGridView dataGridView)
        {
            try
            {
                // Verificar si existen los archivos
                if (!File.Exists(registersFilePath) || !File.Exists(usersFilePath))
                {
                    MessageBox.Show("No hay registros disponibles.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Leer todos los registros y usuarios
                string[] registers = File.ReadAllLines(registersFilePath);
                string[] users = File.ReadAllLines(usersFilePath);

                // Limpiar el DataGridView
                dataGridView.Rows.Clear();
                dataGridView.Columns.Clear();

                // Configurar columnas
                dataGridView.Columns.Add("Id", "ID");
                dataGridView.Columns.Add("Nombres", "Nombres");
                dataGridView.Columns.Add("Apellidos", "Apellidos");
                dataGridView.Columns.Add("Telefono", "Teléfono");
                dataGridView.Columns.Add("Correo", "Correo");
                dataGridView.Columns.Add("Sexo", "Sexo");
                dataGridView.Columns.Add("Edad", "Edad");

                // Procesar cada registro
                foreach (string register in registers)
                {
                    if (!string.IsNullOrEmpty(register))
                    {
                        string[] datos = register.Split(',');
                        if (datos.Length >= 7) // Asegurarse que tenga todos los campos
                        {
                            // Verificar si tiene usuario comparando IDs
                            bool hasUser = false;
                            foreach (string user in users)
                            {
                                if (!string.IsNullOrEmpty(user))
                                {
                                    string[] datosUsuario = user.Split(',');
                                    if (datosUsuario[0] == datos[0]) // Comparar IDs
                                    {
                                        hasUser = true;
                                        break;
                                    }
                                }
                            }

                            // Agregar fila al DataGridView
                            dataGridView.Rows.Add(
                                datos[0],          // ID
                                datos[1],          // Nombres
                                datos[2],          // Apellidos
                                datos[3],          // Teléfono
                                datos[4],          // Correo
                                datos[5],          // Sexo
                                datos[6],          // Edad
                                hasUser ? "si" : "no"  // Indicador de usuario
                            );
                        }
                    }
                }

                // Ajustar el ancho de las columnas
                dataGridView.AutoResizeColumns();

                // Si no hay registros mostrar mensaje
                if (dataGridView.Rows.Count == 0)
                    MessageBox.Show("No hay personas registradas.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al listar personas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // método para eliminar un registro y usuario asociado
        public void DeleteRegister(string id)
        {
            try
            {
                // Verificar que existan los archivos
                if (!File.Exists(registersFilePath) || !File.Exists(usersFilePath))
                {
                    MessageBox.Show("No hay registros para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Verificar que el ID no esté vacío
                if (string.IsNullOrEmpty(id))
                {
                    MessageBox.Show("Debe seleccionar una persona para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Verificar que no se intente eliminar al administrador (ID = 1)
                if (id == "1")
                {
                    MessageBox.Show("No se puede eliminar al administrador del sistema.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Leer todos los registros
                string[] registers = File.ReadAllLines(registersFilePath);
                string[] users = File.ReadAllLines(usersFilePath);

                // Listas para guardar las líneas que se mantendrán
                List<string> newRegisters = new List<string>();
                List<string> newUsers = new List<string>();

                bool findRegister = false;

                // Procesar registros
                foreach (string register in registers)
                {
                    if (!string.IsNullOrEmpty(register))
                    {
                        string[] data = register.Split(',');
                        if (data[0] != id) // Si no es la persona a eliminar, mantener el registro
                            newRegisters.Add(register);

                        else findRegister = true;
                    }
                }

                // Si no se encontró la persona
                if (!findRegister)
                {
                    MessageBox.Show("No se encontró la persona especificada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Procesar usuarios
                foreach (string user in users)
                {
                    if (!string.IsNullOrEmpty(user))
                    {
                        string[] data = user.Split(',');
                        if (data[0] != id) // Si no es el usuario de la persona a eliminar, mantener el registro
                            newRegisters.Add(user);
                    }
                }

                // Confirmar eliminación
                DialogResult confirmacion = MessageBox.Show(
                    "¿Está seguro que desea eliminar esta persona y su usuario asociado?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirmacion == DialogResult.Yes)
                {
                    try
                    {
                        // Escribir los nuevos archivos
                        File.WriteAllLines(registersFilePath, newRegisters);
                        File.WriteAllLines(usersFilePath, newUsers);

                        MessageBox.Show("Persona eliminada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al guardar los cambios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar persona: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método para verificar si los archivos existen
        private bool ValidateFilesExist()
        {
            if (!File.Exists(registersFilePath) || !File.Exists(usersFilePath))
            {
                MessageBox.Show("No hay registros disponibles.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        // Método para buscar una persona por ID y obtener sus datos actuales
        private (bool found, string oldName, string oldLastName) FindPersonById(string id, string[] registers)
        {
            foreach (string register in registers)
            {
                if (!string.IsNullOrEmpty(register))
                {
                    string[] datos = register.Split(',');
                    if (datos[0] == id)
                        return (true, datos[1], datos[2]);
                    
                }
            }
            return (false, "", "");
        }

        // Método para buscar un usuario por ID
        private (bool found, string username, string password) FindUserById(string id, string[] users)
        {
            foreach (string user in users)
            {
                if (!string.IsNullOrEmpty(user))
                {
                    string[] datos = user.Split(',');
                    if (datos[0] == id)
                        return (true, datos[1], datos[2]);
                }
            }
            return (false, "", "");
        }

        // Método para crear la línea de registro actualizada
        private string CreateUpdateRegisterLine(string id, string nombres, string apellidos, string telefono, 
            string correo, string sexo, string edad)
        {
            return $"{id},{nombres},{apellidos},{telefono},{correo},{sexo},{edad}";
        }

        // Método para actualizar las listas de registros
        private List<string> UpdateRegistersList(string[] registers, string id, string newRegisterLine)
        {
            var newRegisters = new List<string>();
            foreach (string register in registers)
            {
                if (!string.IsNullOrEmpty(register))
                {
                    string[] datos = register.Split(',');
                    if (datos[0] == id)
                        newRegisters.Add(newRegisterLine);
                    
                    else newRegisters.Add(register);
                    
                }
            }
            return newRegisters;
        }

        // Método para actualizar las listas de usuarios
        private List<string> UpdateUsersList(string[] users, string id, string newUsername, string currentPassword)
        {
            var newUsers = new List<string>();
            foreach (string user in users)
            {
                if (!string.IsNullOrEmpty(user))
                {
                    string[] datos = user.Split(',');
                    if (datos[0] == id)
                        newUsers.Add($"{id},{newUsername},{currentPassword}");
                    
                    else newUsers.Add(user);
                    
                }
            }
            return newUsers;
        }

        // Método principal refactorizado
        public void UpdateRegister(string id, string nombres, string apellidos, string telefono, string correo, 
            string sexo, string edad)
        {
            try
            {
                if (!ValidateFilesExist()) return;

                if (string.IsNullOrEmpty(id))
                {
                    MessageBox.Show("Debe seleccionar una persona para actualizar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidateRegister(nombres, apellidos, correo, telefono, sexo, edad))
                    return;
                
                string[] registers = File.ReadAllLines(registersFilePath);
                string[] users = File.ReadAllLines(usersFilePath);

                var (personFound, oldName, oldLastName) = FindPersonById(id, registers);
                if (!personFound)
                {
                    MessageBox.Show("No se encontró la persona especificada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var (userFound, currentUsername, currentPassword) = FindUserById(id, users);
                
                // Determinar si necesitamos un nuevo nombre de usuario
                string newUsername = currentUsername;
                if ((oldName != nombres || oldLastName != apellidos) && userFound)
                    newUsername = GenerateUser(nombres, apellidos);
                

                // Crear línea de registro actualizada
                string updateRegisterLine = CreateUpdateRegisterLine(id, nombres, apellidos, telefono, correo, 
                    sexo, edad);

                // Actualizar listas
                var newRegisters = UpdateRegistersList(registers, id, updateRegisterLine);
                var newUsers = UpdateUsersList(users, id, newUsername, currentPassword);

                // Confirmar actualización
                DialogResult confirmacion = MessageBox.Show(
                    "¿Está seguro que desea actualizar esta persona y su usuario asociado?",
                    "Confirmar actualización",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirmacion == DialogResult.Yes)
                {
                    try
                    {
                        File.WriteAllLines(registersFilePath, newRegisters);
                        File.WriteAllLines(usersFilePath, newUsers);

                        string mensajeUsuario = userFound && newUsername != currentUsername
                            ? $"\n\nNuevo usuario: {newUsername}"
                            : "";

                        MessageBox.Show($"Registro actualizado exitosamente.{mensajeUsuario}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al guardar los cambios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar registro: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}