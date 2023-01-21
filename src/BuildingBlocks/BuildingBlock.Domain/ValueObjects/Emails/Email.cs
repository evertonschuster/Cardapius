using BuildingBlock.Domain.Entities;
using BuildingBlock.Domain.ValueObject.Email.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace BuildingBlock.Domain.ValueObjects.Emails
{
    public partial struct Email : IValueObject
    {
        private Email(string email)
        {
            Value = email ?? throw new ArgumentNullException(nameof(email));
        }

        public static string Empty { get => "meunome@email.com"; }

        private string Value { get; init; }

        public static Email Parse(string? email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new EmptyEmailException();
            }

            if (!email.Contains("@"))
            {
                throw new InvalidEmailException();
            }

            var emailProvider = email.Split("@")[1];
            if (string.IsNullOrEmpty(emailProvider))
            {
                throw new InvalidEmailException();
            }

            if (!email.Contains("."))
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
