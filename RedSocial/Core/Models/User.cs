namespace RedSocial.Core.Entities
{
    public class User
    {
        public Guid id { get;} = Guid.NewGuid();
        public string Username { get; set; }
        public List<Guid> FollowingId { get; private set; }
        public List<Post> Posts { get; private set; }

        public User(string username)
        {
            Username = username;
            FollowingId = new List<Guid>();
            Posts = new List<Post>();
        }

        public void Follow(Guid userId)
        {
            if (!FollowingId.Contains(userId))
            {
                FollowingId.Add(userId);
            }
        }

        public void AddPost(Post post)
        {
            Posts.Add(post);
        }
    }
}
