using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace RestaurantNamespace
{
  public class ReviewTest : IDisposable
  {
    public ReviewTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=restaurants_db_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Review.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueIfDescriptionsAreTheSame()
    {
      //Arrange, Act
      Review firstReview = new Review(3, 5);
      Review secondReview = new Review(3, 5);

      //Assert
      Assert.Equal(firstReview, secondReview);
    }
    [Fact]
    public void Test_To_Find_if_Review_is_saving()
    {
      //Arrange, Act
      Review firstReview = new Review(3, 5);

      firstReview.Save();
      List<Review>result = Review.GetAll();
      List<Review>testList = new List<Review>{firstReview};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_To_Assign_ID()
    {
      //Arrange, Act
      Review firstReview = new Review(3, 5);

      firstReview.Save();

      Review savedReview = Review.GetAll()[0];

      int testID = firstReview.getID();
      int result = savedReview.getID();

      //Assert

      Assert.Equal(result, testID);

    }
    [Fact]
    public void Test_Find_FindsReviewInDatabase()
    {
      //Arrange
      Review firstReview = new Review(3, 5);

      firstReview.Save();


      //Act
      Review foundReview = Review.Find(firstReview.getID());


      //Assert
      Assert.Equal(firstReview, foundReview);
    }


    public void Dispose()
    {
      Review.DeleteAll();
    }
  }
}
