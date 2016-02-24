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

    public void Dispose()
    {
      Restaurant.DeleteAll();
      Cuisine.DeleteAll();
    }
  }
}
