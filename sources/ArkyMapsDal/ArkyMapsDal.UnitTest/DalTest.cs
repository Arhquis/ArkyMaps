using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DM = ArkyMapsDomainModel;
using EDM = ArkyMapsDal;
using System.Collections.Generic;

namespace ArkyMapsDal.UnitTest
{
    /// <summary>
    /// The class holds unit tests of data access layer.
    /// </summary>
    [TestClass]
    public class DalTest
    {
        #region attributes
        private ClientUserService m_clientUserService;
        private PhoneUserService m_phoneUserService;
        #endregion


        #region setup
        [TestInitialize]
        public void SetUp()
        {
            DalServices dalServices = new DalServices();

            m_clientUserService = dalServices.ClientUserService;
            m_phoneUserService = dalServices.PhoneUserService;
        }
        #endregion


        #region client user
        [TestMethod]
        public void QueryClientUserByUsernameAndPassword()
        {
            string username = "test";
            string password = "test";
            long expectedId = 1;

            DM.ClientUser clientUser = m_clientUserService.QueryUserByUsernameAndPassword(username, password);

            Assert.IsNotNull(clientUser);
            Assert.AreEqual(expectedId, clientUser.ID);
        }
        #endregion


        #region phone user
        [TestMethod]
        public void QueryPhoneUsers()
        {
            IEnumerable<DM.PhoneUser> phoneUsers = m_phoneUserService.QueryPhoneUsers();

            Assert.IsNotNull(phoneUsers);
        }


        [TestMethod]
        public void CreatePhoneUser()
        {
            DM.PhoneUser phoneUser = new DM.PhoneUser()
            {
                UserName = "UnitTest",
                Password = "UnitTest",
                Name = "Unit Test",
                Male = true,
                Email = "unit.test@unitTest.com"
            };

            bool succeeded = m_phoneUserService.CreatePhoneUser(phoneUser);

            Assert.IsTrue(succeeded);
        }


        [TestMethod]
        public void ModifyPhoneUser()
        {
            string username = "UnitTest";
            string password = "UnitTest";

            DM.PhoneUser phoneUser = m_phoneUserService.QueryPhoneUserByUsernameAndPassword(username, password);

            phoneUser.Name = "Modified phone user name";

            bool succeeded = m_phoneUserService.ModifyPhoneUser(phoneUser);

            Assert.IsTrue(succeeded);
        }


        [TestMethod]
        public void DeletePhoneUser()
        {
            string username = "UnitTest";
            string password = "UnitTest";

            DM.PhoneUser phoneUser = m_phoneUserService.QueryPhoneUserByUsernameAndPassword(username, password);

            bool succeeded = m_phoneUserService.DeletePhoneUser(phoneUser);

            Assert.IsTrue(succeeded);
        }


        [TestMethod]
        public void QueryPhoneUserByUsernameAndPassword()
        {
            string username = "test3";
            string password = "test3";
            long expectedId = 3;

            DM.PhoneUser phoneUser = m_phoneUserService.QueryPhoneUserByUsernameAndPassword(username, password);

            Assert.IsNotNull(phoneUser);
            Assert.AreEqual(expectedId, phoneUser.ID);
        }


        [TestMethod]
        public void QueryPhoneUserById()
        {
            long userId = 2;

            DM.PhoneUser phoneUser = m_phoneUserService.QueryPhoneUserById(userId);

            Assert.IsNotNull(phoneUser);
            Assert.AreEqual(userId, phoneUser.ID);
        }
        #endregion
    }
}
