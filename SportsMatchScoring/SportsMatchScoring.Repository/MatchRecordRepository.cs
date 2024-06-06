using SportsMatchScoring.Data;
using SportsMatchScoring.Repository.Interfaces;
using SportsMatchScoring.Shared.Database;
using SportsMatchScoring.Shared.Models;
using SportsMatchScoring.Shared.Models.DbModels;

namespace SportsMatchScoring.Repository
{
    public class MatchRecordRepository : IMatchRecordRepository
    {
        private readonly SportsMatchContext _context;
        public MatchRecordRepository(SportsMatchContext sportsMatchContext) {       
            _context = sportsMatchContext;       
        }

        public void AddMatchRecord(DbMatchRecord matchRecord)
        {
            _context.MatchRecords.Add(MatchRecordMapping.Map(matchRecord));
            _context.SaveChanges();
        }

        public IEnumerable<MatchRecord> GetAllMatchRecords()
        {
            return _context.MatchRecords.Select(x => x);
        }

        public IEnumerable<MatchRecord> GetMatchRecordById(int matchId)
        {
            return _context.MatchRecords.Where(x => x.Id == matchId);
        }

        public IEnumerable<MatchRecord> GetMatchRecordByTeamName(string name)
        {
            return [.. _context.MatchRecords.Where(x => x.AwayTeam == name || x.HomeTeam == name)];
        }
    }
}
