using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.Products
{
    public class Product
    {
        public int Id { get; private set; }
        [Required]
        public string Name { get; private set; }
        [Required]
        public string Code { get; private set; }
        public decimal PurchasePrice { get; private set; }
        public decimal SalePrice { get; private set; }

        private Product() { }

        internal Product(string name, string code, decimal purchasePrice, decimal salePrice)
        {
            Validate(name, code, purchasePrice, salePrice);

            Name = name;
            Code = code;
            PurchasePrice = purchasePrice;
            SalePrice = salePrice;
        }

        internal void Update(string name, string code, decimal purchasePrice, decimal salePrice)
        {
            Validate(name, code, purchasePrice, salePrice);

            Name = name;
            Code = code;
            PurchasePrice = purchasePrice;
            SalePrice = salePrice;
        }

        private void Validate(string name, string code, decimal purchasePrice, decimal salePrice)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            }

            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentException("Code cannot be null or empty.", nameof(code));
            }

            if (purchasePrice < 0)
            {
                throw new ArgumentException("Purchase price cannot be negative.", nameof(purchasePrice));
            }

            if (salePrice < 0)
            {
                throw new ArgumentException("Sale price cannot be negative.", nameof(salePrice));
            }
        }
    }
}
