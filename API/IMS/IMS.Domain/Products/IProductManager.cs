using IMS.Domain.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.Products
{
    public interface IProductManager
    {
        Task<Product> CreateAsync(ProductCreateUpdateDomainDto domainDto);
        Task<Product> UpdateAsync(int id, ProductCreateUpdateDomainDto domainDto);
    }
}
