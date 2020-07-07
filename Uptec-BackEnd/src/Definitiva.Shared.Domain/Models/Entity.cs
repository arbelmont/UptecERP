using Definitiva.Shared.Domain.Validations;
using System;

namespace Definitiva.Shared.Domain.Models
{
    public abstract class Entity<T> where T: Entity<T>
    {
        public Guid Id { get; protected set; }

        public Validation Validation { get; protected set; }

        public bool Deleted { get; protected set; }

        public abstract bool IsValid();

        public virtual void Delete()
        {
            Deleted = true;
        }

        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity<T>;

            if (ReferenceEquals(this, compareTo)) return true;
            if (compareTo is null) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity<T> a, Entity<T> b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity<T> a, Entity<T> b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 997) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{GetType().Name} [Id: {Id}]";
        }
    }
}
