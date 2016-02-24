using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace RestaurantNamespace
{
  public class HomeModule : NancyModule
  {

    public HomeModule()
    {
      Get["/"] =_=> {
        return View["index.cshtml"];
      };

      Get["/cuisines"] = _ => {
      List<Cuisine> AllCuisines = Cuisine.GetAll();
      return View["allCuisines.cshtml", AllCuisines];
      };

      Get["cuisines/new"] = _ => {
        return View["cuisine_form.cshtml"];
      };

      Post["cuisines/new"] = _ => {
        Cuisine newCuisine = new Cuisine(Request.Form["cuisine-name"]);
        newCuisine.Save();
        return View["success.cshtml"];
      };

      Get["/restaurants/new"] = _ => {
        List<Cuisine> AllCuisines = Cuisine.GetAll();
        return View["restaurant_form.cshtml", AllCuisines];
      };

      Post["/restaurants/new"] = _ => {
        Restaurant newRestaurant = new Restaurant(Request.Form["name"], Request.Form["address"], Request.Form["phoneNumber"], Request.Form["cuisine-id"]);
        newRestaurant.Save();
        return View["success.cshtml"];
      };

      Post["/restaurants/delete"] = _ => {
        Restaurant.DeleteAll();
        return View["success.cshtml"];
      };

      Post["/cuisines/delete"] = _ => {
        Cuisine.DeleteAll();
        return View["success.cshtml"];
      };

      Get["/cuisines/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        var SelectedCuisine = Cuisine.Find(parameters.id);
        var CuisineRestaurants = SelectedCuisine.GetRestaurants();
        model.Add("cuisine", SelectedCuisine);
        model.Add("restaurants", CuisineRestaurants);
        return View["cuisine.cshtml", model];
      };
    }
  }
}
