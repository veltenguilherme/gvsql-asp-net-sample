using growganize;
using Persistence.Controllers.Base.CustomAttributes;
using Persistence.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace gvsql.Models
{
    [Table(TableMapper.partners)]
    public class Partner : Model<Partner>
    {
        [SqlType(SqlTypes.TEXT_NOT_NULL_UNIQUE)]
        public string NickName { get; set; } = string.Empty;

        [SqlType(SqlTypes.GUID)]
        [SqlJoin(TableMapper.persons)]
        public Guid? PersonFk { get; set; }

        [SqlJoin(TableMapper.partners)]
        public Person Person { get; set; } = new Person();
    }
}