using System.Threading.Tasks;

namespace WinterTask.Clients
{
    public interface IClient
    {
        public void LaunchClient();

        public Task ReplyMessage(long chatId, string message);

        public void ShutdownClient();

        public Task<int> CreatePoll(long chatId);

        public Task<User[]> GetPollUsers(int pollMessageId, long chatId);
    }
}