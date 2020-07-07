using System.Collections.Generic;
using System.Linq;

namespace Uptec.Erp.Shared.Domain.ValueObjects
{
    public class Estado
    {
        public byte Codigo { get; private set; }
        public string NomeEstado { get; private set; }
        public string Sigla { get; private set; }
        public string Regiao { get; private set; }
        public decimal AliquotaIcms { get; private set; }
        public decimal AliquotaBaseCalculo { get; private set; }

        public static Estado NovoEstado(string sigla)
        {
            return Listagem().FirstOrDefault(e => e.Sigla == sigla.ToUpper());
        }

        protected Estado()
        {

        }

        public static IEnumerable<Estado> Listagem()
        {
            IEnumerable<Estado> retorno = new List<Estado>
            {
                new Estado() {Codigo = 12, NomeEstado = "Acre", Sigla = "AC", Regiao = "Norte", AliquotaIcms = 7m, AliquotaBaseCalculo = 100m},
                new Estado() {Codigo = 27, NomeEstado = "Alagoas", Sigla = "AL", Regiao = "Nordeste", AliquotaIcms = 7m, AliquotaBaseCalculo = 100m},
                new Estado() {Codigo = 16, NomeEstado = "Amapá", Sigla = "AP", Regiao = "Norte", AliquotaIcms = 7m, AliquotaBaseCalculo = 100m},
                new Estado() {Codigo = 13, NomeEstado = "Amazonas", Sigla = "AM", Regiao = "Norte", AliquotaIcms = 7m, AliquotaBaseCalculo = 100m},
                new Estado() {Codigo = 29, NomeEstado = "Bahia", Sigla = "BA", Regiao = "Nordeste", AliquotaIcms = 7m, AliquotaBaseCalculo = 100m},
                new Estado() {Codigo = 23, NomeEstado = "Ceará", Sigla = "CE", Regiao = "Nordeste", AliquotaIcms = 7m, AliquotaBaseCalculo = 100m},
                new Estado() {Codigo = 53, NomeEstado = "Distrito Federal", Sigla = "DF", Regiao = "Centro-Oeste", AliquotaIcms = 7m, AliquotaBaseCalculo = 100m},
                new Estado() {Codigo = 32, NomeEstado = "Espírito Santo", Sigla = "ES", Regiao = "Sudeste", AliquotaIcms = 7m, AliquotaBaseCalculo = 100m},
                new Estado() {Codigo = 52, NomeEstado = "Goiás", Sigla = "GO", Regiao = "Centro-Oeste", AliquotaIcms = 7m, AliquotaBaseCalculo = 100m},
                new Estado() {Codigo = 21, NomeEstado = "Maranhão", Sigla = "MA", Regiao = "Nordeste", AliquotaIcms = 7m, AliquotaBaseCalculo = 100m},
                new Estado() {Codigo = 51, NomeEstado = "Mato Grosso", Sigla = "MT", Regiao = "Centro-Oeste", AliquotaIcms = 7m, AliquotaBaseCalculo = 100m},
                new Estado() {Codigo = 50, NomeEstado = "Mato Grosso do Sul", Sigla = "MS", Regiao = "Centro-Oeste", AliquotaIcms = 7m, AliquotaBaseCalculo = 100m},
                new Estado() {Codigo = 31, NomeEstado = "Minas Gerais", Sigla = "MG", Regiao = "Sudeste", AliquotaIcms = 12m, AliquotaBaseCalculo = 100m},
                new Estado() {Codigo = 15, NomeEstado = "Pará", Sigla = "PA", Regiao = "Norte", AliquotaIcms = 7m, AliquotaBaseCalculo = 100m},
                new Estado() {Codigo = 25, NomeEstado = "Paraíba", Sigla = "PB", Regiao = "Nordeste", AliquotaIcms = 7m, AliquotaBaseCalculo = 100m},
                new Estado() {Codigo = 41, NomeEstado = "Paraná", Sigla = "PR", Regiao = "Sul", AliquotaIcms = 12m, AliquotaBaseCalculo = 100m},
                new Estado() {Codigo = 26, NomeEstado = "Pernambuco", Sigla = "PE", Regiao = "Nordeste", AliquotaIcms = 7m, AliquotaBaseCalculo = 100m},
                new Estado() {Codigo = 22, NomeEstado = "Piauí", Sigla = "PI", Regiao = "Nordeste", AliquotaIcms = 7m, AliquotaBaseCalculo = 100m},
                new Estado() {Codigo = 33, NomeEstado = "Rio de Janeiro", Sigla = "RJ", Regiao = "Sudeste", AliquotaIcms = 12m, AliquotaBaseCalculo = 100m},
                new Estado() {Codigo = 24, NomeEstado = "Rio Grande do Norte", Sigla = "RN", Regiao = "Nordeste", AliquotaIcms = 7m, AliquotaBaseCalculo = 100m},
                new Estado() {Codigo = 43, NomeEstado = "Rio Grande do Sul", Sigla = "RS", Regiao = "Sul", AliquotaIcms = 12m, AliquotaBaseCalculo = 100m},
                new Estado() {Codigo = 11, NomeEstado = "Rondônia", Sigla = "RO", Regiao = "Norte", AliquotaIcms = 7m, AliquotaBaseCalculo = 100m},
                new Estado() {Codigo = 14, NomeEstado = "Roraima", Sigla = "RR", Regiao = "Norte", AliquotaIcms = 7m, AliquotaBaseCalculo = 100m},
                new Estado() {Codigo = 42, NomeEstado = "Santa Catarina", Sigla = "SC", Regiao = "Sul", AliquotaIcms = 12m, AliquotaBaseCalculo = 100m},
                new Estado() {Codigo = 35, NomeEstado = "São Paulo", Sigla = "SP", Regiao = "Sudeste", AliquotaIcms = 18m, AliquotaBaseCalculo = 30m},
                new Estado() {Codigo = 28, NomeEstado = "Sergipe", Sigla = "SE", Regiao = "Nordeste", AliquotaIcms = 7m, AliquotaBaseCalculo = 100m},
                new Estado() {Codigo = 17, NomeEstado = "Tocantins", Sigla = "TO", Regiao = "Norte", AliquotaIcms = 7m, AliquotaBaseCalculo = 100m}
            };

            return retorno;
        }

        public const byte SiglaMaxLength = 2;
    }
}