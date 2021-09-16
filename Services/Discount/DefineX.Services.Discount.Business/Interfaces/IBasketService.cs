using DefineX.Services.Discount.DataAccess.Concrete.Models;
using Shared.ControllerBases;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefineX.Services.Discount.Business.Interfaces
{
    public interface IBasketService
    {
        Task<Response<List<BasketItemDto>>> BasketOperations(List<BasketDto> basketDtoList);
    }
}
