using BuildingBlock.Domain.ValueObject.Email.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace BuildingBlock.Domain.ValueObjects.Emails
{
    public partial struct Email : IValueObject
    {
        private const string ProviderSeparator = "@";
        private const string WhiteSpace = " ";
        private const string Dot = ".";

        public static string Empty { get => "meunome@email.com"; }

        private Email(string email)
        {
            Value = email ?? throw new ArgumentNullException(nameof(email));
        }

        public string Value { get; init; }


        public static Email Parse(string? email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new EmptyEmailException();
            }

            if (!email.Contains(Email.ProviderSeparator) || email.Contains(Email.WhiteSpace))
            {
                throw new InvalidEmailException();
            }

            var emailProvider = email.Split(Email.ProviderSeparator)[1];
            if (string.IsNullOrEmpty(emailProvider))
            {
                throw new InvalidEmailException();
            }

            if (!email.Contains(Email.Dot))
            {
                throw new InvalidEmailException();
            }

            return new Email(email);
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string? ToString()
        {
            return this.Value;
        }

        public static implicit operator string?(Email email)
        {
            return email.ToString();
        }
    }
}
