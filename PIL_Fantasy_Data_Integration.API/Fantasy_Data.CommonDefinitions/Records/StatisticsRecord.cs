using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Records
{
    public class StatisticsRecord
    {
        public object TeamsStats { get; set; }

        public object PlayersStats { get; set; }
    }
}