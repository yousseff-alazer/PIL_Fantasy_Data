using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.FootBallResponses;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.DAL.DB;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.Helpers;
using Team = PIL_Fantasy_Data_Integration.API.Fantasy_Data.DAL.DB.Team;

namespace PIL_Fantasy_Data_Integration.API.Fantasy_Data.API.Services
{
    public class FootBallService
    {
        public static readonly string apiUrl = "https://v3.football.api-sports.io/";
        public static readonly string apiKey = "706a23099b2fa398f36f1cbd2c6c81a5";
        public readonly fantasy_dataContext _context;

        public FootBallService(fantasy_dataContext context)
        {
            _context = context;
        }

        public static FootBallResponses.Root GetFootBallCountry()
        {
            var relativeUrl =
                "countries";
            var response = UIHelper.CreateRequest(apiUrl, HttpMethod.Get, relativeUrl, apiKey: apiKey);
            //var response = UIHelper.AddProxy(relativeUrl, basicAuthUser: authUser,
            //    basicAuthPassword: authPassword,isProxy:true);
            try
            {
                if (response.StatusCode != HttpStatusCode.OK) return null;
                var result = response.Content.ReadAsStringAsync().Result;

                var token2 = JToken.Parse(result);
                var model = JsonConvert.DeserializeObject<FootBallResponses.Root>(result);
                //var model = JsonConvert.DeserializeObject<List<Root>>(token2["response"].ToString());
                Thread.Sleep(30);
                return model;
                //return null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex.Message, ex.StackTrace);
                return null;
            }
        }

        public static FootBallResponses.Root GetFootBallLeague(string countryName)
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
                var model = JsonConvert.DeserializeObject<FootBallResponses.Root>(result);
                //var model = JsonConvert.DeserializeObject<List<FootBallLeague>>(token2["response"].ToString());
                Thread.Sleep(30);
                return model;
                //return null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex.Message, ex.StackTrace);
                return null;
            }
        }

        public void AddUpdateTeams(League leagueDb, FootBallResponses.Response teamsReses)
        {
            if (teamsReses != null)
                foreach (var Standings in teamsReses.League.Standings)
                    foreach (var footBallTeamsRese in Standings)
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
                            teamDb.ModificationDate = DateTime.UtcNow;
                        }
                    }
        }

        public static FootBallResponses.Root GetFootBallTeamStanding(string league, int startDateYear,
            string TeamIntegrationId = null)
        {
            var relativeUrl = string.Empty;
            if (string.IsNullOrWhiteSpace(TeamIntegrationId))
                relativeUrl =
                    $"standings?league={league}&season={startDateYear}";
            else
                relativeUrl =
                    $"standings?league={league}&season={startDateYear}&team={TeamIntegrationId}";
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
                var model = JsonConvert.DeserializeObject<FootBallResponses.Root>(result);
                //var model = JsonConvert.DeserializeObject<List<FootBallLeague>>(token2["response"].ToString());
                Thread.Sleep(30);
                return model;
                //return null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex.Message, ex.StackTrace);
                return null;
            }
        }

        public void AddEditPlayer(League leagueDb, List<Team> teamDb)
        {
            if (leagueDb != null && teamDb.Count > 0)
                //var postions = _context.PlayerPositions.ToList();
                foreach (var footBallTeamsRese in teamDb)
                {

                    var players = GetFootBallPlayer(footBallTeamsRese.IntegrationId, leagueDb.StartDate.Year,
                        leagueDb.IntegrationId);

                    if (players != null)
                    {
                        var playersToDelete = _context.Players.Where(c => c.TeamId == footBallTeamsRese.Id && !c.IsDeleted.Value).ToList();
                        playersToDelete.ForEach(a => a.IsDeleted = true);
                        var pagingTotal = players.Paging.Total;
                        for (var page = 1; page <= pagingTotal; page++)
                        {
                            players = GetFootBallPlayer(footBallTeamsRese.IntegrationId, leagueDb.StartDate.Year,
                                leagueDb.IntegrationId, page);
                            if (players != null)
                                foreach (var footBallPlayerRese in players.Response)
                                {
                                    var playerDb = _context.Players.FirstOrDefault(c =>
                                        /*!c.IsDeleted.Value &&*/ c.TeamId == footBallTeamsRese.Id &&
                                        c.Team.League.VendorId == 1 &&
                                        !string.IsNullOrWhiteSpace(c.IntegrationId) &&
                                        c.IntegrationId == footBallPlayerRese.Player.Id.ToString());
                                    var pos = footBallPlayerRese.Statistics[0].Games.Position;
                                    if (playerDb == null)
                                    {
                                        var player = new Player
                                        {
                                            CreationDate = DateTime.Now,
                                            IntegrationId = footBallPlayerRese.Player.Id.ToString(),
                                            TeamId = footBallTeamsRese.Id,
                                            Name = footBallPlayerRese.Player.Name,
                                            CountryOfBirth = footBallPlayerRese.Player.Birth.Country,
                                            DateOfBirth = footBallPlayerRese.Player.Birth.Date,
                                            FirstName = footBallPlayerRese.Player.Firstname,
                                            LastName = footBallPlayerRese.Player.Lastname,
                                            Height = footBallPlayerRese.Player.Height,
                                            Injured = footBallPlayerRese.Player.Injured,
                                            Nationality = footBallPlayerRese.Player.Nationality,
                                            Photo = footBallPlayerRese.Player.Photo,
                                            Weight = footBallPlayerRese.Player.Weight,
                                            Age = footBallPlayerRese.Player.Age.ToString(),
                                            GoalsAssists = footBallPlayerRese.Statistics[0].Goals.Assists?.ToString(),
                                            GoalsSaves = footBallPlayerRese.Statistics[0].Goals.Saves?.ToString(),
                                            GoalsTotal = footBallPlayerRese.Statistics[0].Goals.Total?.ToString(),
                                            Minutes = footBallPlayerRese.Statistics[0].Games.Minutes?.ToString(),
                                            Position = pos,
                                            Rating = footBallPlayerRese.Statistics[0].Games.Rating,
                                            PassesTotal = footBallPlayerRese.Statistics[0].Passes.Total?.ToString(),
                                            PassesAccuracy = footBallPlayerRese.Statistics[0].Passes.Accuracy
                                                ?.ToString(),
                                            CardsRed = footBallPlayerRese.Statistics[0].Cards.Red?.ToString(),
                                            CardsYellow = footBallPlayerRese.Statistics[0].Cards.Yellow?.ToString(),
                                            CardsYellowRed = footBallPlayerRese.Statistics[0].Cards.Yellowred
                                                ?.ToString(),
                                            //PositionId = postions.FirstOrDefault(c => c.Name != null && pos != null && c.Name.Contains(pos)).Id
                                        };
                                        _context.Players.Add(player);
                                    }
                                    else
                                    {
                                        playerDb.Injured = footBallPlayerRese.Player.Injured;
                                        playerDb.Photo = footBallPlayerRese.Player.Photo;
                                        playerDb.Weight = footBallPlayerRese.Player.Weight;
                                        playerDb.Age = footBallPlayerRese.Player.Age.ToString();
                                        playerDb.GoalsAssists =
                                            footBallPlayerRese.Statistics[0].Goals.Assists?.ToString();
                                        playerDb.GoalsSaves = footBallPlayerRese.Statistics[0].Goals.Saves?.ToString();
                                        playerDb.GoalsTotal = footBallPlayerRese.Statistics[0].Goals.Total?.ToString();
                                        playerDb.Minutes = footBallPlayerRese.Statistics[0].Games.Minutes?.ToString();
                                        playerDb.Position = pos;
                                        playerDb.Rating = footBallPlayerRese.Statistics[0].Games.Rating;
                                        playerDb.PassesTotal =
                                            footBallPlayerRese.Statistics[0].Passes.Total?.ToString();
                                        playerDb.PassesAccuracy =
                                            footBallPlayerRese.Statistics[0].Passes.Accuracy?.ToString();
                                        playerDb.CardsRed = footBallPlayerRese.Statistics[0].Cards.Red?.ToString();
                                        playerDb.CardsYellow =
                                            footBallPlayerRese.Statistics[0].Cards.Yellow?.ToString();
                                        playerDb.CardsYellowRed =
                                            footBallPlayerRese.Statistics[0].Cards.Yellowred?.ToString();
                                        //playerDb.PositionId = postions.FirstOrDefault(c => c.Name != null && pos != null && c.Name.Contains(pos)).Id;
                                        playerDb.ModificationDate = DateTime.UtcNow;
                                        playerDb.IsDeleted = false;
                                    }
                                }
                        }
                    }
                }
        }

        public void AddEditPlayerSquad(League leagueDb, List<Team> teamDb)
        {
            if (leagueDb != null && teamDb.Count > 0)
                //var postions = _context.PlayerPositions.ToList();
                foreach (var footBallTeamsRese in teamDb)
                {

                    var players = GetFootBallPlayerSquad(footBallTeamsRese.IntegrationId);

                    if (players != null)
                    {
                        var playersToDelete = _context.Players.Where(c => c.TeamId == footBallTeamsRese.Id && !c.IsDeleted.Value).ToList();
                        playersToDelete.ForEach(a => a.IsDeleted = true);
                        if (players != null)
                            foreach (var footBallPlayerRese in players.Response.FirstOrDefault(c => c.Players != null).Players.ToList())
                            {
                                var playerDb = _context.Players.FirstOrDefault(c =>
                                    /*!c.IsDeleted.Value &&*/ c.TeamId == footBallTeamsRese.Id &&
                                    c.Team.League.VendorId == 1 &&
                                    !string.IsNullOrWhiteSpace(c.IntegrationId) &&
                                    c.IntegrationId == footBallPlayerRese.Id.ToString());
                                var pos = footBallPlayerRese.Position;
                                if (playerDb == null)
                                {
                                    var player = new Player
                                    {
                                        CreationDate = DateTime.Now,
                                        IntegrationId = footBallPlayerRese.Id.ToString(),
                                        TeamId = footBallTeamsRese.Id,
                                        Name = footBallPlayerRese.Name,
                                        Photo = footBallPlayerRese.Photo,
                                        Age = footBallPlayerRese.Age.ToString(),
                                        Position = pos
                                    };
                                    _context.Players.Add(player);
                                }
                                else
                                {
                                    playerDb.Name = footBallPlayerRese.Name;
                                    playerDb.Photo = footBallPlayerRese.Photo;
                                    playerDb.Age = footBallPlayerRese.Age.ToString();
                                    playerDb.Position = pos;
                                    playerDb.ModificationDate = DateTime.UtcNow;
                                    playerDb.IsDeleted = false;
                                }
                            }
                    }
                }
        }

        public static FootBallResponses.Root GetFootBallPlayer(string team, int startDateYear, string league,
            int page = 1)
        {
            var relativeUrl =
                $"players?team={team}&season={startDateYear}&league={league}&page={page}";
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
                var model = JsonConvert.DeserializeObject<FootBallResponses.Root>(result);
                Thread.Sleep(40);
                return model;
                //return null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex.Message, ex.StackTrace);
                return null;
            }
        }

        public static FootBallResponses.Root GetFootBallPlayerSquad(string team)
        {
            var relativeUrl =
                $"players/squads?team={team}";
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
                var model = JsonConvert.DeserializeObject<FootBallResponses.Root>(result);
                Thread.Sleep(40);
                return model;
                //return null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex.Message, ex.StackTrace);
                return null;
            }
        }

        public void HandlePlayerRating(Match matchDb, Team team1, PlayerRoot matchesStatsReses)
        {
            try
            {
                foreach (var matchStats in matchesStatsReses.Response.Select(c => c.Players).ToList())
                    foreach (var matchPlayerStas in matchStats)
                    {
                        var playerId = _context.Players.FirstOrDefault(c =>
                            !c.IsDeleted.Value && c.IntegrationId != null && matchPlayerStas.Player != null &&
                            c.IntegrationId == matchPlayerStas.Player.Id.ToString());
                        var ratingExist = _context.PlayerMatchRatings.FirstOrDefault(c => !c.IsDeleted.Value &&
                                                                                          c.PlayerId == playerId.Id &&
                                                                                          c.TeamId == team1.Id &&
                                                                                          c.MatchId == matchDb.Id);

                        if (ratingExist == null)
                        {
                            var playerMatchRating = new PlayerMatchRating
                            {
                                MatchId = matchDb.Id,
                                PlayerId = playerId.Id,
                                TeamId = team1.Id,
                                Minutes = matchPlayerStas.Statistics[0].Games.Minutes?.ToString(),
                                Rating = matchPlayerStas.Statistics[0].Games.Rating
                            };

                            _context.PlayerMatchRatings.Add(playerMatchRating);
                        }
                        else
                        {
                            ratingExist.Rating = matchPlayerStas.Statistics[0].Games.Rating;
                            ratingExist.Minutes = matchPlayerStas.Statistics[0].Games.Minutes?.ToString();
                        }
                    }
            }
            catch (Exception ex)
            {
                _context.SaveChanges();
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }
        }

        public void HandlePlayerStats(Match matchDb, Team team1, PlayerRoot matchesStatsReses)
        {
            try
            {
                foreach (var matchStats in matchesStatsReses.Response.Select(c => c.Players).ToList())
                    foreach (var matchPlayerStas in matchStats)
                    {
                        var playerId = _context.Players.FirstOrDefault(c =>
                            !c.IsDeleted.Value && c.IntegrationId == matchPlayerStas.Player.Id.ToString()).Id;
                        var ratingExist = _context.PlayerMatchStats.FirstOrDefault(c => !c.IsDeleted.Value &&
                                                                                        c.PlayerId == playerId &&
                                                                                        c.TeamId == team1.Id &&
                                                                                        c.MatchId == matchDb.Id);

                        if (ratingExist == null)
                        {
                            var playerMatchRating = new PlayerMatchStat
                            {
                                MatchId = matchDb.Id,
                                PlayerId = playerId,
                                TeamId = team1.Id,
                                CardsRed = matchPlayerStas.Statistics[0].Cards.Red?.ToString(),
                                CardsYellow = matchPlayerStas.Statistics[0].Cards.Yellow?.ToString(),
                                DribblesAttempts = matchPlayerStas.Statistics[0].Dribbles.Attempts?.ToString(),
                                DribblesPast = matchPlayerStas.Statistics[0].Dribbles.Past?.ToString(),
                                DribblesSuccess = matchPlayerStas.Statistics[0].Dribbles.Success?.ToString(),
                                DuelsTotal = matchPlayerStas.Statistics[0].Duels.Total?.ToString(),
                                DuelsWon = matchPlayerStas.Statistics[0].Duels.Won?.ToString(),
                                FoulsCommitted = matchPlayerStas.Statistics[0].Fouls.Committed?.ToString(),
                                FoulsDrawn = matchPlayerStas.Statistics[0].Fouls.Drawn?.ToString(),
                                GoalsAssists = matchPlayerStas.Statistics[0].Goals.Assists?.ToString(),
                                GoalSaves = matchPlayerStas.Statistics[0].Goals.Saves?.ToString(),
                                GoalsConceded = matchPlayerStas.Statistics[0].Goals.Conceded?.ToString(),
                                GoalsTotal = matchPlayerStas.Statistics[0].Goals.Total?.ToString(),
                                Offsides = matchPlayerStas.Statistics[0].Offsides?.ToString(),
                                PassesAccuracy = matchPlayerStas.Statistics[0].Passes.Accuracy?.ToString(),
                                PassesKey = matchPlayerStas.Statistics[0].Passes.Key?.ToString(),
                                PassesTotal = matchPlayerStas.Statistics[0].Passes.Total?.ToString(),
                                PenaltyCommitted = matchPlayerStas.Statistics[0].Penalty.Commited?.ToString(),
                                PenaltyMissed = matchPlayerStas.Statistics[0].Penalty.Missed?.ToString(),
                                PenaltySaved = matchPlayerStas.Statistics[0].Penalty.Saved?.ToString(),
                                PenaltyScored = matchPlayerStas.Statistics[0].Penalty.Scored?.ToString(),
                                PenaltyWon = matchPlayerStas.Statistics[0].Penalty.Won?.ToString(),
                                ShotsOn = matchPlayerStas.Statistics[0].Shots.On?.ToString(),
                                ShotsTotal = matchPlayerStas.Statistics[0].Shots.Total?.ToString(),
                                TacklesBlocks = matchPlayerStas.Statistics[0].Tackles.Blocks?.ToString(),
                                TacklesInterceptions = matchPlayerStas.Statistics[0].Tackles.Interceptions?.ToString(),
                                TacklesTotal = matchPlayerStas.Statistics[0].Tackles.Total?.ToString()
                            };

                            _context.PlayerMatchStats.Add(playerMatchRating);
                        }
                        else
                        {
                            ratingExist.CardsRed = matchPlayerStas.Statistics[0].Cards.Red?.ToString();
                            ratingExist.CardsYellow = matchPlayerStas.Statistics[0].Cards.Yellow?.ToString();
                            ratingExist.DribblesAttempts = matchPlayerStas.Statistics[0].Dribbles.Attempts?.ToString();
                            ratingExist.DribblesPast = matchPlayerStas.Statistics[0].Dribbles.Past?.ToString();
                            ratingExist.DribblesSuccess = matchPlayerStas.Statistics[0].Dribbles.Success?.ToString();
                            ratingExist.DuelsTotal = matchPlayerStas.Statistics[0].Duels.Total?.ToString();
                            ratingExist.DuelsWon = matchPlayerStas.Statistics[0].Duels.Won?.ToString();
                            ratingExist.FoulsCommitted = matchPlayerStas.Statistics[0].Fouls.Committed?.ToString();
                            ratingExist.FoulsDrawn = matchPlayerStas.Statistics[0].Fouls.Drawn?.ToString();
                            ratingExist.GoalsAssists = matchPlayerStas.Statistics[0].Goals.Assists?.ToString();
                            ratingExist.GoalSaves = matchPlayerStas.Statistics[0].Goals.Saves?.ToString();
                            ratingExist.GoalsConceded = matchPlayerStas.Statistics[0].Goals.Conceded?.ToString();
                            ratingExist.GoalsTotal = matchPlayerStas.Statistics[0].Goals.Total?.ToString();
                            ratingExist.Offsides = matchPlayerStas.Statistics[0].Offsides?.ToString();
                            ratingExist.PassesAccuracy = matchPlayerStas.Statistics[0].Passes.Accuracy?.ToString();
                            ratingExist.PassesKey = matchPlayerStas.Statistics[0].Passes.Key?.ToString();
                            ratingExist.PassesTotal = matchPlayerStas.Statistics[0].Passes.Total?.ToString();
                            ratingExist.PenaltyCommitted = matchPlayerStas.Statistics[0].Penalty.Commited?.ToString();
                            ratingExist.PenaltyMissed = matchPlayerStas.Statistics[0].Penalty.Missed?.ToString();
                            ratingExist.PenaltySaved = matchPlayerStas.Statistics[0].Penalty.Saved?.ToString();
                            ratingExist.PenaltyScored = matchPlayerStas.Statistics[0].Penalty.Scored?.ToString();
                            ratingExist.PenaltyWon = matchPlayerStas.Statistics[0].Penalty.Won?.ToString();
                            ratingExist.ShotsOn = matchPlayerStas.Statistics[0].Shots.On?.ToString();
                            ratingExist.ShotsTotal = matchPlayerStas.Statistics[0].Shots.Total?.ToString();
                            ratingExist.TacklesBlocks = matchPlayerStas.Statistics[0].Tackles.Blocks?.ToString();
                            ratingExist.TacklesInterceptions =
                                matchPlayerStas.Statistics[0].Tackles.Interceptions?.ToString();
                            ratingExist.TacklesTotal = matchPlayerStas.Statistics[0].Tackles.Total?.ToString();
                        }
                    }
            }
            catch (Exception ex)
            {
                _context.SaveChanges();
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }
        }

        public void HandlePlayerPoints(Match matchDb, Team team, PlayerRoot matchesStatsReses)
        {
            try
            {
                var scoreReverse = -1;
                if (matchDb.Team1Id == team.Id && matchDb.Team2Score != null)
                    scoreReverse = matchDb.Team2Score.Value;
                else if (matchDb.Team2Id == team.Id && matchDb.Team1Score != null) scoreReverse = matchDb.Team1Score.Value;
                foreach (var matchStats in matchesStatsReses.Response.Select(c => c.Players).ToList())
                    HandleStatsAndRatingAndPoints(matchDb, team, matchStats, scoreReverse);
            }
            catch (Exception ex)
            {
                _context.SaveChanges();
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }
        }

        private void HandleStatsAndRatingAndPoints(Match matchDb, Team team, List<PlayerElement> matchStats, int score)
        {
            foreach (var matchPlayerStas in matchStats)
            {
                var player = _context.Players.FirstOrDefault(c =>
                    !c.IsDeleted.Value && c.IntegrationId == matchPlayerStas.Player.Id.ToString()&&c.TeamId==team.Id);

                if (player != null)
                {
                    var points = 0;
                    var CardsRed = matchPlayerStas.Statistics[0].Cards.Red ?? 0;
                    var CardsYellow = matchPlayerStas.Statistics[0].Cards.Yellow ?? 0;
                    var DribblesAttempts = matchPlayerStas.Statistics[0].Dribbles.Attempts?.ToString();
                    var DribblesPast = matchPlayerStas.Statistics[0].Dribbles.Past?.ToString();
                    var DribblesSuccess = matchPlayerStas.Statistics[0].Dribbles.Success?.ToString();
                    var DuelsTotal = matchPlayerStas.Statistics[0].Duels.Total?.ToString();
                    var DuelsWon = matchPlayerStas.Statistics[0].Duels.Won?.ToString();
                    var FoulsCommitted = matchPlayerStas.Statistics[0].Fouls.Committed?.ToString();
                    var FoulsDrawn = matchPlayerStas.Statistics[0].Fouls.Drawn?.ToString();
                    var GoalsAssists = matchPlayerStas.Statistics[0].Goals.Assists ?? 0;
                    var GoalSaves = matchPlayerStas.Statistics[0].Goals.Saves ?? 0;
                    var GoalsConceded = matchPlayerStas.Statistics[0].Goals.Conceded ?? 0;
                    var GoalsTotal = matchPlayerStas.Statistics[0].Goals.Total ?? 0;
                    var Offsides = matchPlayerStas.Statistics[0].Offsides?.ToString();
                    var PassesAccuracy = matchPlayerStas.Statistics[0].Passes.Accuracy ?? 0;
                    var PassesKey = matchPlayerStas.Statistics[0].Passes.Key?.ToString();
                    var PassesTotal = matchPlayerStas.Statistics[0].Passes.Total?.ToString();
                    var PenaltyCommitted = matchPlayerStas.Statistics[0].Penalty.Commited?.ToString();
                    var PenaltyMissed = matchPlayerStas.Statistics[0].Penalty.Missed ?? 0;
                    var PenaltySaved = matchPlayerStas.Statistics[0].Penalty.Saved ?? 0;
                    var PenaltyScored = matchPlayerStas.Statistics[0].Penalty.Scored?.ToString();
                    var PenaltyWon = matchPlayerStas.Statistics[0].Penalty.Won?.ToString();
                    var ShotsOn = matchPlayerStas.Statistics[0].Shots.On ?? 0;
                    var ShotsTotal = matchPlayerStas.Statistics[0].Shots.Total?.ToString();
                    var TacklesBlocks = matchPlayerStas.Statistics[0].Tackles.Blocks?.ToString();
                    var TacklesInterceptions = matchPlayerStas.Statistics[0].Tackles.Interceptions ?? 0;
                    var TacklesTotal = matchPlayerStas.Statistics[0].Tackles.Total?.ToString();
                    var Minutes = matchPlayerStas.Statistics[0].Games.Minutes?.ToString();
                    var Rating = matchPlayerStas.Statistics[0].Games.Rating;

                    if (!string.IsNullOrWhiteSpace(Minutes))
                    {
                        var minuteValue = NullableTryParseInt32(Minutes) ?? 0;
                        if (minuteValue >= 55)
                            points += 2;
                        else if (minuteValue > 0) points += 1;
                    }

                    switch (player.PositionId)
                    {
                        case 4:
                            if (GoalsTotal > 0) points += 8 * (int)GoalsTotal;
                            if (GoalsAssists > 0) points += 5 * (int)GoalsAssists;
                            if (ShotsOn > 0) points += 1 * (((int)ShotsOn) / 2);
                            if (PassesAccuracy > 0) points += 1 * (((int)PassesAccuracy) / 10);
                            if (TacklesInterceptions > 0) points += 1 * (int)TacklesInterceptions;
                            if (CardsYellow == 1) points -= 1;
                            if (CardsRed == 1) points -= 3;
                            break;
                        case 3:
                            if (GoalsTotal > 0) points += 9 * (int)GoalsTotal;
                            if (GoalsAssists > 0) points += 5 * (int)GoalsAssists;
                            if (ShotsOn > 0) points += 1 * (((int)ShotsOn) / 2);
                            if (PassesAccuracy > 0) points += 1 * (((int)PassesAccuracy) / 10);
                            if (score == 0) points += 1;
                            if (TacklesInterceptions > 0) points += 1 * (int)TacklesInterceptions;
                            if (CardsYellow == 1) points -= 1;
                            if (CardsRed == 1) points -= 3;
                            break;
                        case 2:
                            if (GoalsTotal > 0) points += 10 * (int)GoalsTotal;
                            if (GoalsAssists > 0) points += 5 * (int)GoalsAssists;
                            if (ShotsOn > 0) points += 1 * (((int)ShotsOn) / 2);
                            if (PassesAccuracy > 0) points += 1 * (((int)PassesAccuracy) / 10);
                            if (score == 0) points += 5;
                            if (GoalSaves > 0) points += 2 * (((int)GoalSaves) / 3);
                            if (PenaltySaved > 0) points += 9 * (int)PenaltySaved;
                            if (TacklesInterceptions > 0) points += 1 * (int)TacklesInterceptions;
                            if (CardsYellow == 1) points -= 1;
                            if (CardsRed == 1) points -= 3;
                            if (GoalsConceded > 0) points -= 2 * (((int)GoalsConceded) / 2);
                            break;
                        case 1:
                            if (GoalsTotal > 0) points += 10 * (int)GoalsTotal;
                            if (GoalsAssists > 0) points += 5 * (int)GoalsAssists;
                            if (ShotsOn > 0) points += 1 * (((int)ShotsOn) / 2);
                            if (PassesAccuracy > 0) points += 1 * (((int)PassesAccuracy) / 10);
                            if (score == 0) points += 5;
                            if (TacklesInterceptions > 0) points += 1 * (int)TacklesInterceptions;
                            if (CardsYellow == 1) points -= 1;
                            if (CardsRed == 1) points -= 3;
                            if (GoalsConceded > 0) points -= 2 * (((int)GoalsConceded) / 2);
                            if (PenaltyMissed > 0) points -= 3 * (int)PenaltyMissed;
                            break;
                    }

                    var statExist = _context.PlayerMatchStats.FirstOrDefault(c => !c.IsDeleted.Value &&
                                          c.PlayerId == player.Id &&
                                          c.TeamId == team.Id &&
                                          c.MatchId == matchDb.Id);
                    if (statExist == null)
                    {
                        var playerMatchRating = new PlayerMatchStat
                        {
                            MatchId = matchDb.Id,
                            PlayerId = player.Id,
                            TeamId = team.Id,
                            CardsRed = CardsRed.ToString(),
                            CardsYellow = CardsYellow.ToString(),
                            DribblesAttempts = DribblesAttempts,
                            DribblesPast = DribblesPast,
                            DribblesSuccess = DribblesSuccess,
                            DuelsTotal = DuelsTotal,
                            DuelsWon = DuelsWon,
                            FoulsCommitted = FoulsCommitted,
                            FoulsDrawn = FoulsDrawn,
                            GoalsAssists = GoalsAssists.ToString(),
                            GoalSaves = GoalSaves.ToString(),
                            GoalsConceded = GoalsConceded.ToString(),
                            GoalsTotal = GoalsTotal.ToString(),
                            Offsides = Offsides,
                            PassesAccuracy = PassesAccuracy.ToString(),
                            PassesKey = PassesKey,
                            PassesTotal = PassesTotal,
                            PenaltyCommitted = PenaltyCommitted,
                            PenaltyMissed = PenaltyMissed.ToString(),
                            PenaltySaved = PenaltySaved.ToString(),
                            PenaltyScored = PenaltyScored,
                            PenaltyWon = PenaltyWon,
                            ShotsOn = ShotsOn.ToString(),
                            ShotsTotal = ShotsTotal,
                            TacklesBlocks = TacklesBlocks,
                            TacklesInterceptions = TacklesInterceptions.ToString(),
                            TacklesTotal = TacklesTotal,
                            Points = points
                        };

                        _context.PlayerMatchStats.Add(playerMatchRating);
                    }
                    else
                    {
                        statExist.CardsRed = CardsRed.ToString();
                        statExist.CardsYellow = CardsYellow.ToString();
                        statExist.DribblesAttempts = DribblesAttempts;
                        statExist.DribblesPast = DribblesPast;
                        statExist.DribblesSuccess = DribblesSuccess;
                        statExist.DuelsTotal = DuelsTotal;
                        statExist.DuelsWon = DuelsWon;
                        statExist.FoulsCommitted = FoulsCommitted;
                        statExist.FoulsDrawn = FoulsDrawn;
                        statExist.GoalsAssists = GoalsAssists.ToString();
                        statExist.GoalSaves = GoalSaves.ToString();
                        statExist.GoalsConceded = GoalsConceded.ToString();
                        statExist.GoalsTotal = GoalsTotal.ToString();
                        statExist.Offsides = Offsides;
                        statExist.PassesAccuracy = PassesAccuracy.ToString();
                        statExist.PassesKey = PassesKey;
                        statExist.PassesTotal = PassesTotal;
                        statExist.PenaltyCommitted = PenaltyCommitted;
                        statExist.PenaltyMissed = PenaltyMissed.ToString();
                        statExist.PenaltySaved = PenaltySaved.ToString();
                        statExist.PenaltyScored = PenaltyScored;
                        statExist.PenaltyWon = PenaltyWon;
                        statExist.ShotsOn = ShotsOn.ToString();
                        statExist.ShotsTotal = ShotsTotal;
                        statExist.TacklesBlocks = TacklesBlocks;
                        statExist.TacklesInterceptions = TacklesInterceptions.ToString();
                        statExist.TacklesTotal = TacklesTotal;
                        statExist.Points = points;
                    }

                    var ratingExist = _context.PlayerMatchRatings.FirstOrDefault(c => !c.IsDeleted.Value &&
                                                                                      c.PlayerId == player.Id &&
                                                                                      c.TeamId == team.Id &&
                                                                                      c.MatchId == matchDb.Id);

                    if (ratingExist == null)
                    {
                        var playerMatchRating = new PlayerMatchRating
                        {
                            MatchId = matchDb.Id,
                            PlayerId = player.Id,
                            TeamId = team.Id,
                            Minutes = Minutes,
                            Rating = Rating,
                            Points = points
                        };

                        _context.PlayerMatchRatings.Add(playerMatchRating);
                    }
                    else
                    {
                        ratingExist.Rating = Rating;
                        ratingExist.Minutes = Minutes;
                        ratingExist.Points = points;
                    }

                }
            }
        }

        public static FootBallResponses.Root GetFootBallMatch(string league, int startDateYear, bool live,
            bool today = false)
        {
            var relativeUrl = string.Empty;
            if (!live)
            {
                /*,DateTime.Today.Date.ToString("yyyy-MM-dd")*/

                if (!today)
                    relativeUrl =
                        $"fixtures?league={league}&season={startDateYear}";
                else
                    relativeUrl =
                        $"fixtures?league={league}&season={startDateYear}&date={DateTime.Today.Date.ToString("yyyy-MM-dd")}";
            }
            else
            {
                if (!today)
                    relativeUrl =
                        $"fixtures?league={league}&season={startDateYear}&live=all";
                else
                    relativeUrl =
                        $"fixtures?league={league}&season={startDateYear}&live=all&date={DateTime.Today.Date.ToString("yyyy-MM-dd")}";
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
                var model = JsonConvert.DeserializeObject<FootBallResponses.Root>(result);
                Thread.Sleep(30);
                return model;
                //return null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex.Message, ex.StackTrace);
                return null;
            }
        }

        public static PlayerRoot GetFootBallStats(string match, string team)
        {
            var relativeUrl =
                $"fixtures/players?fixture={match}&team={team}";
            var response = UIHelper.CreateRequest(apiUrl, HttpMethod.Get, relativeUrl, apiKey: apiKey);
            //var response = UIHelper.AddProxy(relativeUrl, basicAuthUser: authUser,
            //    basicAuthPassword: authPassword,isProxy:true);
            try
            {
                if (response.StatusCode != HttpStatusCode.OK) return null;
                var result = response.Content.ReadAsStringAsync().Result;

                //var token2 = JToken.Parse(result);
                //var model = JsonConvert.DeserializeObject<List<FootBallMatchesRes>>(token2["response"].ToString());
                var model = JsonConvert.DeserializeObject<PlayerRoot>(result);

                return model;
                //return null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex.Message, ex.StackTrace);
                return null;
            }
        }

        public static int ExtractNumber(string input)
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

        public static int? NullableTryParseInt32(string text)
        {
            int value;
            return int.TryParse(text, out value) ? value : (int?)null;
        }
    }
}