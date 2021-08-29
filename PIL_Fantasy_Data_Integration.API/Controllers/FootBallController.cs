using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.FootBallResponses;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.DAL.DB;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using League = PIL_Fantasy_Data_Integration.API.Fantasy_Data.DAL.DB.League;
using Player = PIL_Fantasy_Data_Integration.API.Fantasy_Data.DAL.DB.Player;
using Team = PIL_Fantasy_Data_Integration.API.Fantasy_Data.DAL.DB.Team;
using static PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.FootBallResponses.FootBallResponses;

namespace PIL_Fantasy_Data_Integration.API.Fantasy_Data.API.Controllers
{
    //[EnableCors("MyPolicy")]
    [Route("api/fantasydata/[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class FootBallController : Controller
    {
        private static readonly string apiUrl = "https://v3.football.api-sports.io/";
        private static readonly string apiKey = "706a23099b2fa398f36f1cbd2c6c81a5";

        private readonly fantasy_dataContext _context;

        public FootBallController(fantasy_dataContext context)
        {
            _context = context;
        }
        [Route("HandleCountry")]
        [HttpGet]
        public ActionResult HandleCountry()
        {
            try
            {
                var countries = GetFootBallCountry();
                foreach (var countryRes in countries.Response)
                {
                    var countryExist = _context.Countries.Any(c => c.Code == countryRes.Code && c.Name == countryRes.Name);
                    if (!countryExist)
                    {
                        var newCountry = new DAL.DB.Country
                        {
                            Name = countryRes.Name,
                            Code = countryRes.Code,
                            Flag = countryRes.Flag
                        };
                        _context.Countries.Add(newCountry);
                    }
                }
                var status = _context.SaveChanges();
                return Ok(status);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex.Message, ex.StackTrace);
                return Ok(ex.Message + ex.StackTrace);
            }
        }
        private static Root GetFootBallCountry()
        {
            var relativeUrl =
                $"countries";
            var response = UIHelper.CreateRequest(apiUrl, HttpMethod.Get, relativeUrl, apiKey: apiKey);
            //var response = UIHelper.AddProxy(relativeUrl, basicAuthUser: authUser,
            //    basicAuthPassword: authPassword,isProxy:true);
            try
            {
                if (response.StatusCode != HttpStatusCode.OK) return null;
                var result = response.Content.ReadAsStringAsync().Result;

                var token2 = JToken.Parse(result);
                var model = JsonConvert.DeserializeObject<Root>(result);
                //var model = JsonConvert.DeserializeObject<List<Root>>(token2["response"].ToString());
                return model;
                //return null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex.Message, ex.StackTrace);
                return null;
            }
        }

        [Route("HandleLeague")]
        [HttpGet]
        public ActionResult HandleLeague()
        {
            try
            {
                var countryNames =
                    _context.Countries.Where(c => c.Name != null && !c.IsDeleted.Value && c.Show.Value)
                        ?.Select(c => c.Name).ToList();
                foreach (var countryName in countryNames)
                {
                    var leagueFootBall = GetFootBallLeague(countryName);
                    foreach (var footBallLeague in leagueFootBall.Response)
                    {
                        var leagueId = footBallLeague.League.Id.ToString();
                        var leagueStart = Convert.ToDateTime(footBallLeague.Seasons[0].Start);
                        var leagueEnd = Convert.ToDateTime(footBallLeague.Seasons[0].End);
                        var leagueDb = _context.Leagues.FirstOrDefault(c =>
                            !c.IsDeleted.Value && c
                            .VendorId == 1 && !string.IsNullOrWhiteSpace(c.IntegrationId) &&
                            c.IntegrationId == leagueId
                            && c.StartDate == leagueStart);
                        if (leagueDb == null)
                        {
                            var league = new League
                            {
                                CreationDate = DateTime.Now,
                                EndDate = leagueEnd,
                                IntegrationId = leagueId,
                                Color = footBallLeague.Country.Flag ?? "NA",
                                DefaultImageUrl = footBallLeague.League.Logo ?? "NA",
                                LeagueCountry = footBallLeague.Country.Name ?? "NA",
                                LeagueCountryCode = footBallLeague.Country.Code ?? "NA",
                                LeagueDisplayOrder = 100,
                                LeagueIsFriendly = "NA",
                                LeagueType = footBallLeague.League.Type ?? "NA",
                                VendorId = 1, //dsg vendor id
                                StartDate = leagueStart,
                                Name=footBallLeague.League.Name
                            };
                            _context.Leagues.Add(league);
                        }
                    }  
                }
             var status =   _context.SaveChanges();
                return Ok(status);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex.Message, ex.StackTrace);
                return Ok(ex.Message + ex.StackTrace);
            }

        }

        private static Root GetFootBallLeague(string countryName)
        {
            var relativeUrl =
                $"leagues?country={countryName}&current=true";
            var response = UIHelper.CreateRequest(apiUrl, HttpMethod.Get, relativeUrl, apiKey: apiKey);
            //var response = UIHelper.AddProxy(relativeUrl, basicAuthUser: authUser,
            //    basicAuthPassword: authPassword,isProxy:true);
            try
            {
                if (response.StatusCode != HttpStatusCode.OK) return null;
                var result = response.Content.ReadAsStringAsync().Result;

                //var token2 = JToken.Parse(result);
                var model = JsonConvert.DeserializeObject<Root>(result);
                //var model = JsonConvert.DeserializeObject<List<FootBallLeague>>(token2["response"].ToString());
                return model;
                //return null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex.Message, ex.StackTrace);
                return null;
            }
        }

        [Route("HandleTeamStanding")]
        [HttpGet]
        public ActionResult HandleTeamStanding(bool onmatches=false)
        {
            try
            {
                var leagueDbs = _context.Leagues.Where(c =>
                    !c.IsDeleted.Value && c.VendorId == 1 && c.IntegrationId != null && c.Show.Value).ToList();
                foreach (var leagueDb in leagueDbs)
                {
                    if (leagueDb != null)
                    {
                        if (onmatches == true)
                        {
                            var liveMatches = _context.Matches.Where(c => c.LeagueId == leagueDb.Id
     && (c.EndDatetime == null || (c.EndDatetime != null && c.EndDatetime > DateTime.UtcNow.AddMinutes(-15)))
     && c.StartDatetime < DateTime.UtcNow).ToList();
                            foreach ( var match in liveMatches)
                            {
                                var team1Reses = GetFootBallTeamStanding(leagueDb.IntegrationId, leagueDb.StartDate.Year,match.Team1.IntegrationId);
                                var team2Reses = GetFootBallTeamStanding(leagueDb.IntegrationId, leagueDb.StartDate.Year, match.Team2.IntegrationId);
                                AddUpdateTeams(leagueDb, team1Reses.Response[0]);
                                AddUpdateTeams(leagueDb, team2Reses.Response[0]);
                            }
                        }
                        else
                        {
                            var teamsReses = GetFootBallTeamStanding(leagueDb.IntegrationId, leagueDb.StartDate.Year);
                            AddUpdateTeams(leagueDb, teamsReses.Response[0]);
                        }
                    }
                }
                var status=_context.SaveChanges();
                return Ok(status);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex.Message, ex.StackTrace);
                return Ok(ex.Message+ex.StackTrace);
            }
        }

        private void AddUpdateTeams(League leagueDb, FootBallResponses.Response teamsReses)
        {
            if (teamsReses != null )
            {
                foreach (var footBallTeamsRese in teamsReses.League.Standings[0])
                {
                    var teamDb = _context.Teams.FirstOrDefault(c =>
                        !c.IsDeleted.Value && c.League != null && c.League.VendorId == 1 &&
                        !string.IsNullOrWhiteSpace(c.IntegrationId) &&
                        c.IntegrationId == footBallTeamsRese.Team.Id.ToString()
                        && c.LeagueId == leagueDb.Id);
                    if (teamDb == null)
                    {
                        var team = new Team
                        {
                            CreationDate = DateTime.Now,
                            IntegrationId = footBallTeamsRese.Team.Id.ToString(),
                            LeagueId = leagueDb.Id,
                            ImageUrl = footBallTeamsRese.Team.Logo,
                            Points = footBallTeamsRese.Points,
                            PlayedCount = footBallTeamsRese.All.Played,
                            WonCount = footBallTeamsRese.All.Win,
                            LossCount = footBallTeamsRese.All.Lose,
                            DrawCount = footBallTeamsRese.All.Draw,
                            GoalsFor = footBallTeamsRese.All.Goals.For,
                            GoalsAgainst = footBallTeamsRese.All.Goals.Against,
                            OrderInLeague = footBallTeamsRese.Rank,
                            Name = footBallTeamsRese.Team.Name,
                            Group = footBallTeamsRese.Group
                        };
                        _context.Teams.Add(team);
                    }
                    else
                    {
                        teamDb.Points = footBallTeamsRese.Points;
                        teamDb.PlayedCount = footBallTeamsRese.All.Played;
                        teamDb.WonCount = footBallTeamsRese.All.Win;
                        teamDb.LossCount = footBallTeamsRese.All.Lose;
                        teamDb.DrawCount = footBallTeamsRese.All.Draw;
                        teamDb.GoalsFor = footBallTeamsRese.All.Goals.For;
                        teamDb.GoalsAgainst = footBallTeamsRese.All.Goals.Against;
                        teamDb.OrderInLeague = footBallTeamsRese.Rank;
                        teamDb.Group = footBallTeamsRese.Group;
                    }
                }
            }
        }

        private static Root GetFootBallTeamStanding(string league, int startDateYear, string TeamIntegrationId=null)
        {
            var relativeUrl = string.Empty;
            if (string.IsNullOrWhiteSpace(TeamIntegrationId))
            {
                relativeUrl =
     $"standings?league={league}&season={startDateYear}";
            }
            else
            {
                relativeUrl =
$"standings?league={league}&season={startDateYear}&team={TeamIntegrationId}";
            }
            var response = UIHelper.CreateRequest(apiUrl, HttpMethod.Get, relativeUrl, apiKey: apiKey);
            //var response = UIHelper.AddProxy(relativeUrl, basicAuthUser: authUser,
            //    basicAuthPassword: authPassword,isProxy:true);
            try
            {
                //var model = JsonConvert.DeserializeObject<List<FootBallTeamsRes>>(
                //    token2["response"][0]["league"]["standings"][0].ToString());
                //return model;
                if (response.StatusCode != HttpStatusCode.OK) return null;
                var result = response.Content.ReadAsStringAsync().Result;

                //var token2 = JToken.Parse(result);
                var model = JsonConvert.DeserializeObject<Root>(result);
                //var model = JsonConvert.DeserializeObject<List<FootBallLeague>>(token2["response"].ToString());
                return model;
                //return null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex.Message, ex.StackTrace);
                return null;
            }
        }

        [Route("HandlePlayers")]
        [HttpGet]
        public ActionResult HandlePlayers()
        {
            try
            {
                var leagueDbs = _context.Leagues.Where(c =>
                    !c.IsDeleted.Value && c.VendorId == 1 && c.IntegrationId != null && c.Show.Value).ToList();
                foreach (var leagueDb in leagueDbs)
                {
                        var teamDb = _context.Teams.Where(c =>
                        !c.IsDeleted.Value && c.League != null && c.League.VendorId == 1 &&
                        !string.IsNullOrWhiteSpace(c.IntegrationId) && c.LeagueId == leagueDb.Id).ToList();
                    if (leagueDb != null && teamDb.Count > 0)
                    {
                        foreach (var footBallTeamsRese in teamDb)
                        {
                            var players = GetFootBallPlayer(footBallTeamsRese.IntegrationId, leagueDb.StartDate.Year);
                            if (players != null)
                            {
                                var pagingTotal = players.Paging.Total;
                                for (var page = 1; page <= pagingTotal; page++)
                                {
                                    players = GetFootBallPlayer(footBallTeamsRese.IntegrationId, leagueDb.StartDate.Year, page);
                                    foreach (var footBallPlayerRese in players.Response)
                                    {
                                        var playerDb = _context.Players.FirstOrDefault(c =>
                                            !c.IsDeleted.Value && c.TeamId == footBallTeamsRese.Id &&
                                            c.Team.League.VendorId == 1 &&
                                            !string.IsNullOrWhiteSpace(c.IntegrationId) &&
                                            c.IntegrationId == footBallPlayerRese.Player.Id.ToString());
                                        if (playerDb == null)
                                        {
                                            var player = new Player
                                            {
                                                CreationDate = DateTime.Now,
                                                IntegrationId = footBallPlayerRese.Player.Id.ToString(),
                                                TeamId = footBallTeamsRese.Id,
                                                Name= footBallPlayerRese.Player.Name,
                                                CountryOfBirth= footBallPlayerRese.Player.Birth.Country,
                                                DateOfBirth= footBallPlayerRese.Player.Birth.Date,
                                                FirstName= footBallPlayerRese.Player.Firstname,
                                                LastName= footBallPlayerRese.Player.Lastname,
                                                Height= footBallPlayerRese.Player.Height,
                                                Injured= footBallPlayerRese.Player.Injured,
                                                Nationality= footBallPlayerRese.Player.Nationality,
                                                Photo= footBallPlayerRese.Player.Photo,
                                                Weight = footBallPlayerRese.Player.Weight,
                                                Age = footBallPlayerRese.Player.Age.ToString(),
                                                GoalsAssists = footBallPlayerRese.Statistics[0].Goals.Assists?.ToString(),
                                                GoalsSaves = footBallPlayerRese.Statistics[0].Goals.Saves?.ToString(),
                                                GoalsTotal = footBallPlayerRese.Statistics[0].Goals.Total?.ToString(),
                                                Minutes = footBallPlayerRese.Statistics[0].Games.Minutes?.ToString(),
                                                Position = footBallPlayerRese.Statistics[0].Games.Position?.ToString(),
                                                Rating = footBallPlayerRese.Statistics[0].Games.Rating?.ToString(),
                                                PassesTotal = footBallPlayerRese.Statistics[0].Passes.Total?.ToString(),
                                                PassesAccuracy = footBallPlayerRese.Statistics[0].Passes.Accuracy?.ToString(),
                                                CardsRed = footBallPlayerRese.Statistics[0].Cards.Red?.ToString(),
                                                CardsYellow = footBallPlayerRese.Statistics[0].Cards.Yellow?.ToString(),
                                                CardsYellowRed = footBallPlayerRese.Statistics[0].Cards.Yellowred?.ToString()
                                            };
                                            _context.Players.Add(player);
                                        }
                                        else
                                        {
                                            playerDb.Injured = footBallPlayerRese.Player.Injured;
                                            playerDb.Photo = footBallPlayerRese.Player.Photo;
                                            playerDb.Weight = footBallPlayerRese.Player.Weight;
                                            playerDb.Age = footBallPlayerRese.Player.Age.ToString();
                                            playerDb.GoalsAssists = footBallPlayerRese.Statistics[0].Goals.Assists?.ToString();
                                            playerDb.GoalsSaves = footBallPlayerRese.Statistics[0].Goals.Saves?.ToString();
                                            playerDb.GoalsTotal = footBallPlayerRese.Statistics[0].Goals.Total?.ToString();
                                            playerDb.Minutes = footBallPlayerRese.Statistics[0].Games.Minutes?.ToString();
                                            playerDb.Position = footBallPlayerRese.Statistics[0].Games.Position?.ToString();
                                            playerDb.Rating = footBallPlayerRese.Statistics[0].Games.Rating?.ToString();
                                            playerDb.PassesTotal = footBallPlayerRese.Statistics[0].Passes.Total?.ToString();
                                            playerDb.PassesAccuracy = footBallPlayerRese.Statistics[0].Passes.Accuracy?.ToString();
                                            playerDb.CardsRed = footBallPlayerRese.Statistics[0].Cards.Red?.ToString();
                                            playerDb.CardsYellow = footBallPlayerRese.Statistics[0].Cards.Yellow?.ToString();
                                            playerDb.CardsYellowRed = footBallPlayerRese.Statistics[0].Cards.Yellowred?.ToString();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
               var status= _context.SaveChanges();
                return Ok(status);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex.Message, ex.StackTrace);
                return Ok(ex.Message+ ex.StackTrace);
            }

           
        }

        private static Root GetFootBallPlayer(string team, int startDateYear, int page = 1)
        {
            var relativeUrl =
                $"players?team={team}&season={startDateYear}&page={page}";
            var response = UIHelper.CreateRequest(apiUrl, HttpMethod.Get, relativeUrl, apiKey: apiKey);
            //var response = UIHelper.AddProxy(relativeUrl, basicAuthUser: authUser,
            //    basicAuthPassword: authPassword,isProxy:true);
            try
            {
                if (response.StatusCode != HttpStatusCode.OK) return null;
                var result = response.Content.ReadAsStringAsync().Result;

                //var token2 = JToken.Parse(result);
                //var model = JsonConvert.DeserializeObject<List<FootBallPlayersRes>>(result);
                //var model = JsonConvert.DeserializeObject<FootBallPlayersRes>(result);
                var model = JsonConvert.DeserializeObject<Root>(result);

                return model;
                //return null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex.Message, ex.StackTrace);
                return null;
            }
        }

        [Route("HandleMatches")]
        [HttpGet]
        public ActionResult HandleMatches(bool live =false)
        {
            try
            {
                var leagueDbs = _context.Leagues.Where(c =>
    !c.IsDeleted.Value && c.VendorId == 1 && c.IntegrationId != null && c.Show.Value).ToList();

                foreach (var leagueDb in leagueDbs)
                {
                    if (leagueDb != null)
                    {
                        var matchesReses = GetFootBallMatch(leagueDb.IntegrationId, leagueDb.StartDate.Year,live);
                        foreach (var footBallMatchesRese in matchesReses.Response)
                        {
                            //HandleMatchTeams(footBallMatchesRese, leagueDb);
                            var matchDb = _context.Matches.FirstOrDefault(c =>
                                !c.IsDeleted.Value && c.League != null && c.League.VendorId == 1 &&
                                !string.IsNullOrWhiteSpace(c.IntegrationId) &&
                                c.IntegrationId == footBallMatchesRese.Fixture.Id.ToString()
                                && c.LeagueId == leagueDb.Id);
                            var team1 = _context.Teams.FirstOrDefault(c => !c.IsDeleted.Value &&
                                c.IntegrationId == footBallMatchesRese.Teams.Home.Id.ToString() &&
                                c.LeagueId == leagueDb.Id);
                            var team2 = _context.Teams.FirstOrDefault(c => !c.IsDeleted.Value &&
                                c.IntegrationId == footBallMatchesRese.Teams.Away.Id.ToString() &&
                                c.LeagueId == leagueDb.Id);
                            if (matchDb == null && team1 != null && team2 != null)
                            {
                                var match = new Match
                                {
                                    CreationDate = DateTime.Now,
                                    IntegrationId = footBallMatchesRese.Fixture.Id.ToString(),
                                    LeagueId = leagueDb.Id,
                                    StartDatetime = footBallMatchesRese.Fixture.Date,
                                    Status = footBallMatchesRese.Fixture.Status.Long,
                                    Week = ExtractNumber(footBallMatchesRese.League.Round),
                                    Team1Id = team1?.Id ?? 0,
                                    Team2Id = team2?.Id ?? 0,
                                    HomeTeamId = team1?.Id ?? 0,
                                    Team1Score = footBallMatchesRese.Goals.Home,
                                    Team2Score = footBallMatchesRese.Goals.Away,
                                    VendorId=1
                                };
                                if (footBallMatchesRese.Fixture.Status.Elapsed != null &&
                                    footBallMatchesRese.Fixture.Status.Short == "FT")
                                {
                                    var elapsed = footBallMatchesRese.Fixture.Status.Elapsed.Value;
                                    var end = footBallMatchesRese.Fixture.Date.AddMinutes(elapsed);
                                    match.EndDatetime = end;
                                }

                                _context.Matches.Add(match);
                            }
                            else if (team1 != null && team2 != null)
                            {
                                matchDb.StartDatetime = footBallMatchesRese.Fixture.Date;
                                matchDb.Status = footBallMatchesRese.Fixture.Status.Long;
                                matchDb.Week = ExtractNumber(footBallMatchesRese.League.Round);
                                matchDb.Team1Id = team1?.Id ?? 0;
                                matchDb.Team2Id = team2?.Id ?? 0;
                                matchDb.HomeTeamId = team1?.Id ?? 0;
                                matchDb.Team1Score = footBallMatchesRese.Goals.Home;
                                matchDb.Team2Score = footBallMatchesRese.Goals.Away;
                                if (footBallMatchesRese.Fixture.Status.Elapsed != null &&
                                    footBallMatchesRese.Fixture.Status.Short == "FT")
                                {
                                    var elapsed = footBallMatchesRese.Fixture.Status.Elapsed.Value;
                                    var end = footBallMatchesRese.Fixture.Date.AddMinutes(elapsed);
                                    matchDb.EndDatetime = end;
                                }
                            }
                        }

                        var status=_context.SaveChanges();
                        return Ok(status);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex.Message, ex.StackTrace);
                return Ok(ex.Message+ex.StackTrace);
            }
            return Ok("Done");
        }

        private void HandleMatchTeams(FootBallMatchesRes footBallMatchesRese, League leagueDb)
        {
            if (!_context.Teams.Any(c => !c.IsDeleted.Value &&
                c.IntegrationId == footBallMatchesRese.Teams.Home.Id.ToString() &&
                c.LeagueId == leagueDb.Id))
            {
                var team = new Team
                {
                    CreationDate = DateTime.Now,
                    IntegrationId = footBallMatchesRese.Teams.Home.Id.ToString(),
                    LeagueId = leagueDb.Id,
                    ImageUrl = footBallMatchesRese.Teams.Home.Logo
                };
                _context.Teams.Add(team);
            }

            if (!_context.Teams.Any(c => !c.IsDeleted.Value &&
                c.IntegrationId == footBallMatchesRese.Teams.Away.Id.ToString() && c.LeagueId == leagueDb.Id))
            {
                var team = new Team
                {
                    CreationDate = DateTime.Now,
                    IntegrationId = footBallMatchesRese.Teams.Away.Id.ToString(),
                    LeagueId = leagueDb.Id,
                    ImageUrl = footBallMatchesRese.Teams.Home.Logo
                };
                _context.Teams.Add(team);
            }
        }

        private static Root GetFootBallMatch(string league, int startDateYear, bool live)
        {
            var relativeUrl = string.Empty;
            if (!live)
            {
                relativeUrl =
                                $"fixtures?league={league}&season={startDateYear}";
            }
            else
            {
                relativeUrl =
                $"fixtures?league={league}&season={startDateYear}&live=all";
            }
            var response = UIHelper.CreateRequest(apiUrl, HttpMethod.Get, relativeUrl, apiKey: apiKey);
            //var response = UIHelper.AddProxy(relativeUrl, basicAuthUser: authUser,
            //    basicAuthPassword: authPassword,isProxy:true);
            try
            {
                if (response.StatusCode != HttpStatusCode.OK) return null;
                var result = response.Content.ReadAsStringAsync().Result;

                //var token2 = JToken.Parse(result);
                //var model = JsonConvert.DeserializeObject<List<FootBallMatchesRes>>(token2["response"].ToString());
                var model = JsonConvert.DeserializeObject<Root>(result);

                return model;
                //return null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex.Message, ex.StackTrace);
                return null;
            }
        }

        private static int ExtractNumber(string input)
        {
            var b = string.Empty;
            var val = 0;

            for (var i = 0; i < input.Length; i++)
                if (char.IsDigit(input[i]))
                    b += input[i];

            if (b.Length > 0)
                val = int.Parse(b);

            return val;
        }
    }
}