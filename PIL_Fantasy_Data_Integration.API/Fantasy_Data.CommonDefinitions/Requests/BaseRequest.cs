using PIL_Fantasy_Data_Integration.API.Fantasy_Data.DAL.DB;
using System;
using System.Collections.Generic;
using System.Text;

namespace PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Requests
{
    public class BaseRequest
    {
        public fantasy_dataContext _context;

        public const int DefaultPageSize = 10;

        public bool IsDesc { get; set; }

        public string OrderByColumn { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public long CreatedBy { get; set; }

        public long RoleID { get; set; }

        public long LanguageId { get; set; }
        public long OperatorId { get; set; }
        public long LeagueId { get; set; }
        public bool GetValidOnly { get; set; }

        public string BaseUrl { get; set; }
        public string Name { get; set; }
        public long VendorId { get; set; }
        public bool GetAllVendors { get; set; }
        public bool? FromSyria { get; set; }
        public bool GetToday { get; set; }
        public DateTime? DateFilter { get; set; }
        public string GlobalSearchText { get; set; }

        public int AddTimeZone { get; set; }
    }
}