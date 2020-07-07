namespace Definitiva.Shared.Domain.DomainValidator
{
    public interface IDomainSpecification<in T>
    {
        bool IsSatisfiedBy(T entity);
    }
}