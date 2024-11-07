using IMS.Application.Products.Dtos;
using IMS.Application.QueryData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.Products
{
    public interface IProductAppService
    {
        Task<PaginatedList<ProductDto>> GetAllAsync(ProductQueryDto queryDto);
        Task<ProductDto> GetAsync(int id);
        Task<ProductDto> CreateAsync(ProductCreateUpdateDto productCreateUpdateDto);
        Task<ProductDto> UpdateAsync(int id, ProductCreateUpdateDto productCreateUpdateDto);
        Task DeleteAsync(int id);
    }
}
