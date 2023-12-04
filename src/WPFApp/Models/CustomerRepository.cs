using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFApp.Models
{
    public class CustomerRepository
    {
        /// <summary>
        /// Tekststreng, der inkluderer server-id, database-id, vores brugernavn og vores kode
        /// </summary>
        private string connectionString = "Server=10.56.8.36;Database=DB_F23_TEAM_04;User Id=DB_F23_TEAM_04;Password=TEAMDB_DB_04; TrustServerCertificate=True";

        private List<Customer> allCustomers;
        public CustomerRepository() { }

        public int AddCustomer(Customer customer)
        {
            int newId; // Simpel variabel-deklaration uden værdi-tildeling

            using (SqlConnection con = new SqlConnection(connectionString)) // Skaber forb til db med klassen SqlConnection
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
                        object result = cmd.ExecuteScalar();

                        if(result != null) 
                        {
                            newId = Convert.ToInt32(result);
                            return newId;
                        }
                        else 
                        {
                            newId = 0;
                            return newId; 
                        }
                       
                    }

                    catch (Exception ex) // udstiller en eventuel fejlmeddelelse
                    {
                        Console.WriteLine(ex.Message);
                        return -1;
                    }
                }
            }
            
        }

        public void UpdateCustomer(Customer customerWithUpdatedValues)
        {
            using (SqlConnection con = new SqlConnection(connectionString)) // skaber forbindelse til vores db med vores connectionstring
            {
                con.Open(); // Åbner den skabte connection

                
                using (SqlCommand cmd = new SqlCommand("sp_UpdateCustomer", con)) // Anvender vores stored procedure, der ligger i db, hvor en opdateret kunde er parameter
                {                    
                    cmd.CommandType = CommandType.StoredProcedure; // Anvender kommandotypen til stored procedures

                    cmd.Parameters.AddWithValue("@firstName", customerWithUpdatedValues.FirstName); // Indsætter den opdaterede parameter-kundens properties som værdier i SQL-queryens parametre 
                    cmd.Parameters.AddWithValue("@lastName", customerWithUpdatedValues.LastName);
                    cmd.Parameters.AddWithValue("@address", customerWithUpdatedValues.Address);
                    cmd.Parameters.AddWithValue("@phone", customerWithUpdatedValues.Phone);
                    cmd.Parameters.AddWithValue("@email", customerWithUpdatedValues.Email);

                    try //forsøger at eksekvere ovenstående (uden returværdi)
                    {
                        cmd.ExecuteNonQuery();
                    }

                    catch (Exception ex) // udstiller en eventuel fejlmeddelelse
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        public Customer? GetCustomerById(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString)) // skaber forbindelse til vores db med vores connectionstring
            {
                con.Open(); // Åbner den skabte forbindelse

                using (SqlCommand cmd = new SqlCommand("dbo.sp_GetCustomerById", con)) // Anvender vores stored procedure, via klassen SQLCommand
                {                    
                    cmd.CommandType = CommandType.StoredProcedure; // Anvender kommandotypen til stored procedures

                    cmd.Parameters.AddWithValue("@customerId", id);  // Indsætter vores id fra parameter

                    using (SqlDataReader reader = cmd.ExecuteReader())  // Metoden ExecuteReader køres på SQL-Command-objektet cmd.
                                                                        // SQLDataReader objektet repræsenterer den datastrøm, der er resultatet
                                                                        // af database-forespørgslen
                    {
                        try // Read-metoden afprøves i en try-catch
                        {
                            if (reader.Read()) // Hvis readeren læser data...
                            {
                                string firstName = reader.GetString(1);
                                string lastName = reader.GetString(2);
                                string address = reader.GetString(3);
                                string phone = reader.GetString(4);
                                string email = reader.GetString(5);

                                Customer customer = Customer.GetCustomerFromDb(id, firstName, lastName, address, phone, email); // Customer-instans oprettes
                                return customer;                                                                                // - og returneres
                            }
                        }
                        catch (Exception ex) // Eventuel fejl udstilles
                        {
                            Console.WriteLine(ex.Message);

                        }
                    }
                }
            }
            return null;
        }

        public void DeleteCustomerById(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString)) // skaber forbindelse til vores db med vores connectionstring
            {
                con.Open(); // Den skabte forbindelse åbnes

                
                using (SqlCommand cmd = new SqlCommand("dbo.sp_DeleteCustomerById", con)) // Anvender vores stored procedure, der ligger i db, via SQLCommand-klassen
                {                    
                    cmd.CommandType = CommandType.StoredProcedure; // Anvender kommandotypen til stored procedures

                    cmd.Parameters.AddWithValue("@customerId", id); // Indsætter værdier i parametre fra sp

                    try // forsøger at køre ovenstående kode uden returværdi
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex) // evt fejl udstilles
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        public List<Customer> GetAllCustomers()
        {
            allCustomers = new List<Customer>(); // Ny liste af kunder instantieres
                       
            using (SqlConnection con = new SqlConnection(connectionString))  // skaber forbindelse til vores db med vores connectionstring
            {
                con.Open(); // Åbner den skabte forbindelse
                                
                using (SqlCommand cmd = new SqlCommand("sp_GetAllCustomers", con)) // Anvender vores stored procedure, der ligger i db
                {                   
                    cmd.CommandType = CommandType.StoredProcedure;  // Anvender kommandotypen til stored procedures

                    using (SqlDataReader reader = cmd.ExecuteReader()) // Metoden ExecuteReader køres på SQL-Command-objektet cmd.
                                                                       // SQLDataReader objektet repræsenterer den datastrøm, der er resultatet
                                                                       // af database-forespørgslen
                    {
                        try // Metoden afprøves i en try-catch
                        {
                            while (reader.Read()) // mens readeren læser...
                            {
                                int id = reader.GetInt32(0); // Første kolonne indlæses som int og gemmes i variabel
                                string firstName = reader.GetString(1); // Anden kolonne indlæses som streng og gemmes i variabel..
                                string lastName = reader.GetString(2);
                                string address = reader.GetString(3);
                                string phone = reader.GetString(4);
                                string email = reader.GetString(5);

                                Customer customer = Customer.GetCustomerFromDb(id, firstName, lastName, address, phone, email); // Customer objekt/INSTANS dannes
                                allCustomers.Add(customer); // Objektet/INSYANSEN add'es til listen

                            }
                        }
                        catch (Exception ex) // Evt fejl udstilles
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
            return allCustomers; // Listen returneres fra metoden
        }
    }
}
