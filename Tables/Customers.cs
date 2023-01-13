using Persistence.Controllers;
using Sample.Models;

namespace Sample.Tables
{
    public class Customers : Table<Customer>
    {
        public Customers(bool create = false, bool createView = false) : base(create, createView)
        {
        }
    }
}