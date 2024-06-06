using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsMatchScoring.Api.Handlers;
using SportsMatchScoring.Api.Interfaces;
using SportsMatchScoring.Repository.Interfaces;
using SportsMatchScoring.Shared.Models;
using SportsMatchScoring.Shared.Models.DbModels;
using System.Text.Json;

namespace SportsMatchScoring.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreController : ControllerBase
    {
        private readonly IGameHandler _gameHandler;
        private readonly IMatchRecordRepository _matchRecordRepository;

        public ScoreController(IGameHandler gameHandler, IMatchRecordRepository matchRecordRepository)
        {
            _gameHandler = gameHandler;
            _matchRecordRepository = matchRecordRepository;
        }

        [HttpPost]
        [Route("processgame")]
        public ActionResult<string> ProcessGame([FromBody] GameRequest gameRequest)
        {
            _gameHandler.SetupGame(gameRequest);
            var gameResults = _gameHandler.GetResultsMessage();
            var matchDetails = _gameHandler.GetMatchDetails();

            DbMatchRecord record = new()
            {
                HomeTeam = matchDetails.HomeTeam.TeamName,
                AwayTeam = matchDetails.AwayTeam.TeamName,
                HomeSets = matchDetails.HomeSets,
                AwaySets = matchDetails.AwaySets,
                Game = matchDetails.Game.ToString(),
                Result = gameResults
            };

            _matchRecordRepository.AddMatchRecord(record);
            return Ok(JsonSerializer.Serialize(gameResults));
        }

        [HttpGet]
        [Route("getmatchrecords")]
        public ActionResult<IEnumerable<MatchRecord>> GetMatchRecords()
        {
            var result = _matchRecordRepository.GetAllMatchRecords();
            return Ok(result);
        }

        [HttpGet]
        [Route("getmatchbyid")]
        public ActionResult<IEnumerable<MatchRecord>> GetMatchById(int Id)
        {
            var result = _matchRecordRepository.GetMatchRecordById(Id);         
            return Ok(result);
        }

        [HttpGet]
        [Route("getmatchbyteam")]
        public ActionResult<IEnumerable<MatchRecord>> GetMatchByTeam(string name)
        {
            var result = _matchRecordRepository.GetMatchRecordByTeamName(name);
            return Ok(result);
        }
    }
}
