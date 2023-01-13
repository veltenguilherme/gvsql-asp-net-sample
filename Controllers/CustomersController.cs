using Microsoft.AspNetCore.Mvc;
using Persistence.Controllers.Base.Queries;
using Sample.Models;
using Sample.Tables;

namespace Sample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly Customers customers;

        public CustomersController(Customers customers) => this.customers = customers;

        [HttpGet("getAll")]
        public async Task<List<Customer>> GetAll() => await this.customers.ToListAsync();

        [HttpPost("updateOrInsert")]
        public async Task<Customer> UpdateOrInsert(Customer customer) => await this.customers.UpdateOrInsertAsync(customer);

        [HttpGet("getByFirstName")]
        public async Task<List<Customer>> GetByFirstName(string name) => await this.customers.ToListAsync(new Query<Customer>(x => x.Person.FirstName == name));

        [HttpDelete("Remove")]
        public async Task<List<Customer>> Remove(Guid guid) => await this.customers.RemoveAsync(new Query<Customer>(x => x.Guid == guid).Obj);
    }
}