-- phpMyAdmin SQL Dump
-- version 4.7.9
-- https://www.phpmyadmin.net/
--
-- Host: fantasy_db:3310
-- Generation Time: Aug 23, 2021 at 04:03 PM
-- Server version: 5.7.22
-- PHP Version: 7.2.2

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `fantasy_data`
--

-- --------------------------------------------------------

--
-- Table structure for table `country`
--

CREATE TABLE `country` (
  `ID` bigint(20) NOT NULL,
  `Iso` varchar(45) CHARACTER SET utf8 NOT NULL,
  `Code` varchar(150) CHARACTER SET utf8 DEFAULT NULL,
  `CreatedBy` bigint(20) DEFAULT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `IsDeleted` bit(1) NOT NULL DEFAULT b'0',
  `ModificationDate` datetime DEFAULT NULL,
  `ModifiedBy` bigint(20) DEFAULT NULL,
  `IntegrationId` varchar(250) CHARACTER SET utf8 DEFAULT NULL,
  `DiffHours` int(11) DEFAULT NULL,
  `Name` varchar(250) CHARACTER SET utf8 DEFAULT NULL,
  `Flag` varchar(250) CHARACTER SET utf8 DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `country_localize`
--

CREATE TABLE `country_localize` (
  `ID` bigint(20) NOT NULL,
  `CountryId` bigint(20) NOT NULL,
  `LanguageId` varchar(150) CHARACTER SET utf8 NOT NULL,
  `Name` varchar(150) CHARACTER SET utf8 NOT NULL,
  `CreatedBy` bigint(20) DEFAULT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `IsDeleted` bit(1) NOT NULL DEFAULT b'0',
  `ModificationDate` datetime DEFAULT NULL,
  `ModifiedBy` bigint(20) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `league`
--

CREATE TABLE `league` (
  `ID` bigint(20) NOT NULL,
  `CreatedBy` bigint(20) DEFAULT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `IsDeleted` bit(1) NOT NULL DEFAULT b'0',
  `ModificationDate` datetime DEFAULT NULL,
  `ModifiedBy` bigint(20) DEFAULT NULL,
  `StartDate` datetime NOT NULL,
  `EndDate` datetime NOT NULL,
  `DefaultImageUrl` varchar(250) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Color` varchar(150) COLLATE utf8_unicode_ci DEFAULT NULL,
  `IntegrationId` varchar(250) COLLATE utf8_unicode_ci DEFAULT NULL,
  `LeagueCountry` varchar(150) COLLATE utf8_unicode_ci DEFAULT NULL,
  `LeagueCountryCode` varchar(150) COLLATE utf8_unicode_ci DEFAULT NULL,
  `LeagueDisplayOrder` int(11) DEFAULT NULL,
  `LeagueIsFriendly` varchar(150) COLLATE utf8_unicode_ci DEFAULT NULL,
  `LeagueType` varchar(150) COLLATE utf8_unicode_ci DEFAULT NULL,
  `VendorId` bigint(20) DEFAULT '1'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `league_localize`
--

CREATE TABLE `league_localize` (
  `ID` bigint(20) NOT NULL,
  `LeagueId` bigint(20) NOT NULL,
  `LanguageId` varchar(150) COLLATE utf8_unicode_ci NOT NULL,
  `Name` varchar(150) COLLATE utf8_unicode_ci NOT NULL,
  `CreatedBy` bigint(20) DEFAULT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `IsDeleted` bit(1) NOT NULL DEFAULT b'0',
  `ModificationDate` datetime DEFAULT NULL,
  `ModifiedBy` bigint(20) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `match`
--

CREATE TABLE `match` (
  `ID` bigint(20) NOT NULL,
  `CreatedBy` bigint(20) DEFAULT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `IsDeleted` bit(1) NOT NULL DEFAULT b'0',
  `ModificationDate` datetime DEFAULT NULL,
  `ModifiedBy` bigint(20) DEFAULT NULL,
  `Team1Id` bigint(20) NOT NULL,
  `Team2Id` bigint(20) NOT NULL,
  `StartDatetime` datetime NOT NULL,
  `EndDatetime` datetime DEFAULT NULL,
  `Team1Score` int(11) DEFAULT NULL,
  `Team2Score` int(11) DEFAULT NULL,
  `IntegrationId` varchar(250) COLLATE utf8_unicode_ci DEFAULT NULL,
  `HomeTeamId` bigint(20) NOT NULL,
  `Week` int(11) NOT NULL,
  `LeagueId` bigint(20) NOT NULL,
  `Status` varchar(150) COLLATE utf8_unicode_ci DEFAULT NULL,
  `VendorId` bigint(20) NOT NULL DEFAULT '1'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `match_localize`
--

CREATE TABLE `match_localize` (
  `ID` bigint(20) NOT NULL,
  `MatchId` bigint(20) NOT NULL,
  `LanguageId` varchar(150) COLLATE utf8_unicode_ci NOT NULL,
  `Title` varchar(250) COLLATE utf8_unicode_ci NOT NULL,
  `Description` mediumtext COLLATE utf8_unicode_ci,
  `CreatedBy` bigint(20) DEFAULT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `IsDeleted` bit(1) NOT NULL DEFAULT b'0',
  `ModificationDate` datetime DEFAULT NULL,
  `ModifiedBy` bigint(20) DEFAULT NULL,
  `MatchFacts` longtext COLLATE utf8_unicode_ci
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `player`
--

CREATE TABLE `player` (
  `ID` bigint(20) NOT NULL,
  `CreatedBy` bigint(20) DEFAULT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `IsDeleted` bit(1) NOT NULL DEFAULT b'0',
  `ModificationDate` datetime DEFAULT NULL,
  `ModifiedBy` bigint(20) DEFAULT NULL,
  `TeamId` bigint(20) NOT NULL,
  `Injured` bit(1) NOT NULL DEFAULT b'0',
  `IntegrationId` varchar(250) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Nationality` varchar(150) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Position` varchar(150) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Type` varchar(150) COLLATE utf8_unicode_ci DEFAULT NULL,
  `DateOfBirth` varchar(150) COLLATE utf8_unicode_ci DEFAULT NULL,
  `CountryOfBirth` varchar(150) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Height` varchar(150) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Weight` varchar(150) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Foot` varchar(150) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ShirtNumber` varchar(150) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Name` varchar(150) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FirstName` varchar(150) COLLATE utf8_unicode_ci DEFAULT NULL,
  `LastName` varchar(150) COLLATE utf8_unicode_ci DEFAULT NULL,
  `photo` varchar(150) COLLATE utf8_unicode_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `player_localize`
--

CREATE TABLE `player_localize` (
  `ID` bigint(20) NOT NULL,
  `PlayerId` bigint(20) NOT NULL,
  `LanguageId` varchar(150) COLLATE utf8_unicode_ci NOT NULL,
  `Name` varchar(150) COLLATE utf8_unicode_ci NOT NULL,
  `CreatedBy` bigint(20) DEFAULT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `IsDeleted` bit(1) NOT NULL DEFAULT b'0',
  `ModificationDate` datetime DEFAULT NULL,
  `ModifiedBy` bigint(20) DEFAULT NULL,
  `Nationality` varchar(150) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Position` varchar(150) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Type` varchar(150) COLLATE utf8_unicode_ci DEFAULT NULL,
  `DateOfBirth` varchar(150) COLLATE utf8_unicode_ci DEFAULT NULL,
  `CountryOfBirth` varchar(150) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Height` varchar(150) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Weight` varchar(150) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Foot` varchar(150) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FirstName` varchar(150) COLLATE utf8_unicode_ci DEFAULT NULL,
  `LastName` varchar(150) COLLATE utf8_unicode_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `team`
--

CREATE TABLE `team` (
  `ID` bigint(20) NOT NULL,
  `CreatedBy` bigint(20) DEFAULT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `IsDeleted` bit(1) NOT NULL DEFAULT b'0',
  `ModificationDate` datetime DEFAULT NULL,
  `ModifiedBy` bigint(20) DEFAULT NULL,
  `LeagueId` bigint(20) NOT NULL,
  `Points` int(11) NOT NULL DEFAULT '0',
  `ImageUrl` varchar(250) COLLATE utf8_unicode_ci NOT NULL,
  `OrderInLeague` int(11) NOT NULL DEFAULT '0',
  `WonCount` int(11) NOT NULL DEFAULT '0',
  `LossCount` int(11) NOT NULL DEFAULT '0',
  `DrawCount` int(11) NOT NULL DEFAULT '0',
  `Group` char(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  `IntegrationId` varchar(250) COLLATE utf8_unicode_ci DEFAULT NULL,
  `PlayedCount` int(11) DEFAULT '0',
  `GoalsFor` int(11) DEFAULT '0',
  `GoalsAgainst` int(11) DEFAULT '0',
  `Name` varchar(250) COLLATE utf8_unicode_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `team_localize`
--

CREATE TABLE `team_localize` (
  `ID` bigint(20) NOT NULL,
  `TeamId` bigint(20) NOT NULL,
  `LanguageId` varchar(150) COLLATE utf8_unicode_ci NOT NULL,
  `Name` varchar(150) COLLATE utf8_unicode_ci NOT NULL,
  `CreatedBy` bigint(20) DEFAULT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `IsDeleted` bit(1) NOT NULL DEFAULT b'0',
  `ModificationDate` datetime DEFAULT NULL,
  `ModifiedBy` bigint(20) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `vendor`
--

CREATE TABLE `vendor` (
  `ID` bigint(20) NOT NULL,
  `Name` varchar(150) COLLATE utf8_unicode_ci NOT NULL,
  `CreatedBy` bigint(20) DEFAULT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `IsDeleted` bit(1) NOT NULL DEFAULT b'0',
  `ModificationDate` datetime DEFAULT NULL,
  `ModifiedBy` bigint(20) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `country`
--
ALTER TABLE `country`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `country_localize`
--
ALTER TABLE `country_localize`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `fk_countryLocalize_country_idx` (`CountryId`);

--
-- Indexes for table `league`
--
ALTER TABLE `league`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `league_localize`
--
ALTER TABLE `league_localize`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `fk_league_leagueLocaloize_idx` (`LeagueId`);

--
-- Indexes for table `match`
--
ALTER TABLE `match`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `matchTeam1Id_team_idx` (`Team1Id`),
  ADD KEY `matchTeam2Id_team_idx` (`Team2Id`),
  ADD KEY `match_league_idx` (`LeagueId`);

--
-- Indexes for table `match_localize`
--
ALTER TABLE `match_localize`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `fk_match_localize_match_idx` (`MatchId`);

--
-- Indexes for table `player`
--
ALTER TABLE `player`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `fk_player_team_idx` (`TeamId`);

--
-- Indexes for table `player_localize`
--
ALTER TABLE `player_localize`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `fk_playerLocalize_player_idx` (`PlayerId`);

--
-- Indexes for table `team`
--
ALTER TABLE `team`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `fk_team_league_idx` (`LeagueId`);

--
-- Indexes for table `team_localize`
--
ALTER TABLE `team_localize`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `fk_teamLocalize_team_idx` (`TeamId`);

--
-- Indexes for table `vendor`
--
ALTER TABLE `vendor`
  ADD PRIMARY KEY (`ID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `country`
--
ALTER TABLE `country`
  MODIFY `ID` bigint(20) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `country_localize`
--
ALTER TABLE `country_localize`
  MODIFY `ID` bigint(20) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `league`
--
ALTER TABLE `league`
  MODIFY `ID` bigint(20) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `league_localize`
--
ALTER TABLE `league_localize`
  MODIFY `ID` bigint(20) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `match`
--
ALTER TABLE `match`
  MODIFY `ID` bigint(20) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `match_localize`
--
ALTER TABLE `match_localize`
  MODIFY `ID` bigint(20) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `player`
--
ALTER TABLE `player`
  MODIFY `ID` bigint(20) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `player_localize`
--
ALTER TABLE `player_localize`
  MODIFY `ID` bigint(20) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `team`
--
ALTER TABLE `team`
  MODIFY `ID` bigint(20) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `team_localize`
--
ALTER TABLE `team_localize`
  MODIFY `ID` bigint(20) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `vendor`
--
ALTER TABLE `vendor`
  MODIFY `ID` bigint(20) NOT NULL AUTO_INCREMENT;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `country_localize`
--
ALTER TABLE `country_localize`
  ADD CONSTRAINT `fk_countryLocalize_country` FOREIGN KEY (`CountryId`) REFERENCES `country` (`ID`);

--
-- Constraints for table `league_localize`
--
ALTER TABLE `league_localize`
  ADD CONSTRAINT `fk_league_leagueLocaloize` FOREIGN KEY (`LeagueId`) REFERENCES `league` (`ID`);

--
-- Constraints for table `match`
--
ALTER TABLE `match`
  ADD CONSTRAINT `matchTeam1Id_team` FOREIGN KEY (`Team1Id`) REFERENCES `team` (`id`),
  ADD CONSTRAINT `matchTeam2Id_team` FOREIGN KEY (`Team2Id`) REFERENCES `team` (`id`),
  ADD CONSTRAINT `match_league` FOREIGN KEY (`LeagueId`) REFERENCES `league` (`ID`);

--
-- Constraints for table `match_localize`
--
ALTER TABLE `match_localize`
  ADD CONSTRAINT `fk_match_localize_match` FOREIGN KEY (`MatchId`) REFERENCES `match` (`ID`);

--
-- Constraints for table `player`
--
ALTER TABLE `player`
  ADD CONSTRAINT `fk_player_team` FOREIGN KEY (`TeamId`) REFERENCES `team` (`id`);

--
-- Constraints for table `player_localize`
--
ALTER TABLE `player_localize`
  ADD CONSTRAINT `fk_playerLocalize_player` FOREIGN KEY (`PlayerId`) REFERENCES `player` (`ID`);

--
-- Constraints for table `team`
--
ALTER TABLE `team`
  ADD CONSTRAINT `fk_team_league` FOREIGN KEY (`LeagueId`) REFERENCES `league` (`ID`);

--
-- Constraints for table `team_localize`
--
ALTER TABLE `team_localize`
  ADD CONSTRAINT `fk_teamLocalize_team` FOREIGN KEY (`TeamId`) REFERENCES `team` (`ID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
