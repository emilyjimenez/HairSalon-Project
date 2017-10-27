using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;


namespace HairSalon.Models
{
  public class Client
  {
    public string Name {get; private set;}
    public int Birthday {get; private set;}
    public string Email {get; private set;}
    public int Id {get; private set;}
    public int StylistId {get; private set;}

    public Client(string name, int birthday, string email, int stylistId, int id = 0)
    {
      Name = name;
      Birthday = birthday;
      Email = email;
      Id = id;
      StylistId = stylistId;
    }

    public static void ClearAll()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM clients;";
        cmd.ExecuteNonQuery();

        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
      }
  }
}
