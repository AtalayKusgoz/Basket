using DefineX.Services.Discount.Business.Interfaces;
using DefineX.Services.Discount.Entities.Concrete;
using Newtonsoft.Json;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefineX.Services.Discount.Business.Concrete
{
    public class RedisManager : IRedisDataService
    {
        private readonly RedisService _redisService;
        public RedisManager(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<Product> GetProduct(int productId)
        {
            var exist = await _redisService.GetDb().StringGetAsync(productId.ToString());
            if (String.IsNullOrEmpty(exist))
            {
                return null;
            }
            var data = JsonConvert.DeserializeObject<Product>(exist);
            return data;
        }
        public async Task<bool> SaveProduct(Product product)
        {
            var status = await _redisService.GetDb().StringSetAsync(product.Id.ToString(), JsonConvert.SerializeObject(product));
            return status;
        }
        public async Task<List<GetProductDto>> GetProducts()
        {
            var exist = await _redisService.GetDb().StringGetAsync("Products");
            if (String.IsNullOrEmpty(exist))
            {
                return null;
            }
            var data = JsonConvert.DeserializeObject<List<GetProductDto>>(exist);
            return data;
        }
        public async Task<bool> SaveProducts(List<GetProductDto> getProductDto)
        {
            var status = await _redisService.GetDb().StringSetAsync("Products", JsonConvert.SerializeObject(getProductDto));
            return status;
        }
    }
}
