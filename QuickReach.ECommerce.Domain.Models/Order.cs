using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickReach.ECommerce.Domain.Models
{
    public class Order : EntityBase
    {
        public int CartId { get; set; }
        public int BuyerId { get; set; }
        public List<OrderItem> Orders { get; set; }
        public Order(int customerId,int cartId)
        {
            CartId = cartId;
            BuyerId = customerId;
            Orders = new List<OrderItem>();
        }
        public Order()
        {

        }
        public void AddItem(OrderItem item)
        {
            ((ICollection<OrderItem>)this.Orders).Add(item);
        }

        public OrderItem GetItem(int productId)
        {
            return ((ICollection<OrderItem>)this.Orders).FirstOrDefault(pc => pc.Id == this.ID &&
                               pc.ProductId == productId);
        }

        public void RemoveItem(int productId)
        {
            var item = this.GetItem(productId);

            ((ICollection<OrderItem>)this.Orders).Remove(item);
        }

    }

}

