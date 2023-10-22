using growganize;
using Persistence.Controllers.Base.CustomAttributes;
using Persistence.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace gvsql.Models
{
    [Table(TableMapper.users)]
    public class User : Model<User>
    {
        [SqlType(SqlTypes.TEXT_NOT_NULL_UNIQUE)]
        public string NickName { get; set; } = string.Empty;

        [SqlType(SqlTypes.TEXT_NOT_NULL)]
        public string Password { get; set; } = string.Empty;

        [SqlType(SqlTypes.GUID)]
        [SqlJoin(TableMapper.persons)]
        public Guid? PersonFk { get; set; }

        [SqlJoin(TableMapper.users)]
        public Person Person { get; set; } = new Person();
    }
}