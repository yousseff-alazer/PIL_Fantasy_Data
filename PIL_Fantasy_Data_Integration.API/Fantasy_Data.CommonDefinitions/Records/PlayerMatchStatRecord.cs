using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Records
{
    public class PlayerMatchStatRecord
    {
        public long Id { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? ModificationDate { get; set; }
        public long? ModifiedBy { get; set; }
        public long TeamId { get; set; }
        public long MatchId { get; set; }
        public long PlayerId { get; set; }
        public string IntegrationId { get; set; }
        public string Offsides { get; set; }
        public string ShotsTotal { get; set; }
        public string ShotsOn { get; set; }
        public string GoalsTotal { get; set; }
        public string GoalsConceded { get; set; }
        public string GoalsAssists { get; set; }
        public string GoalSaves { get; set; }
        public string PassesTotal { get; set; }
        public string PassesKey { get; set; }
        public string PassesAccuracy { get; set; }
        public string TacklesTotal { get; set; }
        public string TacklesBlocks { get; set; }
        public string TacklesInterceptions { get; set; }
        public string DuelsTotal { get; set; }
        public string DuelsWon { get; set; }
        public string DribblesAttempts { get; set; }
        public string DribblesSuccess { get; set; }
        public string DribblesPast { get; set; }
        public string FoulsDrawn { get; set; }
        public string FoulsCommitted { get; set; }
        public string CardsRed { get; set; }
        public string CardsYellow { get; set; }
        public string PenaltyWon { get; set; }
        public string PenaltyCommitted { get; set; }
        public string PenaltyScored { get; set; }
        public string PenaltyMissed { get; set; }
        public string PenaltySaved { get; set; }

    }
}