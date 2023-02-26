using Marketplace.Core.Entities;
using Marketplace.Core.Interfaces;

namespace Marketplace.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _uow;

    public UserService(IUnitOfWork uow) => _uow = uow;

    public void AddUser(AppUser user)
    {
        _uow.AppUserRepository.Add(user);

        _uow.Save();
    }

    public AppUser GetUserByIdentifier(string identifier) => 
        _uow.AppUserRepository.Find(us => us.StsIdentifier == identifier).FirstOrDefault()!;
}
