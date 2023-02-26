using System;
using System.Collections.Generic;

#nullable disable

namespace PIL_Fantasy_Data_Integration.API.Fantasy_Data.DAL.DB
{
    public partial class FantasyRule
    {
        public FantasyRule()
        {
            FantasyRuleLocalizes = new HashSet<FantasyRuleLocalize>();
        }

        public long Id { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? ModificationDate { get; set; }
        public long? ModifiedBy { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Message { get; set; }
        public int? Max { get; set; }
        public int? Min { get; set; }

        public virtual ICollection<FantasyRuleLocalize> FantasyRuleLocalizes { get; set; }
    }
}
