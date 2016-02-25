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
      bool idEquality = this.getID() == newRestaurant.getID();
      bool nameEquality = this.getName() == newRestaurant.getName();
      bool addressEquality = this.getAddress() == newRestaurant.getAddress();
      bool phoneNumberEquality = this.getPhoneNumber() == newRestaurant.getPhoneNumber();
      bool cuisineIdEquality = this.getCusineId() == newRestaurant.getCusineId();

      return (idEquality && nameEquality && addressEquality && phoneNumberEquality && cuisineIdEquality);
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
      Restaurant newRestaurant = new Restaurant(name, address, phoneNumber, cusineId, restaurantId);
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

    public static Restaurant Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM restaurant WHERE id = @RestaurantId;", conn);
      SqlParameter restaurantIdParameter = new SqlParameter();
      restaurantIdParameter.ParameterName = "@RestaurantId";
      restaurantIdParameter.Value = id.ToString();
      cmd.Parameters.Add(restaurantIdParameter);
      rdr = cmd.ExecuteReader();

      int foundRestaurantId = 0;
      string foundRestaurantName = null;
      string foundRestaurantAddress = null;
      string foundRestaurantPhoneNumber = null;
      int foundCuisineId = 0;

      while(rdr.Read())
      {
        foundRestaurantId = rdr.GetInt32(0);
        foundRestaurantName = rdr.GetString(1);
        foundRestaurantAddress = rdr.GetString(2);
        foundRestaurantPhoneNumber = rdr.GetString(3);
        foundCuisineId = rdr.GetInt32(4);
      }
      Restaurant foundRestaurant = new Restaurant(foundRestaurantName, foundRestaurantAddress, foundRestaurantPhoneNumber, foundCuisineId, foundRestaurantId);

    if (rdr != null)
    {
      rdr.Close();
    }
    if (conn != null)
    {
      conn.Close();
    }
    return foundRestaurant;

    }

    public void UpdateName(string newName)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE restaurant SET name = @NewName OUTPUT INSERTED.name WHERE id = @restaurantId;", conn);

      SqlParameter newNameParameter = new SqlParameter();
      newNameParameter.ParameterName = "@NewName";
      newNameParameter.Value = newName;
      cmd.Parameters.Add(newNameParameter);


      SqlParameter restaurantIdParameter = new SqlParameter();
      restaurantIdParameter.ParameterName = "@restaurantId";
      restaurantIdParameter.Value = this.getID();
      cmd.Parameters.Add(restaurantIdParameter);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._name = rdr.GetString(0);
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

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM restaurant WHERE id = @restaurantId;", conn);

      SqlParameter categoryIdParameter = new SqlParameter();
      categoryIdParameter.ParameterName = "@restaurantId";
      categoryIdParameter.Value = this.getID();

      cmd.Parameters.Add(categoryIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }
  }
}
