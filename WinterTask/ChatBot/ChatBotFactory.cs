namespace WinterTask.ChatBot
{
    public class ChatBotFactory
    {
        public static IChatBot CreateChatBot(string chatId)
        {
            return new ChatBot(chatId);
        }
    }
}