using IMS.Domain.Products.Dtos;
using IMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.Products
{
    public class ProductManager : IProductManager
    {
        private readonly IRepository<Product> _productRepository;

        public ProductManager(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> CreateAsync(ProductCreateUpdateDomainDto domainDto)
        {
            Validate(domainDto);

            var product = new Product(domainDto.Name, domainDto.Code, domainDto.PurchasePrice, domainDto.SalePrice);

            return await Task.FromResult(product);
        }

        public async Task<Product> UpdateAsync(int id, ProductCreateUpdateDomainDto domainDto)
        {
            Validate(domainDto);

            var product = await _productRepository.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                throw new Exception($"Product with ID {id} not found.");
            }

            product.Update(domainDto.Name, domainDto.Code, domainDto.PurchasePrice, domainDto.SalePrice);

            return product;
        }

        private void Validate(ProductCreateUpdateDomainDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                throw new ArgumentException("Product name cannot be null or empty.", nameof(dto.Name));
            }

            if (string.IsNullOrWhiteSpace(dto.Code))
            {
                throw new ArgumentException("Product code cannot be null or empty.", nameof(dto.Code));
            }

            if(dto.PurchasePrice < 0)
            {
                throw new ArgumentException("Product price cannot be negative.", nameof(dto.PurchasePrice));
            }

            if (dto.SalePrice < 0)
            {
                throw new ArgumentException("Product price cannot be negative.", nameof(dto.SalePrice));
            }
        }
    }
}
