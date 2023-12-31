﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using StartingASP.NETWebApp.Core;
using StartingASP.NETWebApp.Data;

namespace StartingASP.NETWebApp.Pages;

public class Edit : PageModel
{
    private readonly IRestaurantData _restaurantData;
    private readonly IHtmlHelper _htmlHelper;
    [BindProperty] public Core.Restaurant Restaurant { get; set; }
    public IEnumerable<SelectListItem> Cuisines { get; set; }

    public Edit(IRestaurantData restaurantData, IHtmlHelper htmlHelper)
    {
        _restaurantData = restaurantData;
        _htmlHelper = htmlHelper;
    }

    public IActionResult OnGet(int? restaurantId)
    {
        Cuisines = _htmlHelper.GetEnumSelectList<CuisineType>();

        if (restaurantId.HasValue)
        {
            Restaurant = _restaurantData.GetById(restaurantId.Value);
        }
        else
        {
            Restaurant = new Core.Restaurant();
        }
        
        if (Restaurant == null)
        {
            return RedirectToPage("/NotFound");
        }

        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            Cuisines = _htmlHelper.GetEnumSelectList<CuisineType>();
            return Page();
        }

        if (Restaurant.Id <= 0 )
        {
            Restaurant = _restaurantData.Add(Restaurant);
        }
        else
        {
            Restaurant = _restaurantData.Update(Restaurant);
        }
        _restaurantData.Commit();
        return RedirectToPage("/Detail", new { restaurantId = Restaurant.Id });
    }
}