namespace WinterTask.TelegramBot
{
    public interface IBot
    {
        public void ReplyToMessage(string message, string id);
    }
}