using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;


namespace HairSalon.Models
{
  public class Stylist
  {
    public string Name {get; private set;}
    public int Rate {get; private set;}
    public string Skills {get; private set;}
    public int Id {get; private set;}

    public Stylist(string name, int rate, string skills, int id = 0)
    {
      Name = name;
      Rate = rate;
      Skills = skills;
      Id = id;
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylists;";
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Stylist> GetAll()
    {
      List<Stylist> allStylists = new List<Stylist>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int stylistId = rdr.GetInt32(0);
        string stylistName = rdr.GetString(1);
        int stylistRate = rdr.GetInt32(2);
        string stylistSkills = rdr.GetString(3);
        Stylist newStylist = new Stylist(stylistName, stylistRate, stylistSkills, stylistId);
        allStylists.Add(newStylist);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allStylists;
    }

    public override bool Equals(System.Object otherStylist)
    {
      if (!(otherStylist is Stylist))
      {
        return false;
      }
      else
      {
        Stylist newStylist = (Stylist) otherStylist;
        bool idEquality = (this.Id == newStylist.Id);
        bool nameEquality = (this.Name == newStylist.Name);
        bool rateEquality = (this.Rate == newStylist.Rate);
        bool skillsEquality = (this.Skills == newStylist.Skills);
        return (idEquality && nameEquality && rateEquality && skillsEquality);
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylists (name, rate, skills) VALUES (@StylistName, @StylistRate, @StylistSkills);";

      //name
      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@StylistName";
      name.Value = this.Name;
      cmd.Parameters.Add(name);

      //rate
      MySqlParameter rate = new MySqlParameter();
      rate.ParameterName = "@StylistRate";
      rate.Value = this.Rate;
      cmd.Parameters.Add(rate);

      //skills
      MySqlParameter skills = new MySqlParameter();
      skills.ParameterName = "@StylistSkills";
      skills.Value = this.Skills;
      cmd.Parameters.Add(skills);

      cmd.ExecuteNonQuery();
      Id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static Stylist Find(int searchId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = searchId;
      cmd.Parameters.Add(thisId);

      int stylistId = 0;
      string stylistName = "";
      int stylistRate = 0;
      string stylistSkills = "";

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        stylistId = rdr.GetInt32(0);
        stylistName = rdr.GetString(1);
        stylistRate = rdr.GetInt32(2);
        stylistSkills = rdr.GetString(3);
      }

      Stylist foundStylist = new Stylist(stylistName, stylistRate, stylistSkills, stylistId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundStylist;
    }

    public void Update(string newName, int newRate, string newSkills)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE stylists SET name = @newName, rate = @newRate, skills = @newSkills WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = Id;
      cmd.Parameters.Add(searchId);

      //name
      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@newName";
      name.Value = newName;
      cmd.Parameters.Add(name);

      //rate
      MySqlParameter rate = new MySqlParameter();
      rate.ParameterName = "@newRate";
      rate.Value = newRate;
      cmd.Parameters.Add(rate);

      //skills
      MySqlParameter skills = new MySqlParameter();
      skills.ParameterName = "@newSkills";
      skills.Value = newSkills;
      cmd.Parameters.Add(skills);

      cmd.ExecuteNonQuery();
      Name = newName;
      Rate = newRate;
      Skills = newSkills;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylists where id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = this.Id;
      cmd.Parameters.Add(searchId);

      cmd.ExecuteNonQuery();
      conn.Close();

      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public override int GetHashCode()
    {
      return this.Id.GetHashCode();
    }
  }
}
