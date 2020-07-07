using ExpressMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using Uptec.Erp.Api.ViewModels.Producao.Arquivos;
using Uptec.Erp.Producao.Domain.Arquivos.Models;

namespace Uptec.Erp.Api.Mappers.Producao.Map
{
    public class ArquivoMapper
    {
        public ArquivoMapper()
        {
            Mapper.Register<ArquivoAddViewModel, Arquivo>()
                    .Instantiate(src => ObterArquivo(src.Dados));
        }

        private Arquivo ObterArquivo(IFormFile file)
        {
            string conteudo = string.Empty;
            int tamanho = 0;

            if (file.Length > 0)
            {
                Int32.TryParse((file.Length / 1024).ToString(), out tamanho);

                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    conteudo = reader.ReadToEnd();
                }
            }

            return new Arquivo(Guid.NewGuid(), file.FileName, tamanho, file.ContentType, conteudo);
        }
    }
}
