using growganize;
using Persistence.Controllers.Base.CustomAttributes;
using Persistence.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace gvsql.Models
{
    [Table(TableMapper.sales)]
    public class Sale : Model<Sale>
    {
        [SqlType(SqlTypes.INTEGER_NOT_NULL_UNIQUE)]
        public int Code { get; set; }

        [SqlType(SqlTypes.GUID)]
        [SqlJoin(TableMapper.users)]
        public Guid? UserFk { get; set; }

        [SqlJoin(TableMapper.sales)]
        public User User { get; set; } = new User();

        [SqlType(SqlTypes.GUID)]
        [SqlJoin(TableMapper.customers)]
        public Guid? CustomerFk { get; set; }

        [SqlJoin(TableMapper.sales)]
        public Customer Customer { get; set; } = new Customer();

        [SqlType(SqlTypes.GUID)]
        [SqlJoin(TableMapper.partners)]
        public Guid? PartnerFk { get; set; }

        [SqlJoin(TableMapper.sales)]
        public Partner Partner { get; set; } = new Partner();
    }
}