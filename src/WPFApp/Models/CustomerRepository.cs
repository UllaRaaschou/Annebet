using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFApp.Models
{
    // Implementerer interfacet IProductRepository
    public class CustomerRepository : ICustomerRepository
    {
        // Tekststreng, der inkluderer server-id, database-id, vores brugernavn og vores kode
        private string connectionString = "Server=10.56.8.36;Database=DB_F23_TEAM_04;User Id=DB_F23_TEAM_04;Password=TEAMDB_DB_04; TrustServerCertificate=True";


        public int AddCustomer(Customer customer)
        {
            // Simpel variabel-deklaration uden værdi-tildeling
            int newId;

            // Skaber forb til db med klassen SqlConnection-klassen
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open(); // Åbner den skabte connection

                // Anvender vores stored procedure, der ligger i db
                using (SqlCommand cmd = new SqlCommand("dbo.sp_AddCustomer", con)) 
                {
                    // Anvender kommandotypen til stored procedures (både INSERT INTO og SELECT SCOPE_IDENTITY as NewId)
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Indsætter værdier i parametrene fra SQL-statement
                    cmd.Parameters.AddWithValue("@firstName", customer.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", customer.LastName);
                    cmd.Parameters.AddWithValue("@address", customer.Address);
                    cmd.Parameters.AddWithValue("@phone", customer.Phone);
                    cmd.Parameters.AddWithValue("@email", customer.Email);

                    // Forsøger at eksekvere ovenstående ( med newId som returværdi)
                    try
                    {
                        // Returværdien fra ExecuteScalar er af typen object
                        object result = cmd.ExecuteScalar();

                        // Hvis der er kommet noget retur
                        if (result != null)  
                        {
                            //Returværid parses til int
                            newId = Convert.ToInt32(result); 
                            
                            //Int returneres og er den add'ede customers id
                            return newId; 
                        }
                        else
                        {
                            // Hvis der ikke er kommet noget retur
                            newId = 0; 
                            return newId;
                        }

                    }

                    // Udstiller en eventuel fejlmeddelelse
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return -1;
                    }
                }
            }
        }

        
        public List<Customer> GetAllCustomers(string firstName, string lastName)
        {
            // Instantiering af tom Customerliste
            List<Customer> allSpecifiedCustomer = new List<Customer>();

            // Skaber forbindelse til vores db med vores connectionstring
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Åbner den skabte forbindelse
                con.Open();

                // Anvender vores stored procedure, via klassen SQLCommand
                using (SqlCommand cmd = new SqlCommand("dbo.sp_GetAllCustomers", con))
                {
                    // Anvender kommandotypen til stored procedures
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Indsætter vores id fra parameter
                    cmd.Parameters.AddWithValue("@firstName", firstName); 
                    cmd.Parameters.AddWithValue("@lastName", lastName);

                    // Metoden ExecuteReader køres på SQL-Command-objektet cmd.
                    // SQLDataReader objektet repræsenterer den datastrøm, der er resultatet
                    // af database-forespørgslen
                    using (SqlDataReader reader = cmd.ExecuteReader()) 
                                                                       
                                                                       
                    {
                        // Read-metoden afprøves i en try-catch
                        try
                        {
                            // Sålænge readeren læser data...
                            while (reader.Read()) 
                            {
                                //SQL kolonne 0 sættes til int'en id
                                int id = reader.GetInt32(0);  
                                string address = reader.GetString(1);
                                string phone = reader.GetString(2);
                                string email = reader.GetString(3);

                                // Customer-instans oprettes
                                Customer customer = Customer.CreateCustomerFromDb(id, firstName, lastName, address, phone, email);

                                // kunden med efterspurgte for- og efternavn add'es til listen 
                                allSpecifiedCustomer.Add(customer);                                                                        // - og returneres
                            }
                        }
                        // Eventuel fejl udstilles
                        catch (Exception ex) 
                        {
                            Console.WriteLine(ex.Message);
                            return null;
                        }
                    }
                }
            }
            // Listen returneres
            return allSpecifiedCustomer; 
        }


        public void UpdateCustomer(Customer customerWithUpdatedValues)
        {
            // Skaber forbindelse til vores db med vores connectionstring
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Åbner den skabte connection
                con.Open();

                // Anvender vores stored procedure, der ligger i db, hvori en opdateret kunde er parameter
                using (SqlCommand cmd = new SqlCommand("sp_UpdateCustomer", con))
                {
                    // Anvender kommandotypen til stored procedures
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Indsætter den opdaterede parameter-kundens properties som værdier i SQL-queryens parametre
                    cmd.Parameters.AddWithValue("@customerId", customerWithUpdatedValues.Id); 
                    cmd.Parameters.AddWithValue("@lastName", customerWithUpdatedValues.LastName);
                    cmd.Parameters.AddWithValue("@firstName", customerWithUpdatedValues.FirstName);
                    cmd.Parameters.AddWithValue("@address", customerWithUpdatedValues.Address);
                    cmd.Parameters.AddWithValue("@phone", customerWithUpdatedValues.Phone);
                    cmd.Parameters.AddWithValue("@email", customerWithUpdatedValues.Email);

                    // Forsøger at eksekvere ovenstående (uden returværdi)
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }

                    // Udstiller en eventuel fejlmeddelelse
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }


        public void UpdateCustomer(Customer customerWithUpdatedValues, int id)
        {
            throw new NotImplementedException();
        }


        public void DeleteCustomerById(int id)
        {
            // Skaber forbindelse til vores db med vores connectionstring
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Den skabte forbindelse åbnes
                con.Open();

                // Anvender vores stored procedure, der ligger i db, via SQLCommand-klassen
                using (SqlCommand cmd = new SqlCommand("dbo.sp_DeleteCustomerById", con))
                {
                    // Anvender kommandotypen til stored procedures
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Indsætter værdier i parametre fra den stored procedure
                    cmd.Parameters.AddWithValue("@customerId", id);

                    // Forsøger at køre ovenstående kode uden returværdi
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    // Evt fejl udstilles
                    catch (Exception ex) 
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }      
    }
}
