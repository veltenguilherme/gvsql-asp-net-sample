using Persistence.Controllers;
using Sample.Models;

namespace Sample.Tables
{
    public class Partners : Table<Partner>
    {
        public Partners(bool create = false, bool createView = false) : base(create, createView)
        {
        }
    }
}