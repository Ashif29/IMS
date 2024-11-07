using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.Enums.SortingParameters
{
    public enum ProductSortBy
    {
        //name must be matched to the database column name.
        Name = 1,
        SalePrice = 2,
        PurchasePrice = 3,
    }
}
