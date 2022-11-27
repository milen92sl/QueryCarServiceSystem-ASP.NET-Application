namespace QueryServiceSystem2.Data
{
    public class DataConstants
    {
        public class User
        {
            public const int FullNameMaxLength = 40;
            public const int FullNameMinLength = 5;
            public const int PasswordMaxLength = 20;
            public const int PasswordMinLength = 6;

        }
        public class Query
        {
            public const int DescriptionMinLength = 10;
        }

        public class Car
        {
            public const int NameMaxLength = 25;
        }

        public class Mechanic
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 25;
            public const int PhoneNumberMaxLength = 30;
            public const int PhoneNumberMinLength = 6;
        }

    }
}
