
using Administration.Domain.Restaurants.Models;

namespace Administration.Domain.Restaurants.Repositories
{
    public interface IRestaurantRepository
    {
        void Save(Restaurant model);
    }
}
