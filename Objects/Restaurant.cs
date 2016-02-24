using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace RestaurantNamespace
{
  public class Restaurant
  {
    private int _id;
    private string _name;
    private string _address;
    private string _phoneNumber;
    private int _cusine_id;

  public Restaurant(string name, string address, string phoneNumber, int cusineId, int id = 0)
  {
    _id = id;
    _name = name;
    _address = address;
    _phoneNumber = phoneNumber;
    _cusine_id = cusineId;
  }

  public override bool Equals(System.Object otherRestaurant)
  {
    if(!(otherRestaurant is Restaurant))
    {
      return false;
    }
    else
    {
      Restaurant newRestaurant = (Restaurant) otherRestaurant;
      bool nameEquality = this.getName() == newRestaurant.getName();
      bool addressEquality = this.getAddress() == newRestaurant.getAddress();
      bool phoneNumberEquality = this.getPhoneNumber() == newRestaurant.getPhoneNumber();
      bool cuisineIdEquality = this.getCusineId() == newRestaurant.getCusineId();

      return (nameEquality && addressEquality && phoneNumberEquality && cuisineIdEquality);
    }
  }

  public void Save()
  {

    SqlConnection conn = DB.Connection();
    SqlDataReader rdr;
    conn.Open();

    SqlCommand cmd = new SqlCommand("INSERT INTO restaurant (name, address, phone_number, cusine_id) OUTPUT INSERTED.id VALUES (@RestaurantName, @RestaurantAddress, @RestaurantPhoneNumber, @RestaurantCuisineId);", conn);

    SqlParameter nameParameter = new SqlParameter();
    nameParameter.ParameterName = "@RestaurantName";
    nameParameter.Value = this.getName();

    SqlParameter addressParameter = new SqlParameter();
    addressParameter.ParameterName = "@RestaurantAddress";
    addressParameter.Value = this.getAddress();

    SqlParameter phoneParameter = new SqlParameter();
    phoneParameter.ParameterName = "@RestaurantPhoneNumber";
    phoneParameter.Value = this.getPhoneNumber();

    SqlParameter cusineParameter = new SqlParameter();
    cusineParameter.ParameterName = "@RestaurantCuisineId";
    cusineParameter.Value = this.getCusineId();

    cmd.Parameters.Add(nameParameter);
    cmd.Parameters.Add(addressParameter);
    cmd.Parameters.Add(phoneParameter);
    cmd.Parameters.Add(cusineParameter);

    rdr = cmd.ExecuteReader();

    while(rdr.Read())
    {
      this._id = rdr.GetInt32(0);
    }
    if (rdr != null)
    {
      rdr.Close();
    }
    if (conn != null)
    {
      conn.Close();
    }
  }

  public static void DeleteAll()
  {
    SqlConnection conn = DB.Connection();
    conn.Open();
    SqlCommand cmd = new SqlCommand("DELETE FROM restaurant;", conn);
    cmd.ExecuteNonQuery();
  }

  public int getID()
  {
    return _id;
  }

  public string getName()
  {
    return _name;
  }

  public string getAddress()
  {
    return _address;
  }

  public string getPhoneNumber()
  {
    return _phoneNumber;
  }

  private int getCusineId()
  {
    return _cusine_id;
  }

  public static List<Restaurant> GetAll()
  {
    List<Restaurant> allRestaurants = new List<Restaurant>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();
      SqlCommand cmd = new SqlCommand("SELECT * FROM restaurant;", conn);
            rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int restaurantId = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string address = rdr.GetString(2);
        string phoneNumber = rdr.GetString(3);
        int cusineId = rdr.GetInt32(4);
        Restaurant newRestaurant = new Restaurant(name, address, phoneNumber, cusineId);
        allRestaurants.Add(newRestaurant);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allRestaurants;

      }
  }
}
