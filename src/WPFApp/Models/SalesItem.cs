using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFApp.Models
{
    public abstract class SalesItem
    {             

        public int Id{ get;  }
        public string Type { get; }
        public string Name { get; }
        public decimal Price { get; }
        public string Description { get; }  

       
    }
}
