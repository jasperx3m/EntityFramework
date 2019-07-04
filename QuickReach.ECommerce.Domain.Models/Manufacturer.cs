using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace QuickReach.ECommerce.Domain.Models
{
    public class Manufacturer : EntityBase
    {

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Description { get; set; }

        [Required]
        public bool IsActive { get; set; }
        public IEnumerable<ProductManufacturer> ProductManufacturers { get; set; }
        public Manufacturer()
        {
            this.ProductManufacturers = new List<ProductManufacturer>();
        }

        public void AddProduct(int productId)
        {

            var productManufacturer = new ProductManufacturer()
            {
                ManufacturerID = this.ID,
                ProductID = productId,
            };

            ((ICollection<ProductManufacturer>)this.ProductManufacturers).Add(productManufacturer);
        }

        public ProductManufacturer GetProduct(int productId)
        {
            return ((ICollection<ProductManufacturer>)this.ProductManufacturers)
                    .FirstOrDefault(pc => pc.ManufacturerID == this.ID &&
                               pc.ProductID == productId);
        }

        public void RemoveProduct(int productId)
        {
            var child = this.GetProduct(productId);

            ((ICollection<ProductManufacturer>)this.ProductManufacturers).Remove(child);
        }
    }
}
