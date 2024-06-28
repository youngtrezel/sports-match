using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<string>> ProcessGame([FromBody] GameRequest gameRequest)
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
        public async Task<ActionResult<IEnumerable<MatchRecord>>> GetMatchRecords()
        {
            var result = await _matchRecordRepository.GetAllMatchRecords();
            return Ok(JsonSerializer.Serialize(result));
        }

        [HttpGet]
        [Route("getmatchbyid")]
        public async Task<ActionResult<IEnumerable<MatchRecord>>> GetMatchById(Guid Id)
        {
            var result = await _matchRecordRepository.GetMatchRecordById(Id);         
            return Ok(JsonSerializer.Serialize(result));
        }

        [HttpGet]
        [Route("getmatchbyteam")]
        public async Task<ActionResult<IEnumerable<MatchRecord>>> GetMatchByTeam(string name)
        {
            var result = await _matchRecordRepository.GetMatchRecordByTeamName(name);
            return Ok(JsonSerializer.Serialize(result));
        }

        [HttpDelete]
        [Route("deletematch")]
        public async Task<ActionResult> DeleteMatchRecord(Guid id)
        {
            var result = await _matchRecordRepository.GetMatchRecordById(id);
            if(result == null)
            {
                return BadRequest();
            }
            else
            {
                await _matchRecordRepository.DeleteMatchRecord(id);
                return Ok();
            }           
        }
    }
}
