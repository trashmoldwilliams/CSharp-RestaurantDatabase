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


    public void Dispose()
    {
      Restaurant.DeleteAll();
    }
  }
}
