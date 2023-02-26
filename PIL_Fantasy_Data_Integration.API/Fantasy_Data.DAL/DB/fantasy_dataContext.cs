using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PIL_Fantasy_Data_Integration.API.Fantasy_Data.DAL.DB
{
    public partial class fantasy_dataContext : DbContext
    {
        public fantasy_dataContext()
        {
        }

        public fantasy_dataContext(DbContextOptions<fantasy_dataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<CountryLocalize> CountryLocalizes { get; set; }
        public virtual DbSet<FantasyRule> FantasyRules { get; set; }
        public virtual DbSet<FantasyRuleLocalize> FantasyRuleLocalizes { get; set; }
        public virtual DbSet<League> Leagues { get; set; }
        public virtual DbSet<LeagueLocalize> LeagueLocalizes { get; set; }
        public virtual DbSet<Match> Matches { get; set; }
        public virtual DbSet<MatchLocalize> MatchLocalizes { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<PlayerLocalize> PlayerLocalizes { get; set; }
        public virtual DbSet<PlayerMatchRating> PlayerMatchRatings { get; set; }
        public virtual DbSet<PlayerMatchStat> PlayerMatchStats { get; set; }
        public virtual DbSet<PlayerPosition> PlayerPositions { get; set; }
        public virtual DbSet<PositionRule> PositionRules { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<TeamLocalize> TeamLocalizes { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8")
                .UseCollation("utf8_unicode_ci");

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("country");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("ID");

                entity.Property(e => e.Code)
                    .HasMaxLength(150)
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.CreatedBy).HasColumnType("bigint(20)");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.DiffHours).HasColumnType("int(11)");

                entity.Property(e => e.Flag)
                    .HasMaxLength(250)
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.IntegrationId)
                    .HasMaxLength(250)
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.Iso)
                    .HasMaxLength(45)
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasColumnType("bigint(20)");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Show)
                    .IsRequired()
                    .HasDefaultValueSql("b'0'");
            });

            modelBuilder.Entity<CountryLocalize>(entity =>
            {
                entity.ToTable("country_localize");

                entity.HasIndex(e => e.CountryId, "fk_countryLocalize_country_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("ID");

                entity.Property(e => e.CountryId).HasColumnType("bigint(20)");

                entity.Property(e => e.CreatedBy).HasColumnType("bigint(20)");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.LanguageId)
                    .IsRequired()
                    .HasMaxLength(150)
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasColumnType("bigint(20)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150)
                    .UseCollation("utf8_general_ci");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CountryLocalizes)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_countryLocalize_country");
            });

            modelBuilder.Entity<FantasyRule>(entity =>
            {
                entity.ToTable("fantasy_rule");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasColumnType("bigint(20)");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Description).HasMaxLength(150);

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.Max).HasColumnType("int(7)");

                entity.Property(e => e.Message).HasMaxLength(250);

                entity.Property(e => e.Min).HasColumnType("int(7)");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasColumnType("bigint(20)");

                entity.Property(e => e.Title).HasMaxLength(250);
            });

            modelBuilder.Entity<FantasyRuleLocalize>(entity =>
            {
                entity.ToTable("fantasy_rule_localize");

                entity.HasIndex(e => e.FantasyRuleId, "fk_fantasy_rule_localize_fantasy_rule_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasColumnType("bigint(20)");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Description).HasColumnType("mediumtext");

                entity.Property(e => e.FantasyRuleId).HasColumnType("bigint(20)");

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.LanguageId)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasColumnType("bigint(20)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.FantasyRule)
                    .WithMany(p => p.FantasyRuleLocalizes)
                    .HasForeignKey(d => d.FantasyRuleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_fantasy_rule_localize_fantasy_rule");
            });

            modelBuilder.Entity<League>(entity =>
            {
                entity.ToTable("league");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("ID");

                entity.Property(e => e.Color).HasMaxLength(150);

                entity.Property(e => e.CreatedBy).HasColumnType("bigint(20)");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.DefaultImageUrl).HasMaxLength(250);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.IntegrationId).HasMaxLength(250);

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.LeagueCountry).HasMaxLength(150);

                entity.Property(e => e.LeagueCountryCode).HasMaxLength(150);

                entity.Property(e => e.LeagueDisplayOrder).HasColumnType("int(11)");

                entity.Property(e => e.LeagueIsFriendly).HasMaxLength(150);

                entity.Property(e => e.LeagueType).HasMaxLength(150);

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasColumnType("bigint(20)");

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.Property(e => e.Show)
                    .IsRequired()
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.VendorId)
                    .HasColumnType("bigint(20)")
                    .HasDefaultValueSql("'1'");
            });

            modelBuilder.Entity<LeagueLocalize>(entity =>
            {
                entity.ToTable("league_localize");

                entity.HasIndex(e => e.LeagueId, "fk_league_leagueLocaloize_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasColumnType("bigint(20)");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.LanguageId)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.LeagueId).HasColumnType("bigint(20)");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasColumnType("bigint(20)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.HasOne(d => d.League)
                    .WithMany(p => p.LeagueLocalizes)
                    .HasForeignKey(d => d.LeagueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_league_leagueLocaloize");
            });

            modelBuilder.Entity<Match>(entity =>
            {
                entity.ToTable("match");

                entity.HasIndex(e => e.Team1Id, "matchTeam1Id_team_idx");

                entity.HasIndex(e => e.Team2Id, "matchTeam2Id_team_idx");

                entity.HasIndex(e => e.LeagueId, "match_league_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasColumnType("bigint(20)");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Description).HasColumnType("mediumtext");

                entity.Property(e => e.EndDatetime).HasColumnType("datetime");

                entity.Property(e => e.HomeTeamId).HasColumnType("bigint(20)");

                entity.Property(e => e.IntegrationId).HasMaxLength(250);

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.IsSync)
                    .IsRequired()
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.LeagueId).HasColumnType("bigint(20)");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasColumnType("bigint(20)");

                entity.Property(e => e.StartDatetime).HasColumnType("datetime");

                entity.Property(e => e.Status).HasMaxLength(150);

                entity.Property(e => e.Team1Id).HasColumnType("bigint(20)");

                entity.Property(e => e.Team1Score).HasColumnType("int(11)");

                entity.Property(e => e.Team2Id).HasColumnType("bigint(20)");

                entity.Property(e => e.Team2Score).HasColumnType("int(11)");

                entity.Property(e => e.Title).HasMaxLength(250);

                entity.Property(e => e.VendorId)
                    .HasColumnType("bigint(20)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Week).HasColumnType("int(11)");

                entity.HasOne(d => d.League)
                    .WithMany(p => p.Matches)
                    .HasForeignKey(d => d.LeagueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("match_league");

                entity.HasOne(d => d.Team1)
                    .WithMany(p => p.MatchTeam1s)
                    .HasForeignKey(d => d.Team1Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("matchTeam1Id_team");

                entity.HasOne(d => d.Team2)
                    .WithMany(p => p.MatchTeam2s)
                    .HasForeignKey(d => d.Team2Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("matchTeam2Id_team");
            });

            modelBuilder.Entity<MatchLocalize>(entity =>
            {
                entity.ToTable("match_localize");

                entity.HasIndex(e => e.MatchId, "fk_match_localize_match_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasColumnType("bigint(20)");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Description).HasColumnType("mediumtext");

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.LanguageId)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.MatchId).HasColumnType("bigint(20)");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasColumnType("bigint(20)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.Match)
                    .WithMany(p => p.MatchLocalizes)
                    .HasForeignKey(d => d.MatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_match_localize_match");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.ToTable("player");

                entity.HasIndex(e => e.PositionId, "fk_player_position_idx");

                entity.HasIndex(e => e.TeamId, "fk_player_team_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("ID");

                entity.Property(e => e.Age).HasMaxLength(150);

                entity.Property(e => e.CardsRed).HasMaxLength(45);

                entity.Property(e => e.CardsYellow).HasMaxLength(45);

                entity.Property(e => e.CardsYellowRed).HasMaxLength(45);

                entity.Property(e => e.CountryOfBirth).HasMaxLength(150);

                entity.Property(e => e.CreatedBy).HasColumnType("bigint(20)");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.DateOfBirth).HasMaxLength(150);

                entity.Property(e => e.FirstName).HasMaxLength(150);

                entity.Property(e => e.GoalsAssists).HasMaxLength(45);

                entity.Property(e => e.GoalsSaves).HasMaxLength(45);

                entity.Property(e => e.GoalsTotal).HasMaxLength(45);

                entity.Property(e => e.Height).HasMaxLength(150);

                entity.Property(e => e.Injured)
                    .IsRequired()
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.IntegrationId).HasMaxLength(250);

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.LastName).HasMaxLength(150);

                entity.Property(e => e.Minutes).HasMaxLength(45);

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasColumnType("bigint(20)");

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.Property(e => e.Nationality).HasMaxLength(150);

                entity.Property(e => e.PassesAccuracy).HasMaxLength(45);

                entity.Property(e => e.PassesTotal).HasMaxLength(45);

                entity.Property(e => e.Photo)
                    .HasMaxLength(150)
                    .HasColumnName("photo");

                entity.Property(e => e.Position).HasMaxLength(150);

                entity.Property(e => e.PositionId).HasColumnType("bigint(20)");

                entity.Property(e => e.Price).HasMaxLength(45);

                entity.Property(e => e.Rating).HasMaxLength(45);

                entity.Property(e => e.TeamId).HasColumnType("bigint(20)");

                entity.Property(e => e.Weight).HasMaxLength(150);

                entity.HasOne(d => d.PositionNavigation)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.PositionId)
                    .HasConstraintName("fk_player_position");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_player_team");
            });

            modelBuilder.Entity<PlayerLocalize>(entity =>
            {
                entity.ToTable("player_localize");

                entity.HasIndex(e => e.PlayerId, "fk_playerLocalize_player_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("ID");

                entity.Property(e => e.CountryOfBirth).HasMaxLength(150);

                entity.Property(e => e.CreatedBy).HasColumnType("bigint(20)");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.DateOfBirth).HasMaxLength(150);

                entity.Property(e => e.FirstName).HasMaxLength(150);

                entity.Property(e => e.Foot).HasMaxLength(150);

                entity.Property(e => e.Height).HasMaxLength(150);

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.LanguageId)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.LastName).HasMaxLength(150);

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasColumnType("bigint(20)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Nationality).HasMaxLength(150);

                entity.Property(e => e.PlayerId).HasColumnType("bigint(20)");

                entity.Property(e => e.Position).HasMaxLength(150);

                entity.Property(e => e.Type).HasMaxLength(150);

                entity.Property(e => e.Weight).HasMaxLength(150);

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.PlayerLocalizes)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_playerLocalize_player");
            });

            modelBuilder.Entity<PlayerMatchRating>(entity =>
            {
                entity.ToTable("player_match_rating");

                entity.HasIndex(e => e.MatchId, "fk_player_match_rat_match_idx");

                entity.HasIndex(e => e.PlayerId, "fk_player_match_rat_player_idx");

                entity.HasIndex(e => e.TeamId, "fk_player_match_team_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasColumnType("bigint(20)");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IntegrationId).HasMaxLength(250);

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.MatchId).HasColumnType("bigint(20)");

                entity.Property(e => e.Minutes).HasMaxLength(150);

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasColumnType("bigint(20)");

                entity.Property(e => e.PlayerId).HasColumnType("bigint(20)");

                entity.Property(e => e.Points).HasColumnType("bigint(20)");

                entity.Property(e => e.Rating).HasMaxLength(150);

                entity.Property(e => e.TeamId).HasColumnType("bigint(20)");

                entity.HasOne(d => d.Match)
                    .WithMany(p => p.PlayerMatchRatings)
                    .HasForeignKey(d => d.MatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_player_match_rat_match");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.PlayerMatchRatings)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_player_match_rat_player");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.PlayerMatchRatings)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_player_match_rat_team");
            });

            modelBuilder.Entity<PlayerMatchStat>(entity =>
            {
                entity.ToTable("player_match_stats");

                entity.HasIndex(e => e.TeamId, "fk_player_match_rat_team_idx");

                entity.HasIndex(e => e.MatchId, "fk_player_match_stat_match_idx");

                entity.HasIndex(e => e.PlayerId, "fk_player_match_stat_player_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("ID");

                entity.Property(e => e.CardsRed).HasMaxLength(45);

                entity.Property(e => e.CardsYellow).HasMaxLength(45);

                entity.Property(e => e.CreatedBy).HasColumnType("bigint(20)");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.DribblesAttempts).HasMaxLength(45);

                entity.Property(e => e.DribblesPast).HasMaxLength(45);

                entity.Property(e => e.DribblesSuccess).HasMaxLength(45);

                entity.Property(e => e.DuelsTotal).HasMaxLength(45);

                entity.Property(e => e.DuelsWon).HasMaxLength(45);

                entity.Property(e => e.FoulsCommitted).HasMaxLength(45);

                entity.Property(e => e.FoulsDrawn).HasMaxLength(45);

                entity.Property(e => e.GoalSaves).HasMaxLength(150);

                entity.Property(e => e.GoalsAssists).HasMaxLength(150);

                entity.Property(e => e.GoalsConceded).HasMaxLength(150);

                entity.Property(e => e.GoalsTotal).HasMaxLength(150);

                entity.Property(e => e.IntegrationId).HasMaxLength(250);

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.MatchId).HasColumnType("bigint(20)");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasColumnType("bigint(20)");

                entity.Property(e => e.Offsides).HasMaxLength(150);

                entity.Property(e => e.PassesAccuracy).HasMaxLength(150);

                entity.Property(e => e.PassesKey).HasMaxLength(150);

                entity.Property(e => e.PassesTotal).HasMaxLength(150);

                entity.Property(e => e.PenaltyCommitted).HasMaxLength(45);

                entity.Property(e => e.PenaltyMissed).HasMaxLength(45);

                entity.Property(e => e.PenaltySaved).HasMaxLength(45);

                entity.Property(e => e.PenaltyScored).HasMaxLength(45);

                entity.Property(e => e.PenaltyWon).HasMaxLength(45);

                entity.Property(e => e.PlayerId).HasColumnType("bigint(20)");

                entity.Property(e => e.Points).HasColumnType("bigint(20)");

                entity.Property(e => e.ShotsOn).HasMaxLength(150);

                entity.Property(e => e.ShotsTotal).HasMaxLength(150);

                entity.Property(e => e.TacklesBlocks).HasMaxLength(45);

                entity.Property(e => e.TacklesInterceptions).HasMaxLength(45);

                entity.Property(e => e.TacklesTotal).HasMaxLength(150);

                entity.Property(e => e.TeamId).HasColumnType("bigint(20)");

                entity.HasOne(d => d.Match)
                    .WithMany(p => p.PlayerMatchStats)
                    .HasForeignKey(d => d.MatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_player_match_stat_match");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.PlayerMatchStats)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_player_match_stat_player");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.PlayerMatchStats)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_player_match_stat_team");
            });

            modelBuilder.Entity<PlayerPosition>(entity =>
            {
                entity.ToTable("player_position");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("ID");

                entity.Property(e => e.Code).HasMaxLength(150);

                entity.Property(e => e.CreatedBy).HasColumnType("bigint(20)");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasColumnType("bigint(20)");

                entity.Property(e => e.Name).HasMaxLength(250);
            });

            modelBuilder.Entity<PositionRule>(entity =>
            {
                entity.ToTable("position_rule");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasColumnType("bigint(20)");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.Message).HasMaxLength(250);

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasColumnType("bigint(20)");

                entity.Property(e => e.PositionId).HasColumnType("bigint(20)");

                entity.Property(e => e.RuleId).HasColumnType("bigint(20)");
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.ToTable("team");

                entity.HasIndex(e => e.LeagueId, "fk_team_league_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasColumnType("bigint(20)");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.DrawCount).HasColumnType("int(11)");

                entity.Property(e => e.GoalsAgainst)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.GoalsFor)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Group).HasMaxLength(250);

                entity.Property(e => e.ImageUrl)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.IntegrationId).HasMaxLength(250);

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.LeagueId).HasColumnType("bigint(20)");

                entity.Property(e => e.LossCount).HasColumnType("int(11)");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasColumnType("bigint(20)");

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.OrderInLeague).HasColumnType("int(11)");

                entity.Property(e => e.PlayedCount)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Points).HasColumnType("int(11)");

                entity.Property(e => e.WonCount).HasColumnType("int(11)");

                entity.HasOne(d => d.League)
                    .WithMany(p => p.Teams)
                    .HasForeignKey(d => d.LeagueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_team_league");
            });

            modelBuilder.Entity<TeamLocalize>(entity =>
            {
                entity.ToTable("team_localize");

                entity.HasIndex(e => e.TeamId, "fk_teamLocalize_team_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasColumnType("bigint(20)");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.LanguageId)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasColumnType("bigint(20)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.TeamId).HasColumnType("bigint(20)");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TeamLocalizes)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_teamLocalize_team");
            });

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.ToTable("vendor");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasColumnType("bigint(20)");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasColumnType("bigint(20)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
