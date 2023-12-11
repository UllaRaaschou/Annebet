//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace WPFApp.Models
//{
//    public abstract class SalesItemRepository
//   {
//        private string connectionString = "Server=10.56.8.36;Database=DB_F23_TEAM_04;User Id=DB_F23_TEAM_04;Password=TEAMDB_DB_04; TrustServerCertificate=True";

//        protected void UpdateSalesItem(SalesItem salesItem, EnumCategory category)
//        {
//            using (SqlConnection con = new SqlConnection(connectionString)) // skaber forbindelse til vores db med vores connectionstring
//            {
//                con.Open(); // Åbner den skabte connection

//                using (SqlCommand cmd = new SqlCommand("sp_UpdateSalesItem", con)) // Anvender vores stored procedure, der ligger i db, hvor en opdateret kunde er parameter
//                {
//                    cmd.CommandType = CommandType.StoredProcedure; // Anvender kommandotypen til stored procedures

//                    // Jeg har opdateret 
//                    cmd.Parameters.AddWithValue("@salesItemId", salesItem.Id);// customerWithUpdatedValues.Id);
//                    cmd.Parameters.AddWithValue("@category", category); // Indsætter den opdaterede parameter-kundens properties som værdier i SQL-queryens parametre 
//                    cmd.Parameters.AddWithValue("@type", salesItem.Type);
//                    cmd.Parameters.AddWithValue("@name", salesItem.Name);
//                    cmd.Parameters.AddWithValue("@description", salesItem.Description);
//                    cmd.Parameters.AddWithValue("@price", salesItem.Price);

//                    try //forsøger at eksekvere ovenstående (uden returværdi)
//                    {
//                        cmd.ExecuteNonQuery();
//                    }

//                    catch (Exception ex) // udstiller en eventuel fejlmeddelelse
//                    {
//                        Console.WriteLine(ex.Message);
//                    }
//                }
//            }
//        }

       
//   }






//    public class ProductRepository : SalesItemRepository
//    {
//       public void UpdateProduct(Product product)
//        {
//            base.UpdateSalesItem(product, EnumCategory.Product);
//        }

        
//    }
//}


