using QuickReach.ECommerce.Domain;
using QuickReach.ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickReach.ECommerce.Infra.Data.Repositories
{
    public class CustomerRepository : RepositoryBase<Customer> ,ICustomerRepository
    {
        public CustomerRepository(ECommerceDbContext context) : base(context) //calls constructor of repositorybase
        {

        }
        public IEnumerable<Customer> Retrieve(string search = "", int skip = 0, int count = 10)
        {
            var result = this.context.Customers
                .Where(p => p.FirstName.Contains(search) ||
                            p.LastName.Contains(search) ||
                            p.CardNumber.Contains(search) ||
                            p.SecurityNumber.Contains(search) ||
                            p.Expiration.Contains(search) ||
                            p.CardHolderName.Contains(search) ||
                            p.CardType.ToString().Contains(search) ||
                            p.Street.Contains(search) ||
                            p.City.Contains(search) ||
                            p.State.Contains(search) ||
                            p.Country.Contains(search) ||
                            p.ZipCode.Contains(search) 
                            )
                .Skip(skip)
                .Take(count)
                .ToList();

            return result;
        }
    }
}


