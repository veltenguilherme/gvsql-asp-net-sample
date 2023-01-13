using Microsoft.AspNetCore.Mvc;
using Persistence.Controllers.Base.Queries;
using Sample.Models;
using Sample.Tables;

namespace Sample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly Sales sales;

        public SalesController(Sales sales) => this.sales = sales;

        [HttpGet("getAll")]
        public async Task<List<Sale>> GetAll() => await this.sales.ToListAsync();

        [HttpPost("updateOrInsert")]
        public async Task<Sale> UpdateOrInsert(Sale sale) => await this.sales.UpdateOrInsertAsync(sale);

        [HttpGet("getByFirstName")]
        public async Task<List<Sale>> GetByFirstName(string name) => await this.sales.ToListAsync(new Query<Sale>(x => x.Partner.Person.FirstName == name));

        [HttpDelete("Remove")]
        public async Task<int> RemoveByGuid(Guid guid) => await this.sales.RemoveAsync(new Sale() { Guid = guid });
    }
}