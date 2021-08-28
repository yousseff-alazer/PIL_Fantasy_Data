using System;
using System.Collections.Generic;
using System.Text;

namespace PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Records
{
    public class ItemLocalizeRecord
    {
        public long Id { get; set; }
        public long ItemId { get; set; }
        public long LanguageId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Description { get; set; }
        public string MatchFacts { get; set; }
        public string Nationality { get; set; }
        public string Position { get; set; }
        public string Type { get; set; }
        public string DateOfBirth { get; set; }
        public string CountryOfBirth { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Foot { get; set; }
        public string ShirtNumber { get; set; }
    }
}