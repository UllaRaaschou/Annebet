using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFApp.Commands;
using WPFApp.Models;

namespace WPFApp.ViewModels
{
    public class CustomerUpdateViewModel : INotifyPropertyChanged  // Interface nedarves
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
               
        public CustomerUpdateViewModel() 
        {
            CustomersToView = new ObservableCollection<CustomerToViewModel>(); // instansierer collection til fremsøgte customers
                      
        }
        public ObservableCollection<CustomerToViewModel> CustomersToView { get; }

        //public ObservableCollection<CustomerSearchViewModel> SearchedCustomers { get; } // Collections sættes til property
        public ICommand CustomerSearchCommand { get; } = new CustomerSearchCommand(); // Ny instans af Command-klassen, der implementerer Icommand-interfacet
        public ICommand CustomerUpdateCommand { get; } = new CustomerUpdateCommand();

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged(nameof(FirstName)); // Metoden kaldes, fordi FirstName ændres.
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
                OnPropertyChanged(nameof(LastName));
            }
        }

        private CustomerToViewModel selectedCustomer; // Property til listbox's selected item

        public CustomerToViewModel SelectedCustomer
        {
            get { return selectedCustomer; }
            set { selectedCustomer = value;
                OnPropertyChanged(nameof(SelectedCustomer));
            }
        }

        

    }
}
