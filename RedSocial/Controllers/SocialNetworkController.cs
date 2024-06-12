using Microsoft.AspNetCore.Mvc;
using RedSocial.Application.UseCases;
using RedSocial.Core.Entities;
using System.Text.Json;

namespace RedSocial.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SocialNetworkController : ControllerBase
    {
        private readonly CreatePostUseCase _createPostUseCase;
        private readonly FollowUserUseCase _followUserUseCase;
        private readonly GetDashboardUseCase _getDashboardUseCase;
        private readonly GetWallUseCase _getWallUseCase;

        public SocialNetworkController(
            CreatePostUseCase createPostUseCase,
            FollowUserUseCase followUserUseCase,
            GetDashboardUseCase getDashboardUseCase,
            GetWallUseCase getWallUseCase)
        {
            _createPostUseCase = createPostUseCase;
            _followUserUseCase = followUserUseCase;
            _getDashboardUseCase = getDashboardUseCase;
            _getWallUseCase = getWallUseCase;
        }

        [HttpPost]
        public IActionResult PostMessage(Post post)
        {
            try
            {
                _createPostUseCase.Execute(post);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        public IActionResult FollowUser(string followerUsername, string followeeUsername)
        {
            try
            {
                _followUserUseCase.Execute(followerUsername, followeeUsername);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{username}")]
        public IActionResult GetDashboard(string username)
        {
            try
            {
                var posts = _getDashboardUseCase.Execute(username);
                return Ok(posts);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{username}")]
        public IActionResult GetWall(string username)
        {
            try
            {
                var posts = _getWallUseCase.Execute(username);
                return Ok(posts);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}
