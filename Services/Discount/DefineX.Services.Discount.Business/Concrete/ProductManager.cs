using AutoMapper;
using DefineX.Services.Discount.Business.Interfaces;
using DefineX.Services.Discount.DataAccess.Interfaces;
using DefineX.Services.Discount.Entities.Concrete;
using Shared.ControllerBases;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefineX.Services.Discount.Business.Concrete
{
    public class ProductManager : GenericManager<Product>, IProductService
    {
        private readonly IProductDal _productDal;
        private readonly IMapper _mapper;
        private readonly IRedisDataService _redisDataService;
        public ProductManager(IProductDal productDal, IMapper mapper, IRedisDataService redisDataService) : base(productDal)
        {
            _productDal = productDal;
            _mapper = mapper;
            _redisDataService = redisDataService;
        }
        public async Task<Response<List<GetProductDto>>> GetProductAsync()
        {
            var redisData = await _redisDataService.GetProducts();
            if (redisData != null)
            {
                return Response<List<GetProductDto>>.Success(redisData, 200, redisData.Count);
            }
            var result = await _productDal.GetProductAsync();  
            return Response<List<GetProductDto>>.Success(_mapper.Map<List<GetProductDto>>(result), 200, result.Count);
        }

        public async Task<Response<ProductAddDto>> AddProductAsync(ProductAddDto productAddDto)
        {
            var result = await _productDal.AddProductAsync(_mapper.Map<Product>(productAddDto), productAddDto.Coupons);
            if (result != null)
            {
                await SaveRedis(result);
                await SaveRedis();
                return Response<ProductAddDto>.Success(productAddDto, 200, 1);
            }
            return Response<ProductAddDto>.Fail("Not Acceptable", 406);
        }

        public async Task<Response<ProductUpdateDto>> UpdateProductAsync(ProductUpdateDto productUpdateDto)
        {
            var result = await _productDal.UpdateProductAsync(_mapper.Map<Product>(productUpdateDto), productUpdateDto.Coupons);
            if (result != null)
            {
                await SaveRedis(result);
                await SaveRedis();
                return Response<ProductUpdateDto>.Success(productUpdateDto, 200, 1);
            }
            return Response<ProductUpdateDto>.Fail("Not Acceptable", 406);
        }
        private async Task SaveRedis()
        {
            await _redisDataService.SaveProducts(_mapper.Map<List<GetProductDto>>(await _productDal.GetProductAsync()));
        }
        private async Task SaveRedis(Product product)
        {
            await _redisDataService.SaveProduct(product);
        }
    }
}
