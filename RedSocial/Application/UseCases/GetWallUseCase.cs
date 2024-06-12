using RedSocial.Core.Entities;
using RedSocial.Core.Interfaces;

namespace RedSocial.Application.UseCases
{
    public class GetWallUseCase
    {
        private readonly IUserRepository _userRepository;

        public GetWallUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<Post> Execute(string username)
        {
            var user = _userRepository.GetByUsername(username);
            if (user == null) return new List<Post>();
            return user.Posts.OrderByDescending(p => p.Timestamp).ToList();
        }
    }
}
