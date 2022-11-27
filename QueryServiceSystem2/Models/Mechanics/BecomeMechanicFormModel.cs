namespace QueryServiceSystem2.Models.Mechanics
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Mechanic;

    public class BecomeMechanicFormModel
    {
        [Required(ErrorMessage = "Три имена на механик са задължителни")]
        [MaxLength(NameMaxLength, ErrorMessage = "Имената не могат да съдържат повече от {0} символа")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Работен код/ИД на механик е задължителен")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Телефонен номер е задължителен")]
        [MaxLength(PhoneNumberMaxLength, ErrorMessage = "Телефонният номер не може да съдържа повече от {0} цифри")]
        public string PhoneNumber { get; set; }
    }
}
