using Definitiva.Shared.Domain.Models;
using Definitiva.Shared.Domain.Validations;
using System;
using Uptec.Erp.Producao.Domain.Arquivos.Validations;

namespace Uptec.Erp.Producao.Domain.Arquivos.Models
{
    public class Arquivo : Entity<Arquivo>
    {
        public string Nome { get; private set; }
        public int Tamanho { get; private set; }
        public string Tipo { get; private set; }
        public string Dados { get; private set; }
        public DateTime DataGravacao { get; private set; }

        protected Arquivo()
        {
        }

        public Arquivo(Guid id, string nome, int tamanho, string tipo, string dados)
        {
            Nome = nome;
            Tamanho = tamanho;
            Tipo = tipo;
            Dados = dados;
            Id = id;
        }

        public void SetId()
        {
            Id = Guid.NewGuid();
        }

        public void SetDataGravacao(DateTime dataGravacao)
        {
            DataGravacao = dataGravacao;
        }

        public override bool IsValid()
        {
            Validation = new Validation(new ArquivoValidation().Validate(this), new ArquivoSystemValidation().Validate(this));

            return Validation.IsValid();
        }

        public const byte NumeroNotaMaxLenght = byte.MaxValue;
        public const byte TipoArquivoMaxLenght = 50;
    }
}