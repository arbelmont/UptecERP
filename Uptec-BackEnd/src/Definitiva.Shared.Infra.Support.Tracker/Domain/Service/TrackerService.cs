using Definitiva.Shared.Infra.Support.Tracker.Domain.Interfaces;

namespace Definitiva.Shared.Infra.Support.Tracker.Domain.Service
{
    public class TrackerService : ITrackerService
    {
        private readonly ITrackerRepository _trackerRepository;

        public TrackerService(ITrackerRepository trackerService)
        {
            _trackerRepository = trackerService;
        }

        public void Trace(string resume, string trace)
        {
            var tracker = new Model.Tracker(resume, trace);

            if (tracker.IsValid())
                _trackerRepository.Add(tracker);
        }
    }
}
