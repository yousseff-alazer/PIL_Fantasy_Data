﻿using PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Records;
using System;
using System.Collections.Generic;
using System.Text;

namespace PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Responses
{
    public class PlayerResponse : BaseResponse
    {
        public List<PlayerRecord> PlayerRecords { get; set; }
    }
}
