using Persistence.Controllers;
using Sample.Models;

namespace Sample.Tables
{
    public class Sales : Table<Sale>
    {
        public Sales(bool create = false, bool createView = false) : base(create, createView)
        {
        }
    }
}