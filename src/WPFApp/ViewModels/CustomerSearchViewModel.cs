using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFApp.Commands;
using WPFApp.Models;

namespace WPFApp.ViewModels
{
    public class CustomerSearchViewModel : INotifyPropertyChanged  // Interface-nedarves 
    {
        public event PropertyChangedEventHandler PropertyChanged; // Interface implementeres som event

        /// <summary>
        /// Metode, der kan bruges i klasser, der implementerer INotifyPropertyChanged-interfacet.
        /// Hvis der sker en ændring i en property, sammenlignes den med property'ens private felt. 
        /// Er der sket en ændring i værdien, udløses OnPropertyChanged-metoden, 
        /// der igen udløser PropertyChanged-begivenheden, som alle abonnenter informeres om
        /// </summary>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public CustomerSearchViewModel(Customer customer) // konstuktør, der tager en Customer som parameter. Formålet er alene at indsætte et lag mellem model og view
        {
            id = customer.Id;
            FirstName = customer.FirstName;
            LastName = customer.LastName;
            Address = customer.Address;
            Phone = customer.Phone;
            Email = customer.Email;
        }


        private int id;

        public int Id
        {
            get { return id; }
            set { id = value;
                OnPropertyChanged(nameof(id)); // Metoden kaldes, fordi FirstName ændres.
                                                      // Det udløser PropertyChanged-eventet og abonnenter informeres
            }
        }


        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged(nameof(firstName)); // Metoden kaldes, fordi FirstName ændres.
                                                      // Det udløser PropertyChanged-eventet og abonnenter informeres
            }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged(nameof(lastName));
            }
        }

        private string address;
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged(nameof(address));
            }
        }

        private string phone;
        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                OnPropertyChanged(nameof(phone));
            }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged(nameof(email));
            }
        }







    }
}

