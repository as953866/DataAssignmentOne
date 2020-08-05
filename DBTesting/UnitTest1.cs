using System;
using DataAssignmentOne.DBUtility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DBTesting
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CheckDBOpenConnection()
        {
            DBOperation operation = new DBOperation();
            Assert.AreEqual(operation.CheckConnectionState(), true);
        }

        [TestMethod]
        public void CheckDBCloseConnection()
        {
            DBOperation operation = new DBOperation();
            operation.CloseConnection();
            Assert.AreEqual(operation.CheckConnectionState(), false);
        }
    }
}
