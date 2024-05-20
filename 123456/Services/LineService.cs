using System.Drawing;
using Line.Messaging;
using Line.Messaging.Webhooks;

public class LineService : ILineService
{
    private readonly WeatherService _weatherService;
 
    public LineService()
    {
        _weatherService = new WeatherService();
    }

    public async Task<List<ISendMessage>> ProcessTextEventMessageAsync(string channelId, string userId, string message)
    {
        var result = null as List<ISendMessage>;

        /*
        if (message.Contains("陰天"))
        {
            return  new List<ISendMessage>
            {
                new ImageMessage("https://i.imgur.com/L8BRKO7.jpeg"
                ,"https://i.imgur.com/L8BRKO7.jpeg"
                ,null),
                
            };  
        }*/
        if (message == ("天氣"))
        {
            string[] imageUrl = new string[]
                    {
                        "https://i.imgur.com/L8BRKO7.jpeg",
                        "https://i.imgur.com/L8BRKO7.jpeg",
                        "https://i.imgur.com/L8BRKO7.jpeg",
                    };
            Random rnd = new Random((int)DateTime.Now.TimeOfDay.TotalSeconds);
            int index= rnd.Next(0, imageUrl.Length);
            return new List<ISendMessage>
            {
                new ImageMessage(imageUrl[index], imageUrl[index], null),
            };
        }
        
        if (message.Contains("Weather"))
        {
            List<WeatherModel> data = await _weatherService.List();  //呼叫自己定義的服務
            int index=  (new Random()).Next(0, data.Count);  //取亂數
             
            return  new List<ISendMessage>
            {
                new TextMessage(data[index].src, data[index].src, null),
            };
        }

        result = new List<ISendMessage>
        {
            new TextMessage($"Receive a sticker event message \nchannelId={channelId}  \nuserId={userId}")
        };
        return result; 
    }

        
    public async Task<List<ISendMessage>> ProcessStickerEventMessageAsync(string channelId, string userId,string packageId, string stickerId)
    {
        var result = null as List<ISendMessage>;

        result = new List<ISendMessage>
        {
            new TextMessage($"Receive a sticker event message \nchannelId={channelId}  \nuserId={userId}")
        };
        return result;
    }

    public async Task<List<ISendMessage>> ProcessImageEventMessageAsync(string channelId, string userId,string originalContentUrl,
        string previewImageUrl)
    {
        var result = null as List<ISendMessage>;

        result = new List<ISendMessage>
        {
            new TextMessage($"Receive a image event message \nchannelId={channelId}  \nuserId={userId}")
        };
        return result;
    }

    public async Task<List<ISendMessage>> ProcessImageEventMessageAsync(string channelId, string userId, Image image)
    {
        var result = null as List<ISendMessage>;

        result = new List<ISendMessage>
        {
            new TextMessage($"Receive a image event message \nchannelId={channelId}  \nuserId={userId}")
        };
        return result;
    }

    public async Task<List<ISendMessage>> ProcessVideoEventMessageAsync(string channelId, string userId,string originalContentUrl, string previewImageUrl)
    {
        var result = null as List<ISendMessage>;

        result = new List<ISendMessage>
        {
            new TextMessage($"Receive a video event message \nchannelId={channelId}  \nuserId={userId}")
        };
        return result;
    }

    public async Task<List<ISendMessage>> ProcessAudioEventMessageAsync(string channelId, string userId,string originalContentUrl, int duration)
    {
        var result = null as List<ISendMessage>;

        result = new List<ISendMessage>
        {
            new TextMessage($"Receive a audio event message \nchannelId={channelId}  \nuserId={userId}")
        };
        return result;
    }

    public async Task<List<ISendMessage>> ProcessLocationEventMessageAsync(string channelId, string userId,string title, string address, float latitude, float longitude)
    {
        var result = null as List<ISendMessage>;

        result = new List<ISendMessage>
        {
            new TextMessage($"Receive a location event message \nchannelId={channelId}  \nuserId={userId}")
        };
        return result;
    }
}