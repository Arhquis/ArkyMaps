using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace ArkyMapService
{
    public class ServiceController
    {
        #region attributes
        private static ServiceController m_instance;
        private List<ServiceHost> m_services = new List<ServiceHost>();
        private Dictionary<long, IMapServiceCallback> m_registeredUsers = new Dictionary<long,IMapServiceCallback>();
        #endregion


        #region singleton implementation
        public static ServiceController Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new ServiceController();
                }

                return m_instance;
            }
        }
        #endregion


        #region constructors
        private ServiceController()
        {
            // NOTE: intentionally left blank.
        }
        #endregion


        #region lifetime
        internal bool StartService()
        {
            bool rvSucceeded = false;

            try
            {
                m_services.Add(new ServiceHost(typeof(MapService)));
                m_services.Add(new ServiceHost(typeof(PhoneService)));

                foreach (ServiceHost service in m_services)
                {
                    service.Open();
                }

                rvSucceeded = true;
            }
            catch (Exception ex)
            {
                foreach (ServiceHost service in m_services)
                {
                    if (service.State == CommunicationState.Opened)
                    {
                        service.Close();
                    }
                }

                throw ex;
            }

            return rvSucceeded;
        }


        internal void StopService()
        {
            foreach (ServiceHost service in m_services)
            {
                service.Close();
            }
        }
        #endregion


        #region map methods
        public bool LoginUser(string userName, string password, IMapServiceCallback callback)
        {
            // if registered user
            m_registeredUsers.Add(1, callback);

            return true;
        }


        public void LogoutUser(long userId)
        {
            m_registeredUsers.Remove(userId);
        }
        #endregion


        #region phone methods
        public void NewLocation(long userId, long lon, long lat)
        {
            foreach (IMapServiceCallback user in m_registeredUsers.Values)
            {
                user.NewLocation(userId, lon, lat);
            }
        }
        #endregion
    }
}
