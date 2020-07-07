using System.Collections.Generic;

namespace Uptec.Erp.Api.ViewModels.Shared
{
    public class PagedViewModel<T> where T : class 
    {
        public IEnumerable<T> List { get; set; }
        public int Total { get; set; }
    }
}