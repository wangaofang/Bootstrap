using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Bootstrap.Entities;

namespace Bootstrap.Dtos
{
    public class ProductDto
    {
        public ProductDto()
        {
            Materials=new List<MaterialDto>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public ICollection<MaterialDto> Materials { get; set; }

        public int MaterialCount=>Materials.Count;
    }
}