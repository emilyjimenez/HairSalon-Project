using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;

namespace HairSalon.Models.Tests
{
  [TestClass]
  public class ClientTests : IDisposable
  {
    public ClientTests()
    {
    DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=emily_jimenez_test;";
    }

    public void Dispose()
    {
      Client.ClearAll();
    }

    [TestMethod]
    public void GetAll_ClientEmptyAtFirst_0()
    {
      int result = Client.GetAll().Count;
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void GetAllClientsByStylist_GetsClientsInDatabaseByStylistId_ClientList()
    {
      Client client1 = new Client("Joanne", 092982, "joanneS@me.com", 1);
      client1.Save();
      Client client2 = new Client("Cynthia", 101188, "c.smith@.com", 2);
      client2.Save();
      Client client3 = new Client("Becky", 121479, "b_sanchez@yahoo.com", 1);
      client3.Save();

      List<Client> testList = Client.GetAllClientsByStylist(1);
      List<Client> expectedList = new List<Client>{client1, client3};

      CollectionAssert.AreEqual(testList, expectedList);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfClientsAreTheSame_Client()
    {
      Client client1 = new Client("Joanne", 092982, "joanneS@me.com", 1);
      Client client2 = new Client("Joanne", 092982, "joanneS@me.com", 1);

      Assert.AreEqual(client1, client2);
    }

    [TestMethod]
    public void Save_SavesClientToDatabase_ClientList()
    {
      Client testClient = new Client("Joanne", 092982, "joanneS@me.com", 1);
      testClient.Save();

      List<Client> result = Client.GetAll();
      List<Client> testList = new List<Client>{testClient};

      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Find_FindsClientInDatabase_Client()
    {
      Client testClient = new Client("Joanne", 092982, "joanneS@me.com", 1);
      testClient.Save();

      Client foundClient = Client.Find(testClient.Id);

      Assert.AreEqual(testClient, foundClient);
    }

    [TestMethod]
    public void Update_UpdateClientInDatabase_Client()
    {
      Client testClient = new Client("Joanne", 092982, "joanneS@me.com", 1);
      testClient.Save();

      string newName = "Cynthia";
      int newBirthday = 101188;
      string newEmail = "c.smith@gmail.com";

      Client newClient = new Client(newName, newBirthday, newEmail, 1);
      testClient.Update(newName, newBirthday, newEmail);

      Assert.AreEqual(newClient.Name, testClient.Name);
      Assert.AreEqual(newClient.Birthday, testClient.Birthday);
      Assert.AreEqual(newClient.Email, testClient.Email);
    }

    [TestMethod]
    public void Delete_DeletesClientInDatabase_Client()
    {
      Client client1 = new Client("Joanne", 092982, "joanneS@me.com", 1);
      client1.Save();
      Client client2 = new Client("Cynthia", 101188, "c.smith@.com", 2);
      client2.Save();

      List<Client> testList = new List<Client>{client2};
      client1.Delete();

      List<Client> result = Client.GetAll();

      CollectionAssert.AreEqual(testList, result);
    }
  }
}
