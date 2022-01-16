using WinterTask.Clients;

namespace WinterTask
{
    public interface IChatBot
    {
        public void ReplyToMessage(IClient botClient, string message);
    }
}