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
        private WeatherService _weatherService;

        public WeatherController(IServiceProvider serviceProvider, ILineService lineService, LineBotConfig config)
        {
            _lineService = lineService;
            _lineBotConfig = config;

            _httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            _httpContext = _httpContextAccessor.HttpContext;
            _weatherService = new WeatherService();
        }

        // GET: api/Weather
        [HttpGet]
        public async Task<WeatherModel> Get()
        {
          //  return new string[] { "value1", "value2" };
         var list = await   _weatherService.List();
         return list;
        }

        // GET: api/Weather/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

      
    }
}