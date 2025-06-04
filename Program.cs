using Model357App.View;
using System;
using System.Windows.Forms;

namespace Model357App
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // crear usuario administrador por defecto si no existe
            RegisterCrud admin = new RegisterCrud();
            admin.InsertAdmin("evelly", "forgionny", "evelly@unicesar.edu.co", "3122747339", "Femenino", "23");   
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new UpdateRegister());
        }
    }
}