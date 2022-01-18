namespace WinterTask.Clients
{
    public class User
    {
        public User(string id, string username)
        {
            Id = id;
            UserName = username;
        }

        public string Id { get; }

        public string UserName { get; }
    }
}