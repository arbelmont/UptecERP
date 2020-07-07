
namespace Uptec.Erp.Shared.Domain.Models.Impostos
{
    public class AliquotaImpostos
    {
        public decimal BaseCalculo { get; private set; }
        public decimal Ipi { get; private set; }
        public decimal Icms { get; private set; }
        public decimal Pis { get; private set; }
        public decimal Cofins { get; private set; }

        public AliquotaImpostos(decimal baseCalculo,decimal ipi, decimal icms, decimal pis, decimal cofins)
        {
            BaseCalculo = baseCalculo;
            Ipi = ipi;
            Icms = icms;
            Pis = pis;
            Cofins = cofins;
        }
    }
}
