using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;
using SquadMobile.Models;
using TheBankMVC.Models;

namespace SquadMobile.Services
{
    public class DataService<T> : IDataService where T : class, new()
    {
        private HttpClient client;
        private string APIName { get; set; }
        private string BaseAPI { get; set; } = @"api";

        public DataService(string aPIName)
        {
            client = CreateHTTPClient();
            APIName = aPIName;
        }


        private HttpClient CreateHTTPClient()
        {
            Uri baseURL;
            if (!string.IsNullOrEmpty(App.AzureBackendUrl))
            {
                baseURL = new Uri($"{App.AzureBackendUrl}/");
            }
            else
            {
                throw new Exception("ServiceURL not provided");
            }

            if (client != null)
            {
                Uri uri = client.BaseAddress;
                if (!uri.Equals(baseURL))
                {
                    client.Dispose();
                    client = new HttpClient
                    {
                        BaseAddress = baseURL
                    };
                }
            }
            else
            {
                client = new HttpClient
                {
                    BaseAddress = baseURL
                };
            }

            return client;
        }

        private bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
        public async Task<IEnumerable<T>> GetAsync(bool forceRefresh = false)
        {
            if (forceRefresh && IsConnected)
            {
                var responseList = await GetAsync<IEnumerable<T>, T>(BaseAPI+ "/" + APIName, string.Empty);
                return responseList;
            }
            return default;
        }

        public Task<O> Post<O, I>(string api, string method, I input)
        {
            throw new NotImplementedException();
        }

        public Task<O> Post<O, I>(string api, string method, IList<I> input)
        {
            throw new NotImplementedException();
        }

        public async Task<O> GetAsync<O, I>(string api, string method, I input = null) where I : class
        {
            var uriString = api;
            if (!string.IsNullOrEmpty(method))
            {
                uriString += "/" + method;
            }

            if (input != null)
            {
                Type type = typeof(I);
                var properties = type.GetProperties();
                List<KeyValuePair<string, string>> uRIParameters = new List<KeyValuePair<string, string>>();
                
                foreach (var property in properties)
                {
                    var value = property.GetValue(input);
                    if (value != null)
                    {
                        var key = property.Name;
                        uRIParameters.Add(new KeyValuePair<string, string>(key, value.ToString())); 
                    }
                }

                if (uRIParameters != null && uRIParameters.Count > 0)
                {
                    string requestParams = new FormUrlEncodedContent(uRIParameters).ReadAsStringAsync().Result;
                    uriString += "?" + requestParams;
                }
            }
            var json = await client.GetStringAsync(uriString);
            var response = await Task.Run(() => JsonConvert.DeserializeObject<O>(json));
            return response;
        }

        public Task<O> Get<O, I>(string api, string method, IList<I> input)
        {
            throw new NotImplementedException();
        }
    }
}
