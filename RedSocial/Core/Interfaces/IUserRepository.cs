using RedSocial.Core.Entities;

namespace RedSocial.Core.Interfaces
{
    public interface IUserRepository
    {
        User GetByUsername(string username);
        User GetById(Guid id);
        void Add(User user);
    }
}
