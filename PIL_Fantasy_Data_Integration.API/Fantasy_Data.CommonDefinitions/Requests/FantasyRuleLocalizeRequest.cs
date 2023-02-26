using PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Records;
using System;
using System.Collections.Generic;
using System.Text;

namespace PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Requests
{
    public class FantasyRuleLocalizeRequest : BaseRequest
    {
        public List<FantasyRuleLocalizeRecord> FantasyRuleLocalizeRecords { get; set; }

        public FantasyRuleLocalizeRecord FantasyRuleLocalizeRecord { get; set; }
    }
}
