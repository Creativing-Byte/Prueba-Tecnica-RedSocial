using RedSocial.Application.UseCases;
using RedSocial.Core.Entities;
using RedSocial.Core.Interfaces;
using RedSocial.Core.Services;
using RedSocial.Infrastructure.Repositories;

namespace RedSocial.Tests.Services;

public class UserServiceTests
{
    [Fact]
    public void User_Can_Follow_Other_User()
    {
        // Arrange
        var userRepository = new InMemoryUserRepository();
        var userService = new UserService(userRepository);

        var follower = "@Alicia";
        var followee = "@Ivan";

        // Act
        userService.FollowUser(follower, followee);

        // Assert
        var user = userRepository.GetByUsername(follower);
        var followed = userRepository.GetByUsername(followee);
        Assert.Contains(followed.id, user.FollowingId);
    }

}