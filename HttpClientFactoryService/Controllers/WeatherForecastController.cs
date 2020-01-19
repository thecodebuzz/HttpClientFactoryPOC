using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HttpClientFactoryService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShoppingController : ControllerBase
    {

        static readonly HttpClient client = new HttpClient();

        private readonly ILogger<ShoppingController> _logger;

        public ShoppingController(ILogger<ShoppingController> logger, IHttpClientFactory _clientFactory)
        {
            _logger = logger;
            client.GetAsync("");
        }

        [HttpGet]
        public IActionResult GetAccount()
        {
            var httpClientHandler = new HttpClientHandler()
            {
                Credentials = new NetworkCredential("user", "****"),
            };

            HttpClient httpClient = new HttpClient(httpClientHandler);

            httpClient.BaseAddress = new Uri("http://thecodebuzz.com");

            HttpResponseMessage response;
            using (var httpclient = new HttpClient())
            {
                response = httpclient.GetAsync("http://thecodebuzz.com").Result;
            }
            return Ok(response.Content.ReadAsStringAsync().Result);

        }
    }
}
