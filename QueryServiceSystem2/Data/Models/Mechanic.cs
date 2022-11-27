namespace QueryServiceSystem2.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.Mechanic;

    public class Mechanic
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Три имена на механик са задължителни")]
        [MaxLength(NameMaxLength, ErrorMessage = "Имената не могат да съдържат повече от {0} символа")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Работен код/ИД на механик е задължителен")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Телефонен номер е задължителен")]
        [MaxLength(PhoneNumberMaxLength, ErrorMessage = "Телефонният номер не може да съдържа повече от {0} цифри")]
        public string PhoneNumber { get; set; }

        [Required]
        public string UserId { get; set; }

        public IEnumerable<Query> Queries { get; init; } = new List<Query>();
    }
}
