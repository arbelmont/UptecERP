using Uptec.Erp.Shared.Domain.Enums;
using Uptec.Erp.Shared.Domain.ValueObjects.CnpjOuCpf.Factories;

namespace Uptec.Erp.Shared.Domain.ValueObjects.CnpjOuCpf
{
    public class CnpjOuCpf : ICnpjOuCpf
    {
        public string Numero => _documento.Numero;
        public bool EhValido => _documento.EhValido;
        public TipoPessoa TipoPessoa => _documento.TipoPessoa;
        public string NumeroFormatado => _documento.NumeroFormatado;
        public string NumeroInvalidoFormatado => _documento.NumeroInvalidoFormatado;

        ICnpjOuCpf _documento = null;
        public CnpjOuCpf(string numero)
        {
            var cnpjOuCpfConcreteFactory = new CnpjOuCpfConcreteFactory();
            _documento = cnpjOuCpfConcreteFactory.ObterCnpjOuCpf(numero);
        }
    }
}
