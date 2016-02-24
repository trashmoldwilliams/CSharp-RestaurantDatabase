using System.Data;
using System.Data.SqlClient;

namespace RestaurantNamespace
{
  public class DB
  {
    public static SqlConnection Connection()
    {
      SqlConnection conn = new SqlConnection(DBConfiguration.ConnectionString);
      return conn;
    }
  }
}
