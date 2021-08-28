using PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Records;
using System;
using System.Collections.Generic;
using System.Text;

namespace PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Responses
{
    public class TeamResponse : BaseResponse
    {
        public List<TeamRecord> TeamRecords { get; set; }
    }
}
