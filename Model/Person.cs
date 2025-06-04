using System;
using System.Windows.Forms;

namespace Model357App
{
    internal class Person
    {
        private string name;
        private string lastname;
        private string email;
        private string phoneNumber;
        private int age;
        private string sex;
        private bool isGraduated;
        private DateTimePicker dateGrade;

        // crear constructor para inicializar los campos y comprobar que no sean nulos
        public Person(string name, string lastname, string email, string phoneNumber, int age, string sex, bool isGraduated, DateTimePicker dateGrade)
        {
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            this.lastname = lastname ?? throw new ArgumentNullException(nameof(lastname));
            this.email = email ?? throw new ArgumentNullException(nameof(email));
            this.phoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
            this.age = age;
            this.sex = sex ?? throw new ArgumentNullException(nameof(sex));
            this.isGraduated = isGraduated;
            this.dateGrade = dateGrade ?? throw new ArgumentNullException(nameof(dateGrade));
        }

        // crear propiedades para acceder a los campos
        public string Name
        {
            get { return name; }
            set { name = value ?? throw new ArgumentNullException(nameof(value)); }
        }

        public string LastName
        {
            get { return lastname; }
            set { lastname = value ?? throw new ArgumentNullException(nameof(value)); }
        }

        public string Email
        {
            get { return email; }
            set { email = value ?? throw new ArgumentNullException(nameof(value)); }
        }

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value ?? throw new ArgumentNullException(nameof(value)); }
        }

        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        public string Sex
        {
            get { return sex; }
            set { sex = value ?? throw new ArgumentNullException(nameof(value)); }
        }

        public bool IsGraduated
        {
            get { return isGraduated; }
            set { isGraduated = value; }
        }

        public DateTimePicker DateGrade
        {
            get { return dateGrade; }
            set { dateGrade = value ?? throw new ArgumentNullException(nameof(value)); }
        }

    }
}
       
