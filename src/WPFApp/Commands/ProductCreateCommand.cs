﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPFApp.Models;
using WPFApp.ViewModels;

namespace WPFApp.Commands
{
    public class ProductCreateCommand : ICommand
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
            bool result = false;  // variablen sættes i første omgang til false

            if (parameter is ProductCreateViewModel pcvm) // parameter tjekkes 
            {
                if (pcvm.Type != null && pcvm.Name != null && pcvm.Description != null // Det tjekkes, om alle nødvendige tekstbokse er udfyldt
                    && pcvm.Price != null)
                {
                    result = true; // så sættes variblen til true;
                }
                return result; // variablen returneres

            }
            return false; // hvis datacontext ikke er sat korrekt, returneres false
        }


        /// <summary>
        /// Metoden, der udfører opret_product_funktionen og får den add'et til database.
        /// Parameteren er i xaml-koden sat som "CommandParameter = Binding", og datakontekst er i code behind sat til pcvm.
        /// </summary>
        public void Execute(object? parameter) 
        {
            if (parameter is ProductCreateViewModel pcvm) // parameter tjekkes
            {
                ProductRepository productRepo = new ProductRepository(); // productRepo instantiers
                productRepo.AddProduct(Product.CreateProductFromUI(pcvm.Type, pcvm.Name, pcvm.Description, pcvm.Price)); // repo add'er product i db
                pcvm.Type = null; // Tekstboksene nulstilles
                pcvm.Name = null;
                pcvm.Description = null;
                pcvm.Price = 0;
                MessageBox.Show("Produkt oprettet");
            }
            else 
            {
                throw new NotImplementedException();
            }

        }
    }
}