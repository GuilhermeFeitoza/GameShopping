using GameShopping.Web.Models;
using GameShopping.Web.Services.IServices;
using GameShopping.Web.Utils;

namespace GameShopping.Web.Services
{
    public class ProductService : IProductService
    {


        private readonly HttpClient _httpClient;
        public const string BasePath = "api/v1/product";
        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient; 
            _httpClient.BaseAddress = new Uri("http://localhost:5010");
        }
        public async Task<IEnumerable<ProductModel>> GetAllProductsAsync()
        {
           var response = await _httpClient.GetAsync(BasePath);
            return await response.ReadContentAs<List<ProductModel>>();
        }

        public async Task<ProductModel> GetProductByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{BasePath}/{id}");
            return await response.ReadContentAs<ProductModel>();
        }
        public async Task<ProductModel> CreateProduct(ProductModel product)
        {
            var response = await _httpClient.PostAsJson(BasePath, product);
            if (response.IsSuccessStatusCode) { 
            return await response.ReadContentAs<ProductModel>();
            }
            throw new Exception("Something went wrong while caliing API");
        }

        public async Task<bool> DeleteProductById(long id)
        {

            var response = await _httpClient.DeleteAsync(BasePath);
            if (response.IsSuccessStatusCode)
            {
                return await response.ReadContentAs<
                    bool>();
            }
            throw new Exception("Something went wrong while caliing API");
        }


        public async Task<ProductModel> UpdateProduct(ProductModel product)
        {
            var response = await _httpClient.PutAsJsonAsync(BasePath, product);
            if (response.IsSuccessStatusCode)
            {
                return await response.ReadContentAs<ProductModel>();
            }
            throw new Exception("Something went wrong while caliing API");
        }
    }
}
