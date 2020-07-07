using Definitiva.Shared.Infra.Support.Helpers;
using System;

namespace Definitiva.Shared.Infra.Support.Tracker.Domain.Model
{
    public class Tracker
    {
        public Guid Id { get; private set; }
        public DateTime Date { get; private set; }
        public string Resume { get; private set; }
        public string Trace { get; private set; }

        public Tracker(string resume, string trace)
        {
            Id = Guid.NewGuid();
            Date = DateTime.Now;
            Resume = resume.Left(80);
            Trace = trace;
        }

        #region Validação
        public bool IsValid()
        {
            return !(Trace.IsNullOrWhiteSpace() && Resume.IsNullOrWhiteSpace());
        }
        #endregion
    }
}
