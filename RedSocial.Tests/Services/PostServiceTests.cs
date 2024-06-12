using RedSocial.Application.UseCases;
using RedSocial.Core.Entities;
using RedSocial.Core.Services;
using RedSocial.Infrastructure.Repositories;

namespace RedSocial.Tests.Services;

public class PostServiceTests
{
    [Fact]
    public void User_Can_Create_Post()
    {
        // Arrange
        var userRepository = new InMemoryUserRepository();
        var postRepository = new InMemoryPostRepository();
        var postService = new PostService(postRepository, userRepository);


        var content = "Hola Mundo";
        var timestamp = DateTime.Now;
        var Author = "@Alfonso";

        Post newpost = new Post(Author,content,timestamp);

        // Act
        postService.CreatePost(newpost);

        // Assert
        var user = userRepository.GetByUsername(newpost.Author);
        Assert.Single(user.Posts);
        Assert.Equal(content, user.Posts.First().Content);
    }

    [Fact]
    public void User_Can_Get_Wall()
    {
        // Arrange
        var userRepository = new InMemoryUserRepository();
        var userService = new UserService(userRepository);
        var postRepository = new InMemoryPostRepository();
        var postService = new PostService(postRepository, userRepository);
        var wall = new GetWallUseCase(userRepository);

        var user = "@Ivan";
        var content = "Hola Mundo";
        var timestamp = DateTime.Now;
        Post newpost = new Post(user, content, timestamp);

        for (var i = 0; i < 5; i++)
        {
            postService.CreatePost(newpost);
        }

        // Act
        var expectedWall = wall.Execute(user); 

        // Assert
        var u = userRepository.GetByUsername(user);
        Assert.Equal(expectedWall, u.Posts);
    }

    [Fact]
    public void User_Can_Get_Dashboard()
    {
        // Arrange
        var postRepository = new InMemoryPostRepository();
        var userRepository = new InMemoryUserRepository();
        var userService = new UserService(userRepository);
        var dashboard = new GetDashboardUseCase(userRepository);
        var postService = new PostService(postRepository, userRepository);

        var user = "@Alicia";

        var followed1 = "@Alfonso";
        var followed2 = "@Ivan";
        var content = "Hola Mundo";
        var timestamp = DateTime.Now;

        Post newpost = new Post(followed1, content, timestamp);
        Post newpost2 = new Post(followed2, content, timestamp);
        postService.CreatePost(newpost);
        postService.CreatePost(newpost2);

        userService.FollowUser(user, followed1);
        userService.FollowUser(user, followed2);

        // Act
        var userDashboard = dashboard.Execute(user);

        // Assert
        var u = userRepository.GetByUsername(user);
        var f1 = userRepository.GetByUsername(followed1);
        var f2 = userRepository.GetByUsername(followed2);
        var expectedDashobard = new List<Post>();
        expectedDashobard.AddRange(f1.Posts);
        expectedDashobard.AddRange(f2.Posts);

        Assert.Equal(expectedDashobard, userDashboard);
    }
}