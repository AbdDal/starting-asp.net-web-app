using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StartingASP.NETWebApp.Data;

namespace StartingASP.NETWebApp.Pages;

public class Detail : PageModel
{
    private readonly IRestaurantData _restaurantData;
    public Core.Restaurant Restaurant { get; set; }

    public Detail(IRestaurantData restaurantData)
    {
        _restaurantData = restaurantData;
    }
    
    public IActionResult OnGet(int restaurantId)
    {
        Restaurant = _restaurantData.GetById(restaurantId);

        if (Restaurant == null)
        {
            return RedirectToPage("/NotFound");
        }

        return Page();
    }
}