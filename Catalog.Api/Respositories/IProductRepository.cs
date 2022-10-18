using Catalog.Api.Entities;

namespace Catalog.Api.Respositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> Getproduct(string id);
        Task<bool> Updateproduct(Product product);
        Task<bool> Deleteproduct(string id);
        Task CreateProduct(Product product);
    }
}
