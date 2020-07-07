using System;
using Uptec.Erp.Shared.Domain.Enums;
using Definitiva.Shared.Infra.Support.Helpers;
using Uptec.Erp.Shared.Domain.ValueObjects.CnpjOuCpf.Factories;

namespace Uptec.Erp.Shared.Domain.ValueObjects.CnpjOuCpf
{
    public class Cnpj : ICnpjOuCpf
    {
        public const byte MaxLength = 14;
        public string Numero { get; private set; }
        public bool EhValido { get; private set; }
        public TipoPessoa TipoPessoa { get; } = TipoPessoa.Juridica;
        public string NumeroFormatado => ObterNumeroFormatado();
        public string NumeroFormatadoSemValidacao => ObterNumeroFormatadoSemValidacao();
        public string NumeroInvalidoFormatado => "00.000.000/0000-00";

        public Cnpj(string cnpj)
        {
            Numero = ObterNumeroAjustado(cnpj);
            EhValido = Validar(Numero);
        }

        protected Cnpj() { }

        private string ObterNumeroFormatado()
        {
            EhValido = Validar(Numero);
            return EhValido ? Convert.ToUInt64(Numero).ToString(@"00\.000\.000\/0000\-00") : NumeroInvalidoFormatado;
        }

        private string ObterNumeroFormatadoSemValidacao()
        {
            return Convert.ToUInt64(Numero).ToString(@"00\.000\.000\/0000\-00");
        }

        private bool Validar(string numero)
        {
            var cnpj = ObterNumeroAjustado(numero);

            if (cnpj.Length != 14)
                return false;

            if (cnpj.FormedByTheSameCharacter())
                return false;

            var tempCnpj = cnpj.Substring(0, 12);
            var amount = 0;
            for (var i = 0; i < 12; i++)
                amount += int.Parse(tempCnpj[i].ToString()) * multiplier1[i];

            var rest = (amount % 11);
            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            var digit = rest.ToString();
            tempCnpj = tempCnpj + digit;
            amount = 0;
            for (var i = 0; i < 13; i++)
                amount += int.Parse(tempCnpj[i].ToString()) * multiplier2[i];

            rest = (amount % 11);
            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            digit = digit + rest.ToString();

            return cnpj.EndsWith(digit);
        }

        private string ObterNumeroAjustado(string numero)
        {
            var retorno = numero.ReplaceNull().GetOnlyNumbers().PadLeft(14, '0');

            if (numero.Length == 15 && numero.EndsWith("0"))
                return numero.Right(14);

            return retorno;
        }

        static readonly int[] multiplier1 = new[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        static readonly int[] multiplier2 = new[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
    }
}
