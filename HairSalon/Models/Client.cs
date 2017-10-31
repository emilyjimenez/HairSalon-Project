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

    public static List<Client> GetAll()
    {
      List<Client> allClients = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int clientId = rdr.GetInt32(0);
        string clientName = rdr.GetString(1);
        int clientBirthday = rdr.GetInt32(2);
        string clientEmail = rdr.GetString(3);
        int stylistId = rdr.GetInt32(4);

        Client newClient = new Client(clientName, clientBirthday, clientEmail, stylistId, clientId);
        allClients.Add(newClient);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allClients;
    }

    public static List<Client> GetAllClientsByStylist(int inputId)
    {
      List<Client> stylistClients = new List<Client>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients WHERE stylist_id = @stylistId;";

      MySqlParameter stylistId = new MySqlParameter();
      stylistId.ParameterName = "@stylistId";
      stylistId.Value = inputId;
      cmd.Parameters.Add(stylistId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int returnId = rdr.GetInt32(0);
        string returnName = rdr.GetString(1);
        int returnBirthday = rdr.GetInt32(2);
        string returnEmail = rdr.GetString(3);
        int returnStylistId = rdr.GetInt32(4);
        Client returnClient = new Client(returnName, returnBirthday, returnEmail, returnStylistId, returnId);
        stylistClients.Add(returnClient);
      }

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return stylistClients;
    }

    public override bool Equals(System.Object otherClient)
    {
      if (!(otherClient is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client) otherClient;
        bool idEquality = (this.Id == newClient.Id);
        bool nameEquality = (this.Name == newClient.Name);
        bool birthdayEquality = (this.Birthday == newClient.Birthday);
        bool emailEquality = (this.Email == newClient.Email);
        bool stylistIdEquality = (this.StylistId == newClient.StylistId);
        return (idEquality && nameEquality && birthdayEquality && emailEquality && stylistIdEquality);
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO clients (name, birthday, email, stylist_id) VALUES (@ClientName, @ClientBirthday, @ClientEmail, @ClientStylistId);";
      //name
      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@ClientName";
      name.Value = this.Name;
      cmd.Parameters.Add(name);
      //birthday
      MySqlParameter birthday = new MySqlParameter();
      birthday.ParameterName = "@ClientBirthday";
      birthday.Value = this.Birthday;
      cmd.Parameters.Add(birthday);
      //email
      MySqlParameter email = new MySqlParameter();
      email.ParameterName = "@ClientEmail";
      email.Value = this.Email;
      cmd.Parameters.Add(email);
      //stylist
      MySqlParameter stylistId = new MySqlParameter();
      stylistId.ParameterName = "@ClientStylistId";
      stylistId.Value = this.StylistId;
      cmd.Parameters.Add(stylistId);

      cmd.ExecuteNonQuery();
      Id = (int)cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static Client Find(int searchId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = searchId;

      cmd.Parameters.Add(thisId);

      int clientId = 0;
      string clientName = "";
      int clientBirthday = 0;
      string clientEmail = "";
      int clientStylistId = 0;

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        clientId = rdr.GetInt32(0);
        clientName = rdr.GetString(1);
        clientBirthday = rdr.GetInt32(2);
        clientEmail = rdr.GetString(3);
        clientStylistId = rdr.GetInt32(4);
      }

      Client foundClient = new Client(clientName, clientBirthday, clientEmail, clientStylistId, clientId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundClient;
    }

    public void Update(string newName, int newBirthday, string newEmail)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE clients SET name = @newName, birthday = @newBirthday, email = @newEmail WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = Id;
      cmd.Parameters.Add(searchId);

      //name
      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@newName";
      name.Value = newName;
      cmd.Parameters.Add(name);
      //birthday
      MySqlParameter birthday = new MySqlParameter();
      birthday.ParameterName = "@newBirthday";
      birthday.Value = newBirthday;
      cmd.Parameters.Add(birthday);
      //email
      MySqlParameter email = new MySqlParameter();
      email.ParameterName = "@newEmail";
      email.Value = newEmail;
      cmd.Parameters.Add(email);

      cmd.ExecuteNonQuery();
      Name = newName;
      Birthday = newBirthday;
      Email = newEmail;

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
      cmd.CommandText = @"DELETE FROM clients WHERE id = @searchId;";

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

    public static void DeleteClientsByStylist(int inputId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM clients WHERE stylist_id = @stylistId;";

      MySqlParameter stylistId = new MySqlParameter();
      stylistId.ParameterName = "@stylistId";
      stylistId.Value = inputId;
      cmd.Parameters.Add(stylistId);

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
