using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace RestaurantNamespace
{
  public class CuisineTest : IDisposable
  {
    public CuisineTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=restaurants_db_test;Integrated Security=SSPI;";

    }

    [Fact]
    public void Test_Cuisine_Empty()
    {
      //Arrange, Act
      int result = Cuisine.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueForSameName()
    {
      //Arrange, Act
      Cuisine firstCuisine = new Cuisine("Italian");
      Cuisine secondCuisine = new Cuisine("Italian");

      //Assert
      Assert.Equal(firstCuisine, secondCuisine);
    }

    [Fact]
    public void Test_Save_SavesCuisineToDatabase()
    {
      //Arrange
      Cuisine testCuisine = new Cuisine("Italian");
      testCuisine.Save();

      //Act
      List<Cuisine> result = Cuisine.GetAll();
      List<Cuisine> testList = new List<Cuisine>{testCuisine};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_AssignsIdToCuisineObject()
    {
      //Arrange
      Cuisine testCuisine = new Cuisine("Italian");

      testCuisine.Save();

      //Act
      Cuisine savedCuisine = Cuisine.GetAll()[0];

      int result = savedCuisine.GetId();
      int testId = testCuisine.GetId();

      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_Find_FindsCuisineInDatabase()
    {
      //Arrange
      Cuisine testCuisine = new Cuisine("Italian");
      testCuisine.Save();

      //Act
      Cuisine foundCuisine = Cuisine.Find(testCuisine.GetId());

      //Assert
      Assert.Equal(testCuisine, foundCuisine);
    }

    [Fact]
    public void Test_Update_UpdatesCuisineInDatabase()
    {
      //Arrange
      string name = "Chinese";
      Cuisine testCuisine = new Cuisine(name);
      testCuisine.Save();
      string newName = "Italian";

      //Act
      testCuisine.Update(newName);

      string result = testCuisine.GetName();

      //Assert
      Assert.Equal(newName, result);
    }

    [Fact]
    public void Test_Delete_DeletesCuisineFromDatabase()
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
      testCuisine1.Delete();
      List<Cuisine> resultCuisines = Cuisine.GetAll();
      List<Cuisine> testCuisineList = new List<Cuisine> {testCuisine2};

      List<Restaurant> resultRestaurantss = Restaurant.GetAll();
      List<Restaurant> testRestaurantsList = new List<Restaurant> {testRestaurants2};

      //Assert
      Assert.Equal(testCuisineList, resultCuisines);
      Assert.Equal(testRestaurantsList, resultRestaurantss);
    }

    public void Dispose()
    {
      Restaurant.DeleteAll();
      Cuisine.DeleteAll();
    }
  }
}
