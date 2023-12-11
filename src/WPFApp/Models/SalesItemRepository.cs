using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFApp.Models
{
    public abstract class SalesItemRepository
    {
        private string connectionString = "Server=10.56.8.36;Database=DB_F23_TEAM_04;User Id=DB_F23_TEAM_04;Password=TEAMDB_DB_04; TrustServerCertificate=True";

        protected int AddSalesItem(SalesItem salesItem, EnumCategory category)
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
                        object result = cmd.ExecuteScalar();

                        if (result != null)
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

        protected void UpdateSalesItem(SalesItem salesItemWithUpdatedValues, EnumCategory category)
        {
            using (SqlConnection con = new SqlConnection(connectionString)) // skaber forbindelse til vores db med vores connectionstring
            {
                con.Open(); // Åbner den skabte connection

                using (SqlCommand cmd = new SqlCommand("sp_UpdateSalesItem", con)) // Anvender vores stored procedure, der ligger i db, hvor en opdateret kunde er parameter
                {
                    cmd.CommandType = CommandType.StoredProcedure; // Anvender kommandotypen til stored procedures

                    // Jeg har opdateret 
                    cmd.Parameters.AddWithValue("@salesItemId", salesItemWithUpdatedValues.Id);// customerWithUpdatedValues.Id);
                    cmd.Parameters.AddWithValue("@category", category); // Indsætter den opdaterede parameter-kundens properties som værdier i SQL-queryens parametre 
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

        public void DeleteSalesItemById(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString)) // skaber forbindelse til vores db med vores connectionstring
            {
                con.Open(); // Den skabte forbindelse åbnes


                using (SqlCommand cmd = new SqlCommand("dbo.sp_DeleteSalesItemById", con)) // Anvender vores stored procedure, der ligger i db, via SQLCommand-klassen
                {
                    cmd.CommandType = CommandType.StoredProcedure; // Anvender kommandotypen til stored procedures

                    cmd.Parameters.AddWithValue("@salesItemId", id); // Indsætter værdier i parametre fra sp

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


        protected SqlDataReader GetAllSalesItemsFromCategoryAndTypeAndName(EnumCategory category, string type, string name)
        {
            SqlConnection con = new SqlConnection(connectionString); // skaber forbindelse til vores db med vores connectionstring
            
            con.Open(); // Åbner den skabte forbindelse

            SqlCommand cmd = new SqlCommand("dbo.sp_GetAllSalesItemsFromCategoryAndTypeAndName_abs", con); // Anvender vores stored procedure, via klassen SQLCommand

            
            cmd.CommandType = CommandType.StoredProcedure; // Anvender kommandotypen til stored procedures

            cmd.Parameters.AddWithValue("@category", category);  // Indsætter vores id fra parameter
            cmd.Parameters.AddWithValue("@type", type);
            cmd.Parameters.AddWithValue("@name", name);

            return cmd.ExecuteReader();  // Metoden ExecuteReader køres på SQL-Command-objektet cmd.
                                             // SQLDataReader objektet repræsenterer den datastrøm, der er resultatet
                                             // af database-forespørgslen


            

        }
    }
    
}


