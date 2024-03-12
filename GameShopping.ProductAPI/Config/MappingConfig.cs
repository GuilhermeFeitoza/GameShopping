using AutoMapper;
using GameShopping.ProductAPI.Data.ValueObjects;
using GameShopping.ProductAPI.Model;

namespace GameShopping.ProductAPI.Config
{
    public class MappingConfig
    {



        public static MapperConfiguration RegisterMaps()
        {


            var mappingConfig = new MapperConfiguration(config => {

                config.CreateMap<ProductVO, Product>();
                config.CreateMap<Product, ProductVO>();
            
            
            }
            
            );


            return mappingConfig;
        }
    }
}
