using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFApp.Models
{
    public class POTRepository
    {
        private string connectionString = "Server=10.56.8.36;Database=DB_F23_TEAM_04;User Id=DB_F23_TEAM_04;Password=TEAMDB_DB_04; TrustServerCertificate=True";
        public POTRepository() { }

        public int AddSalesItem(SalesItem salesItem) 
        {
            int newId; // Simpel variabel-deklaration uden værdi-tildeling
            string category = "";
            if(salesItem is Product) { category = "Product"; }       // tester, om salesItem er product eller treatment og sætter category derefter
            if(salesItem is Treatment) { category = "Treatment"; }

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
    }
}
