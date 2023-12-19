namespace WPFApp.Models
{
        public class Treatment : SalesItem // Nedarvning fra SalesItem
        {
            private Treatment(int id, string type, string name, string description, decimal price) :  //privat constructor til brug for de 2 create-Metoder 
            base(id, EnumCategory.Treatment, type, name, description, price) { }                                                                                                   // create-metoder
           

            
            /// <summary>
            /// Create-metode ud fra UI-input, dvs uden et id
            /// </summary>        
            public static Treatment CreateTreatmentFromUI(string type, string name, string description, decimal price)
            {
                Treatment treatment = new Treatment(0, type, name, description, price);
                return treatment;
            }




            /// <summary>
            /// Create-metode ud fra Db, dvs med et id
            /// </summary>    
            public static Treatment CreateTreatmentFromDb(int id, string type, string name, string description, decimal price)
            {
                Treatment treatment = new Treatment(id, type, name, description, price);
                return treatment;
            }
            
        

            
        }
}