using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientFactoryService.Controllers
{
    public class AccountClient
    {
        private readonly HttpClient _client;

        private readonly IConfiguration _configuration;
        public AccountClient(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }
        public async Task<string>GetAccount()
        {
            var uri = new Uri(_configuration.GetValue<string>("AccountURL"));

            var response = await _client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                return null;
            }
           
        }
    }
}
