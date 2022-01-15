namespace WinterTask.Clients
{
    public interface IClient
    {
        public void LaunchClient();

        public void ReplyToMessage(string id, string message);

        public void ShutdownClient();
    }
}