using Microsoft.AspNetCore.Mvc;
using Persistence.Controllers.Base.Queries;
using Sample.Models;
using Sample.Tables;

namespace Sample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly Persons persons;

        public PersonsController(Persons persons) => this.persons = persons;

        [HttpGet("getAll")]
        public async Task<List<Person>> GetAll() => await this.persons.ToListAsync();

        [HttpPost("updateOrInsert")]
        public async Task<Person> UpdateOrInsert(Person person) => await this.persons.UpdateOrInsertAsync(person);

        [HttpGet("getByFirstName")]
        public async Task<List<Person>> GetByFirstName(string name) => await this.persons.ToListAsync(new Query<Person>(x => x.FirstName == name));

        [HttpDelete("Remove")]
        public async Task<int> RemoveByGuid(Guid guid) => await this.persons.RemoveAsync(new Person() { Guid = guid });
    }
}