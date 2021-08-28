using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace PIL_Fantasy_Data_Integration.API.Fantasy_Data.Helpers
{
    public class UIHelper
    {
        public static HttpResponseMessage CreateRequest(string baseurl, HttpMethod method, string relativeUrl,
            string jsonObj = null, string lang = "", string basicAuthUser = "", string basicAuthPassword = "", string apiKey = "", string host = "", string chargeKey = "")
        {
            using (var client = new HttpClient())
            {
                client.Timeout.Add(new TimeSpan(0, 0, 0, 180));
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if (!string.IsNullOrWhiteSpace(basicAuthUser) && !string.IsNullOrWhiteSpace(basicAuthPassword))
                {
                    var byteArray = new UTF8Encoding().GetBytes(basicAuthUser + ":" + basicAuthPassword);
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                    //LogHelper.LogException("Authorization", Convert.ToBase64String(byteArray));
                }
                //client.DefaultRequestHeaders.Add("Accept-Language", lang);
                if (!string.IsNullOrWhiteSpace(apiKey) && !string.IsNullOrWhiteSpace(host))
                {
                    client.DefaultRequestHeaders.Add("x-rapidapi-key", apiKey);
                    client.DefaultRequestHeaders.Add("x-rapidapi-host", host);
                }
                else if (!string.IsNullOrWhiteSpace(apiKey))
                {
                    client.DefaultRequestHeaders.Add("x-rapidapi-key", apiKey);
                }
                if (!string.IsNullOrWhiteSpace(chargeKey))
                {
                    client.DefaultRequestHeaders.Add("charge-key", chargeKey);
                }
                HttpRequestMessage request = new HttpRequestMessage(method, relativeUrl);

                if (jsonObj != null)
                    request.Content = new StringContent(jsonObj, Encoding.UTF8, "application/json");

                var Res = new HttpResponseMessage();
                try
                {
                    //LogHelper.LogInfo(" CreateRequest Info :" + relativeUrl);
                    Res = client.SendAsync(request).Result;

                    if (Res.StatusCode != HttpStatusCode.OK)
                    {
                        try
                        {
                            var result = Res.Content.ReadAsStringAsync().Result;
                            var x = result.ToString();

                            //LogHelper.LogInfo("Not Ok Response.StatusCode:" + Res.StatusCode + " result: " + " " + x + " " );

                            //LogHelper.LogException(x, "from radwan");
                        }
                        catch (Exception ex)
                        {
                            LogHelper.LogInfo("Exception Response.StatusCode:" + Res.StatusCode + " " + relativeUrl + " " + ex.Message + " " + ex.InnerException.Message);
                            //LogHelper.LogException("baseurl", baseurl);
                            //LogHelper.LogException(ex.Message, ex.StackTrace);
                        }
                    }
                    //else LogHelper.LogInfo("Response read result  Success:" + relativeUrl);
                }
                catch (Exception ex)
                {
                    //LogHelper.LogInfo(" Get response Faild:" + relativeUrl);
                    LogHelper.LogException(ex.Message + " " + relativeUrl, ex.StackTrace);
                }

                return Res;
            }
        }

        public static string HtmlToPlainText(string htmlString)
        {
            var text = Regex.Replace(htmlString, @"<(.|\n)*?>", "");
            return text;
        }

        public static HttpResponseMessage AddRequestToServiceApi(string url, JObject json, string basicAuthUser = "", string basicAuthPassword = "", string bearerToken = "", string chargeKey = "", string APIKey = "", string APIPassword = "")
        {
            var Res = new HttpResponseMessage();
            try
            {
                using (var client = new HttpClient())
                {
                    var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
                    LogHelper.LogException("AddRequestToServiceApi Url : ", url);
                    LogHelper.LogException("AddRequestToServiceApi Content : ", json.ToString());
                    if (!string.IsNullOrWhiteSpace(basicAuthUser) && !string.IsNullOrWhiteSpace(basicAuthPassword))
                    {
                        var byteArray = new UTF8Encoding().GetBytes(basicAuthUser + ":" + basicAuthPassword);
                        client.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                        //LogHelper.LogException("Authorization", Convert.ToBase64String(byteArray));
                    }
                    if (!string.IsNullOrWhiteSpace(bearerToken))
                    {
                        client.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", bearerToken);
                    }
                    if (!string.IsNullOrWhiteSpace(chargeKey))
                    {
                        client.DefaultRequestHeaders.Add("charge-key", chargeKey);
                    }
                    if (!string.IsNullOrWhiteSpace(APIKey) && !string.IsNullOrWhiteSpace(APIKey))
                    {
                        client.DefaultRequestHeaders.Add("APIKey", APIKey);
                        client.DefaultRequestHeaders.Add("APIPassword", APIPassword);
                    }
                    //var xxx = client.PostAsync(url, content);
                    Res = client.PostAsync(url, content).Result;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }

            return Res;
        }
    }
}