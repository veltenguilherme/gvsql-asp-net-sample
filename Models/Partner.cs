using Persistence.Controllers.Base.CustomAttributes;
using Persistence.Models;
using Sample.Tables;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sample.Models
{
    [Table("partner")]
    public class Partner : Model<Partner>
    {
        [DefaultValue("partner_001")]
        [Column("nick_name")]
        [SqlType(SqlTypes.TEXT_NOT_NULL_UNIQUE)]
        public string? NickName { get; set; }

        [Column("person_fk")]
        [SqlFk("person", "uuid", SqlFkTypes.ON_DELETE_CASCADE_ON_UPDATE_NO_ACTION_NOT_NULL)]
        [SqlType(SqlTypes.GUID)]
        public Guid? PersonGuid { get; set; }

        [SqlJoinType("partner", "person_fk", SqlJoinTypes.INNER)]
        public Person Person { get; set; } = new Person();
    }
}