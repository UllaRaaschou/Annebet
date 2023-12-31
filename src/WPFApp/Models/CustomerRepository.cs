﻿using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace WPFApp.Models
{
    // Implementerer interfacet IProductRepository
    public class CustomerRepository : ICustomerRepository
    {
        /// <summary>
        /// Modtager connectionstring fra den statiske klasse ConnectionStringManager
        /// </summary>        
        static string connectionString = ConnectionStringManager.ConnectionString;



        /// Add-metode, der tager en Customer
        public int AddCustomer(Customer customer)
        {
            int newId; // Simpel variabel-deklaration uden værdi-tildeling

            using (SqlConnection con = new SqlConnection(connectionString)) // Skaber forb til db med klassen SqlConnection-klassen
            {
                con.Open(); // Åbner den skabte connection
                using (SqlCommand cmd = new SqlCommand("dbo.sp_AddCustomer", con)) // Anvender vores stored procedure, der ligger i db
                {
                    cmd.CommandType = CommandType.StoredProcedure; // Anvender kommandotypen til stored procedures (både INSERT INTO og SELECT SCOPE_IDENTITY as NewId)

                    cmd.Parameters.AddWithValue("@firstName", customer.FirstName); //Indsætter værdier i parametrene fra SQL-statement
                    cmd.Parameters.AddWithValue("@lastName", customer.LastName);
                    cmd.Parameters.AddWithValue("@address", customer.Address);
                    cmd.Parameters.AddWithValue("@phone", customer.Phone);
                    cmd.Parameters.AddWithValue("@email", customer.Email);

                    try // forsøger at eksekvere ovenstående ( med newId som returværdi)
                    {
                        object result = cmd.ExecuteScalar();  // returværdien fra ExecuteScalar er af typen object

                        if (result != null) // hvis der er kommet noget retur 
                        {
                            newId = Convert.ToInt32(result); //returværid parses til int
                            return newId; //int returneres og er den add'ede customers id
                        }
                        else
                        {
                            newId = 0; // hvis der ikke er kommet noget retur
                            return newId;
                        }
                    }
                    catch (Exception ex) // udstiller en eventuel fejlmeddelelse
                    {
                        throw new Exception("Det var ikke muligt at tilføje en kunde til databasen", ex);
                    }
                }
            }
        }



        /// Update-metode, der tager et SalesItem
        public void UpdateCustomer(Customer customerWithUpdatedValues)
        {
            using (SqlConnection con = new SqlConnection(connectionString)) // skaber forbindelse til vores db med vores connectionstring
            {
                con.Open(); // Åbner den skabte connection
                using (SqlCommand cmd = new SqlCommand("sp_UpdateCustomer", con)) // Anvender vores stored procedure, der ligger i db, hvori en opdateret kunde er parameter
                {
                    cmd.CommandType = CommandType.StoredProcedure; // Anvender kommandotypen til stored procedures

                    cmd.Parameters.AddWithValue("@customerId", customerWithUpdatedValues.Id); // Indsætter den opdaterede parameter-kundens properties som værdier i SQL-queryens parametre 
                    cmd.Parameters.AddWithValue("@lastName", customerWithUpdatedValues.LastName);
                    cmd.Parameters.AddWithValue("@firstName", customerWithUpdatedValues.FirstName);
                    cmd.Parameters.AddWithValue("@address", customerWithUpdatedValues.Address);
                    cmd.Parameters.AddWithValue("@phone", customerWithUpdatedValues.Phone);
                    cmd.Parameters.AddWithValue("@email", customerWithUpdatedValues.Email);

                    try //forsøger at eksekvere ovenstående (uden returværdi)
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex) // udstiller en eventuel fejlmeddelelse
                    {
                        throw new Exception("Kunden kunne ikke opdateres", ex);
                    }
                }
            }
        }



        /// Update-metode
        public void DeleteCustomerById(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString)) // skaber forbindelse til vores db med vores connectionstring
            {
                con.Open(); // Den skabte forbindelse åbnes

                using (SqlCommand cmd = new SqlCommand("dbo.sp_DeleteCustomerById", con)) // Anvender vores stored procedure, der ligger i db, via SQLCommand-klassen
                {
                    cmd.CommandType = CommandType.StoredProcedure; // Anvender kommandotypen til stored procedures

                    cmd.Parameters.AddWithValue("@customerId", id); // Indsætter værdier i parametre fra den stored procedure

                    try // forsøger at køre ovenstående kode uden returværdi
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex) // evt fejl udstilles
                    {
                        throw new Exception("Kunden kunne ikke slettes", ex);
                    }
                }
            }
        }



        /// Update-metode, der tager firstName & lastName
        public List<Customer> GetAllCustomers(string firstName, string lastName)
        {
            List<Customer> allSpecifiedCustomer = new List<Customer>(); // instantiering af tom Customerliste
            using (SqlConnection con = new SqlConnection(connectionString)) // skaber forbindelse til vores db med vores connectionstring
            {
                con.Open(); // Åbner den skabte forbindelse

                using (SqlCommand cmd = new SqlCommand("dbo.sp_GetAllCustomers", con)) // Anvender vores stored procedure, via klassen SQLCommand
                {
                    cmd.CommandType = CommandType.StoredProcedure; // Anvender kommandotypen til stored procedures

                    cmd.Parameters.AddWithValue("@firstName", firstName);  // Indsætter vores id fra parameter
                    cmd.Parameters.AddWithValue("@lastName", lastName);

                    using (SqlDataReader reader = cmd.ExecuteReader())  // Metoden ExecuteReader køres på SQL-Command-objektet cmd.
                                                                        // SQLDataReader objektet repræsenterer den datastrøm, der er resultatet
                                                                        // af database-forespørgslen
                    {
                        try // Read-metoden afprøves i en try-catch
                        {
                            while (reader.Read()) // Sålænge readeren læser data...
                            {
                                int id = reader.GetInt32(0);  //SQL kolonne 0 sættes til int'en id
                                string address = reader.GetString(1);
                                string phone = reader.GetString(2);
                                string email = reader.GetString(3);

                                Customer customer = Customer.CreateCustomerFromDb(id, firstName, lastName, address, phone, email); // Customer-instans oprettes
                                allSpecifiedCustomer.Add(customer); // kunden med efterspurgte for- og efternavn add'es til listen                                                                         // - og returneres
                            }
                        }
                        catch (Exception ex) // Eventuel fejl udstilles
                        {
                            throw new Exception("Kunder med specificerede for- og efternavn kunne ikke fremsøges", ex);

                        }
                    }
                }
            }
            return allSpecifiedCustomer; // listen returneres
        }
    }
}
