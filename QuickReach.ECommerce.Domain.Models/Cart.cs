using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickReach.ECommerce.Domain.Models
{
    public class Cart : EntityBase
    {
        public int BuyerId { get; set; }
        public List<CartItem> Items { get; set; }
        public Cart(int customerId)
        {
            BuyerId = customerId;
            Items = new List<CartItem>();
        }
        public Cart()
        {

        }
        public void AddItem(CartItem item)
        {
            ((ICollection<CartItem>)this.Items).Add(item);
        }

        public CartItem GetItem(int productId)
        {
            return ((ICollection<CartItem>)this.Items).FirstOrDefault(pc => pc.Id == this.ID &&
                               pc.ProductId == productId);
        }

        public void RemoveItem(int productId)
        {
            var item = this.GetItem(productId);

            ((ICollection<CartItem>)this.Items).Remove(item);
        }

    }
}
