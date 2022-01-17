using System.Collections.Generic;

namespace WinterTask.ChatBot
{
    public class BotMessage
    {
        public BotMessage(string text, Dictionary<string, string> availableOperations)
        {
            Text = text;
            AvailableOperations = availableOperations;
        }

        public string Text { get; }
        public Dictionary<string, string> AvailableOperations { get; }
    }
}