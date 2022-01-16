namespace WinterTask.Clients
{
    public interface IClient
    {
        public void LaunchClient();

        public void SendMessage(string chatId, string message);

        public void ShutdownClient();
    }
}