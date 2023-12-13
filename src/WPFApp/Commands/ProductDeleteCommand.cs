﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPFApp.Models;
using WPFApp.ViewModels;

namespace WPFApp.Commands
{
    public class ProductDeleteCommand : ICommand
    {
        /// <summary>
        /// CanExecuteChanged-eventet har fået add'et et RequerySuggested-event. 
        /// Requery udløses så snart WPF mener, at command properties skal re-evalueres - ofte sfa bruger-acts.
        /// Dette trigger så CanExecuteChanged-eventet og dermed en re-evaluering af CanExecute-metoden, som så måske
        /// bliver true, hvorved knap bliver enabled og Execute-metoden kan udføres
        /// </summary>
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            bool result = false; // variablen sættes i første omgang til false

            if (parameter is ProductDeleteViewModel pdvm) // tjek af, om datakontekst er den rette
            {
                if (pdvm.SelectedProduct != null) // tjek af, at der er valgt en customer i listbox
                {
                    if (pdvm.SelectedProduct.Type != null && pdvm.SelectedProduct.Name != null && // tjek af, at alle selected Customers properties er udfyldt
                        pdvm.SelectedProduct.Description != null && pdvm.SelectedProduct.Price != null)
                    {
                        result = true; // hvis ok, sættes variabel til  true
                    }
                }
                return result; //variabel returneres
            }
            return false; // hvis parameter test fejler, returenes false
        }

        public void Execute(object? parameter)
        {
            if (parameter is ProductDeleteViewModel pdvm) // tjek af parameter
            {
                ProductRepository productRepo = new ProductRepository();  // Instantiering af ProductRepo
                productRepo.DeleteProductById(pdvm.SelectedProduct.Id); // Repo deleter selected Product fra db
                MessageBox.Show("Produkt slettet");
                pdvm.SelectedProduct = null;// Tekstbokse nulstilles
                pdvm.Type = null;
                pdvm.Name = null;
            }
        }
    }
}