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
        private string connectionString = "Server=10.56.8.36;Database=DB_F23_TEAM_04;User Id=DB_F23_TEAM_04;Password=TEAMDB_DB_04; TrustServerCertificate=True";
        public CustomerRepository() { }

        public int AddCustomer(Customer customer)
        {
            int newId;
            // skaber forbindelse til vores db med vores connectionstring
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Anvender vores stored procedure, der ligger i db
                using (SqlCommand cmd = new SqlCommand("dbo.sp_AddCustomer", con))
                {

                    // Anvender kommandotypen til stored procedures
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Indsætter parameter-kundens properties som værdier i SQL-queryens parametre (@)
                    cmd.Parameters.AddWithValue("@firstName", customer.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", customer.LastName);
                    cmd.Parameters.AddWithValue("@address", customer.Address);
                    cmd.Parameters.AddWithValue("@phone", customer.Phone);
                    cmd.Parameters.AddWithValue("@email", customer.Email);

                    

                    // forsøger at eksekvere ovenstående (uden returværdi)
                    try
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

                    // udstiller en eventuel fejlmeddelelse
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return -1;
                    }
                }
            }
            
        }

        public void UpdateCustomer(Customer customerWithUpdatedValues)
        {
            // skaber forbindelse til vores db med vores connectionstring
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Anvender vores stored procedure, der ligger i db
                using (SqlCommand cmd = new SqlCommand("sp_UpdateCustomer", con))
                {

                    // Anvender kommandotypen til stored procedures
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Indsætter den opdaterede parameter-kundens properties som værdier i SQL-queryens parametre (@)
                    cmd.Parameters.AddWithValue("@firstName", customerWithUpdatedValues.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", customerWithUpdatedValues.LastName);
                    cmd.Parameters.AddWithValue("@address", customerWithUpdatedValues.Address);
                    cmd.Parameters.AddWithValue("@phone", customerWithUpdatedValues.Phone);
                    cmd.Parameters.AddWithValue("@email", customerWithUpdatedValues.Email);

                    // forsøger at eksekvere ovenstående (uden returværdi)
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }

                    // udstiller en eventuel fejlmeddelelse
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

            }
        }

        public Customer? GetCustomerById(int id)
        {
            // skaber forbindelse til vores db med vores connectionstring
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Anvender vores stored procedure, der ligger i db
                using (SqlCommand cmd = new SqlCommand("dbo.sp_GetCustomerById", con))
                {

                    // Anvender kommandotypen til stored procedures
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Indsætter vores id fra parameter
                    cmd.Parameters.AddWithValue("@customerId", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        try
                        {
                            if (reader.Read())
                            {
                                string firstName = reader.GetString(1);
                                string lastName = reader.GetString(2);
                                string address = reader.GetString(3);
                                string phone = reader.GetString(4);
                                string email = reader.GetString(5);

                                Customer customer = Customer.GetCustomerFromDb(id, firstName, lastName, address, phone, email);
                                return customer;
                            }
                        }
                        catch (Exception ex)
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
            // skaber forbindelse til vores db med vores connectionstring
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Anvender vores stored procedure, der ligger i db
                using (SqlCommand cmd = new SqlCommand("dbo.sp_DeleteCustomerById", con))
                {

                    // Anvender kommandotypen til stored procedures
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@customerId", id);

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        public List<Customer> GetAllCustomers()
        {
            List<Customer> allCustomers = new List<Customer>();

            // skaber forbindelse til vores db med vores connectionstring
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Anvender vores stored procedure, der ligger i db
                using (SqlCommand cmd = new SqlCommand("sp_GetAllCustomers", con))
                {

                    // Anvender kommandotypen til stored procedures
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        try
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string firstName = reader.GetString(1);
                                string lastName = reader.GetString(2);
                                string address = reader.GetString(3);
                                string phone = reader.GetString(4);
                                string email = reader.GetString(5);

                                Customer customer = Customer.GetCustomerFromDb(id, firstName, lastName, address, phone, email);
                                allCustomers.Add(customer);

                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
            return allCustomers;
        }
    }
}
