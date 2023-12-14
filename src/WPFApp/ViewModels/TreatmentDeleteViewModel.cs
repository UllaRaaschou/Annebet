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
    public class TreatmentDeleteViewModel : INotifyPropertyChanged
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

        public ICommand TreatmentSearchCommand { get; } = new TreatmentSearchCommand();
        public ICommand TreatmentDeleteCommand { get; } = new TreatmentDeleteCommand();

        public ObservableCollection<TreatmentToViewModel> TreatmentsToView { get; }

        public TreatmentDeleteViewModel()
        {
            TreatmentsToView = new ObservableCollection<TreatmentToViewModel>();
        }

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
    }
}