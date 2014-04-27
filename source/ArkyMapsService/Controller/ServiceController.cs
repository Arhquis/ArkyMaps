using ArkyMapsDal;
using ArkyMapsDomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

using DM = ArkyMapsDomainModel;

namespace ArkyMapService
{
    public class ServiceController
    {
        #region attributes
        private Logger m_logger;

        private static ServiceController m_instance;

        private DalServices m_dalServices;
        private List<ServiceHost> m_services = new List<ServiceHost>();

        private Dictionary<DM.ClientUser, IMapServiceCallback> m_registeredUsers = new Dictionary<DM.ClientUser, IMapServiceCallback>();
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

            m_logger = new Logger(Logger.LogMode.Console);

            m_dalServices = new DalServices();

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
        /// <summary>
        /// Try to log in the <see cref="ClientUser"/> with the given username and password.
        /// </summary>
        /// <param name="username">Username of <see cref="ClientUser"/>.</param>
        /// <param name="password">Password of <see cref="ClientUser"/>.</param>
        /// <param name="callback">Callback object belongs to <see cref="ClientUser"/>.</param>
        /// <returns>True if log in succeeded, false otherwise.</returns>
        public bool LoginClientUser(string username, string password, IMapServiceCallback callback)
        {
            bool rvSucceeded = false;

            try
            {
                DM.ClientUser user = m_dalServices.ClientUserService.QueryUserByUsernameAndPassword(username, password);

                if (user != null)
                {
                    m_registeredUsers.Add(user, callback);

                    rvSucceeded = true;

                    m_logger.WriteLog(Messages.MESSAGE_CLIENT_USER_LOGIN_SUCCEEDED, user.Name, DateTime.Now.ToString());
                }
                else
                {
                    m_logger.WriteLog(Messages.MESSAGE_CLIENT_USER_LOGIN_FAIlED, username);
                }
            }
            catch (Exception ex)
            {
                m_logger.WriteLog(Messages.ERROR_QUERY_CLIENT_USER_BY_NAME_AND_PASSWORD, ex.Message);
            }

            return rvSucceeded;
        }


        /// <summary>
        /// Log out the <see cref="ClientUser"/> belongs to user ID.
        /// </summary>
        /// <param name="userId"></param>
        public void LogoutClientUser(long userId)
        {
            DM.ClientUser user = m_registeredUsers.Keys.SingleOrDefault(u => u.ID == userId);

            if (user != null)
            {
                m_registeredUsers.Remove(user);

                m_logger.WriteLog(Messages.MESSAGE_CLIENT_USER_LOGOUT_SUCCEEDED, user.Name, DateTime.Now.ToString());
            }
        }
        #endregion


        #region phone methods
        /// <summary>
        /// Try to log in the <see cref="ClientUser"/> with the given username and password.
        /// </summary>
        /// <param name="username">Username of <see cref="ClientUser"/>.</param>
        /// <param name="password">Password of <see cref="ClientUser"/>.</param>
        /// <param name="callback">Callback object belongs to <see cref="ClientUser"/>.</param>
        /// <returns>The ID of user logged in.</returns>
        public long LoginPhoneUser(string username, string password)
        {
            long rvUserId = -1;

            try
            {
                DM.PhoneUser user = m_dalServices.PhoneUserService.QueryUserByUsernameAndPassword(username, password);

                if (user != null)
                {
                    rvUserId = user.ID;

                    m_logger.WriteLog(Messages.MESSAGE_PHONE_USER_LOGIN_SUCCEEDED, user.Name, DateTime.Now.ToString());
                }
                else
                {
                    m_logger.WriteLog(Messages.MESSAGE_PHONE_USER_LOGIN_FAIlED, username);
                }
            }
            catch (Exception ex)
            {
                m_logger.WriteLog(Messages.ERROR_QUERY_PHONE_USER_BY_NAME_AND_PASSWORD, ex.Message);
            }

            return rvUserId;
        }


        /// <summary>
        /// Saves incoming new location and send it to every connected map clients.
        /// </summary>
        /// <param name="userId">ID of user sent location data.</param>
        /// <param name="lonLat">Location value.</param>
        public void NewLocation(long userId, LonLat lonLat)
        {
            DM.Location location = new DM.Location
            {
                PhoneUserId = userId,
                Value = lonLat
            };

            foreach (IMapServiceCallback userCallback in m_registeredUsers.Values)
            {
                userCallback.NewLocation(location);
            }
        }
        #endregion
    }
}
