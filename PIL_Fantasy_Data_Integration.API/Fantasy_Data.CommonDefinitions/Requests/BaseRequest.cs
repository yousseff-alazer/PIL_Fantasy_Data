using PIL_Fantasy_Data_Integration.API.Fantasy_Data.DAL.DB;
using System;
using System.Collections.Generic;
using System.Text;

namespace PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Requests
{
    public class BaseRequest
    {
        public fantasy_dataContext _context;

        public  int DefaultPageSize = 80;

        public bool IsDesc { get; set; }

        public string OrderByColumn { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public string LanguageId { get; set; }
        public string BaseUrl { get; set; }
        public string Name { get; set; }
        public long VendorId { get; set; }
    }
}