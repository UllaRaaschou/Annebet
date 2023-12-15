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
    // Interface-nedarves 
    public class CustomerCreateViewModel : INotifyPropertyChanged
    {
        // Interface implementeres som event
        public event PropertyChangedEventHandler PropertyChanged;
 
        
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


        // Property til Create command sættes til instans af klassen
        public ICommand CustomerCreateCommand { get; } = new CustomerCreateCommand();


        // Metoden kaldes, fordi FirstName ændres.
        // Det udløser PropertyChanged-eventet og abonnenter informeres
        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            { 
                firstName = value;
                OnPropertyChanged(nameof(firstName));                                                    
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
