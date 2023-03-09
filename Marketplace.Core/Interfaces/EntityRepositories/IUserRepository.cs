using Marketplace.Core.Entities;

namespace Marketplace.Core.Interfaces;

public interface IUserRepository : IRepository<AppUser>
{
    AppUser GetByStsIdIncludingLikedProducts(string stsId);
}
