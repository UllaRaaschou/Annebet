﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFApp.Commands;

namespace WPFApp.ViewModels
{
    // Interface-nedarves 
    public class CustomerDeleteViewModel: INotifyPropertyChanged
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


        // Property til Search command sættes til instans af klassen
        public ICommand CustomerSearchCommand { get; } = new CustomerSearchCommand();
        // Property til Delete command sættes til instans af klassen
        public ICommand CustomerDeleteCommand { get; } = new CustomerDeleteCommand();


        // Observable Collection laves til property
        public ObservableCollection<CustomerToViewModel> CustomersToView { get; }


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

        // Property til listbox's selected item
        private CustomerToViewModel selectedCustomer;
        public CustomerToViewModel SelectedCustomer
        {
            get { return selectedCustomer; }
            set
            {
                selectedCustomer = value;
                OnPropertyChanged(nameof(selectedCustomer));
            }
        }


        // Instansierer  Observable Collection til fremsøgte customers
        public CustomerDeleteViewModel()
        {
            CustomersToView = new ObservableCollection<CustomerToViewModel>();
        }
    }
}
