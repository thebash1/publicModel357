using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

public class UserOperations
{
    private readonly string dataFolderPath;
    private readonly string usersFilePath;

    public UserOperations()
    {
        try
        {
            // Obtenemos la ruta del directorio actual donde están los archivos del proyecto
            string projectPath = Application.StartupPath;

            // Subimos un nivel para llegar al directorio del proyecto
            string solutionPath = Directory.GetParent(projectPath).Parent.Parent.FullName;

            // Creamos la carpeta DataFiles en el directorio del proyecto
            dataFolderPath = Path.Combine(solutionPath, "DataFiles");
            usersFilePath = Path.Combine(dataFolderPath, "users.txt");

            CreateDataFolder();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al inicializar las rutas: {ex.Message}", "Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void CreateDataFolder()
    {
        try
        {
            // Crear el directorio si no existe
            if (!Directory.Exists(dataFolderPath))
            {
                Directory.CreateDirectory(dataFolderPath);
            }

            // Crear el archivo si no existe
            if (!File.Exists(usersFilePath))
            {
                File.Create(usersFilePath).Close();
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al crear la carpeta o archivo: {ex.Message}");
        }
    }

    public bool AddUser(string username, string password)
    {
        try
        {
            // Validar campos vacíos
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("El usuario y la contraseña son obligatorios",
                              "Campos Vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validar que no haya comas en el usuario o contraseña
            if (username.Contains(",") || password.Contains(","))
            {
                MessageBox.Show("El usuario y la contraseña no pueden contener comas (,)",
                              "Carácter Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Leer todos los usuarios existentes
            List<string> usuarios = new List<string>();
            if (File.Exists(usersFilePath))
            {
                usuarios = File.ReadAllLines(usersFilePath).ToList();
            }

            // Verificar si el usuario ya existe
            foreach (string linea in usuarios)
            {
                if (!string.IsNullOrEmpty(linea))
                {
                    string[] datos = linea.Split(',');
                    if (datos.Length >= 2 && datos[1].Equals(username, StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show($"El usuario {username} ya existe en el sistema",
                                      "Usuario Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }

            // Obtener el siguiente ID
            int nuevoId = 1;
            if (usuarios.Count > 0)
            {
                // Obtener el último ID y sumarle 1
                string ultimaLinea = usuarios.Last();
                if (!string.IsNullOrEmpty(ultimaLinea))
                {
                    string[] datos = ultimaLinea.Split(',');
                    if (datos.Length > 0 && int.TryParse(datos[0], out int ultimoId))
                    {
                        nuevoId = ultimoId + 1;
                    }
                }
            }

            // Crear la nueva línea de usuario
            string nuevaLinea = $"{nuevoId},{username},{password}";

            // Agregar el nuevo usuario al archivo
            File.AppendAllText(usersFilePath, nuevaLinea + Environment.NewLine);

            MessageBox.Show("Usuario registrado exitosamente",
                          "Registro Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;
        }
        catch (UnauthorizedAccessException)
        {
            MessageBox.Show("No tienes permisos para escribir en el archivo de usuarios",
                          "Error de Permisos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
        catch (IOException ex)
        {
            MessageBox.Show($"Error al acceder al archivo de usuarios: {ex.Message}",
                          "Error de Archivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error inesperado al registrar el usuario: {ex.Message}",
                          "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
    }
}