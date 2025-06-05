using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Model357App
{
    public class UserAuth
    {
        public enum UserRole
        {
            Administrator = 1,
            User = 2,
            Graduate = 3
        }

        private readonly string usersFilePath;
        private readonly string registersFilePath;
        private static string currentUsername;
        private static UserRole? currentUserRole;

        public UserAuth()
        {
            string dataFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DataFiles");
            usersFilePath = Path.Combine(dataFolderPath, "users.txt");
            registersFilePath = Path.Combine(dataFolderPath, "registers.txt");
        }

        // Valida las credenciales y asigna el usuario y su rol
        public bool ValidateLogin(string username, string password)
        {
            try
            {
                if (!File.Exists(usersFilePath))
                    return false;

                var users = File.ReadAllLines(usersFilePath);
                foreach (var line in users)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        string[] datos = line.Split(',');
                        if (datos.Length >= 4 && // Verificar que tenga todos los campos incluido el rol
                            datos[1].Equals(username, StringComparison.OrdinalIgnoreCase) &&
                            datos[2].Equals(password))
                        {
                            currentUsername = username;
                            currentUserRole = (UserRole)int.Parse(datos[3]);
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

        // Verificar si el usuario actual es administrador
        public static bool IsAdmin()
        {
            return currentUserRole == UserRole.Administrator;
        }

        // Verificar si el usuario actual es usuario regular
        public static bool IsUser()
        {
            return currentUserRole == UserRole.User;
        }

        // Verificar si el usuario actual es egresado
        public static bool IsGraduate()
        {
            return currentUserRole == UserRole.Graduate;
        }

        // Obtener el rol del usuario actual
        public static UserRole? GetCurrentUserRole()
        {
            return currentUserRole;
        }

        // Verificar si el usuario tiene un rol específico
        public static bool HasRole(UserRole role)
        {
            return currentUserRole == role;
        }

        // Obtener el rol de un usuario específico por su ID
        public UserRole? GetUserRoleById(string userId)
        {
            try
            {
                if (!File.Exists(usersFilePath))
                    return null;

                var userLine = File.ReadAllLines(usersFilePath)
                    .FirstOrDefault(line => !string.IsNullOrEmpty(line) && 
                                         line.Split(',')[0] == userId);

                if (userLine != null)
                {
                    string[] datos = userLine.Split(',');
                    if (datos.Length >= 4)
                    {
                        return (UserRole)int.Parse(datos[3]);
                    }
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        // Verificar si hay una sesión activa
        public static bool IsSessionActive()
        {
            return !string.IsNullOrEmpty(currentUsername) && currentUserRole.HasValue;
        }

        // Obtener el nombre de usuario actual
        public static string GetCurrentUser()
        {
            return currentUsername;
        }

        // Cerrar la sesión actual
        public static void CloseSession()
        {
            currentUsername = null;
            currentUserRole = null;
        }

        // Obtener el nombre del rol en texto
        public static string GetRoleText(UserRole role)
        {
            switch (role)
            {
                case UserRole.Administrator:
                    return "Administrador";
                case UserRole.User:
                    return "Usuario";
                case UserRole.Graduate:
                    return "Egresado";
                default:
                    throw new NotImplementedException();
            }
        }

        // Obtener el rol actual en texto
        public static string GetCurrentRoleText()
        {
            return currentUserRole.HasValue ? GetRoleText(currentUserRole.Value) : "No definido";
        }
    }
}