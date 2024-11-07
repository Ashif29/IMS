using IMS.Domain.Enums.SortingParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.Products.Dtos
{
    public class ProductQueryDto
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public string? Name { get; set; }
        public string? Code { get; set; }
        public SortOrder SortOrder { get; set; } = SortOrder.ASC;
        public ProductSortBy SortBy { get; set; } = ProductSortBy.Name;
    }
}