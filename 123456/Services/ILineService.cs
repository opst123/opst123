using System.Drawing;
using Line.Messaging;

public interface ILineService
{
    Task<List<ISendMessage>> ProcessTextEventMessageAsync(string channelId, string userId, string message);

    Task<List<ISendMessage>> ProcessStickerEventMessageAsync(string channelId, string userId, string packageId,
        string stickerId);

    Task<List<ISendMessage>> ProcessImageEventMessageAsync(string channelId, string userId, string originalContentUrl,
        string previewImageUrl);

    Task<List<ISendMessage>> ProcessImageEventMessageAsync(string channelId, string userId, Image image);

    Task<List<ISendMessage>> ProcessVideoEventMessageAsync(string channelId, string userId, string originalContentUrl,
        string previewImageUrl);

    Task<List<ISendMessage>> ProcessAudioEventMessageAsync(string channelId, string userId, string originalContentUrl,
        int duration);

    Task<List<ISendMessage>> ProcessLocationEventMessageAsync(string channelId, string userId, string title,
        string address,
        float latitude, float longitude
    );
}