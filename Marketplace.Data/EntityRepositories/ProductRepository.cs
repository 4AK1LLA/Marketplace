using Marketplace.Core.Entities;
using Marketplace.Core.Interfaces;
using Marketplace.Shared.Constants;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Data;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(MarketplaceContext context) : base(context) { }

    public IEnumerable<Product> GetByCategoryNameIncludingTagValuesAndPhotos(string name, int page) =>
        GetAll()
        .AsQueryable()
        .Where(pr => EF.Functions.Like(pr.Category!.Name!, $"%{name}%"))
        .OrderBy(pr => pr.Id)
        .Skip((page - 1) * 16)
        .Take(16)
        .Include(pr => pr.Photos)
        .Include(pr => pr.TagValues!.Where(tv => AppConstants.SignificantTagIds.Contains(tv.Tag!.Identifier)))
        .ThenInclude(tv => tv.Tag);

    public Product GetIncludingTagValuesAndPhotos(int id) =>
        GetAll()
        .AsQueryable()
        .Include(pr => pr.Photos)
        .Include(pr => pr.TagValues)!
        .ThenInclude(tv => tv.Tag)
        .FirstOrDefault(pr => pr.Id == id)!;

    public int CountByCategoryName(string name) =>
        GetAll()
        .AsQueryable()
        .Where(pr => EF.Functions.Like(pr.Category!.Name!, $"%{name}%"))
        .Count();

    public Product GetIncludingUsersThatLiked(int id) =>
        GetAll()
        .AsQueryable()
        .Include(pr => pr.UsersThatLiked)
        .FirstOrDefault(pr => pr.Id == id)!;

    public IEnumerable<Product> GetLiked(string userStsId) =>
        GetAll()
        .AsQueryable()
        .Where(pr => pr.UsersThatLiked.Any(us => us.StsIdentifier == userStsId))
        .Include(pr => pr.Photos)
        .Include(pr => pr.TagValues!.Where(tv => AppConstants.SignificantTagIds.Contains(tv.Tag!.Identifier)))
        .ThenInclude(tv => tv.Tag);
}