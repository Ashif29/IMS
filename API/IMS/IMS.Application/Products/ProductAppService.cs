using AutoMapper;
using IMS.Application.Helpers;
using IMS.Application.Products.Dtos;
using IMS.Application.QueryData;
using IMS.Domain.Products;
using IMS.Domain.Products.Dtos;
using IMS.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.Products
{
    public class ProductAppService : IProductAppService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Product> _productRepository;
        private readonly IProductManager _productManager;

        public ProductAppService(
            IMapper mapper,
            IRepository<Product> productRepository,
            IProductManager productManager)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _productManager = productManager;
        }

        public async Task<PaginatedList<ProductDto>> GetAllAsync(ProductQueryDto queryDto)
        {
            var query = await _productRepository.GetQueryableAsync();

            query = ApplyFilters(query, queryDto.Name, queryDto.Code);

            query = Helper.Sort(query, queryDto.SortBy.ToString(), queryDto.SortOrder);

            var productDtos = query.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Code = p.Code,
                SalePrice = p.SalePrice,
            });

            var pagedProducts = await PaginatedList<ProductDto>.CreateAsync(
                productDtos,
                queryDto.PageNumber,
                queryDto.PageSize);

            return pagedProducts;
        }

        public async Task<ProductDto> GetAsync(int id)
        {
            var product = await _productRepository.FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> CreateAsync(ProductCreateUpdateDto productCreateUpdateDto)
        {
            var productDomainDto = _mapper.Map<ProductCreateUpdateDomainDto>(productCreateUpdateDto);

            var product = await _productManager.CreateAsync(productDomainDto);

            await _productRepository.InsertAsync(product);

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> UpdateAsync(int id, ProductCreateUpdateDto productCreateUpdateDto)
        {
            var productDomainDto = _mapper.Map<ProductCreateUpdateDomainDto>(productCreateUpdateDto);

            var updatedProduct = await _productManager.UpdateAsync(id, productDomainDto);

            await _productRepository.UpdateAsync(updatedProduct);

            return _mapper.Map<ProductDto>(updatedProduct);
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _productRepository.FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                throw new Exception("Product not found");
            }

            await _productRepository.DeleteAsync(product);
        }

        private IQueryable<Product> ApplyFilters(IQueryable<Product> query, string name, string code)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(p => EF.Functions.ILike(p.Name, $"%{name}%"));
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(p => EF.Functions.ILike(p.Name, $"%{name}%"));
            }
            return query;
        }
    }

}
