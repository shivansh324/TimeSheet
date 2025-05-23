using System.Text.Json;
using System.Text;
using TimeSheet.Models.API_Models;
using TimeSheet_Module.Services.Interfaces;
using static System.Net.WebRequestMethods;
using Microsoft.AspNetCore.Mvc;

namespace TimeSheet_Module.Services.Implementations
{
    public class PostData
    {
        private readonly HttpClient _httpClient;

        public PostData(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("BC_Client");
        }

        //public async Task<string> PostDataAsync(BCPostModelList data, string start_date, string end_date)
        public async Task<Dictionary<string, string>> PostDataAsync(List<BCPostModelList> data)
        {
            try
            {
                Dictionary<string, string> response = new Dictionary<string, string>();
                Console.WriteLine("PostDataAsync method called"+DateTime.Now);
                foreach (BCPostModelList temp in data)
                {
                    string jsonData = JsonSerializer.Serialize(temp);
                    var jsonContent = new StringContent(
                        jsonData,
                        Encoding.UTF8,
                        "application/json"
                    );

                    string url = $"http://192.168.100.26:9148/BC230/api/bctech/demo/v1.0/companies(63148974-cbbd-4121-8fda-f606612e5c1f)/TimeSheetHdr?$expand=TimeSheetLines";
                    HttpResponseMessage api_response = await _httpClient.PostAsync(url, jsonContent);
                    response.Add(temp.timeSheetNo, api_response.Content.ReadAsStringAsync().Result.ToString());
                }
                Console.WriteLine("PotDataAsync method completed"+DateTime.Now);
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}