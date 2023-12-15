using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFApp.Models
{
    // Abstrakt Parent-Repo med SQL-kode til brug for children (Product/treatment) repos
    public abstract class SalesItemRepository
    {
        // Tekststreng, der inkluderer server-id, database-id, vores brugernavn og vores kode
        private string connectionString = "Server=10.56.8.36;Database=DB_F23_TEAM_04;User Id=DB_F23_TEAM_04;Password=TEAMDB_DB_04; TrustServerCertificate=True";


        /// Add-metode, der tager et salesItem og bruger category til differentiator
        /// mellem children Product og Treatment.
        /// Metoden er protected og nedarves til children
        protected int AddSalesItem(SalesItem salesItem, EnumCategory category)                                                                                                                                                              
        {
            // Simpel variabel-deklaration uden værdi-tildeling
            int newId;

            // Skaber forb til db med klassen SqlConnection
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Åbner den skabte connection
                con.Open();

                // Anvender vores stored procedure, der ligger i db
                using (SqlCommand cmd = new SqlCommand("dbo.sp_AddSalesItem", con))
                {
                    // Anvender kommandotypen til stored procedures (både INSERT INTO og SELECT SCOPE_IDENTITY as NewId)
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Indsætter værdier i parametrene fra SQL-statement
                    cmd.Parameters.AddWithValue("@category", category);
                    cmd.Parameters.AddWithValue("@type", salesItem.Type);
                    cmd.Parameters.AddWithValue("@name", salesItem.Name);
                    cmd.Parameters.AddWithValue("@description", salesItem.Description);
                    cmd.Parameters.AddWithValue("@price", salesItem.Price);

                    // Forsøger at eksekvere ovenstående ( med newId som returværdi)
                    try
                    {
                        // Metoden returnerer et object
                        object result = cmd.ExecuteScalar(); 

                        if (result != null)
                        {
                            //Objectet parses til int, der er salesItems id
                            newId = Convert.ToInt32(result); 
                            return newId;
                        }
                        else
                        {
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


        // Update-metode, der tager et salesItem og bruger category til differentiator
        // mellem children Product og Treatment
        // Metoden er protected og nedarves til children
        protected void UpdateSalesItem(SalesItem salesItemWithUpdatedValues, EnumCategory category)                                                                                                                                                                                                      
        {
            // skaber forbindelse til vores db med vores connectionstring
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Åbner den skabte connection
                con.Open();

                // Anvender vores stored procedure, der ligger i db, hvor en opdateret kunde er parameter
                using (SqlCommand cmd = new SqlCommand("sp_UpdateSalesItem", con))
                {
                    // Anvender kommandotypen til stored procedures
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Indsætter den opdaterede parameter-kundens
                    // Properties som værdier i SQL-queryens parametre
                    cmd.Parameters.AddWithValue("@salesItemId", salesItemWithUpdatedValues.Id); 
                                                                                                 
                    cmd.Parameters.AddWithValue("@category", category);
                    cmd.Parameters.AddWithValue("@type", salesItemWithUpdatedValues.Type);
                    cmd.Parameters.AddWithValue("@name", salesItemWithUpdatedValues.Name);
                    cmd.Parameters.AddWithValue("@description", salesItemWithUpdatedValues.Description);
                    cmd.Parameters.AddWithValue("@price", salesItemWithUpdatedValues.Price);

                    //Forsøger at eksekvere ovenstående (uden returværdi)
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



        /// Bruger category som differentiator, når children skal arve metoden
        /// Metoden er protected og nedarves til children
        protected SqlDataReader GetAllSalesItems(EnumCategory category, string type, string name)
                                                                                                 
        {
            // Skaber forbindelse til vores db med vores connectionstring
            // Ingen brug af using, da de lukker readeren ned, hvorved children ikke kan bruge reader-metoden
            SqlConnection con = new SqlConnection(connectionString);

            // Åbner den skabte forbindelse
            con.Open();

            // Anvender vores stored procedure, via klassen SQLCommand
            // Ingen brug af using, da de lukker readeren ned, hvorved children ikke kan bruge reader-metoden
            SqlCommand cmd = new SqlCommand("dbo.sp_GetAllSalesItemsFromCategoryAndTypeAndName_abs", con);

            // Anvender kommandotypen til stored procedures
            cmd.CommandType = CommandType.StoredProcedure;

            // Indsætter vores parametre
            cmd.Parameters.AddWithValue("@category", category); 
            cmd.Parameters.AddWithValue("@type", type);
            cmd.Parameters.AddWithValue("@name", name);

            // Metoden ExecuteReader køres på SQLDataReader objektet og repræsenterer den datastrøm, der er resultatet af database-forespørgslen
            // Ingen brug af using, da de lukker readeren ned, hvorved children ikke kan bruge reader-metoden
            return cmd.ExecuteReader();                           
        }


        ///Metoden er protected og nedarves til children. Differentierer ikke mellem children-objekter
        protected void DeleteSalesItemById(int id)
        {
            // Skaber forbindelse til vores db med vores connectionstring
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Den skabte forbindelse åbnes
                con.Open();

                // Anvender vores stored procedure, der ligger i db, via SQLCommand-klassen
                using (SqlCommand cmd = new SqlCommand("dbo.sp_DeleteSalesItemById", con))
                {
                    // Anvender kommandotypen til stored procedures
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Indsætter værdier i parametre fra stored procedure
                    cmd.Parameters.AddWithValue("@salesItemId", id);

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


