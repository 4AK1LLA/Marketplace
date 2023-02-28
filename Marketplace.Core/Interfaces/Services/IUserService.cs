using Marketplace.Core.Entities;

namespace Marketplace.Core.Interfaces;

public interface IUserService
{
    void AddUser(AppUser user);

    AppUser GetUserByName(string userName);
}
