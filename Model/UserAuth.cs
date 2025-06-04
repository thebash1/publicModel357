using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Model357App
{
    public class UserAuth
    {
        private readonly string usersFilePath;
        private static string currentUsername;

        public UserAuth()
        {
            string dataFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DataFiles");
            usersFilePath = Path.Combine(dataFolderPath, "users.txt");
        }

        // Valida las credenciales y asigna el usuario de la sesión iniciada a la variable estática currentUsername
        public bool ValidateLogin(string username, string password)
        {
            try
            {
                if (!File.Exists(usersFilePath))
                    return false;

                var userLine = File.ReadAllLines(usersFilePath)
                    .FirstOrDefault(line => !string.IsNullOrEmpty(line) &&
                                         line.Split(',')[1].Equals(username, StringComparison.OrdinalIgnoreCase) &&
                                         line.Split(',')[2].Equals(password));

                if (userLine != null)
                {
                    currentUsername = username;
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool ValidateAdmin()
        {
            if (currentUsername == "admin") return true;
            
            else return false;
        }

        public static bool IsSessionActive()
        {
            return !string.IsNullOrEmpty(currentUsername);
        }

        public static string GetCurrentUser()
        {
            return currentUsername;
        }

        public static void CloseSession()
        {
            currentUsername = null;
        }


    }
}