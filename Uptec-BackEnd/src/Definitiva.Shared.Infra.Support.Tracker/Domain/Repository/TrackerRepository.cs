using Definitiva.Shared.Infra.Support.Tracker.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Definitiva.Shared.Infra.Support.Tracker.Domain.Repository
{
    public class TrackerRepository : ITrackerRepository
    {
        private readonly string _connectionString;

        public TrackerRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void Add(Model.Tracker tracker)
        {
            using (var cnn = new SqlConnection(_connectionString))
            {
                cnn.Open();

                using (var cmd = new SqlCommand(
@"INSERT INTO 
Trackers(Id, Date, Resume, Trace)
VALUES (@id, @date, @resume, @trace)", cnn))
                {
                    cmd.Parameters.AddWithValue("@id", tracker.Id);
                    cmd.Parameters.AddWithValue("@date", tracker.Date);
                    cmd.Parameters.AddWithValue("@resume", tracker.Resume);
                    cmd.Parameters.AddWithValue("@trace", tracker.Trace);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
