namespace Uptec.Erp.Shared.Domain.Specifications
{
    public class ValorMaiorqueZeroSpecification
    {
        public bool IsSatisfiedBy(int valor)
        {
            return valor > 0;
        }

        public bool IsSatisfiedBy(decimal valor)
        {
            return valor > 0;
        }
    }
}
