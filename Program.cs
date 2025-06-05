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
            AdminCrud adminDefault = new AdminCrud();
            adminDefault.CreateAdmin("evelly", "forgionny", "3122747339", "evelly@unicesar.edu.co", "Femenino", "23", "evellyf01", "evelly7339");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
