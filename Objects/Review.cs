using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace RestaurantNamespace
{
  public class Review
  {
    private int _id;
    private int _restaurant_id;
    private int _stars;

    public Review(int stars, int restaurantId, int id = 0)
    {
      _stars = stars;
      _restaurant_id = restaurantId;
      _id = id;
    }

    public override bool Equals(System.Object otherReview)
    {
      if(!(otherReview is Review))
      {
        return false;
      }
      else
      {
        Review newReview = (Review) otherReview;
        bool idEquality = this.getID() == newReview.getID();
        bool restaurantIdEquity = this.getRestaurantId() == newReview.getRestaurantId();
        bool starsEquity = this.getStars() == newReview.getStars();

        return (idEquality && restaurantIdEquity && starsEquity);
      }
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO reviews (stars, restaurant_id) OUTPUT INSERTED.id VALUES (@Stars, @RestaurantId);", conn);

      SqlParameter restaurantIdParameter = new SqlParameter();
      restaurantIdParameter.ParameterName = "@RestaurantId";
      restaurantIdParameter.Value = this.getRestaurantId();

      SqlParameter StarsParameter = new SqlParameter();
      StarsParameter.ParameterName = "@Stars";
      StarsParameter.Value = this.getStars();

      cmd.Parameters.Add(restaurantIdParameter);
      cmd.Parameters.Add(StarsParameter);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM reviews;", conn);
      cmd.ExecuteNonQuery();
    }

    public int getID() {
      return _id;
    }

    public int getRestaurantId() {
      return _restaurant_id;
    }

    public int getStars() {
      return _stars;
    }


    public static List<Review> GetAll()
    {
      List<Review> allReviews = new List<Review>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();
      SqlCommand cmd = new SqlCommand("SELECT * FROM reviews;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int reviewId = rdr.GetInt32(0);
        int restaurant_id = rdr.GetInt32(1);
        int stars = rdr.GetInt32(2);

        Review newReview = new Review(stars, restaurant_id, reviewId);
        allReviews.Add(newReview);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allReviews;
    }

    public static Review Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM reviews WHERE id = @ReviewId;", conn);
      SqlParameter reviewIdParameter = new SqlParameter();
      reviewIdParameter.ParameterName = "@ReviewId";
      reviewIdParameter.Value = id.ToString();
      cmd.Parameters.Add(reviewIdParameter);
      rdr = cmd.ExecuteReader();

      int foundReviewId = 0;
      int foundRestaurantId = 0;
      int foundStars = 0;


      while(rdr.Read())
      {
        foundReviewId = rdr.GetInt32(0);
        foundRestaurantId = rdr.GetInt32(1);
        foundStars = rdr.GetInt32(2);

      }
      Review foundReview = new Review(foundStars, foundRestaurantId, foundReviewId);

    if (rdr != null)
    {
      rdr.Close();
    }
    if (conn != null)
    {
      conn.Close();
    }
    return foundReview;

    }



  }
}
