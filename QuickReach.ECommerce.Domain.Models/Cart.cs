using System;
using System.Collections.Generic;
using System.Text;

namespace QuickReach.ECommerce.Domain.Models
{
    public class Cart : EntityBase
    {
        public string BuyerId { get; set; }
        public List<CartItem> Items { get; set; }
        public Cart(string customerId)
        {
            BuyerId = customerId;
            Items = new List<CartItem>();
        }

    }
}
