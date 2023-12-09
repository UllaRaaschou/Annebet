using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using WPFApp.Models;

namespace WPFApp.Models
{
    public class POTRepository
    {
        private string connectionString = "Server=10.56.8.36;Database=DB_F23_TEAM_04;User Id=DB_F23_TEAM_04;Password=TEAMDB_DB_04; TrustServerCertificate=True";
        public POTRepository() { }

        public int AddSalesItem(SalesItem salesItem)
        {
            EnumCategory category = EnumCategory.Product;
            int newId; // Simpel variabel-deklaration uden værdi-tildeling


            if (salesItem is Product)   // Testen typen af salesItem
            {
                Product product = (Product)salesItem;
                category = EnumCategory.Product;

                using (SqlConnection con = new SqlConnection(connectionString)) // Skaber forb til db med klassen SqlConnection
                {
                    con.Open(); // Åbner den skabte connection

                    using (SqlCommand cmd = new SqlCommand("dbo.sp_AddSalesItem", con)) // Anvender vores stored procedure, der ligger i db
                    {
                        cmd.CommandType = CommandType.StoredProcedure; // Anvender kommandotypen til stored procedures (både INSERT INTO og SELECT SCOPE_IDENTITY as NewId)

                        cmd.Parameters.AddWithValue("@category", category); //Indsætter værdier i parametrene fra SQL-statement
                        cmd.Parameters.AddWithValue("@type", product.Type);
                        cmd.Parameters.AddWithValue("@name", product.Name);
                        cmd.Parameters.AddWithValue("@description", product.Description);
                        cmd.Parameters.AddWithValue("@price", product.Price);

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
            else if (salesItem is Treatment) 
            {
                Treatment treatment = (Treatment)salesItem;
                category=EnumCategory.Treatment;

                using (SqlConnection con = new SqlConnection(connectionString)) // Skaber forb til db med klassen SqlConnection
                {
                    con.Open(); // Åbner den skabte connection

                    using (SqlCommand cmd = new SqlCommand("dbo.sp_AddSalesItem", con)) // Anvender vores stored procedure, der ligger i db
                    {
                        cmd.CommandType = CommandType.StoredProcedure; // Anvender kommandotypen til stored procedures (både INSERT INTO og SELECT SCOPE_IDENTITY as NewId)

                        cmd.Parameters.AddWithValue("@category", category); //Indsætter værdier i parametrene fra SQL-statement
                        cmd.Parameters.AddWithValue("@type", treatment.Type);
                        cmd.Parameters.AddWithValue("@name", treatment.Name);
                        cmd.Parameters.AddWithValue("@description", treatment.Description);
                        cmd.Parameters.AddWithValue("@price", treatment.Price);

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
            }   return -1;             
        }

        public List<SalesItem> GetAllSalesItemsFromCategoryAndTypeAndName(EnumCategory category, string type, string name)
        {
            List<SalesItem> allSpecifiedSalesitems = new List<SalesItem>();
            using (SqlConnection con = new SqlConnection(connectionString)) // skaber forbindelse til vores db med vores connectionstring
            {
                con.Open(); // Åbner den skabte forbindelse

                using (SqlCommand cmd = new SqlCommand("dbo.sp_GetAllSalesItemsFromCategoryAndTypeAndName", con)) // Anvender vores stored procedure, via klassen SQLCommand

                {
                    cmd.CommandType = CommandType.StoredProcedure; // Anvender kommandotypen til stored procedures

                    cmd.Parameters.AddWithValue("@category", category);  // Indsætter vores id fra parameter
                    cmd.Parameters.AddWithValue("@type", type);
                    cmd.Parameters.AddWithValue("@name", name);

                    using (SqlDataReader reader = cmd.ExecuteReader())  // Metoden ExecuteReader køres på SQL-Command-objektet cmd.
                                                                        // SQLDataReader objektet repræsenterer den datastrøm, der er resultatet
                                                                        // af database-forespørgslen
                    {
                        try // Read-metoden afprøves i en try-catch
                        {
                            while (reader.Read()) // Sålænge readeren læser data...
                            {
                                int id = reader.GetInt32(0);    // De efterspurgte SQL-kolonner læses
                                string description = reader.GetString(1);
                                decimal price = reader.GetDecimal(2);

                                if (category is EnumCategory.Product)
                                {
                                    Product product = Product.CreateProductFromDb(id, type, name, description, price);
                                    allSpecifiedSalesitems.Add(product);
                                }
                                else if (category is EnumCategory.Treatment)
                                {
                                    Treatment treatment = Treatment.CreateTreatmentFromDb(id, type, name, description, price);
                                    allSpecifiedSalesitems.Add((treatment));
                                }
                                else { throw new Exception("Category ikke defineret"); }

                            }
                        }
                        catch (Exception ex) // Eventuel fejl udstilles
                        {
                            Console.WriteLine(ex.Message);
                            return null;

                        }
                    }
                }
            }
            return allSpecifiedSalesitems; // listen returneres
        }

        public List<SalesItem> GetAllSalesItems()
        {
            List<SalesItem> allSpecifiedSalesitems = new List<SalesItem>();
            using (SqlConnection con = new SqlConnection(connectionString)) // skaber forbindelse til vores db med vores connectionstring
            {
                con.Open(); // Åbner den skabte forbindelse

                using (SqlCommand cmd = new SqlCommand("dbo.sp_GetAllSalesItems", con)) // Anvender vores stored procedure, via klassen SQLCommand

                {
                    cmd.CommandType = CommandType.StoredProcedure; // Anvender kommandotypen til stored procedures

                    using (SqlDataReader reader = cmd.ExecuteReader())  // Metoden ExecuteReader køres på SQL-Command-objektet cmd.
                                                                        // SQLDataReader objektet repræsenterer den datastrøm, der er resultatet
                                                                        // af database-forespørgslen
                    {
                        try // Read-metoden afprøves i en try-catch
                        {
                            while (reader.Read()) // Sålænge readeren læser data... 
                            {
                                int id = reader.GetInt32(0); // De efterspurgte SQL-kolonner læses
                                string categoryString = reader.GetString(1); // Enum hentes i første omgang ned som string
                                string type = reader.GetString(2);
                                string name = reader.GetString(3);
                                string description = reader.GetString(4);
                                decimal price = reader.GetDecimal(5);

                                EnumCategory category;
                                if (Enum.TryParse(categoryString, out category))  // string forsøges parset til Enumværdi

                                    if (category is EnumCategory.Product)
                                    {
                                        Product product = Product.CreateProductFromDb(id, type, name, description, price);
                                        allSpecifiedSalesitems.Add(product);
                                    }
                                    else if (category is EnumCategory.Treatment)
                                    {
                                        Treatment treatment = Treatment.CreateTreatmentFromDb(id, type, name, description, price);
                                        allSpecifiedSalesitems.Add((treatment));
                                    }
                                    else { throw new Exception("Category ikke defineret"); }

                            }
                        }
                        catch (Exception ex) // Eventuel fejl udstilles
                        {
                            Console.WriteLine(ex.Message);
                            return null;
                        }
                    }
                }
            }
            return allSpecifiedSalesitems; // listen returneres
        }

        public void UpdateSalesItem(SalesItem salesItemWithUpdatedValues)
        {
            if (salesItemWithUpdatedValues is Product)
            {
                Product product = (Product)salesItemWithUpdatedValues;
                EnumCategory category = EnumCategory.Product;

                using (SqlConnection con = new SqlConnection(connectionString)) // skaber forbindelse til vores db med vores connectionstring
                {
                    con.Open(); // Åbner den skabte connection

                    using (SqlCommand cmd = new SqlCommand("sp_UpdateSalesItem", con)) // Anvender vores stored procedure, der ligger i db, hvor en opdateret kunde er parameter
                    {
                        cmd.CommandType = CommandType.StoredProcedure; // Anvender kommandotypen til stored procedures

                        // Jeg har opdateret 
                        cmd.Parameters.AddWithValue("@salesItemId", product.Id);// customerWithUpdatedValues.Id);
                        cmd.Parameters.AddWithValue("@category", category); // Indsætter den opdaterede parameter-kundens properties som værdier i SQL-queryens parametre 
                        cmd.Parameters.AddWithValue("@type", product.Type);
                        cmd.Parameters.AddWithValue("@name", product.Name);
                        cmd.Parameters.AddWithValue("@description", product.Description);
                        cmd.Parameters.AddWithValue("@price", product.Price);

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
            else if (salesItemWithUpdatedValues is Treatment)
            {
                Treatment treatment = (Treatment)salesItemWithUpdatedValues;
                EnumCategory category = EnumCategory.Treatment;

                using (SqlConnection con = new SqlConnection(connectionString)) // skaber forbindelse til vores db med vores connectionstring
                {
                    con.Open(); // Åbner den skabte connection

                    using (SqlCommand cmd = new SqlCommand("sp_UpdateSalesItem", con)) // Anvender vores stored procedure, der ligger i db, hvor en opdateret kunde er parameter
                    {
                        cmd.CommandType = CommandType.StoredProcedure; // Anvender kommandotypen til stored procedures

                        // Jeg har opdateret 
                        cmd.Parameters.AddWithValue("@salesItemId", treatment.Id);// customerWithUpdatedValues.Id);
                        cmd.Parameters.AddWithValue("@category", category); // Indsætter den opdaterede parameter-kundens properties som værdier i SQL-queryens parametre 
                        cmd.Parameters.AddWithValue("@type", treatment.Type);
                        cmd.Parameters.AddWithValue("@name", treatment.Name);
                        cmd.Parameters.AddWithValue("@description", treatment.Description);
                        cmd.Parameters.AddWithValue("@price", treatment.Price);

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
            else
            {
                throw new Exception("Opdatering mislykket");
            }
        }
    }    
}
