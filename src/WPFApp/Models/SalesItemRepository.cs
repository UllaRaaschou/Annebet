﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFApp.Models
{
    public abstract class SalesItemRepository : IDisposable // Abstrakt Parent-Repo med SQL-kode til brug for children (Product/treatment) repos
                                                            // Implementerer IDisposable for at kunne genbruge Repositoriets connection til Db i hele
                                                            // Repo'ets levetid.
    {
        private const string connectionString = "Server=10.56.8.36;Database=DB_F23_TEAM_04;User Id=DB_F23_TEAM_04;Password=TEAMDB_DB_04; TrustServerCertificate=True";
        // sættes til en konstant for at kunne genbruges
        protected int AddSalesItem(SalesItem salesItem, EnumCategory category) // Add-metode, der tager et salesItem og bruger category til differentiator
                                                                                // mellem children Product og Treatment.
                                                                                //Metoden er protected og nedarves til children
        {
            int newId; // Simpel variabel-deklaration uden værdi-tildeling

            SqlConnection con = GetOpenSqlConnection(); // Skaber forb til db med klassen SqlConnection  ***** bruger ikke "using" til at dispose
                           
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

        protected void UpdateSalesItem(SalesItem salesItemWithUpdatedValues, EnumCategory category) // Update-metode, der tager et salesItem og bruger category til differentiator
                                                                                                    // mellem children Product og Treatment
                                                                                                    //Metoden er protected og nedarves til children
        {
            SqlConnection con = GetOpenSqlConnection(); // skaber forbindelse til vores db med vores connectionstring ***** uden brug af "using"

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

        protected void DeleteSalesItemById(int id)  //Metoden er protected og nedarves til children. Differentierer ikke mellem children-objekter
        {
            SqlConnection con = GetOpenSqlConnection(); // skaber forbindelse til vores db med vores connectionstring *****uden brug af "using"

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


        protected SqlDataReader GetAllSalesItems(EnumCategory category, string type, string name) // Bruger category som differentiator, når children skal arve metoden
                                                                                                  //Metoden er protected og nedarves til children
        {
            SqlConnection con = GetOpenSqlConnection(); // skaber forbindelse til vores db med vores connectionstring
            // Ingen brug af using, da de lukker readeren ned, hvorved children ikke kan bruge reader-metoden

            using (SqlCommand cmd = new SqlCommand("dbo.sp_GetAllSalesItemsFromCategoryAndTypeAndName_abs", con)) // Anvender vores stored procedure, via klassen SQLCommand
                                                                                                                  // Ingen brug af using, da de lukker readeren ned, hvorved children ikke kan bruge reader-metoden
            {
                cmd.CommandType = CommandType.StoredProcedure; // Anvender kommandotypen til stored procedures

                cmd.Parameters.AddWithValue("@category", category);  // Indsætter vores parametre
                cmd.Parameters.AddWithValue("@type", type);
                cmd.Parameters.AddWithValue("@name", name);

                return cmd.ExecuteReader();  // Metoden ExecuteReader køres på SQLDataReader objektet og repræsenterer den datastrøm, der er resultatet af database-forespørgslen
                                             // Ingen brug af using, da de lukker readeren ned, hvorved children ikke kan bruge reader-metoden
            }              
        }


        private SqlConnection sqlConnection = new SqlConnection(connectionString); // Instatiering af SqlConnection-klassen




        /// <summary>
        /// Dispose-metoden søger for, at alt lukkes ned korrekt
        /// </summary>
        public void Dispose()           
        {
            sqlConnection.Dispose(); // Instansen af SqlConnection sættes til at kalde Dispose-metoden, så Sql-connection lukkes korrekt ned.
                                     //  Som konsekvens overflødigøres brugen af "using" i forb med Sqlconnection  
        }

        
        
        
        /// <summary>
        /// Metode, der enten vedligeholder en åbning af - eller selv åbner -  en SqlConnection
        /// </summary>
        private SqlConnection GetOpenSqlConnection()  
        {
            if(sqlConnection.State != ConnectionState.Open) 
            {
                sqlConnection.Open();
            }

            return sqlConnection;
        }
    }
    
}


