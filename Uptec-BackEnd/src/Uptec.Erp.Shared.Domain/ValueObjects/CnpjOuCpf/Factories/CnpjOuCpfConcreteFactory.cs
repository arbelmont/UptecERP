using Definitiva.Shared.Infra.Support.Helpers;

namespace Uptec.Erp.Shared.Domain.ValueObjects.CnpjOuCpf.Factories
{
    public class CnpjOuCpfConcreteFactory : CnpjOuCpfFactory
    {
        public override ICnpjOuCpf ObterCnpjOuCpf(string numero)
        {
            var numeroSemMascara = numero.GetOnlyNumbers();

            if (numeroSemMascara.Length > 11)
                return new Cnpj(numeroSemMascara);
            else
                return new Cpf(numeroSemMascara);
        }
    }
}
