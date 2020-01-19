using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HttpClientFactoryService.Controllers
{
    public class AccountPersonalController : Controller
    {
        private readonly AccountClient _client;
        public AccountPersonalController(AccountClient clientFactory)
        {
            _client = clientFactory;
        }

        [HttpGet]
        public async Task<Account> OnGet()
        {
            var responseString = _client.GetAccount().Result;
            var catalog = JsonConvert.DeserializeObject<Account>(responseString);
            return catalog;
        }
    }
}
