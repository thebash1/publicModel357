using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Model357App
{
    internal class Except
    {
        public string validateRadioButton(RadioButton radiobtn) // verificar que haya una opción seleccionada en el radio button
        {
            // mejorar método de validación
            if (radiobtn == null) 
                return null;

            else if (radiobtn.Checked == false) 
                return null;

            else 
                return radiobtn.Text;
        }

        // método de testeo para imprimir los datos del formulario
        public void printData(string[] arrX)
        {
            string text = "";
            foreach (var item in arrX)
            {
                text += item + ", "; // añade el contenido de cada posición del vector a la variable
            }
            MessageBox.Show(text); // imprime el contenido almacenado en la variable
        }

        // función que retorna un objeto radio button el cual tiene la información de la opción marcada dentro de las disponibles que contiene el group box
        public RadioButton getValueGroupBox(GroupBox groupbox)
        {
            RadioButton radiobtn = groupbox.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked); // accede y filtra los controles de tipo radio button, (firstordefault > busca el primero que tenga la propiedad true) 
            return radiobtn;
        }

        public bool validateComboBox(ComboBox combobox)
        {
            if (string.IsNullOrEmpty(combobox.Text) || combobox.SelectedIndex == -1) 
                return false; // valida si el campo está vacio o no hay seleccionado nada
            
            return true;
        }

        // obtener y devolver el contenido del combo box
        public string getValueComboBox(ComboBox combobox)
        {
            if (!validateComboBox(combobox)) 
                return combobox.Text;
            
            return null;
        }

        // validar que contiene @ cada correo ingresado
        public bool validateEmail(string email)
        {
            // validar que el correo contenga alguna de estas direcciones
            string[] proveers = {"@unicesar.edu.co", "@gmail.com", "@outlook.com", "@hotmail.com", "@live.com", "@yahoo.com", "@zoho.com", "@protonmail.com"};
            foreach(var proveer in proveers)
            {
                if (email.Contains(proveer))
                    return true;
            }

            return false;
        }

        // retornar dirección de correo
        public string returnEmail(TextBox email)
        {
            string textEmail = email.Text;
            if (validateEmail(textEmail)) 
                return email.Text;
            
            showErrorTextBox(email);
            return null;
        }

        public void showErrorTextBox(TextBox textbox)
        {
            if (textbox == null || string.IsNullOrEmpty(textbox.Text))
                throw new ArgumentException("Error campo vacío");

            else MessageBox.Show(textbox.Text);
        }

        // obtiene los datetimepicker y retorna el año de inicio y fin del programa buscando por el último caracter "/"
        public bool validateDate(DateTimePicker dateStar, DateTimePicker dateEnd)
        {
            try
            {
                if (dateStar == null || dateEnd == null)
                    return false;

                DateTime fechaInicio = dateStar.Value;
                DateTime fechaFin = dateEnd.Value;

                // Verificar que la fecha inicial sea menor
                if (fechaInicio >= fechaFin)
                    return false;
                
                // Calcular diferencia en años
                TimeSpan diferencia = fechaFin - fechaInicio;
                double años = diferencia.TotalDays / 365.25; // Consideramos años bisiestos

                if (años < 5)
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al validar fechas: {ex.Message}");
                return false;
            }
                
        }

        public int[] returnDate(DateTimePicker dateStar, DateTimePicker dateEnd)
        {
            // retorna un arreglo con el año de inicio y fin del programa
            int yearStart = int.Parse(returnSubstring(dateStar.Text, "/"));
            int yearEnd = int.Parse(returnSubstring(dateEnd.Text, "/"));
            
            // aceptamos hasta 40 años de edad en personas
            if (yearStart < 1985 || yearEnd < 1985)
                MessageBox.Show("Error año inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            
            return new int[] { yearStart, yearEnd };
        }

        // problemas con el método
        //public string returnDateText(DateTimePicker date)
        //{
        //    string dateText = date.Text;
        //    if (string.IsNullOrEmpty(dateText))
        //        throw new ArgumentException("Error campo vacío");

        //    return dateText;    
        //}

        // retorna el substring del caracter a buscar, por el último indice en la cadena
        public string returnSubstring(string str, string character)
        {
            string substring = str.Substring(returnIndexString(str, character));
            if (string.IsNullOrEmpty(substring))
                throw new ArgumentException("Error campo vacío");

            return substring;
        }

        public int returnIndexString(string str, string character)
        {
            int index = str.LastIndexOf(character);
            if (index != -1 && index != 0) 
                return index + 1;
            
            return 0;
        }

        public bool validatePhone(string phone)
        {
            // Si el teléfono está vacío, retornar false
            if (string.IsNullOrEmpty(phone) || !phone.All(char.IsDigit))
                return false;

            // verificar que el teléfono contenga 10 números
            if (phone.Length != 10)
                return false; 

            return true;
        }

        public string isGraduated(DateTimePicker dateInit, DateTimePicker dateEnd)
        {
            DateTime fechaInicio = dateInit.Value;
            DateTime fechaFin = dateEnd.Value;
            DateTime fechaActual = DateTime.Now;
            TimeSpan tiempoTranscurrido = fechaActual - fechaFin;
            double añosTrasncurridos = tiempoTranscurrido.TotalDays / 365.25; // Consideramos años bisiestos

            if (validateDate(dateInit, dateEnd))
                return "no";

            if (añosTrasncurridos < 5)
                return "no";
            
            return "no";
        }


    }
}
