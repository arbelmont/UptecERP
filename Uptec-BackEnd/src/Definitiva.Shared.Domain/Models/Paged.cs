using System.Collections.Generic;

namespace Definitiva.Shared.Domain.Models
{
    public class Paged<T> where T : Entity<T>
    {
        public IEnumerable<T> List { get; set; }
        public int Total { get; set; }
    }
}