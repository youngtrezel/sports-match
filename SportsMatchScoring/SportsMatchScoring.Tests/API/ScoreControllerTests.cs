using Azure.Core;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SportsMatchScoring.Data;
using SportsMatchScoring.Shared.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;



namespace SportsMatchScoring.Tests.API
{
    public class ScoreControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {

        private readonly CustomWebApplicationFactory<Program> _factory;
        private HttpClient _httpClient;

        public ScoreControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _httpClient = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,

            });
        }
        [Fact]
        public async Task GetMatchRecords_ReturnSuccessWithMatchRecords()
        {
            using( var scope = _factory.Services.CreateScope() )
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<SportsMatchContext>();
                db.Database.EnsureCreated();
                Seeding.SeedDb(db);  
            }

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var response = await _httpClient.GetAsync("/api/score/getmatchrecords");

            if(!response.IsSuccessStatusCode)
            {
                Assert.Fail();
            }

            var json = await response.Content.ReadAsStringAsync();
            var result = System.Text.Json.JsonSerializer.Deserialize<List<MatchRecord>>(TrimJsonForList(json), options);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            result.Should().HaveCount(21);
        }

        [Fact]
        public async Task DeleteMatch_RemovesMatchSuccesfully()
        {
            int beforeDelete = 0;
            int afterDelete = 0;
            Guid toDeleteId = Guid.Empty;

            using (var scope = _factory.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<SportsMatchContext>();
                db.Database.EnsureCreated();
                Seeding.SeedDb(db);
            }

            // Get Id for a MatchRecord from Db
            string name = "Manchester";
            var teamNameResponse = await _httpClient.GetAsync($"/api/score/getmatchbyteam?name={name}");
            if (!teamNameResponse.IsSuccessStatusCode)
            {
                Assert.Fail();
            }
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var json = await teamNameResponse.Content.ReadAsStringAsync();
            var result = System.Text.Json.JsonSerializer.Deserialize<MatchRecord>(TrimJsonForObject(json), options);
            toDeleteId = result.Id;


            // Get MatchRecords count from Db
            var getMatchResponseBefore = await _httpClient.GetAsync("/api/score/getmatchrecords");
            if (!getMatchResponseBefore.IsSuccessStatusCode)
            {
                Assert.Fail();
            }
            var json2 = await getMatchResponseBefore.Content.ReadAsStringAsync();
            var beforeList = System.Text.Json.JsonSerializer.Deserialize<List<MatchRecord>>(TrimJsonForList(json2), options);
            beforeDelete = beforeList.Count;

            // Delete MatchRecord 
            var deleteResponse = await _httpClient.DeleteAsync($"/api/score/deletematch?id={toDeleteId}");
            if (!deleteResponse.IsSuccessStatusCode)
            {
                Assert.Fail();
            }

            // Get MatchRecords count from Db
            var getMatchResponseAfter = await _httpClient.GetAsync("/api/score/getmatchrecords");
            if (!getMatchResponseAfter.IsSuccessStatusCode)
            {
                Assert.Fail();
            }
            var json3 = await getMatchResponseAfter.Content.ReadAsStringAsync();
            var afterList = System.Text.Json.JsonSerializer.Deserialize<List<MatchRecord>>(TrimJsonForList(json3), options);
            afterDelete = afterList.Count;

            // Assert database has one less item
            beforeDelete.Should().BeGreaterThan(afterDelete);
        }

        [Fact]
        public async Task GetMatchById_RturnsMatchSuccesfully()
        {
            Guid toFindId = Guid.Empty;

            using (var scope = _factory.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<SportsMatchContext>();
                db.Database.EnsureCreated();
                Seeding.SeedDb(db);
            }

            // Get Id for a MatchRecord from Db
            string name = "Manchester";
            var teamNameResponse = await _httpClient.GetAsync($"/api/score/getmatchbyteam?name={name}");
            if (!teamNameResponse.IsSuccessStatusCode)
            {
                Assert.Fail();
            }
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var json = await teamNameResponse.Content.ReadAsStringAsync();
            var result = System.Text.Json.JsonSerializer.Deserialize<MatchRecord>(TrimJsonForObject(json), options);
            toFindId = result.Id;

            // Get MatchRecord by Id
            var getResponse = await _httpClient.GetAsync($"/api/score/getmatchbyid?id={toFindId}");
            if (!getResponse.IsSuccessStatusCode)
            {
                Assert.Fail();
            }

            var json2 = await getResponse.Content.ReadAsStringAsync();
            var result2 = System.Text.Json.JsonSerializer.Deserialize<MatchRecord>(TrimJsonForObject(json2), options);
            toFindId.Should().Be(result2.Id);
        }

        [Fact]
        public async Task GetMatchByTeam_ReturnSuccesshWithCorrectTeamsMatch()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<SportsMatchContext>();
                db.Database.EnsureCreated();
                Seeding.SeedDb(db);
            }

            string name = "Manchester";
            var response = await _httpClient.GetAsync($"/api/score/getmatchbyteam?name={name}");

            if (!response.IsSuccessStatusCode)
            {
                Assert.Fail();
            }

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var json = await response.Content.ReadAsStringAsync();
            var mr = TrimJsonForObject(json);
            var result = System.Text.Json.JsonSerializer.Deserialize<MatchRecord>(mr, options);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            result.Should().BeOfType<MatchRecord>();
            result.HomeTeam.Should().BeEquivalentTo(name);
        }

        [Fact]
        public async Task ProcessGame_ReturnsCorrectResultsString()
        {
            string[] scoresForSet = ["101010101010101010101010", "101010101010101010101010", "101010101010101010101010"];
            GameRequest request = new()
            {
                HomeTeamName = "Ravens",
                AwayTeamName = "Broncos",
                Game = Games.Squash,
                Scores = scoresForSet
            };

            var byteContent = GetTestHttpContent(request);
            var response = await _httpClient.PostAsync("/api/score/processgame", byteContent);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
                json.Should().Contain("Ravens and Broncos draw (0 - 0) Scores: 12-12, 12-12, 12-12.");
            }
        }

        private ByteArrayContent GetTestHttpContent(object obj)
        {
            var requestContent = JsonConvert.SerializeObject(obj);
            var buffer = Encoding.UTF8.GetBytes(requestContent);
            var byteContent = new ByteArrayContent(buffer);

            if (obj is string) {
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
            } else
            {
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            return byteContent;
        }

        private string TrimJsonForList(string json)
        {
            int start = json.ToString().IndexOf('[');
            int end = json.ToString().IndexOf(']');
            return json.ToString().Substring(start, end - (start - 1));
        }

        private string TrimJsonForObject(string json)
        {
            int start = json.ToString().IndexOf('[');
            int end = json.ToString().IndexOf(']');
            return json.ToString().Substring(start+1, end - (start+1));
        }

    }
}
