using System;
using System.Collections.Generic;
using System.Linq;
using Uptec.Erp.Shared.Domain.Enums;

namespace Uptec.Erp.Api.ViewModels.Shared
{
    public class EnumViewModel
    {
        public int Value { get; set; }
        public string Name { get; set; }

        public static List<EnumViewModel> TelefoneTipo => GetTelefoneTipo();
        public static List<EnumViewModel> EnderecoTipo => GetEnderecoTipo();
        public static List<EnumViewModel> UnidadeMedida => GetUnidadeMedida();
        public static List<EnumViewModel> TipoEmissor => GetTipoEmissor();
        public static List<EnumViewModel> StatusNfEntrada => GetStatusNfEntrada();

        public static List<EnumViewModel> TransportadoraTipoEntregaPadrao => GetTransportadoraTipoEntregaPadrao();

        private static List<EnumViewModel> GetTelefoneTipo()
        {
            var result = ((TelefoneTipo[])Enum.GetValues(typeof(TelefoneTipo))).Select(c => new EnumViewModel { Value = (int)c, Name = c.ToString() }).ToList();
            result.Insert(0, new EnumViewModel { Value = 0, Name = "Selecione..." });

            return result;
        }

        private static List<EnumViewModel> GetEnderecoTipo()
        {
            var result = ((EnderecoTipo[])Enum.GetValues(typeof(EnderecoTipo))).Select(c => new EnumViewModel { Value = (int)c, Name = c.ToString() }).ToList();
            result.Insert(0, new EnumViewModel { Value = 0, Name = "Selecione..." });

            return result;
        }

        private static List<EnumViewModel> GetTransportadoraTipoEntregaPadrao()
        {
            var result = ((TransportadoraTipoEntregaPadrao[])Enum.GetValues(typeof(TransportadoraTipoEntregaPadrao))).Select(c => new EnumViewModel { Value = (int)c, Name = c.ToString() }).ToList();
            result.Insert(0, new EnumViewModel { Value = 0, Name = "Selecione..." });

            return result;
        }

        private static List<EnumViewModel> GetUnidadeMedida()
        {
            var result = ((UnidadeMedida[])Enum.GetValues(typeof(UnidadeMedida))).Select(c => new EnumViewModel { Value = (int)c, Name = c.ToString() }).ToList();
            result.Insert(0, new EnumViewModel { Value = 0, Name = "Selecione..." });

            return result;
        }

        private static List<EnumViewModel> GetTipoEmissor()
        {
            var result = ((TipoEmissor[])Enum.GetValues(typeof(TipoEmissor))).Select(c => new EnumViewModel { Value = (int)c, Name = c.ToString() }).ToList();
            return result;
        }

        private static List<EnumViewModel> GetStatusNfEntrada()
        {
            var result = ((StatusNfEntrada[])Enum.GetValues(typeof(StatusNfEntrada))).Select(c => new EnumViewModel { Value = (int)c, Name = c.ToString() }).ToList();
            return result;
        }
    }
}