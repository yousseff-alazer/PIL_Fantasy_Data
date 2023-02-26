using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.API.Services;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.DAL.DB;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.Helpers;

namespace PIL_Fantasy_Data_Integration.API.Fantasy_Data.API.Controllers
{
    //[EnableCors("MyPolicy")]
    [Route("api/fantasydata/[controller]")]
    //[ApiExplorerSettings(IgnoreApi = true)]
    [AllowAnonymous]
    public class FootBallController : Controller
    {
        private readonly FootBallService _footBallService;

        public FootBallController(fantasy_dataContext context)
        {
            _footBallService = new FootBallService(context);
        }

        [Route("HandleCountry")]
        [HttpGet]
        public ActionResult HandleCountry()
        {
            var status = 0;
            try
            {
                var countries = FootBallService.GetFootBallCountry();
                foreach (var countryRes in countries.Response)
                {
                    var countryExist =
                        _footBallService._context.Countries.Any(c =>
                            c.Code == countryRes.Code && c.Name == countryRes.Name);
                    if (!countryExist)
                    {
                        var newCountry = new Country
                        {
                            Name = countryRes.Name,
                            Code = countryRes.Code,
                            Flag = countryRes.Flag
                        };
                        _footBallService._context.Countries.Add(newCountry);
                    }
                }
            }
            catch (Exception ex)
            {
                 status = _footBallService._context.SaveChanges();
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }

             status = _footBallService._context.SaveChanges();
            return Ok(status);
        }

        [Route("HandleLeague")]
        [HttpGet]
        public ActionResult HandleLeague()
        {
            var status = 0;
            try
            {
                var countryNames = _footBallService._context.Countries
                    .Where(c => c.Name != null && !c.IsDeleted.Value && c.Show.Value)
                    ?.Select(c => c.Name).ToList();
                foreach (var countryName in countryNames)
                {
                    var leagueFootBall = FootBallService.GetFootBallLeague(countryName);
                    foreach (var footBallLeague in leagueFootBall.Response)
                    {
                        var leagueId = footBallLeague.League.Id.ToString();
                        var leagueStart = Convert.ToDateTime(footBallLeague.Seasons[0].Start);
                        var leagueEnd = Convert.ToDateTime(footBallLeague.Seasons[0].End);
                        var leagueDb = _footBallService._context.Leagues.FirstOrDefault(c =>
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
                                VendorId = 1, //football vendor id
                                StartDate = leagueStart,
                                Name = footBallLeague.League.Name
                            };
                            _footBallService._context.Leagues.Add(league);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                 status = _footBallService._context.SaveChanges();
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }

             status = _footBallService._context.SaveChanges();
            return Ok(status);
        }

        [Route("HandleTeamStanding")]
        [HttpGet]
        public ActionResult HandleTeamStanding(bool onmatches = false)
        {
            var status = 0;
            try
            {
                var leagueDbs = _footBallService._context.Leagues.Where(c =>
                    !c.IsDeleted.Value && c.VendorId == 1 && c.IntegrationId != null && c.Show.Value).ToList();
                foreach (var leagueDb in leagueDbs)
                    if (leagueDb != null)
                    {
                        if (onmatches)
                        {
                            var liveMatches = _footBallService._context.Matches.Where(c => c.LeagueId == leagueDb.Id
                                                                                           && (c.EndDatetime == null ||
                                                                                               (c.EndDatetime != null &&
                                                                                               c.EndDatetime.Value.AddMinutes(154) >=
                                                                                               DateTime.UtcNow))
                                                                                           && c.StartDatetime <
                                                                                           DateTime.UtcNow).ToList();
                            foreach (var match in liveMatches)
                            {
                                var team1Reses = FootBallService.GetFootBallTeamStanding(leagueDb.IntegrationId,
                                    leagueDb.StartDate.Year, match.Team1.IntegrationId);
                                var team2Reses = FootBallService.GetFootBallTeamStanding(leagueDb.IntegrationId,
                                    leagueDb.StartDate.Year, match.Team2.IntegrationId);
                                _footBallService.AddUpdateTeams(leagueDb, team1Reses.Response[0]);
                                _footBallService.AddUpdateTeams(leagueDb, team2Reses.Response[0]);
                            }
                        }
                        else
                        {
                            var teamsReses =
                                FootBallService.GetFootBallTeamStanding(leagueDb.IntegrationId,
                                    leagueDb.StartDate.Year);
                            //var xx = teamsReses.Response[0];
                            _footBallService.AddUpdateTeams(leagueDb, teamsReses.Response[0]);
                        }
                    }
            }
            catch (Exception ex)
            {
                status = _footBallService._context.SaveChanges();
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }

             status = _footBallService._context.SaveChanges();
            return Ok(status);
        }

        [Route("HandlePlayers")]
        [HttpGet]
        public ActionResult HandlePlayers(bool onmatches = false)
        {
            var status = 0;
            try
            {
                var leagueDbs = _footBallService._context.Leagues.Where(c =>
                    !c.IsDeleted.Value && c.VendorId == 1 && c.IntegrationId != null && c.Show.Value).ToList();
                foreach (var leagueDb in leagueDbs)
                    if (onmatches)
                    {
                        var liveMatches = _footBallService._context.Matches.Where(c => c.LeagueId == leagueDb.Id
                                                                                           && (c.EndDatetime == null ||
                                                                                               (c.EndDatetime != null &&
                                                                                               c.EndDatetime.Value.AddMinutes(154) >=
                                                                                               DateTime.UtcNow))
                                                                                           && c.StartDatetime <
                                                                                           DateTime.UtcNow).ToList();
                        foreach (var match in liveMatches)
                        {
                            var team1Db = _footBallService._context.Teams.Where(c =>
                                !c.IsDeleted.Value && c.League != null && c.League.VendorId == 1 &&
                                !string.IsNullOrWhiteSpace(c.IntegrationId) && c.LeagueId == leagueDb.Id &&
                                c.Id == match.Team1Id).ToList();
                            _footBallService.AddEditPlayer(leagueDb, team1Db);
                            var team2Db = _footBallService._context.Teams.Where(c =>
                                !c.IsDeleted.Value && c.League != null && c.League.VendorId == 1 &&
                                !string.IsNullOrWhiteSpace(c.IntegrationId) && c.LeagueId == leagueDb.Id &&
                                c.Id == match.Team1Id).ToList();
                            _footBallService.AddEditPlayer(leagueDb, team2Db);
                        }
                    }
                    else
                    {
    //                    var teamDb2 = _footBallService._context.Teams.Where(c =>
    //!c.IsDeleted.Value && c.League != null && c.League.VendorId == 1 &&
    //!string.IsNullOrWhiteSpace(c.IntegrationId) && c.LeagueId == leagueDb.Id).ToList();
    //                    _footBallService.AddEditPlayer(leagueDb, teamDb2);
    //                    _footBallService._context.SaveChanges();
                        var teamDb = _footBallService._context.Teams.Where(c =>
                            !c.IsDeleted.Value && c.League != null && c.League.VendorId == 1 &&
                            !string.IsNullOrWhiteSpace(c.IntegrationId) && c.LeagueId == leagueDb.Id).ToList();
                        _footBallService.AddEditPlayer(leagueDb, teamDb);
                    }
            }
            catch (Exception ex)
            {
                status = _footBallService._context.SaveChanges();
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }

             status = _footBallService._context.SaveChanges();
            return Ok(status);
        }

        [Route("HandleMatches")]
        [HttpGet]
        public ActionResult HandleMatches(bool live = false, bool Today = false)
        {
            var status = 0;
            try
            {
                var leagueDbs = _footBallService._context.Leagues.Where(c =>
                    !c.IsDeleted.Value && c.VendorId == 1 && c.IntegrationId != null && c.Show.Value ).ToList();

                foreach (var leagueDb in leagueDbs)
                    if (leagueDb != null)
                    {
                        var matchesReses = FootBallService.GetFootBallMatch(leagueDb.IntegrationId,
                            leagueDb.StartDate.Year, live, Today);
                        foreach (var footBallMatchesRese in matchesReses.Response)
                        {
                            //HandleMatchTeams(footBallMatchesRese, leagueDb);
                            var matchDb = _footBallService._context.Matches.FirstOrDefault(c =>
                                !c.IsDeleted.Value && c.League != null && c.League.VendorId == 1 &&
                                !string.IsNullOrWhiteSpace(c.IntegrationId) &&
                                c.IntegrationId == footBallMatchesRese.Fixture.Id.ToString()
                                && c.LeagueId == leagueDb.Id);
                            var team1 = _footBallService._context.Teams.FirstOrDefault(c => !c.IsDeleted.Value &&
                                                                                            c.IntegrationId ==
                                                                                            footBallMatchesRese.Teams
                                                                                                .Home.Id.ToString() &&
                                                                                            c.LeagueId == leagueDb.Id);
                            var team2 = _footBallService._context.Teams.FirstOrDefault(c => !c.IsDeleted.Value &&
                                                                                            c.IntegrationId ==
                                                                                            footBallMatchesRese.Teams
                                                                                                .Away.Id.ToString() &&
                                                                                            c.LeagueId == leagueDb.Id);
                            if (matchDb == null && team1 != null && team2 != null)
                            {
                                var match = new Match
                                {
                                    CreationDate = DateTime.UtcNow,
                                    IntegrationId = footBallMatchesRese.Fixture.Id.ToString(),
                                    LeagueId = leagueDb.Id,
                                    StartDatetime = footBallMatchesRese.Fixture.Date.ToUniversalTime(),
                                    Status = footBallMatchesRese.Fixture.Status.Long,
                                    Week = FootBallService.ExtractNumber(footBallMatchesRese.League.Round),
                                    Team1Id = team1?.Id ?? 0,
                                    Team2Id = team2?.Id ?? 0,
                                    HomeTeamId = team1?.Id ?? 0,
                                    Team1Score = footBallMatchesRese.Goals.Home,
                                    Team2Score = footBallMatchesRese.Goals.Away,
                                    VendorId = 1
                                };
                                if (footBallMatchesRese.Fixture.Status.Elapsed != null &&
                                    footBallMatchesRese.Fixture.Status.Long == "Match Finished" && match.EndDatetime == null)
                                {
                                    var elapsed = footBallMatchesRese.Fixture.Status.Elapsed.Value;
                                    var end = footBallMatchesRese.Fixture.Date.ToUniversalTime().AddMinutes(elapsed).AddMinutes(19);
                                    match.EndDatetime = end;
                                }

                                _footBallService._context.Matches.Add(match);
                            }
                            else if (team1 != null && team2 != null)
                            {
                                matchDb.StartDatetime = footBallMatchesRese.Fixture.Date.ToUniversalTime();
                                matchDb.Status = footBallMatchesRese.Fixture.Status.Long;
                                matchDb.Week = FootBallService.ExtractNumber(footBallMatchesRese.League.Round);
                                matchDb.Team1Id = team1?.Id ?? 0;
                                matchDb.Team2Id = team2?.Id ?? 0;
                                matchDb.HomeTeamId = team1?.Id ?? 0;
                                matchDb.Team1Score = footBallMatchesRese.Goals.Home;
                                matchDb.Team2Score = footBallMatchesRese.Goals.Away;
                                matchDb.ModificationDate = DateTime.UtcNow;
                                if (footBallMatchesRese.Fixture.Status.Elapsed != null &&
                                    footBallMatchesRese.Fixture.Status.Long == "Match Finished" && matchDb.EndDatetime == null)
                                {
                                    var elapsed = footBallMatchesRese.Fixture.Status.Elapsed.Value;
                                    var end = footBallMatchesRese.Fixture.Date.ToUniversalTime().AddMinutes(elapsed).AddMinutes(19);
                                    matchDb.EndDatetime = end;
                                }
                            }
                        }
                    }
            }
            catch (Exception ex)
            {
                 status = _footBallService._context.SaveChanges();
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }

            status = _footBallService._context.SaveChanges();
            return Ok(status);
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("HandleMatchesRating")]
        [HttpGet]
        public ActionResult HandleMatchesRating(bool live = false, bool Today = true)
        {
            var status = 0;
            try
            {
                var leagueDbs = _footBallService._context.Leagues.Where(c =>
                    !c.IsDeleted.Value && c.VendorId == 1 && c.IntegrationId != null && c.Show.Value ).ToList();

                foreach (var leagueDb in leagueDbs)
                    if (leagueDb != null)
                    {
                        var matchesReses = FootBallService.GetFootBallMatch(leagueDb.IntegrationId,
                            leagueDb.StartDate.Year, live, Today);
                        foreach (var footBallMatchesRese in matchesReses.Response)
                        {
                            //HandleMatchTeams(footBallMatchesRese, leagueDb);
                            var matchDb = _footBallService._context.Matches.FirstOrDefault(c =>
                                !c.IsDeleted.Value && c.League != null && c.League.VendorId == 1 &&
                                !string.IsNullOrWhiteSpace(c.IntegrationId) &&
                                c.IntegrationId == footBallMatchesRese.Fixture.Id.ToString()
                                && c.LeagueId == leagueDb.Id && c.EndDatetime != null);
                            var team1 = _footBallService._context.Teams.FirstOrDefault(c => !c.IsDeleted.Value &&
                                                                                            c.IntegrationId ==
                                                                                            footBallMatchesRese.Teams
                                                                                                .Home.Id.ToString() &&
                                                                                            c.LeagueId == leagueDb.Id);
                            var team2 = _footBallService._context.Teams.FirstOrDefault(c => !c.IsDeleted.Value &&
                                                                                            c.IntegrationId ==
                                                                                            footBallMatchesRese.Teams
                                                                                                .Away.Id.ToString() &&
                                                                                            c.LeagueId == leagueDb.Id);
                            if (matchDb != null && team1 != null && team2 != null)
                            {
                                var matchesStatsReses =
                                    FootBallService.GetFootBallStats(matchDb.IntegrationId, team1.IntegrationId);
                                _footBallService.HandlePlayerRating(matchDb, team1, matchesStatsReses);
                                var matchesStatsReses2 =
                                    FootBallService.GetFootBallStats(matchDb.IntegrationId, team2.IntegrationId);
                                _footBallService.HandlePlayerRating(matchDb, team2, matchesStatsReses2);
                            }
                        }
                    }
            }
            catch (Exception ex)
            {
                status = _footBallService._context.SaveChanges();
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }

            status = _footBallService._context.SaveChanges();

            return Ok(status);
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("HandleMatchesStats")]
        [HttpGet]
        public ActionResult HandleMatchesStats(bool live = false, bool Today = true)
        {
            var status = 0;
            try
            {
                var leagueDbs = _footBallService._context.Leagues.Where(c =>
                    !c.IsDeleted.Value && c.VendorId == 1 && c.IntegrationId != null && c.Show.Value ).ToList();

                foreach (var leagueDb in leagueDbs)
                    if (leagueDb != null)
                    {
                        var matchesReses = FootBallService.GetFootBallMatch(leagueDb.IntegrationId,
                            leagueDb.StartDate.Year, live, Today);
                        foreach (var footBallMatchesRese in matchesReses.Response)
                        {
                            //HandleMatchTeams(footBallMatchesRese, leagueDb);
                            var matchDb = _footBallService._context.Matches.FirstOrDefault(c =>
                                !c.IsDeleted.Value && c.League != null && c.League.VendorId == 1 &&
                                !string.IsNullOrWhiteSpace(c.IntegrationId) &&
                                c.IntegrationId == footBallMatchesRese.Fixture.Id.ToString()
                                && c.LeagueId == leagueDb.Id && c.EndDatetime != null &&
                                c.EndDatetime >= DateTime.UtcNow && c.EndDatetime <= DateTime.UtcNow.AddMinutes(154));
                            var team1 = _footBallService._context.Teams.FirstOrDefault(c => !c.IsDeleted.Value &&
                                                                                            c.IntegrationId ==
                                                                                            footBallMatchesRese.Teams
                                                                                                .Home.Id.ToString() &&
                                                                                            c.LeagueId == leagueDb.Id);
                            var team2 = _footBallService._context.Teams.FirstOrDefault(c => !c.IsDeleted.Value &&
                                                                                            c.IntegrationId ==
                                                                                            footBallMatchesRese.Teams
                                                                                                .Away.Id.ToString() &&
                                                                                            c.LeagueId == leagueDb.Id);
                            if (matchDb != null && team1 != null && team2 != null)
                            {
                                var matchesStatsReses =
                                    FootBallService.GetFootBallStats(matchDb.IntegrationId, team1.IntegrationId);
                                _footBallService.HandlePlayerStats(matchDb, team1, matchesStatsReses);
                                var matchesStatsReses2 =
                                    FootBallService.GetFootBallStats(matchDb.IntegrationId, team2.IntegrationId);
                                _footBallService.HandlePlayerStats(matchDb, team2, matchesStatsReses2);
                            }
                        }
                    }
            }
            catch (Exception ex)
            {
                status = _footBallService._context.SaveChanges();
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }

             status = _footBallService._context.SaveChanges();

            return Ok(status);
        }

        [Route("HandleMatchesPoints")]
        [HttpGet]
        public ActionResult HandleMatchesPoints()
        {
            var status = 0;
            try
            {
                //var x =
                //                  FootBallService.GetFootBallStats("710561", "33");
                //var matchDbx = _footBallService._context.Matches.FirstOrDefault(c =>c.IntegrationId== "710561");
                //var teamx = _footBallService._context.Teams.FirstOrDefault(c => c.IntegrationId == "33");
                //_footBallService.HandlePlayerPoints(matchDbx, teamx, x);
                var leagueDbs = _footBallService._context.Leagues.Where(c =>
                    !c.IsDeleted.Value && c.VendorId == 1 && c.IntegrationId != null && c.Show.Value).ToList();

                foreach (var leagueDb in leagueDbs)
                    if (leagueDb != null)
                    {
                        var matchsDb = _footBallService._context.Matches.Where(c =>
                            !c.IsDeleted.Value && c.League != null && c.League.VendorId == 1
                            && c.LeagueId == leagueDb.Id && c.EndDatetime != null && c.EndDatetime.Value.AddHours(24) >= DateTime.UtcNow &&!c.IsSync.Value).ToList();
                        foreach (var matchDb in matchsDb)
                        {
                            var team1 = _footBallService._context.Teams.FirstOrDefault(c => !c.IsDeleted.Value &&
                                                                                            c.Id == matchDb.Team1Id &&
                                                                                            c.LeagueId == leagueDb.Id);
                            var team2 = _footBallService._context.Teams.FirstOrDefault(c =>
                                !c.IsDeleted.Value && c.Id == matchDb.Team2Id &&
                                c.LeagueId == leagueDb.Id);

                            if (matchDb != null && team1 != null && team2 != null)
                            {
                                var matchesStatsReses =
                                    FootBallService.GetFootBallStats(matchDb.IntegrationId, team1.IntegrationId);
                                
                                if (matchesStatsReses != null)
                                {
                                    _footBallService.HandlePlayerPoints(matchDb, team1, matchesStatsReses);
                                }
                                    var matchesStatsReses2 =
                                    FootBallService.GetFootBallStats(matchDb.IntegrationId, team2.IntegrationId);
                                if (matchesStatsReses2!=null)
                                {
                                    _footBallService.HandlePlayerPoints(matchDb, team2, matchesStatsReses2);
                                }
                                
                            }
                        }
                    }
            }
            catch (Exception ex)
            {
                status = _footBallService._context.SaveChanges();
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }

             status = _footBallService._context.SaveChanges();

            return Ok(status);
        }

        [Route("HandlePlayersPosition")]
        [HttpGet]
        public ActionResult HandlePlayersPosition()
        {
            var status =0;
            try
            {
                var leagueIds = _footBallService._context.Leagues.Where(c =>
                        !c.IsDeleted.Value && c.VendorId == 1 && c.IntegrationId != null && c.Show.Value )
                    .Select(c => c.Id)
                    .ToList();
                var postions = _footBallService._context.PlayerPositions.ToList();
                foreach (var leagueId in leagueIds)
                {
                    var teamIds = _footBallService._context.Teams.Where(c =>
                            !c.IsDeleted.Value && c.League != null && c.League.VendorId == 1 &&
                            !string.IsNullOrWhiteSpace(c.IntegrationId) && c.LeagueId == leagueId).Select(c => c.Id)
                        .ToList();
                    foreach (var teamId in teamIds)
                    {
                        var players = _footBallService._context.Players.Where(c => c.TeamId == teamId).ToList();
                        foreach (var player in players)
                        {
                            var posId = postions.FirstOrDefault(c =>
                                c.Name != null && player != null && c.Name.Contains(player.Position)).Id;
                            player.PositionId = posId;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                status = _footBallService._context.SaveChanges();
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }

             status = _footBallService._context.SaveChanges();
            return Ok(status);
        }

        [Route("HandlePlayersSquad")]
        [HttpGet]
        public ActionResult HandlePlayersSquad()
        {
            var status = 0;
            try
            {
                var leagueDbs = _footBallService._context.Leagues.Where(c =>
                    !c.IsDeleted.Value && c.VendorId == 1 && c.IntegrationId != null && c.Show.Value).ToList();
                foreach (var leagueDb in leagueDbs)
                {
                    var teamDb = _footBallService._context.Teams.Where(c =>
                        !c.IsDeleted.Value && c.League != null && c.League.VendorId == 1 &&
                        !string.IsNullOrWhiteSpace(c.IntegrationId) && c.LeagueId == leagueDb.Id).ToList();
                    _footBallService.AddEditPlayerSquad(leagueDb, teamDb);
                }

            }
            catch (Exception ex)
            {
                status = _footBallService._context.SaveChanges();
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }

            status = _footBallService._context.SaveChanges();
            return Ok(status);
        }

        //private void HandleMatchTeams(FootBallMatchesRes footBallMatchesRese, League leagueDb)
        //{
        //    if (!_context.Teams.Any(c => !c.IsDeleted.Value &&
        //        c.IntegrationId == footBallMatchesRese.Teams.Home.Id.ToString() &&
        //        c.LeagueId == leagueDb.Id))
        //    {
        //        var team = new Team
        //        {
        //            CreationDate = DateTime.Now,
        //            IntegrationId = footBallMatchesRese.Teams.Home.Id.ToString(),
        //            LeagueId = leagueDb.Id,
        //            ImageUrl = footBallMatchesRese.Teams.Home.Logo
        //        };
        //        _context.Teams.Add(team);
        //    }

        //    if (!_context.Teams.Any(c => !c.IsDeleted.Value &&
        //        c.IntegrationId == footBallMatchesRese.Teams.Away.Id.ToString() && c.LeagueId == leagueDb.Id))
        //    {
        //        var team = new Team
        //        {
        //            CreationDate = DateTime.Now,
        //            IntegrationId = footBallMatchesRese.Teams.Away.Id.ToString(),
        //            LeagueId = leagueDb.Id,
        //            ImageUrl = footBallMatchesRese.Teams.Home.Logo
        //        };
        //        _context.Teams.Add(team);
        //    }
        //}
    }
}