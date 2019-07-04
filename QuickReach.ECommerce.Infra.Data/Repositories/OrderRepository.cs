﻿using QuickReach.ECommerce.Domain;
using QuickReach.ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickReach.ECommerce.Infra.Data.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {

        public OrderRepository(ECommerceDbContext context) : base(context) //calls constructor of repositorybase
        {


        }
        public override Order Retrieve(int entityId)
        {
            var entity = this.context.Orders
                        .Where(c => c.ID == entityId)
                        .FirstOrDefault();
            return entity;


        }
        public IEnumerable<Order> Retrieve(string search = "", int skip = 0, int count = 10)
        {
            var result = this.context.Orders
                .Where(c => c.ID.ToString().Contains(search) ||
                c.Orders.ToString()
                .Contains(search))
                .Skip(skip)
                .Take(count)
                .ToList();

            return result;
        }
    }
}
