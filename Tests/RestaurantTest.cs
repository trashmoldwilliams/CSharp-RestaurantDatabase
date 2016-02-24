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

    public void Dispose()
    {
      Restaurant.DeleteAll();
    }
  }
}
