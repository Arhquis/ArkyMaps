using System;
using System.ServiceModel;

namespace ArkyMapService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class PhoneService : IPhoneService
    {
        #region attributes
        private ServiceController m_controller;
        #endregion


        #region constructors
        public PhoneService()
        {
            // NOTE: intentionally left blank.
        }
        #endregion


        #region IPhoneService members
        public long Login(string userName, string password)
        {
            Console.WriteLine("Phone login happened");

            return 1;
        }


        public void NewLocation(long userId, long lon, long lat)
        {
            ServiceController.Instance.NewLocation(userId, lon, lat);
        }
        #endregion
    }
}
