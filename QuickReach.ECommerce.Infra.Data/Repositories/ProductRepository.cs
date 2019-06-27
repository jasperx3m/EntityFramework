using Microsoft.EntityFrameworkCore;
using QuickReach.ECommerce.Domain;
using QuickReach.ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickReach.ECommerce.Infra.Data.Repositories
{
    public class ProductRepository : RepositoryBase <Product>, IProductRepository
    {
        public ProductRepository(ECommerceDbContext context) : base(context) //calls constructor of repositorybase
        {

        }
        public override Product Create(Product newEntity)
        {
            var categoryIsExisting = this.context
                .Categories.Find(newEntity.CategoryID);
            if (categoryIsExisting==null)
            {
                throw new System.Exception("There is no such category"); 
            }
            else
            {
                this.context.Products
                        .Add(newEntity);
                this.context.SaveChanges();
                return newEntity;

            }

        }
        public IEnumerable<Product> Retrieve(string search = "", int skip = 0, int count = 10)
        {
            var result = this.context.Products
                .Where(p => p.Name.Contains(search) ||
                            p.Description.Contains(search) 
                            )
                .Skip(skip)
                .Take(count)
                .ToList();

            return result;
        }
    }
}
