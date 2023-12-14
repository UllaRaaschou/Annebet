using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFApp.Models
{
    public abstract class SalesItemRepository // Abstrakt Parent-Repo med SQL-kode til brug for children (Product/treatment) repos
    {
        private string connectionString = "Server=10.56.8.36;Database=DB_F23_TEAM_04;User Id=DB_F23_TEAM_04;Password=TEAMDB_DB_04; TrustServerCertificate=True";

        protected int AddSalesItem(SalesItem salesItem, EnumCategory category) // Add-metode, der tager et salesItem og bruger category til differentiator
                                                                                // mellem children Product og Treatment.
                                                                                //Metoden er protected og nedarves til children
        {
            int newId; // Simpel variabel-deklaration uden værdi-tildeling

            using (SqlConnection con = new SqlConnection(connectionString)) // Skaber forb til db med klassen SqlConnection
            {
                con.Open(); // Åbner den skabte connection
                using (SqlCommand cmd = new SqlCommand("dbo.sp_AddSalesItem", con)) // Anvender vores stored procedure, der ligger i db
                {
                    cmd.CommandType = CommandType.StoredProcedure; // Anvender kommandotypen til stored procedures (både INSERT INTO og SELECT SCOPE_IDENTITY as NewId)

                    cmd.Parameters.AddWithValue("@category", category); //Indsætter værdier i parametrene fra SQL-statement
                    cmd.Parameters.AddWithValue("@type", salesItem.Type);
                    cmd.Parameters.AddWithValue("@name", salesItem.Name);
                    cmd.Parameters.AddWithValue("@description", salesItem.Description);
                    cmd.Parameters.AddWithValue("@price", salesItem.Price);

                    try // forsøger at eksekvere ovenstående ( med newId som returværdi)
                    {
                        object result = cmd.ExecuteScalar(); // metoden returnerer et object

                        if (result != null)
                        {
                            newId = Convert.ToInt32(result); //objectet parses til int, der er salesItems id
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

        protected void UpdateSalesItem(SalesItem salesItemWithUpdatedValues, EnumCategory category) // Update-metode, der tager et salesItem og bruger category til differentiator
                                                                                                    // mellem children Product og Treatment
                                                                                                    //Metoden er protected og nedarves til children
        {
            using (SqlConnection con = new SqlConnection(connectionString)) // skaber forbindelse til vores db med vores connectionstring
            {
                con.Open(); // Åbner den skabte connection

                using (SqlCommand cmd = new SqlCommand("sp_UpdateSalesItem", con)) // Anvender vores stored procedure, der ligger i db, hvor en opdateret kunde er parameter
                {
                    cmd.CommandType = CommandType.StoredProcedure; // Anvender kommandotypen til stored procedures

                    cmd.Parameters.AddWithValue("@salesItemId", salesItemWithUpdatedValues.Id);  // Indsætter den opdaterede parameter-kundens
                                                                                                 // properties som værdier i SQL-queryens parametre 
                    cmd.Parameters.AddWithValue("@category", category);
                    cmd.Parameters.AddWithValue("@type", salesItemWithUpdatedValues.Type);
                    cmd.Parameters.AddWithValue("@name", salesItemWithUpdatedValues.Name);
                    cmd.Parameters.AddWithValue("@description", salesItemWithUpdatedValues.Description);
                    cmd.Parameters.AddWithValue("@price", salesItemWithUpdatedValues.Price);

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

        protected void DeleteSalesItemById(int id)  //Metoden er protected og nedarves til children. Differentierer ikke mellem children-objekter
        {
            using (SqlConnection con = new SqlConnection(connectionString)) // skaber forbindelse til vores db med vores connectionstring
            {
                con.Open(); // Den skabte forbindelse åbnes

                using (SqlCommand cmd = new SqlCommand("dbo.sp_DeleteSalesItemById", con)) // Anvender vores stored procedure, der ligger i db, via SQLCommand-klassen
                {
                    cmd.CommandType = CommandType.StoredProcedure; // Anvender kommandotypen til stored procedures

                    cmd.Parameters.AddWithValue("@salesItemId", id); // Indsætter værdier i parametre fra stored procedure

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


        protected SqlDataReader GetAllSalesItems(EnumCategory category, string type, string name) // Bruger category som differentiator, når children skal arve metoden
                                                                                                  //Metoden er protected og nedarves til children
        {
            SqlConnection con = new SqlConnection(connectionString); // skaber forbindelse til vores db med vores connectionstring
            // Ingen brug af using, da de lukker readeren ned, hvorved children ikke kan bruge reader-metoden

            con.Open(); // Åbner den skabte forbindelse

            SqlCommand cmd = new SqlCommand("dbo.sp_GetAllSalesItemsFromCategoryAndTypeAndName_abs", con); // Anvender vores stored procedure, via klassen SQLCommand
            // Ingen brug af using, da de lukker readeren ned, hvorved children ikke kan bruge reader-metoden
            
            cmd.CommandType = CommandType.StoredProcedure; // Anvender kommandotypen til stored procedures

            cmd.Parameters.AddWithValue("@category", category);  // Indsætter vores parametre
            cmd.Parameters.AddWithValue("@type", type);
            cmd.Parameters.AddWithValue("@name", name);

            return cmd.ExecuteReader();  // Metoden ExecuteReader køres på SQLDataReader objektet og repræsenterer den datastrøm, der er resultatet af database-forespørgslen
            // Ingen brug af using, da de lukker readeren ned, hvorved children ikke kan bruge reader-metoden
              
        }
    }
    
}


