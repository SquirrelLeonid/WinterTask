namespace WinterTask
{
    public class ChatBotFactory
    {
        public static IChatBot CreateChatBot()
        {
            return new ChatBot();
        }
    }
}