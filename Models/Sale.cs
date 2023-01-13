using Persistence.Controllers.Base.CustomAttributes;
using Persistence.Models;
using Sample.Tables;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sample.Models
{
    [Table("sales")]
    public class Sale : Model<Sale>
    {
        [Column("code")]
        [SqlType(SqlTypes.INTEGER_NOT_NULL)]
        public int Code { get; set; }

        [Column("user_fk")]
        [SqlFk("usr", "uuid", SqlFkTypes.ON_DELETE_CASCADE_ON_UPDATE_NO_ACTION_NOT_NULL)]
        [SqlType(SqlTypes.GUID)]
        public Guid? UserGuid { get; set; }

        [SqlJoinType("sales", "user_fk", SqlJoinTypes.INNER)]
        public User User { get; set; } = new User();

        [Column("customer_fk")]
        [SqlFk("customer", "uuid", SqlFkTypes.ON_DELETE_CASCADE_ON_UPDATE_NO_ACTION_NOT_NULL)]
        [SqlType(SqlTypes.GUID)]
        public Guid? CustomerGuid { get; set; }

        [SqlJoinType("sales", "customer_fk", SqlJoinTypes.INNER)]
        public Customer Customer { get; set; } = new Customer();

        [Column("partner_fk")]
        [SqlFk("partner", "uuid", SqlFkTypes.ON_DELETE_CASCADE_ON_UPDATE_NO_ACTION)]
        [SqlType(SqlTypes.GUID)]
        public Guid? PartnerGuid { get; set; }

        [SqlJoinType("sales", "partner_fk", SqlJoinTypes.LEFT)]
        public Partner Partner { get; set; } = new Partner();
    }
}