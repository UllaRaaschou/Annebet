using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using WPFApp.Commands;

namespace WPFApp.ViewModels
{
    // Interface-nedarves 
    public class TreatmentDeleteViewModel : INotifyPropertyChanged
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
        public ICommand TreatmentSearchCommand { get; } = new TreatmentSearchCommand();
        // Property til Delete command sættes til instans af klassen
        public ICommand TreatmentDeleteCommand { get; } = new TreatmentDeleteCommand();


        // Observable Collection laves til property
        public ObservableCollection<TreatmentToViewModel> TreatmentsToView { get; }

 
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


        // Property til selected item fra listbox
        private TreatmentToViewModel selectedTreatment;
        public TreatmentToViewModel SelectedTreatment
        {
            get { return selectedTreatment; }
            set
            {
                selectedTreatment = value;
                OnPropertyChanged(nameof(selectedTreatment));
            }
        }


        // Instansierer  Observable Collection til fremsøgte treatments
        public TreatmentDeleteViewModel()
        {
            TreatmentsToView = new ObservableCollection<TreatmentToViewModel>();
        }
    }
}