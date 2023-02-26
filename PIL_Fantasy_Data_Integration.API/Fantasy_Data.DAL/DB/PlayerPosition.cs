using System;
using System.Collections.Generic;

#nullable disable

namespace PIL_Fantasy_Data_Integration.API.Fantasy_Data.DAL.DB
{
    public partial class PlayerPosition
    {
        public PlayerPosition()
        {
            Players = new HashSet<Player>();
        }

        public long Id { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? ModificationDate { get; set; }
        public long? ModifiedBy { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public virtual ICollection<Player> Players { get; set; }
    }
}
