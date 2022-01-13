using DeveloperTest.Business.Interfaces;
using DeveloperTest.Database;
using DeveloperTest.Database.Models;
using DeveloperTest.Models;
using System.Linq;

namespace DeveloperTest.Business
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext context;

        public CustomerService(ApplicationDbContext context) => this.context = context;

        public CustomerModel CreateCustomer(BaseCustomerModel model)
        {
            var addedCustomer = context.Customers.Add(new Customer
            {
                Name = model.Name,
                Type = model.Type
            });

            context.SaveChanges();

            return new CustomerModel
            {
                Id = addedCustomer.Entity.CustomerId,
                Name = addedCustomer.Entity.Name,
                Type = addedCustomer.Entity.Type
            };
        }

        public CustomerModel GetCustomer(int customerId) => context.Customers.Where(x => x.CustomerId == customerId).Select(x => new CustomerModel
        {
            Id = x.CustomerId,
            Name = x.Name,
            Type = x.Type
        }).SingleOrDefault();

        public CustomerModel[] GetCustomers() => context.Customers.Select(x => new CustomerModel
        {
            Id = x.CustomerId,
            Name = x.Name,
            Type = x.Type
        }).ToArray();
    }
}
