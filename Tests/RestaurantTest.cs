using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace RestaurantNamespace
{
  public class RestaurantTest : IDisposable
  {
    public RestaurantTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=restaurants_db_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Restaurant.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueIfDescriptionsAreTheSame()
    {
      //Arrange, Act
      Restaurant firstRestaurant = new Restaurant("Red Lobster", "SW Funtime Lane", "503-555-5555", 3);
      Restaurant secondRestaurant = new Restaurant("Red Lobster", "SW Funtime Lane", "503-555-5555", 3);

      //Assert
      Assert.Equal(firstRestaurant, secondRestaurant);
    }
    [Fact]
    public void Test_To_Find_if_Restaurant_is_saving()
    {
      //Arrange, Act
      Restaurant firstRestaurant = new Restaurant("Red Lobster", "SW Funtime Lane", "503-555-5555", 3);

      firstRestaurant.Save();
      List<Restaurant> result = Restaurant.GetAll();
      List<Restaurant> testList = new List<Restaurant>{firstRestaurant};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_To_Assign_ID()
    {
      //Arrange, Act
      Restaurant restaurant = new Restaurant("Red Lobster", "SW Funtime Lane", "503-555-5555", 3);

      restaurant.Save();
      Restaurant savedRestaurant = Restaurant.GetAll()[0];

      int testID = restaurant.getID();
      int result = savedRestaurant.getID();

      //Assert
      Assert.Equal(testID, result);
    }
    [Fact]
    public void Test_Find_FindsRestaurantInDatabase()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("Red Lobster", "SW Funtime Lane", "503-555-5555", 3);
      testRestaurant.Save();

      //Act
      Restaurant foundRestaurant = Restaurant.Find(testRestaurant.getID());

      //Assert
      Assert.Equal(testRestaurant, foundRestaurant);
    }

    [Fact]

    public void Test_Update_UpdatesCuisineInDatabase()
    {
      string name1 = "Thai";
      Cuisine testCuisine1 = new Cuisine(name1);
      testCuisine1.Save();
      //Arrange
      string name = "RedLobster";
      Restaurant testRestaurant = new Restaurant(name, "happyStreet", "333444343", testCuisine1.GetId());
      testRestaurant.Save();
      string newName = "GreenLobster";

      //Act
      testRestaurant.UpdateName(newName);

      string result = testRestaurant.getName();

      //Assert
      Assert.Equal(newName, result);
    }

    [Fact]
    public void Test_Delete_DeletesRestaurantFromDatabase()
    {
      //Arrange
      string name1 = "Thai";
      Cuisine testCuisine1 = new Cuisine(name1);
      testCuisine1.Save();

      string name2 = "Italian";
      Cuisine testCuisine2 = new Cuisine(name2);
      testCuisine2.Save();

      Restaurant testRestaurants1 = new Restaurant("RedLobster", "happyStreet", "333444343", testCuisine1.GetId());
      testRestaurants1.Save();
      Restaurant testRestaurants2 = new Restaurant("GreenLobster", "happyStreet", "333444343", testCuisine2.GetId());
      testRestaurants2.Save();

      //Act
      testRestaurants1.Delete();
      // List<Cuisine> resultCuisines = Cuisine.GetAll();
      // List<Cuisine> testCuisineList = new List<Cuisine> {testCuisine2};

      List<Restaurant> resultRestaurants = Restaurant.GetAll();
      List<Restaurant> testRestaurantsList = new List<Restaurant> {testRestaurants2};

      //Assert
      // Assert.Equal(testCuisineList, resultCuisines);
      Assert.Equal(testRestaurantsList, resultRestaurants);
    }

    public void Dispose()
    {
      Restaurant.DeleteAll();
      Cuisine.DeleteAll();
    }
  }
}
