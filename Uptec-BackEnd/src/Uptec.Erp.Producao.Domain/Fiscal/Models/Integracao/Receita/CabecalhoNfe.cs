using Definitiva.Shared.Domain.Models;
using Definitiva.Shared.Domain.Validations;
using System;
using Uptec.Erp.Shared.Domain.Enums.NFe;
using Uptec.Erp.Shared.Domain.Enums.NFe.Processamento;

namespace Uptec.Erp.Producao.Domain.Fiscal.Models.Integracao.Receita
{
    public class CabecalhoNfe : Entity<CabecalhoNfe>
    {
        public string NomeEmitente { get; private set; }
        public string DocumentoEmitente { get; private set; }
        public string ChaveNfe { get; private set; }
        public decimal ValorTotal { get; private set; }
        public DateTime DataEmissao { get; private set; }
        public SituacaoNfe Situacao { get; private set; }
        public ManifestacaoStatus? ManifestacaoDestinatario { get; private set; }
        public string JustificativaManifestacao { get; private set; }
        public bool NfeCompleta { get; private set; }
        public TipoNfe TipoNfe { get; private set; }
        public int Versao { get; private set; }
        public string DigestValue { get; private set; }
        public int? NumeroCartaCorrecao { get; private set; }
        public string CartaCorrecao { get; private set; }
        public DateTime? DataCartaCorrecao { get; private set; }
        public DateTime? DataCancelamento { get; private set; }
        public string JustificativaCancelamento { get; private set; }
        public DateTime DataInclusao { get; private set; }
        public DateTime? DataManifestacao { get; private set; }
        public string Notificacao { get; set; }

        public CabecalhoNfe(string nomeEmitente, string documentoEmitente, string chaveNfe,
                            decimal valorTotal, DateTime dataEmissao, SituacaoNfe situacao,
                            ManifestacaoStatus? manifestacaoDestinatario, string justificativaManifestacao,
                            bool nfeCompleta, TipoNfe tipoNfe, int versao, string digestValue, 
                            int? numeroCartaCorrecao, string cartaCorrecao, DateTime? dataCartaCorrecao, 
                            DateTime? dataCancelamento, string justificativaCancelamento,
                            DateTime? dataManifestacao, string notificacao)
        {
            NomeEmitente = nomeEmitente;
            DocumentoEmitente = documentoEmitente;
            ChaveNfe = chaveNfe;
            ValorTotal = valorTotal;
            DataEmissao = dataEmissao;
            Situacao = situacao;
            ManifestacaoDestinatario = manifestacaoDestinatario;
            JustificativaManifestacao = justificativaManifestacao;
            NfeCompleta = nfeCompleta;
            TipoNfe = tipoNfe;
            Versao = versao;
            DigestValue = digestValue;
            NumeroCartaCorrecao = numeroCartaCorrecao;
            CartaCorrecao = cartaCorrecao;
            DataCartaCorrecao = dataCartaCorrecao;
            DataCancelamento = dataCancelamento;
            JustificativaCancelamento = justificativaCancelamento;
            DataManifestacao = dataManifestacao;
            Notificacao = notificacao;
            
            Validation = new Validation(new FluentValidation.Results.ValidationResult(), new FluentValidation.Results.ValidationResult());
        }

        public void SetId(Guid id)
        {
            Id = id;
        }

        public void SetDataInclusao(DateTime dataInclusao)
        {
            DataInclusao = dataInclusao;
        }

        public void SetManifestacaoDestinatario(ManifestacaoStatus? manifestacaoDestinatario)
        {
            ManifestacaoDestinatario = manifestacaoDestinatario;
        }

        public void SetJustificativaManifestacao(string justificativaManifestacao)
        {
            JustificativaManifestacao = justificativaManifestacao;
        }

        public override bool IsValid()
        {
            //Validation = new Validation(new NotaEntradaValidation().Validate(this), new NotaEntradaSystemValidation().Validate(this));
            //return Validation.IsValid();
            return true;
        }

        public const byte NomeEmitenteMaxLenght = 150;
        public const byte DocumentoEmitenteMaxLenght = 14;
        public const byte ChaveNfeLenght = 50;
        public const byte DigestValueMaxLenght = 50;
        public const byte CartaCorrecaoMaxLenght = 250;
        public const byte JustificativaCancelamentoMaxLenght = 250;
        public const byte JustificativaManifestacaoMinLenght = 15;
        public const byte JustificativaManifestacaoMaxLenght = 255;
    }
}
