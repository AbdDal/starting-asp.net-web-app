using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StartingASP.NETWebApp.Data;

namespace StartingASP.NETWebApp.Pages;

public class Restaurant : PageModel
{
    private readonly IRestaurantData _restaurantData;
    public IEnumerable<Core.Restaurant> Resturants { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public string SearchTerm { get; set; }

    public Restaurant(IRestaurantData restaurantData)
    {
        _restaurantData = restaurantData;
    }
    public void OnGet()
    {
        Resturants = _restaurantData.GetRestaurantByName(SearchTerm);
    }
}