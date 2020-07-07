using System;
using System.Collections.Generic;

namespace Uptec.Erp.Shared.Domain.Models.Cfop
{
    public static class CfopUptec
    {
        public static List<string> CfopsEntrada => GetCfopsEntrada();
        public static List<string> CfopsEntradaPeca => GetCfopsEntradaPeca();
        public static List<string> CfopsEntradaFornecedorPeca => GetCfopsEntradaFornecedorPeca();
        public static List<string> CfopsEntradaClientePeca => GetCfopsEntradaClientePeca();
        public static List<string> CfopsEntradaComponente => GetCfopsEntradaComponente();

        public static List<string> CfopsSaida => GetCfopsSaida();
        public static List<string> CfopsSaidaServico => GetCfopsSaidaServico();


        #region entrada
        private static List<string> GetCfopsEntradaPeca()
        {
            return new List<string> {"5901", "5949", "6901", "5924", "6924", "5920", "6920" };
        }

        private static List<string> GetCfopsEntradaFornecedorPeca()
        {
            return new List<string> { "5924", "6924" };
        }

        private static List<string> GetCfopsEntradaClientePeca()
        {
            return new List<string> { "5901", "5949", "6901" };
        }

        private static List<string> GetCfopsEntradaComponente()
        {
            //TODO: a confirmar com o Drezé quais os cfops de entrada de componentes
            return new List<string> { "5102" };
        }

        private static List<string> GetCfopsEntrada()
        {
            var cfops = new List<string>();

            GetCfopsEntradaPeca().ForEach(obj => cfops.Add(obj));
            GetCfopsEntradaComponente().ForEach(obj => cfops.Add(obj));

            return cfops;
        }
        #endregion

        #region saida
        private static List<string> GetCfopsSaida()
        {
            return new List<string> { "5902", "5124", "5925", "5125", "6902", "6124", "6125", "6925", "5921", "6921", "5903", "6903" };
        }

        private static List<string> GetCfopsSaidaServico()
        {
            return new List<string> { "5124", "5125", "6124" };
        }

        private static List<string> GetCfopsSaida5901()
        {
            return new List<string> { "5902", "5124" };
        }

        private static List<string> GetCfopsSaida5949()
        {
            return new List<string> { "5925", "5125" };
        }

        private static List<string> GetCfopsSaida6901()
        {
            return new List<string> { "6902", "6124" };
        }
        #endregion

        #region Entrada-Saida
        public static string GetCfopSaidaServico(string cfopEntrada, bool porCobertura = false)
        {
            switch (cfopEntrada)
            {
                case "5901": return "5124";
                case "5949": return "5125";
                case "6901": return porCobertura? "6125" : "6124";
                default: return "";
            }
        }

        public static string GetCfopSaidaRemessa(string cfopEntrada, bool porCobertura = false)
        {
            switch (cfopEntrada)
            {
                case "5901": return "5902";
                case "5949": return "5925";
                case "6901": return porCobertura? "6925" : "6902";
                case "5920": return "5921";
                case "6920": return "6921";
                default: return "";
            }
        }
        #endregion
    }
}
