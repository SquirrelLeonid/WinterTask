namespace WinterTask
{
    public class ChatBotFactory
    {
        public static IChatBot CreateChatBot(long chatId)
        {
            return new ChatBot(chatId);
        }
    }
}