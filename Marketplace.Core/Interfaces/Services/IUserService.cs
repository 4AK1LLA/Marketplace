using Marketplace.Core.Entities;

namespace Marketplace.Core.Interfaces;

public interface IUserService
{
    bool AddUser(AppUser user);

    AppUser GetUserByName(string userName);

    bool RemoveAllLikes(string userStsId);
}
