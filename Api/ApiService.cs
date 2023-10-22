using RestSharp;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Nodes;
using ApiTestDemo.Api.Models;
using Newtonsoft.Json;

namespace ApiTestDemo.Api
{
    public class ApiService
    {
        private string _token;
        private HttpClient _httpClient;

        public ApiService()
        {
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };

            _httpClient = new HttpClient(handler);
            _httpClient.BaseAddress = new Uri("https://efatura.etrsoft.com/fmi/data/v1/databases/testdb/");
        }

        public async Task<bool> GetToken()
        {
            var data = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(Encoding.ASCII.GetBytes($"apitest:test123")));

            _httpClient.DefaultRequestHeaders.Authorization = data;

            // _httpClient.DefaultRequestHeaders.Accept.Add( MediaTypeWithQualityHeaderValue.Parse("application/json"));

            var response =
                await _httpClient.PostAsync("sessions", new StringContent("{}", Encoding.UTF8, "application/json"));

            var tokenResponse = await response.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<BaseResponse<TokenModel>>(tokenResponse);
            _token = token.response.token;

            return true;
        }

        public async Task<List<ItemModel>> GetData()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var response = await _httpClient.PatchAsync("layouts/testdb/records/1", new StringContent("{\"fieldData\": {},\"script\" : \"getData\"}", Encoding.UTF8, "application/json"));
            var dataResponse = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<BaseResponse<DataModel>>(dataResponse);
            return JsonConvert.DeserializeObject<List<ItemModel>>(data.response.scriptResult);
        }
    }
}