using System.Threading.Tasks;

namespace WinterTask.Clients
{
    public interface IClient
    {
        public void LaunchClient();

        public Task ReplyMessage(string chatId, string message);

        public void ShutdownClient();

        public Task<string> CreatePoll(string chatId);

        public Task<User[]> GetPollUsers(string pollMessageId, string chatId);
    }
}