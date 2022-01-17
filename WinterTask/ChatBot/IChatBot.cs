using WinterTask.Clients;

namespace WinterTask.ChatBot
{
    public interface IChatBot
    {
        public BotReply ReplyToMessage(IClient botClient, string message);
    }
}