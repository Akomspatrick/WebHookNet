﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using WebHookPoc.WebHook;

namespace WebHookPoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebhookPOCController : ControllerBase
    {
        //private readonly ILogger _logger;
        private readonly IEgress _egress;
        private readonly IHttpClientFactory _httpClientFactory;

        public WebhookPOCController(IEgress egress, IHttpClientFactory httpClientFactory)
        {
           // _logger = logger;
            _egress = egress;
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost(Name = "/PostData")]
        public async Task<IActionResult> PostData([FromQuery(Name ="traceid")][Required] string traceid)
        {
            var analyticData = new DPData { Data = "AAA", key = "Pesonal key" };
            var dpData = new Dictionary<string, string>
            {
                { "type","com.stepstone..." },
                { "data",  JsonSerializer.Serialize(analyticData) },
                {"key","tobesuppliedin" },
              
            };
            var response = await _egress.SendAnalyticDataAsync(dpData, _httpClientFactory.CreateClient("GalaxyAPI"));
            return Ok(response);
        }
    }
}
