using Persistence.Controllers.Base.CustomAttributes;
using Persistence.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sample.Models
{
    [Table("person")]
    public class Person : Model<Person>
    {
        [DefaultValue("person_001")] //swagger
        [Column("first_name")]
        [SqlType(SqlTypes.TEXT_NOT_NULL)]
        public string? FirstName { get; set; }

        [DefaultValue("Ronaldo")]
        [Column("last_name")]
        [SqlType(SqlTypes.TEXT_NOT_NULL)]
        public string? LastName { get; set; }

        [DefaultValue(Sex.MALE)]
        [Column("sex")]
        [SqlType(SqlTypes.INTEGER_NOT_NULL)]
        public Sex Sex { get; set; }

        [Column("birth")]
        [SqlType(SqlTypes.DATE_NOT_NULL)]
        public DateTime Birth { get; set; }

        [DefaultValue(1)]
        [Column("age")]
        [SqlType(SqlTypes.INTEGER_NOT_NULL)]
        public int Age { get; set; }

        [Column("death")]
        [SqlType(SqlTypes.DATE)]
        public DateTime? Death { get; set; }
    }

    public enum Sex
    {
        FEMALE = 1,
        MALE
    }
}