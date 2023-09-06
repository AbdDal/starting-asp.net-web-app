using StartingASP.NETWebApp.Core;

namespace StartingASP.NETWebApp.Data;

public interface IRestaurantData
{
    IEnumerable<Restaurant> GetAll();
    IEnumerable<Restaurant> GetRestaurantByName(string name = null);
    Restaurant GetById(int id);
    Restaurant Update(Restaurant updatedRestaurant);
    Restaurant Add(Restaurant newRestaurant);
    int Commit();
}

public class InMemoryRestaurantData : IRestaurantData
{
    readonly List<Restaurant> _restaurants = new()
    {
        new Restaurant() { Id = 1, Name = "DemoOne", Location = "Aleppo", Cuisine = CuisineType.Syrian},
        new Restaurant() { Id = 2, Name = "DemoTwo", Location = "Istanbul" , Cuisine = CuisineType.Turkish},
        new Restaurant() { Id = 3, Name = "DemoThree", Location = "Rome", Cuisine = CuisineType.Italian}
    };

    public IEnumerable<Restaurant> GetAll()
    {
        return _restaurants
            .OrderBy(x => x.Name);
    }

    public Restaurant GetById(int id)
    {
        return _restaurants.SingleOrDefault(x => x.Id == id);
    }

    public Restaurant Update(Restaurant updatedRestaurant)
    {
        var restaurant = _restaurants.SingleOrDefault(x => x.Id == updatedRestaurant.Id);
        if (restaurant != null)
        {
            restaurant.Name = updatedRestaurant.Name;
            restaurant.Location = updatedRestaurant.Location;
            restaurant.Cuisine = updatedRestaurant.Cuisine;
        }

        return restaurant;
    }

    public Restaurant Add(Restaurant newRestaurant)
    {
        _restaurants.Add(newRestaurant);
        newRestaurant.Id = _restaurants.Max(x => x.Id) + 1;

        return newRestaurant;
    }

    public int Commit()
    {
        return 0;
    }

    public IEnumerable<Restaurant> GetRestaurantByName(string name)
    {
        return _restaurants
            .Where(x => string.IsNullOrEmpty(name) || x.Name.StartsWith(name))
            .OrderBy(x => x.Name);
    }
}