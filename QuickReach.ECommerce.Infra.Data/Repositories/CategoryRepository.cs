using QuickReach.ECommerce.Domain;
using QuickReach.ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using QuickReach.ECommerce.Infra.Data.Repositories;

namespace QuickReach.ECommerce.Infra.Data.Repository
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {

        public CategoryRepository(ECommerceDbContext context) : base(context) //calls constructor of repositorybase
        {

        }

        public IEnumerable<Category> Retrieve(string search = "", int skip = 0, int count = 10)
        {
            var result = this.context.Categories
                .Where(c => c.Name.Contains(search) ||
                            c.Description.Contains(search))
                .Skip(skip)
                .Take(count)
                .ToList();

            return result;
        }
    }
}