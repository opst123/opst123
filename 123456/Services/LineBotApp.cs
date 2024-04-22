using System.Drawing;
using Line.Messaging;
using Line.Messaging.Webhooks;


public class LineBotApp : WebhookApplication
{
    private readonly LineMessagingClient _messagingClient;
    private readonly LineMessagingClient _contentClient;
    private readonly ILineService _lineService;

    public LineBotApp(LineBotConfig config, ILineService lineService)
    {
        _messagingClient = new LineMessagingClient(config.accessToken);
        _contentClient = new LineMessagingClient(config.accessToken, "https://api-data.line.me/v2");
        _lineService = lineService;
    }

    protected override async Task OnMessageAsync(MessageEvent ev)
    {
        var result = null as List<ISendMessage>;

        //頻道Id
        var channelId = ev.Source.Id;
        //使用者Id
        var userId = ev.Source.UserId;

        switch (ev.Message)
        {
            //文字訊息
            case TextEventMessage textMessage:
                var text = textMessage.Text; //使用者輸入的文字
                result = await _lineService.ProcessTextEventMessageAsync(channelId, userId, text);
                break;
            case StickerEventMessage stickerEventMessage:
                var packageId = stickerEventMessage.PackageId;
                var stickerId = stickerEventMessage.StickerId;
                result = await _lineService.ProcessStickerEventMessageAsync(channelId, userId, packageId, stickerId);
                break;
            case MediaEventMessage mediaEventMessage: //image, video , audio   //    Image,Video,Audio,
                string originalContentUrl;
                string previewImageUrl;
                switch (mediaEventMessage.Type)
                {
                    case EventMessageType.Image:
                        if (mediaEventMessage.ContentProvider.Type == ContentProviderType.Line)
                        {
                            ContentStream imageStream =
                                await _contentClient.GetContentStreamAsync(mediaEventMessage.Id);
                            Image image = Image.FromStream(imageStream);
                            result = await _lineService.ProcessImageEventMessageAsync(channelId, userId, image);
                        }
                        else
                        {
                            originalContentUrl = mediaEventMessage.ContentProvider.OriginalContentUrl;
                            previewImageUrl = mediaEventMessage.ContentProvider.PreviewImageUrl;
                            result = await _lineService.ProcessImageEventMessageAsync(channelId, userId,
                                originalContentUrl,
                                previewImageUrl);
                        }

                        break;

                    case EventMessageType.Video:
                        originalContentUrl = mediaEventMessage.ContentProvider.OriginalContentUrl;
                        previewImageUrl = mediaEventMessage.ContentProvider.PreviewImageUrl;
                        result = await _lineService.ProcessVideoEventMessageAsync(channelId, userId, originalContentUrl,
                            previewImageUrl);
                        break;

                    case EventMessageType.Audio:
                        originalContentUrl = mediaEventMessage.ContentProvider.OriginalContentUrl;
                        var duration = (int)mediaEventMessage.Duration;
                        result = await _lineService.ProcessAudioEventMessageAsync(channelId, userId, originalContentUrl,
                            duration);
                        break;
                }

                break;
            case LocationEventMessage locationEventMessage:
                result = await _lineService.ProcessLocationEventMessageAsync(
                    channelId,
                    userId,
                    locationEventMessage.Title,
                    locationEventMessage.Address,
                    (float)locationEventMessage.Latitude,
                    (float)locationEventMessage.Longitude);
                break;
            case FileEventMessage fileEventMessage:
                break;
            default:
                break;
        }

        if (result != null)
            await _messagingClient.ReplyMessageAsync(ev.ReplyToken, result);
    }
}