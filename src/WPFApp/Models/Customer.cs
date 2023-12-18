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

        private Customer(int id, string firstName, string lastName, string address, string phone, string email) // privat construktør til brug for de create-metoder
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            Phone = phone;
            Email = email;
            Id = id;
        }


        /// <summary>
        /// Metode, der skaber en Customer-instans fra UI-input (uden id)
        /// Metoden er statisk, så den kan kaldes uden en instans af klassen
        /// </summary>
        public static Customer CreateCustomerFromUI(string firstName, string lastName, string address, string phone, string email) 
        {
            return new Customer(0, firstName, lastName, address, phone, email);
        }


        /// <summary>
        /// Metode, der skaber en Customer-instans med værdier fra db (incl. id)
        /// Metoden er statisk, så den kan kaldes uden en instans af klassen
        /// </summary>
        public static Customer CreateCustomerFromDb(int id, string firstName, string lastName, string address, string phone, string email) 
        {
            return new Customer(id, firstName, lastName, address, phone, email);
        }

        


    }
}