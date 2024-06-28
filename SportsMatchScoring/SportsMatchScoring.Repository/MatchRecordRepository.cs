using Microsoft.EntityFrameworkCore;
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

        public async Task AddMatchRecord(DbMatchRecord matchRecordDto)
        {
            MatchRecord record = MatchRecordMapping.Map(matchRecordDto);

            if(record != null)
            {

                try
                {
                    await _context.MatchRecords.AddAsync(record);
                    _context.SaveChanges();
                } 
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

            }
            
        }

        public async Task<IEnumerable<MatchRecord>> GetAllMatchRecords()
        {
            return _context.MatchRecords.ToList();
        }

        public async Task<IEnumerable<MatchRecord>> GetMatchRecordById(Guid matchId)
        {
            IQueryable<MatchRecord> queryable = _context.MatchRecords.Where(x => x.Id == matchId); ;
            IEnumerable<MatchRecord> result = await queryable.ToListAsync();
            return result;
        }

        public async Task<IEnumerable<MatchRecord>> GetMatchRecordByTeamName(string name)
        {
            IQueryable<MatchRecord> queryable = _context.MatchRecords.Where(x => x.AwayTeam == name || x.HomeTeam == name);
            IEnumerable<MatchRecord> result = await queryable.ToListAsync();
            return result;
        }

        public async Task DeleteMatchRecord(Guid id)
        {
            IQueryable<MatchRecord> query = _context.MatchRecords.Where(x => x.Id == id);
            MatchRecord result = query.FirstOrDefault();
            if (result != null)
            {
                 _context.MatchRecords.Remove(result);
                await _context.SaveChangesAsync();
            }
        }
    }
}
