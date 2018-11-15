using System.Collections.Generic;
using Bootstrap.Entities;

namespace Bootstrap.Repository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
        Product GetProduct(int productId,bool includeMaterials=false);
        IEnumerable<Material> GetMaterialsForProduct(int productId);
        Material GetMaterialsForProduct(int productId,int materialId);
        bool ProductExist(int productId);

        void AddProduct(Product product);
        bool Save();

        void DeleteProduct(Product product);
    }
}