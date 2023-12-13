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
	public class ProductUpdateViewModel: INotifyPropertyChanged
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




        public ProductUpdateViewModel()  // constructoer instantierer Observable Collections af fremsøgte Products's ToViewModels
        {
            ProductsToView = new ObservableCollection<ProductToViewModel>();
        }

        public ObservableCollection<ProductToViewModel> ProductsToView { get; } //ObservableCollection sættes som property

		
        private string type;
        public string Type
		{
			get { return type; }
			set { type = value;
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

        public ICommand ProductSearchCommand { get; } = new ProductSearchCommand(); // Ny instans af Command-klassen, der implementerer Icommand-interfacet
        public ICommand ProductUpdateCommand { get; } = new ProductUpdateCommand();



        private ProductToViewModel selectedProduct;  // property til brug for selected item
        public ProductToViewModel SelectedProduct
        {
            get { return selectedProduct; }
            set { selectedProduct = value;
                OnPropertyChanged(nameof(selectedProduct));
            }
        }

    }
}