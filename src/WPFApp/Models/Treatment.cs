namespace WPFApp.Models
{
        public class Treatment : SalesItem
        {
            private Treatment(int id, string type, string name, string description, decimal price) //privat constructor til brug for de 2
                                                                                                    // create-metoder
            {
                Id = id;
                Type = type;
                Name = name;
                Description = description;
                Price = price;
            }

            public int Id { get; }
            public string Type { get; }
            public string Name { get; }
            public string Description { get; }
            public decimal Price { get; }

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