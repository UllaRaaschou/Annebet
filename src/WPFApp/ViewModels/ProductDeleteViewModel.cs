using System;
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
    public class ProductDeleteViewModel : INotifyPropertyChanged
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
        public ICommand ProductSearchCommand { get; } = new ProductSearchCommand();
        // Property til Delete command sættes til instans af klassen
        public ICommand ProductDeleteCommand { get; } = new ProductDeleteCommand();


        // Observable Collection laves til property
        public ObservableCollection<ProductToViewModel> ProductsToView { get; }


        // Metoden kaldes, fordi type ændres.
        // Det udløser PropertyChanged-eventet og abonnenter informeres
        private string type;
        public string Type
        {
            get { return type; }
            set
            {
                type = value;
                OnPropertyChanged(nameof(Type));
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Type));
            }
        }


        // Property til selected item fra listbox
        private ProductToViewModel selectedProduct; 
        public ProductToViewModel SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                OnPropertyChanged(nameof(selectedProduct));
            }
        }


        // Instansierer  Observable Collection til fremsøgte products
        public ProductDeleteViewModel()
        {
            ProductsToView = new ObservableCollection<ProductToViewModel>();
        }
    }
}
