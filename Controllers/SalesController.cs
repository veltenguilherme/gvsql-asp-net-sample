using gvsql.Models;
using gvsql.Tables;
using Microsoft.AspNetCore.Mvc;
using Persistence.Controllers.Base.Queries;

namespace Sample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly Sales sales;
        private readonly Users users;
        private readonly Customers customers;
        private readonly Partners partners;
        private readonly Persons persons;

        public SalesController(Sales sales, Users user, Customers customer, Partners partner, Persons persons)
        {
            this.sales = sales;
            this.users = user;
            this.customers = customer;
            this.partners = partner;
            this.persons = persons;
        }

        [HttpGet("getAll")]
        public async Task<List<Sale>> GetAll() => await sales.ToListAsync();

        [HttpPost("updateOrInsert")]
        public async Task<Sale> UpdateOrInsert() => await sales.UpdateOrInsertAsync(InsertSale().Result);

        [HttpGet("getByCustomerFirstName")]
        public async Task<List<Sale>> GetByCustomerFirstName(string name) => await sales.ToListAsync(new Query<Sale>(x => x.Customer.Person.FirstName == name));

        [HttpGet("getByCode")]
        public async Task<List<Sale>> GetByCode(int code) => await sales.ToListAsync(new Query<Sale>(x => x.Code == code));

        [HttpDelete("Remove")]
        public async Task<int> RemoveByGuid(Guid guid) => await sales.RemoveAsync(new Sale() { Guid = guid });

        private async Task<Sale> InsertSale() =>
                await sales.UpdateOrInsertAsync(new Sale()
                {
                    Code = 1,
                    UserFk = InsertUser().Result?.Guid,
                    CustomerFk = InsertCustomer().Result?.Guid,
                    PartnerFk = InsertPartner().Result?.Guid
                });

        private async Task<Person> InsertPerson()
        {
            return await persons.UpdateOrInsertAsync(new Person()
            {
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString(),
                Birth = new DateTime(1993, 09, 14),
                Age = DateTime.Now.Year - new DateTime(1993, 09, 14).Year,
                Sex = Sex.MALE
            });
        }

        private async Task<User> InsertUser()
        {
            return await users.UpdateOrInsertAsync(new User()
            {
                NickName = Guid.NewGuid().ToString(),
                Password = "1",
                PersonFk = InsertPerson().Result?.Guid
            });
        }

        private async Task<Customer> InsertCustomer()
        {
            return await customers.UpdateOrInsertAsync(new Customer()
            {
                NickName = Guid.NewGuid().ToString(),
                PersonFk = InsertPerson().Result?.Guid
            });
        }

        private async Task<Partner> InsertPartner()
        {
            return await partners.UpdateOrInsertAsync(new Partner()
            {
                NickName = Guid.NewGuid().ToString(),
                PersonFk = InsertPerson().Result?.Guid
            });
        }
    }
}