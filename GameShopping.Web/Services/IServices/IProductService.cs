using GameShopping.Web.Models;

namespace GameShopping.Web.Services.IServices
{
    public interface IProductService
    {

        Task<IEnumerable<ProductModel>> GetAllProductsAsync(); 

        Task<ProductModel> GetProductByIdAsync(int id);

        Task<ProductModel>UpdateProduct(ProductModel product);

        Task<ProductModel> CreateProduct(ProductModel product);

        Task <bool> DeleteProductById(long id);

    }
}
