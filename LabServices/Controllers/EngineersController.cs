using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LabServices.Models;
using LabServices.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LabServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EngineersController : ControllerBase
    {
        private readonly IDataService _dataService;
        private static readonly HttpClient _client = new HttpClient();

        public EngineersController(IDataService dataService)
        {
            _dataService = dataService;
        }

        // GET api/engineers
        [HttpGet]
        public ActionResult<string> GetEngineers()
        {
            return "This is the only API that works without an API key in the header. Please use Postman or CURL to test other endpoints.";
        }

        // GET api/engineers/list
        [HttpGet("list")]
        public ActionResult<IEnumerable<IDictionary<string, string>>> GetEngineerList([FromHeader(Name = "X-API-KEY")] string apiKey = "")
        {
            if (_dataService.GetKeys().Contains(apiKey.ToUpper()))
            {
                List<string> engineers = _dataService.GetEngineers();
                List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

                foreach (string engineer in engineers)
                {
                    Dictionary<string, string> item = new Dictionary<string, string>();
                    item.Add("id", engineer);
                    item.Add("name", engineer);
                    results.Add(item);
                }
                return results;
            }
            return StatusCode(403);
        }

        // POST api/engineers/call
        [HttpPost("call")]
        public IActionResult CallEngineer([FromBody] TriggerParameters parameters, [FromHeader(Name = "X-API-KEY")] string apiKey = "")
        {
            if (_dataService.GetKeys().Contains(apiKey.ToUpper()))
            {
                if (_dataService.GetEngineers().Contains(parameters.engineer.ToUpper()))
                {
                    _dataService.GetKeys().ForEach((key) =>
                    {
                        if (_dataService.GetTriggerUri(key, out string uri))
                        {
                            if (uri != string.Empty)
                            {
                                _client.PostAsync(uri, new StringContent(JsonConvert.SerializeObject(parameters), Encoding.UTF8, "application/json")).Wait();
                            }
                        }
                    });

                    return Ok();
                }
                return NotFound();
            }
            return StatusCode(403);
        }

        private static string GenerateDeleteUri(string scheme, string host, string apikey)
        {
            return $"{scheme}://{host}/api/engineers/unsubscribe/{apikey}";
        }

        // POST api/engineers/subscribe
        [HttpPost("subscribe")]
        public IActionResult Subscribe([FromBody] SubscribeParameters parameters, [FromHeader(Name = "X-API-KEY")] string apiKey = "")
        {
            if (_dataService.GetKeys().Contains(apiKey.ToUpper()))
            {
                if (Uri.TryCreate(parameters.callbackUrl, UriKind.Absolute, out var callback))
                {
                    _dataService.SetTriggerUri(apiKey.ToUpper(), callback.ToString());
                    string deleteUrl = GenerateDeleteUri(Request.Scheme, Request.Host.ToString(), apiKey.ToUpper());
                    Request.HttpContext.Response.Headers.Add("Location", deleteUrl);
                    return Ok();
                }
                return BadRequest();
            }
            return StatusCode(403);
        }

        // DELETE api/engineers/unsubscribe/{key}
        [HttpDelete("unsubscribe/{key}")]
        public IActionResult Unsubscribe(string key = "")
        {
            if (_dataService.GetKeys().Contains(key.ToUpper()))
            {
                _dataService.SetTriggerUri(key.ToUpper(), string.Empty);
                return Ok();
            }
            return NotFound();
        }

        // POST api/engineers/triggertest
        [HttpPost("triggertest")]
        public IActionResult TriggerTest([FromBody] TriggerParameters parameters)
        {
            return Ok();
        }
    }
}
