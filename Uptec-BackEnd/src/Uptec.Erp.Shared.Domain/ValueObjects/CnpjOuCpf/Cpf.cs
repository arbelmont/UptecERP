using System;
using Definitiva.Shared.Infra.Support.Helpers;
using Uptec.Erp.Shared.Domain.Enums;
using Uptec.Erp.Shared.Domain.ValueObjects.CnpjOuCpf.Factories;

namespace Uptec.Erp.Shared.Domain.ValueObjects.CnpjOuCpf
{
    public class Cpf : ICnpjOuCpf
    {
        public const byte MaxLength = 11;
        public string Numero { get; private set; }
        public bool EhValido { get; private set; }
        public TipoPessoa TipoPessoa { get; } = TipoPessoa.Fisica;
        public string NumeroFormatado => ObterNumeroFormatado();
        public string NumeroInvalidoFormatado => "000.000.000-00";

        public Cpf(string cpf)
        {
            Numero = cpf;
            EhValido = Validar(Numero);
        }

        private string ObterNumeroFormatado()
        {
            EhValido = Validar(Numero);
            return EhValido ? Convert.ToUInt64(Numero).ToString(@"000\.000\.000\-00") : NumeroInvalidoFormatado;
        }

        private bool Validar(string numero)
        {
            var cpf = numero.GetOnlyNumbers().PadLeft(11, '0');

            if (cpf.FormedByTheSameCharacter())
                return false;

            var numbers = new byte[11];

            for (var i = 0; i < 11; i++)
                numbers[i] = byte.Parse(cpf[i].ToString());

            var amount = 0;
            for (var i = 0; i < 9; i++)
                amount += (10 - i) * numbers[i];

            var result = amount % 11;

            if (result == 1 || result == 0)
            {
                if (numbers[9] != 0)
                    return false;
            }
            else if (numbers[9] != 11 - result)
                return false;

            amount = 0;
            for (var i = 0; i < 10; i++)
                amount += (11 - i) * numbers[i];

            result = amount % 11;

            if (result == 1 || result == 0)
            {
                if (numbers[10] != 0)
                    return false;
            }
            else if (numbers[10] != 11 - result)
                return false;

            return true;
        }
    }
}
