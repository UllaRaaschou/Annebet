using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using WPFApp.Commands;

namespace WPFApp.ViewModels
{
    // Interface-nedarves 
    public class ProductCreateViewModel : INotifyPropertyChanged
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
        public ICommand ProductCreateCommand { get; } = new ProductCreateCommand();


        // Metoden kaldes, fordi type ændres.
        // Det udløser PropertyChanged-eventet og abonnenter informeres
        private string type;
        public string Type
        {
            get { return type; }
            set
            { 
                type = value;
                OnPropertyChanged(nameof(type));
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(name));
            }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged(nameof(name));
            }
        }

        private decimal price;
        public decimal Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged(nameof(price));
            }
        }
    }
}