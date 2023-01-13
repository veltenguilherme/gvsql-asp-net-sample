using Persistence.Controllers;
using Sample.Models;

namespace Sample.Tables
{
    public class Users : Table<User>
    {
        public Users(bool create = false, bool createView = false) : base(create, createView)
        {
        }
    }
}