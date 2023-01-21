using BuildingBlock.Domain.Exceptions;
using BuildingBlock.Domain.Rules;
using System.Reflection;

namespace BuildingBlock.Domain.Entities
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S4035:Classes implementing \"IEquatable<T>\" should be sealed", Justification = "<Pendente>")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Bug", "S1206:\"Equals(Object)\" and \"GetHashCode()\" should be overridden in pairs", Justification = "<Pendente>")]
#pragma warning disable CS0659 // O tipo substitui Object. Equals (objeto o), mas não substitui o Object.GetHashCode()
    public abstract class ValueObject : IEquatable<ValueObject>, IValueObject
    {
        private List<PropertyInfo>? _properties;

        private List<FieldInfo>? _fields;

        public bool Equals(ValueObject? other)
        {
            return this.Equals(other as object);
        }

        public override bool Equals(object? obj)
        {
            if (obj is not ValueObject)
                return false;

            if (Object.ReferenceEquals(this, obj))
                return true;

            return GetProperties().All(p => PropertiesAreEqual(obj, p))
                && GetFields().All(f => FieldsAreEqual(obj, f));
        }

        private bool PropertiesAreEqual(object obj, PropertyInfo p)
        {
            return object.Equals(p.GetValue(this, null), p.GetValue(obj, null));
        }

        private bool FieldsAreEqual(object obj, FieldInfo f)
        {
            return object.Equals(f.GetValue(this), f.GetValue(obj));
        }

        private IEnumerable<PropertyInfo> GetProperties()
        {
            this._properties ??= GetType()
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .ToList();

            return this._properties;
        }

        private IEnumerable<FieldInfo> GetFields()
        {
            this._fields ??= GetType().GetFields(BindingFlags.Instance | BindingFlags.Public)
                    .ToList();

            return this._fields;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Marcar membros como estáticos", Justification = "<Pendente>")]
        protected void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
            {
                throw new BusinessRuleValidationException(rule);
            }
        }
    }
}
#pragma warning restore CS0659 // O tipo substitui Object. Equals (objeto o), mas não substitui o Object.GetHashCode()