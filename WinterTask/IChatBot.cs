using WinterTask.Clients;

namespace WinterTask
{
    public interface IChatBot
    {
        public BotReply ReplyToMessage(IClient botClient, string message);
    }
}