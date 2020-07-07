using System;
using System.Collections.Generic;
using Uptec.Erp.Producao.Domain.Componentes.Models;

namespace Uptec.Erp.Producao.Domain.Componentes.Interfaces
{
    public interface IComponenteService : IDisposable
    {
        void Add(Componente componente);
        void Update(Componente componente);
        void Delete(Guid id);

        void AddMovimentoEntrada(ComponenteMovimento movimento);
        void AddMovimentoSaida(ComponenteMovimento movimento);
        //void AddMovimentoSaida(List<ComponenteMovimento> movimentos);
        void UpdateMovimento(ComponenteMovimento movimento);
    }
}