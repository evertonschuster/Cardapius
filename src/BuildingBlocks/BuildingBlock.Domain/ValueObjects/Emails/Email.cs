using BuildingBlock.Domain.ValueObject.Email.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace BuildingBlock.Domain.ValueObjects.Emails
{
    public readonly struct Email : IValueObject
    {
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

            if (!EmailValidator.IsValid(email))
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

        public bool IsValid()
        {
            return EmailValidator.IsValid(this.Value);
        }

        public static implicit operator string?(Email email)
        {
            return email.ToString();
        }

        public static bool operator ==(Email left, Email right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Email left, Email right)
        {
            return !(left == right);
        }
    }
}
