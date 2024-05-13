using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _123456.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
            private readonly ILineService _lineService;
            private readonly LineBotConfig _lineBotConfig;
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly HttpContext _httpContext;


            public WeatherController(IServiceProvider serviceProvider, ILineService lineService, LineBotConfig config)
            {
                _lineService = lineService;
                _lineBotConfig = config;

                _httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
                _httpContext = _httpContextAccessor.HttpContext;
            }
            
        // GET: api/Weather
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Weather/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Weather
        /*[HttpPost]
        public void Post([FromBody] string value)
        {
        }
        */
        [HttpPost] //POST {url}/api/linebot/run
        public async Task<IActionResult> Post()
        {
            var events = await _httpContext.Request.GetWebhookEventsAsync(_lineBotConfig.channelSecret);
            var lineBotApp = new LineBotApp(_lineBotConfig, _lineService);
            await lineBotApp.RunAsync(events);

            return Ok();
        }
        // PUT: api/Weather/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Weather/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
