using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;

namespace HairSalon.Models.Tests
{
  [TestClass]
  public class StylistTests : IDisposable
  {
    public StylistTests()
    {
    DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=emily_jimenez_test;";
    }

    public void Dispose()
    {
      Stylist.ClearAll();
    }

    [TestMethod]
    public void GetAll_StylistsEmptyAtFirst_0()
    {
      int result = Stylist.GetAll().Count;

      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Equals_ReturnTrueForSameName_Stylist()
    {
      Stylist stylist1 = new Stylist("Michelle", 50, "Cut and Color", 1);
      Stylist stylist2 = new Stylist("Michelle", 50, "Cut and Color", 1);

      Assert.AreEqual(stylist1, stylist2);
    }

    [TestMethod]
    public void Save_SavesStylistToDatabase_Stylist()
    {
      Stylist testStylist = new Stylist("Michelle", 50, "Cut and Color", 1);
      testStylist.Save();

      List<Stylist> result = Stylist.GetAll();
      List<Stylist> testList = new List<Stylist>{testStylist};

      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Find_FindStylistInDatabase_Stylist()
    {
      Stylist testStylist = new Stylist("Michelle", 50, "Cut and Color", 1);
      testStylist.Save();

      Stylist foundStylist = Stylist.Find(testStylist.Id);

      Assert.AreEqual(testStylist, foundStylist);
    }

    [TestMethod]
    public void Update_UpdateStylistInDatabase_Stylist()
    {
      Stylist testStylist = new Stylist("Michelle", 50, "Cut and Color", 1);
      testStylist.Save();

      string newName = "Claire";
      int newRate = 55;
      string newSkills = "Lash Extensions, Cut and Color";

      Stylist newStylist = new Stylist(newName, newRate, newSkills, 1);
      testStylist.Update(newName, newRate, newSkills);

      Assert.AreEqual(newStylist.Name, testStylist.Name);
      Assert.AreEqual(newStylist.Rate, testStylist.Rate);
      Assert.AreEqual(newStylist.Skills, testStylist.Skills);
    }

    [TestMethod]
    public void Delete_DeleteStylistInDatabase_Stylist()
    {
      Stylist stylist1 = new Stylist("Michelle", 50, "Cut and Color", 1);
      stylist1.Save();
      Stylist stylist2 = new Stylist("Claire", 55, "Lash Extensions, Cut and Color", 2);
      stylist2.Save();

      List<Stylist> testList = new List<Stylist>{stylist2};
      stylist1.Delete();

      List<Stylist> result = Stylist.GetAll();

      CollectionAssert.AreEqual(testList, result);
    }
  }
}
