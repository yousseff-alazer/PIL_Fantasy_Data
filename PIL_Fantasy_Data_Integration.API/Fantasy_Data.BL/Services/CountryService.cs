using PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Records;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.DAL.DB;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Requests;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.Helpers;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Responses;
using System.Net;
using PIL_Fantasy_Data_Integration.API.BL.Services.Managers;

namespace PIL_Fantasy_Data_Integration.API.BL.Services
{
    public class CountryService : BaseService
    {
        public static CountryResponse ListCountry(CountryRequest request)
        {
            var res = new CountryResponse();
            RunBase(request, res, (CountryRequest req) =>
             {

                 try
                 {
                     var query = request._context.Countries.Where(c => !c.IsDeleted.Value).Select(c => new CountryRecord
                     {
                         Id = c.Id,
                         Code = c.Code,
                         Iso = c.Iso,
                         Flag=c.Flag,
                         Name=c.Name,
                         Show=c.Show
                     });

                     if (request.CountryRecord != null)
                         query = CountryServiceManager.ApplyFilter(query, request.CountryRecord);

                     res.TotalCount = query.Count();

                     query = OrderByDynamic(query, request.OrderByColumn, request.IsDesc);

                     query = request.PageSize > 0 ? ApplyPaging(query, request.PageSize, request.PageIndex) : ApplyPaging(query, request.DefaultPageSize, 0);

                     res.CountryRecords = query.ToList();
                     res.Message = HttpStatusCode.OK.ToString();
                     res.Success = true;
                     res.StatusCode = HttpStatusCode.OK;
                 }
                 catch (Exception ex)
                 {
                     res.Message = ex.Message;
                     res.Success = false;
                     LogHelper.LogException(ex.Message, ex.StackTrace);
                 }
                 return res;
             });
            return res;
        }
    }
}
