using System.ComponentModel;

namespace WPFApp.Models
{
    public class Customer
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string Address { get; }
        public string Phone { get; }
        public string Email { get; }
        public int Id { get; private set; }

        private Customer(int id, string firstName, string lastName, string address, string phone, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            Phone = phone;
            Email = email;
            Id = id;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Metode, der skaber en Customer-instans fra UI-input (uden id)
        /// </summary>

        public static Customer MakeNewCustomerFromUI(string firstName, string lastName, string address, string phone, string email) 
        {
            return new Customer(0, firstName, lastName, address, phone, email);
        }

        /// <summary>
        /// Metode, der skaber en Customer-instans med værdier fra db (incl. id)
        /// </summary>
      
        public static Customer GetCustomerFromDb(int id, string firstName, string lastName, string address, string phone, string email) 
        {
            return new Customer(id, firstName, lastName, address, phone, email);
        }

        public override string ToString() 
        {
            return$"{FirstName} {LastName} - {Address} - {Phone} - {Email}";
        }



    }
}