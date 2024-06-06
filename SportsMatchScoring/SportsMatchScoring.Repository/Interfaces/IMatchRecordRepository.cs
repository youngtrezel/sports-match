using SportsMatchScoring.Shared.Models;
using SportsMatchScoring.Shared.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsMatchScoring.Repository.Interfaces
{
    public interface IMatchRecordRepository
    {
        public void AddMatchRecord(DbMatchRecord matchRecord);

        public IEnumerable<MatchRecord> GetAllMatchRecords();

        public IEnumerable<MatchRecord> GetMatchRecordById(int matchId);

        public IEnumerable<MatchRecord> GetMatchRecordByTeamName(string name);
    }
}
