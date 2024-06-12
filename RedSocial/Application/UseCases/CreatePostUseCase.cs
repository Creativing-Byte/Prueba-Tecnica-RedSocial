using RedSocial.Core.Entities;
using RedSocial.Core.Services;

namespace RedSocial.Application.UseCases
{
    public class CreatePostUseCase
    {
        private readonly PostService _postService;

        public CreatePostUseCase(PostService postService)
        {
            _postService = postService;
        }

        public void Execute(Post post)
        {
            _postService.CreatePost(post);
        }
    }
}
