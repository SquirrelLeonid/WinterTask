namespace WinterTask.Bots
{
    public interface IGameBot
    {
        public void ReplyToMessage(string message, string id);

        public void CreateStartGameTask(long chatId);
    }
}