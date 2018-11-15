using System.Collections.Generic;
using System.Linq;
using Bootstrap.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bootstrap.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MyContext _myContext;

        public ProductRepository(MyContext myContext)
        {
            _myContext=myContext;
        }
        public IEnumerable<Material> GetMaterialsForProduct(int productId)
        {
            return _myContext.Materials.Where(x=>x.ProductId==productId).ToList();
        }

        public Material GetMaterialsForProduct(int productId, int materialId)
        {
            return _myContext.Materials.FirstOrDefault(x=>(x.ProductId==productId && x.Id==materialId));
        }

        public Product GetProduct(int productId, bool includeMaterials=false)
        {
            if(includeMaterials)
            {
                return _myContext.Products
                    .Include(x=>x.Materials).FirstOrDefault(x=>x.Id==productId);
            }
            return _myContext.Products.Find(productId);
        }

        public IEnumerable<Product> GetProducts()
        {
            return _myContext.Products.OrderBy(x=>x.Name).ToList();
        }

         public bool ProductExist(int productId)
        {
            return _myContext.Products.Any(x => x.Id == productId);
        }

         public void AddProduct(Product product)
        {
            _myContext.Products.Add(product);
        }

        public bool Save()
        {
            return _myContext.SaveChanges() >= 0;
        }

        public void DeleteProduct(Product product)
        {
            _myContext.Products.Remove(product);
        }
    }
}