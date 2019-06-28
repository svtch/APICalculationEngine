using System;
using System.Collections.Generic;
using APICalculationEngine.Controllers;
using APICalculationEngine.Gestion;
using APICalculationEngine.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using System.Web;
using System.Linq;

namespace APICalculationEngine.Tests
{
    [TestClass]
    public class APITests
    {
        [TestMethod]
        public void TestDbConnction()
        {
            CalculatedMetricController cm = new CalculatedMetricController();
            var co = cm.DBConnexion();
            cm.connection.Close();
            Assert.IsTrue(co);
        }

        [TestMethod]
        public void TestMoyenne()
        {
            CalculGestion cg = new CalculGestion();
            List<int> myValues = new List<int>(new int[] { 5, 5, 5 });
            int total = 0;
            foreach (int i in myValues)
            {
                total = total + i;
            }
            Assert.AreEqual(5,cg.CalcMoyenne(myValues.Count(), total));
        }

        [TestMethod]
        public void TestMedian()
        {
            CalculGestion cg = new CalculGestion();
            List<int> myValues = new List<int>(new int[] { 4, 8, 12 });
            Assert.AreEqual(8, cg.CalcMedian(myValues));
        }

        [TestMethod]
        public void TestGetReturnType()
        {
            CalculatedMetricController cm = new CalculatedMetricController();
            cm.IsTest = true;
            var list = cm.Get("TESTMAC", DateTime.Now, MethodesGlobales.GoodDateAdd(DateTime.Now, 50), 1, "9:m");
            Assert.IsNotNull(list);
        }

    }
}
