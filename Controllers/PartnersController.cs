using Microsoft.AspNetCore.Mvc;
using Persistence.Controllers.Base.Queries;
using Sample.Models;
using Sample.Tables;

namespace Sample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartnersController : ControllerBase
    {
        private readonly Partners partner;

        public PartnersController(Partners partner) => this.partner = partner;

        [HttpGet("getAll")]
        public async Task<List<Partner>> GetAll() => await this.partner.ToListAsync();

        [HttpPost("updateOrInsert")]
        public async Task<Partner> UpdateOrInsert(Partner partner) => await this.partner.UpdateOrInsertAsync(partner);

        [HttpGet("getByFirstName")]
        public async Task<List<Partner>> GetByFirstName(string name) => await this.partner.ToListAsync(new Query<Partner>(x => x.Person.FirstName == name));

        [HttpDelete("Remove")]
        public async Task<int> RemoveByGuid(Guid guid) => await this.partner.RemoveAsync(new Partner() { Guid = guid });
    }
}