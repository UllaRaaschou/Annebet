namespace WPFApp.Models
{
        public class Treatment : SalesItem // Nedarvning fra SalesItem
        {
            //privat constructor til brug for de 2 create-Metoder 
            private Treatment(int id, string type, string name, string description, decimal price)
                : base(id, type, name, description, price) { }                                                         
           
           
            /// Create-metode ud fra UI-input, dvs uden et id       
            public static Treatment CreateTreatmentFromUI(string type, string name, string description, decimal price)
            {
                Treatment treatment = new Treatment(0, type, name, description, price);
                return treatment;
            }


            /// Create-metode ud fra Db, dvs med et id 
            public static Treatment CreateTreatmentFromDb(int id, string type, string name, string description, decimal price)
            {
                Treatment treatment = new Treatment(id, type, name, description, price);
                return treatment;
            }                           
        }
}