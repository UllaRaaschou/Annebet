using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using WPFApp.Commands;

namespace WPFApp.ViewModels
{
    // Interface-nedarves
    public class ProductDeleteViewModel : INotifyPropertyChanged
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



        // Ny instans af Command-klassen, der implementerer Icommand-interfacet
        public ICommand ProductSearchCommand { get; } = new ProductSearchCommand();
        // Ny instans af Command-klassen, der implementerer Icommand-interfacet
        public ICommand ProductDeleteCommand { get; } = new ProductDeleteCommand();



        public ProductDeleteViewModel()
        {
            ProductsToView = new ObservableCollection<ProductToViewModel>(); // instantierer Observable Collection til fremsøgte products
        }
        public ObservableCollection<ProductToViewModel> ProductsToView { get; } // Collection sættes til property



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


        private ProductToViewModel selectedProduct; // Property til selected item fra listbox
        public ProductToViewModel SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                OnPropertyChanged(nameof(selectedProduct));
            }
        }
    }
}
