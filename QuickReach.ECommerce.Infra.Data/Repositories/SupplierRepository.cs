using QuickReach.ECommerce.Domain.Models;
using QuickReach.ECommerce.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickReach.ECommerce.Infra.Data.Repositories
{
    public class SupplierRepository : RepositoryBase<Supplier>, ISupplierRepository
    {
        public SupplierRepository(ECommerceDbContext context) : base(context)
        {

        }
        public IEnumerable<Supplier> Retrieve(string search="", int skip=0,int count = 10)
        {
            var result = this.context.Suppliers
                    .Where(s => s.Name.Contains(search) ||
                            s.Description.Contains(search))
                    .Skip(skip)
                    .Take(count)
                    .ToList();

            return result;
        }
    }
}
