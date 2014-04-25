using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DM = ArkyMapsDomainModel;
using EDM = ArkyMapsDal;

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
        public void QueryPhoneUserByUsernameAndPassword()
        {
            string username = "test3";
            string password = "test3";
            long expectedId = 3;

            DM.PhoneUser phoneUser = m_phoneUserService.QueryUserByUsernameAndPassword(username, password);

            Assert.IsNotNull(phoneUser);
            Assert.AreEqual(expectedId, phoneUser.ID);
        }
        #endregion
    }
}
