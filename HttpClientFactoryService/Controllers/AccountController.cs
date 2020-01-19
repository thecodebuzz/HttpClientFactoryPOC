using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HttpClientFactoryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        public AccountController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> OnGet()
        {
            var uri = new Uri("https://localhost:44364/account");

            var client = _clientFactory.CreateClient("AccountClient");

            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                return Ok(response.Content.ReadAsStreamAsync().Result);
            }
            else
            {
                return StatusCode(500, "Somthing Went Wrong! Error Occured");
            }
        }

        [HttpGet]
        public async Task<IActionResult> OnGet1()
        {
            var uri = new Uri("https://localhost:44364/account");

            var client = _clientFactory.CreateClient();

            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                return Ok(response.Content.ReadAsStreamAsync().Result);
            }
            else
            {
                return StatusCode(500, "Somthing Went Wrong! Error Occured");
            }
        }

    }
}