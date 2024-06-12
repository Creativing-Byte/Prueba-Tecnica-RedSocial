using RedSocial.Core.Entities;
using RedSocial.Core.Interfaces;

namespace RedSocial.Core.Services
{
    public class PostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;

        public PostService(IPostRepository postRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public void CreatePost(Post post)
        {
            var user = _userRepository.GetByUsername(post.Author);
            if (user != null)
            {
                _postRepository.Add(post);
                user.AddPost(post);
            }
        }
    }
}
