using gvsql.Models;
using gvsql.Tables;
using Persistence;

namespace gvsql
{
    public class DbContext
    {
        public DbContext(string databaseName) =>
            CreateDb(databaseName);

        private Database CreateDb(string name, int port = 5432, string user = "postgres", string pass = "postgres", string hostName = "127.0.0.1")
           => new(hostName, port, name, user, pass, GetSchema());

        private static List<Structure> GetSchema()
        {
            return new List<Structure>()
            {
                GetStructure(typeof(Person), typeof(Persons)),
                GetStructure(typeof(User), typeof(Users)),
                GetStructure(typeof(Customer), typeof(Customers)),
                GetStructure(typeof(Partner), typeof(Partners)),
                GetStructure(typeof(Sale), typeof(Sales)),
            };
        }

        private static Structure GetStructure(Type model, Type table) => new() { Model = model, Table = table };
    }
}