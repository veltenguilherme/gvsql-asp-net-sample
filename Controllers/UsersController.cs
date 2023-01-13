using Microsoft.AspNetCore.Mvc;
using Persistence.Controllers.Base.Queries;
using Sample.Models;
using Sample.Tables;

namespace Sample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly Users users;

        public UsersController(Users users) => this.users = users;

        [HttpGet("getAll")]
        public async Task<List<User>> GetAll() => await this.users.ToListAsync();

        [HttpPost("updateOrInsert")]
        public async Task<User> UpdateOrInsert(User user) => await this.users.UpdateOrInsertAsync(user);

        [HttpGet("getByFirstName")]
        public async Task<List<User>> GetByFirstName(string name) => await this.users.ToListAsync(new Query<User>(x => x.Person.FirstName == name));

        [HttpDelete("Remove")]
        public async Task<int> RemoveByGuid(Guid guid) => await this.users.RemoveAsync(new User() { Guid = guid });
    }
}