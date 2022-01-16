namespace WinterTask
{
    public class User
    {
        public User(long id, string username)
        {
            Id = id;
            UserName = username;
        }

        public long Id { get; }

        public string UserName { get; }
    }
}