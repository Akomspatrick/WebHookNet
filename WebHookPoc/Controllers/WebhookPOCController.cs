using Microsoft.AspNetCore.Http;
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


        [HttpGet]
        public IActionResult Get()
        {

            return Ok("Site is Up....");
        }

        [HttpPost(Name = "/PostData")]
        public async Task<IActionResult> PostData([FromBody] DPData analyticData)
        {
            
            
            var dpData = new Dictionary<string, string>
            {
                { "type","com.stepstone..." },
    
                {"key","tobesuppliedin" },

                {"poc_name",analyticData.key },
                {"poc_type",analyticData.Data },

            };
            var response = await _egress.SendAnalyticDataAsync(dpData, _httpClientFactory.CreateClient("GalaxyAPI"));
            return Ok(response);
        }


        [HttpPost("/Receive")]
        public IActionResult Receive()
        {

            Console.WriteLine($"Signal received @ {DateTime.UtcNow.ToString()}");
            return new ObjectResult("FInised");
        }
    }
}
