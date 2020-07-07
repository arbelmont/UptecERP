using Uptec.Erp.Shared.Domain.Enums;

namespace Uptec.Erp.Shared.Domain.ValueObjects.CnpjOuCpf.Factories
{
    public interface ICnpjOuCpf
    {
        string Numero { get; }
        bool EhValido { get; }
        TipoPessoa TipoPessoa { get; }
        string NumeroFormatado { get; }
        string NumeroInvalidoFormatado { get; }
    }
}