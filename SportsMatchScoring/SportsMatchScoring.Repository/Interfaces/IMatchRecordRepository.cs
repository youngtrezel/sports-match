using SportsMatchScoring.Shared.Models;
using SportsMatchScoring.Shared.Models.DbModels;

namespace SportsMatchScoring.Repository.Interfaces
{
    public interface IMatchRecordRepository
    {
        public Task AddMatchRecord(DbMatchRecord matchRecord);

        public Task<IEnumerable<MatchRecord>> GetAllMatchRecords();

        public Task<IEnumerable<MatchRecord>> GetMatchRecordById(Guid matchId);

        public Task<IEnumerable<MatchRecord>> GetMatchRecordByTeamName(string name);

        public Task DeleteMatchRecord(Guid id);
    }
}
