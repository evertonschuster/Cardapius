using Administration.Domain.Restaurants.Models;
using Administration.Domain.Restaurants.Repositories;

namespace Administration.Infra.DataBase.EntityFramework.Restaurants.Repositories
{
    public class RestaurantRepository : Repository<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(IDbContext iDbContext) : base(iDbContext)
        {
        }

        public override void Save(Restaurant entity)
        {
            base.Save(entity);
        }
    }
}
