﻿using QuickReach.ECommerce.Domain;
using QuickReach.ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickReach.ECommerce.Infra.Data.Repositories
{
    public class CartRepository : RepositoryBase<Cart>, ICartRepository
    {

        public CartRepository(ECommerceDbContext context) : base(context) //calls constructor of repositorybase
        {


        }
        public override Cart Retrieve(int entityId)
        {
            var entity = this.context.Carts
                        .Where(c => c.ID == entityId)
                        .FirstOrDefault();
            return entity;


        }
        public IEnumerable<Cart> Retrieve(string search = "", int skip = 0, int count = 10)
        {
            var result = this.context.Carts
                .Where(c => c.ID.ToString().Contains(search)||
                c.Items.ToString()
                .Contains(search))
                .Skip(skip)
                .Take(count)
                .ToList();

            return result;
        }
    }
}