
using Administration.Domain.Restaurants.Models;

namespace Administration.Domain.Restaurants.Repositories
{
    public interface IRestaurantRepository
    {
        Task SaveAsync(Restaurant model);
    }
}
