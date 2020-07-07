using System;
using Definitiva.Shared.Domain.Models;
using Definitiva.Shared.Domain.Validations;
using Uptec.Erp.Shared.Domain.Enums;
using Uptec.Erp.Shared.Domain.Models.Endereco.Validations;
using Uptec.Erp.Shared.Domain.ValueObjects;

namespace Uptec.Erp.Shared.Domain.Models.Endereco
{
    public abstract class Endereco : Entity<Endereco>
    {
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string Cep { get; private set; }
        public Cidade Cidade { get; private set; }
        public Estado Estado { get; private set; }
        public EnderecoTipo Tipo { get; private set; }
        public bool Obrigatorio { get; private set; }
        public decimal AliquotaIcms => GetAliquotaIcms();
        public decimal AliquotaBaseCalculo => GetAliquotaBaseCalculo();
        public string EnderecoCompleto => GetEnderecoCompleto();

        protected Endereco() { }

        protected Endereco(Guid id, string logradouro, string numero, string complemento, string bairro,
                           string cep, string cidade, string estado, EnderecoTipo tipo, bool obrigatorio = true)
        {
            Id = id;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cep = cep;
            Cidade = Cidade.NovaCidade(cidade);
            Estado = Estado.NovoEstado(estado);
            Tipo = tipo;
            Obrigatorio = obrigatorio;
        }

        public override bool IsValid()
        {
            Validation = new Validation(new EnderecoValidation(Obrigatorio).Validate(this));

            return Validation.IsValid();
        }

        private decimal GetAliquotaIcms()
        {
            return Estado.NovoEstado(Estado.Sigla).AliquotaIcms;
        }

        private decimal GetAliquotaBaseCalculo()
        {
            return Estado.NovoEstado(Estado.Sigla).AliquotaBaseCalculo;
        }

        private string GetEnderecoCompleto()
        {
            return $"{Logradouro}, {Numero} {Complemento} {Bairro} - Cep: {Cep} {Cidade.Nome} - {Estado.Sigla}";
        }

        public const byte LogradouroMaxLength = 200;
        public const byte NumeroMaxLength = 10;
        public const byte ComplementoMaxLength = 100;
        public const byte BairroMaxLength = 100;
        public const byte CepMaxLength = 8;
        public const byte EstadoLength = 2;
    }
}