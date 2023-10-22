using growganize;
using Persistence.Controllers.Base.CustomAttributes;
using Persistence.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace gvsql.Models
{    
    [Table(TableMapper.persons)]
    public class Person : Model<Person>
    {        
        [SqlType(SqlTypes.TEXT_NOT_NULL)]
        public string FirstName { get; set; } = string.Empty;

        [SqlType(SqlTypes.TEXT_NOT_NULL)]
        public string LastName { get; set; } = string.Empty;

        [DefaultValue(Sex.MALE)]
        [SqlType(SqlTypes.INTEGER_NOT_NULL)]
        public Sex Sex { get; set; }

        [SqlType(SqlTypes.DATE_NOT_NULL)]
        public DateTime Birth { get; set; }

        [DefaultValue(1)]
        [SqlType(SqlTypes.INTEGER_NOT_NULL)]
        public int Age { get; set; }

        [SqlType(SqlTypes.DATE)]
        public DateTime? Death { get; set; }
    }

    public enum Sex
    {
        FEMALE = 1,
        MALE
    }
}