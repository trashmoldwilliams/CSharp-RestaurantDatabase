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

      Get["/restaurants/delete"] = _ => {
        Restaurant.DeleteAll();
        return View["success.cshtml"];
      };

      Get["/cuisines/delete"] = _ => {
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

      Get["/cuisine/edit/{id}"] = parameters => {
        Cuisine SelectedCuisine = Cuisine.Find(parameters.id);
        return View["cuisine_edit.cshtml", SelectedCuisine];
      };

      Patch["/cuisine/edit/{id}"] = parameters => {
      Cuisine SelectedCuisine = Cuisine.Find(parameters.id);
      SelectedCuisine.Update(Request.Form["cuisine-name"]);
      return View["success.cshtml"];
      };

      Get["/cuisine/delete/{id}"] = parameters => {
        Cuisine SelectedCuisine = Cuisine.Find(parameters.id);
        return View["cuisine_delete.cshtml", SelectedCuisine];
      };

      Delete["/cuisine/delete/{id}"] = parameters => {
        Cuisine SelectedCuisine = Cuisine.Find(parameters.id);
        SelectedCuisine.Delete();
        return View["success.cshtml"];
      };
      Get["/restaurants/edit/{id}"] = parameters => {
        Restaurant SelectedRestaurant = Restaurant.Find(parameters.id);
        return View["restaurant_edit.cshtml", SelectedRestaurant];
      };
      Patch["/restaurants/edit/{id}"] = parameters => {
        Restaurant SelectedRestaurant = Restaurant.Find(parameters.id);
        SelectedRestaurant.UpdateName(Request.Form["edit"]);
        return View["success.cshtml"];
      };

      Get["/restaurant/delete/{id}"] = parameters => {
        Restaurant SelectedRestaurant = Restaurant.Find(parameters.id);
        return View["restaurant_delete.cshtml", SelectedRestaurant];
      };

      Delete["/restaurant/delete/{id}"] = parameters => {
        Restaurant SelectedRestaurant = Restaurant.Find(parameters.id);
        SelectedRestaurant.Delete();
        return View["success.cshtml"];
      };
    }
  }
}
