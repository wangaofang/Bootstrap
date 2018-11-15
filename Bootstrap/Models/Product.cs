using System.Collections.Generic;

namespace Bootstrap.Models
{
    public class Product
    {
         public int Id { get; set; }
         public string  Name { get; set; }
         public decimal Price { get; set; }
         public string Description { get; set; }

         public ICollection<Material> Materials { get; set; }

    }
}