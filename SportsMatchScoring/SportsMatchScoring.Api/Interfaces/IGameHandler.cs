using SportsMatchScoring.Shared.Interfaces;
using SportsMatchScoring.Shared.Models;

namespace SportsMatchScoring.Api.Interfaces
{
    public interface IGameHandler
    {
        public void SetupGame(GameRequest gameRequest);
        public string GetResultsMessage();
        public Match GetMatchDetails();
    }
}
