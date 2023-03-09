using Marketplace.Core.Entities;
using Marketplace.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Data;

public class UserRepository : Repository<AppUser>, IUserRepository
{
    public UserRepository(MarketplaceContext context) : base(context) { }

    public AppUser GetByStsIdIncludingLikedProducts(string stsId) =>
        GetAll()
        .AsQueryable()
        .Include(us => us.LikedProducts)
        .FirstOrDefault(us => us.StsIdentifier == stsId)!;
}
