using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QuickReach.ECommerce.Domain.Models
{
    public class CartItem : IValidatableObject
    {
        public string Id { get; set; }
        public string productId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal OldUnitPrice { get; set; }
        public int Quantity { get; set; }

        public string PictureUrl { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var result = new List<ValidationResult>();
            if (Quantity < 1)
            {
                result.Add(new ValidationResult("Invalid number of units"));
            }
            return result;
        }
    }
}
