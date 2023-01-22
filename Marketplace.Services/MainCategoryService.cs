using Marketplace.Core.Entities;
using Marketplace.Core.Interfaces;
using Marketplace.Core.Interfaces.Services;

namespace Marketplace.Services;

public class MainCategoryService : IMainCategoryService
{
    private readonly IUnitOfWork _uow;

    public MainCategoryService(IUnitOfWork uow) => _uow = uow;

    public IEnumerable<MainCategory> GetAllMainCategories() => _uow.MainCategoryRepository.GetAll();

    public void SeedMainCategoryData() //Temporary
    {
        if (_uow.MainCategoryRepository.Count() != 0)
        {
            return;
        }

        var mainCategories = new List<MainCategory>()
        {
            new MainCategory {
                Name = "Help",
                PhotoUrl = "https://res.cloudinary.com/dfs6whqzs/image/upload/v1674242955/main-categories/donate-heart-solid-96_n70lpq.png",
                SubCategories = new List<Category> { 
                    new Category { Name = "I need help" },
                    new Category { Name = "I offer help" }
                }
            },
            new MainCategory { 
                Name = "For children", 
                PhotoUrl = "https://res.cloudinary.com/dfs6whqzs/image/upload/v1674242955/main-categories/baby-carriage-solid-96_nbpuhk.png",
                SubCategories = new List<Category> {
                    new Category { Name = "Baby clothes" },
                    new Category { Name = "Toys" },
                    new Category { Name = "Nutrition" }
                }
            },
            new MainCategory { 
                Name = "Realty", 
                PhotoUrl = "https://res.cloudinary.com/dfs6whqzs/image/upload/v1674242955/main-categories/key-solid-96_hftmhp.png",
                SubCategories = new List<Category> {
                    new Category { Name = "Apartments" },
                    new Category { Name = "Houses" },
                    new Category { Name = "Garages" }
                }
            },
            new MainCategory { 
                Name = "Transport", 
                PhotoUrl = "https://res.cloudinary.com/dfs6whqzs/image/upload/v1674242955/main-categories/car-solid-96_tzqygl.png",
                SubCategories = new List<Category> {
                    new Category { Name = "Cars" },
                    new Category { Name = "Bikes" },
                    new Category { Name = "Buses" }
                }
            },
            new MainCategory { 
                Name = "Spare parts", 
                PhotoUrl = "https://res.cloudinary.com/dfs6whqzs/image/upload/v1674242955/main-categories/wrench-solid-96_btyqrm.png",
                SubCategories = new List<Category> {
                    new Category { Name = "Auto spare parts" },
                    new Category { Name = "Video recorders" },
                    new Category { Name = "Wheels and tires" }
                }
            },
            new MainCategory { 
                Name = "Job", 
                PhotoUrl = "https://res.cloudinary.com/dfs6whqzs/image/upload/v1674242819/main-categories/briefcase-solid-96_fhc7se.png",
                SubCategories = new List<Category> {
                    new Category { Name = "Security" },
                    new Category { Name = "Medicine" },
                    new Category { Name = "IT" }
                }
            },
            new MainCategory { 
                Name = "Animals", 
                PhotoUrl = "https://res.cloudinary.com/dfs6whqzs/image/upload/v1674242819/main-categories/cat-solid-96_i35w4q.png",
                SubCategories = new List<Category> {
                    new Category { Name = "Cats" },
                    new Category { Name = "Dogs" },
                    new Category { Name = "Animal products" }
                }
            },
            new MainCategory { 
                Name = "House and garden", 
                PhotoUrl = "https://res.cloudinary.com/dfs6whqzs/image/upload/v1674242819/main-categories/building-house-solid-96_ucduvn.png",
                SubCategories = new List<Category> {
                    new Category { Name = "Furniture" },
                    new Category { Name = "Garden" },
                    new Category { Name = "Interior items" }
                }
            },
            new MainCategory { 
                Name = "Electronics", 
                PhotoUrl = "https://res.cloudinary.com/dfs6whqzs/image/upload/v1674239930/main-categories/tv-solid-96_1_pifkzw.png",
                SubCategories = new List<Category> {
                    new Category { Name = "Smartphones" },
                    new Category { Name = "PCs" },
                    new Category { Name = "TV" }
                }
            },
            new MainCategory {
                Name = "Business and services", 
                PhotoUrl = "https://res.cloudinary.com/dfs6whqzs/image/upload/v1674242819/main-categories/dollar-circle-solid-96_trmr52.png",
                SubCategories = new List<Category> {
                    new Category { Name = "Transport services" },
                    new Category { Name = "Law services" },
                    new Category { Name = "Babysitters" }
                }
            },
            new MainCategory { 
                Name = "Fashion and style", 
                PhotoUrl = "https://res.cloudinary.com/dfs6whqzs/image/upload/v1674242819/main-categories/t-shirt-solid-96_y3kgrq.png",
                SubCategories = new List<Category> {
                    new Category { Name = "Women clothes" },
                    new Category { Name = "Accessories" },
                    new Category { Name = "Underwear" }
                }
            },
            new MainCategory { 
                Name = "Hobby, rest and sport", 
                PhotoUrl = "https://res.cloudinary.com/dfs6whqzs/image/upload/v1674242819/main-categories/football-regular-96_wgwuwe.png",
                SubCategories = new List<Category> {
                    new Category { Name = "Musical Instruments" },
                    new Category { Name = "Books and magazines" },
                    new Category { Name = "Antiques" }
                }
            },
            new MainCategory { 
                Name = "For free", 
                PhotoUrl = "https://res.cloudinary.com/dfs6whqzs/image/upload/v1674242819/main-categories/purchase-tag-solid-96_e0pi0g.png"
            },
            new MainCategory { 
                Name = "Exchange", 
                PhotoUrl = "https://res.cloudinary.com/dfs6whqzs/image/upload/v1674242819/main-categories/collapse-horizontal-regular-96_kfdrld.png"
            },
            new MainCategory { 
                Name = "Generators", 
                PhotoUrl = "https://res.cloudinary.com/dfs6whqzs/image/upload/v1674242819/main-categories/bulb-solid-96_fsawfj.png"
            }
        };

        _uow.MainCategoryRepository.AddRange(mainCategories);
        _uow.Save();
    }
}