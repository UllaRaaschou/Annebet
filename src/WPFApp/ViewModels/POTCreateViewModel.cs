using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFApp.ViewModels
{
    public class POTCreateViewModel : INotifyPropertyChanged
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

        public POTCreateViewModel() { }


        private string category;

        public string Category
        {
            get { return category; }
            set { category = value;
                OnPropertyChanged(nameof(Category));
                }
        }

        private string type;

        public string Type
        {
            get { return type; }
            set { type = value;
                OnPropertyChanged(nameof(Type));
                }
        }
        private string description;

        public string Description
        {
            get { return description; }
            set { description = value;
                OnPropertyChanged(nameof(Description));
                }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value;
                OnPropertyChanged(nameof(Name));
            }
        }


        private decimal price;

        public decimal Price
        {
            get { return price; }
            set { price = value;
                OnPropertyChanged(nameof(Price));
                }
        }




    }

    
   
}
