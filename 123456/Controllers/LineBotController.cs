using Line.Messaging;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class LineBotController : ControllerBase
{
    private readonly ILineService _lineService;
    private readonly LineBotConfig _lineBotConfig;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly HttpContext _httpContext;


    public LineBotController(IServiceProvider serviceProvider, ILineService lineService, LineBotConfig config)
    {
        _lineService = lineService;
        _lineBotConfig = config;

        _httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
        _httpContext = _httpContextAccessor.HttpContext;
    }

    [HttpPost("run")] //POST {url}/api/linebot/run
    public async Task<IActionResult> Post()
    {
        var events = await _httpContext.Request.GetWebhookEventsAsync(_lineBotConfig.channelSecret);
        var lineBotApp = new LineBotApp(_lineBotConfig, _lineService);
        await lineBotApp.RunAsync(events);

        return Ok();
    }

    [HttpGet("run")] //GET {url}/api/linebot/run
    public async Task<IActionResult> Get()
    {
        return Ok();
    }
}