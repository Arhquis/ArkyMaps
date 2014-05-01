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

        private Dictionary<long, IMapServiceCallback> m_registeredUsers = new Dictionary<long, IMapServiceCallback>();
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
        /// <returns>The logged in <see cref="ClientUser"/> if it was successfull, null otherwise.</returns>
        public DM.ClientUser LoginClientUser(string username, string password, IMapServiceCallback callback)
        {
            DM.ClientUser rvClientUser = null;

            try
            {
                rvClientUser = m_dalServices.ClientUserService.QueryUserByUsernameAndPassword(username, password);

                if (rvClientUser != null)
                {
                    // debug purpose only
                    if (m_registeredUsers.ContainsKey(rvClientUser.ID))
                    {
                        m_registeredUsers.Remove(rvClientUser.ID);
                    }

                    m_registeredUsers.Add(rvClientUser.ID, callback);


                    m_logger.WriteLog(Messages.MESSAGE_CLIENT_USER_LOGIN_SUCCEEDED, rvClientUser.Name, DateTime.Now.ToString());
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

            return rvClientUser;
        }


        /// <summary>
        /// Log out the <see cref="ClientUser"/> belongs to user ID.
        /// </summary>
        /// <param name="userId"></param>
        public void LogoutClientUser(long userId)
        {
            if (m_registeredUsers.ContainsKey(userId))
            {
                m_registeredUsers.Remove(userId);

                m_logger.WriteLog(Messages.MESSAGE_CLIENT_USER_LOGOUT_SUCCEEDED, userId.ToString(), DateTime.Now.ToString());
            }
        }


        /// <summary>
        /// Query all <see cref="DM.PhoneUser"/> entities.
        /// </summary>
        /// <returns>Collection of <see cref="DM.PhoneUser"/> entities.</returns>
        public IEnumerable<DM.PhoneUser> QueryPhoneUsers()
        {
            IEnumerable<DM.PhoneUser> rvPhoneUsers = null;

            try
            {
                rvPhoneUsers = m_dalServices.PhoneUserService.QueryPhoneUsers();
            }
            catch (Exception ex)
            {
                m_logger.WriteLog(Messages.ERROR_QUERY_PHONE_USERS, ex.Message);
            }

            return rvPhoneUsers;
        }


        /// <summary>
        /// Query <see cref="DM.PhoneUser"/> entity by ID.
        /// </summary>
        /// <param name="userId">ID of a <see cref="DM.PhoneUser"/> entity.</param>
        /// <returns>The <see cref="DM.PhoneUser"/> entity with the specififed ID.</returns>
        public DM.PhoneUser QueryPhoneUserById(long userId)
        {
            DM.PhoneUser rvPhoneUser = null;

            try
            {
                rvPhoneUser = m_dalServices.PhoneUserService.QueryPhoneUserById(userId);
            }
            catch (Exception ex)
            {
                m_logger.WriteLog(Messages.ERROR_QUERY_PHONE_USER_BY_ID, ex.Message);
            }

            return rvPhoneUser;
        }


        /// <summary>
        /// Creates a new <see cref="PhoneUser"/> entity.
        /// </summary>
        /// <param name="phoneUser">A <see cref="DM.PhoneUser"/> entity to save in database.</param>
        /// <returns>True if saving was successfull, false otherwise.</returns>
        public bool CreatePhoneUser(DM.PhoneUser phoneUser)
        {
            bool rvSucceeded = false;

            try
            {
                rvSucceeded = m_dalServices.PhoneUserService.CreatePhoneUser(phoneUser);
            }
            catch (Exception ex)
            {
                m_logger.WriteLog(Messages.ERROR_CREATE_PHONE_USER, ex.Message);
            }

            return rvSucceeded;
        }


        /// <summary>
        /// Modify the specified <see cref="PhoneUser"/> entity.
        /// </summary>
        /// <param name="phoneUser">A <see cref="DM.PhoneUser"/> entity to modify in database.</param>
        /// <returns>True if modification was successfull, false otherwise.</returns>
        public bool ModifyPhoneUser(DM.PhoneUser phoneUser)
        {
            bool rvSucceeded = false;

            try
            {
                rvSucceeded = m_dalServices.PhoneUserService.ModifyPhoneUser(phoneUser);
            }
            catch (Exception ex)
            {
                m_logger.WriteLog(Messages.ERROR_MODIFY_PHONE_USER, ex.Message);
            }

            return rvSucceeded;
        }


        /// <summary>
        /// Delete the specified <see cref="PhoneUser"/> entity.
        /// </summary>
        /// <param name="phoneUser">A <see cref="DM.PhoneUser"/> entity to delete.</param>
        /// <returns>True if delete was successfull, false otherwise.</returns>
        public bool DeletePhoneUser(DM.PhoneUser phoneUser)
        {
            bool rvSucceeded = false;

            try
            {
                rvSucceeded = m_dalServices.PhoneUserService.DeletePhoneUser(phoneUser);
            }
            catch (Exception ex)
            {
                m_logger.WriteLog(Messages.ERROR_DELETE_PHONE_USER, ex.Message);
            }

            return rvSucceeded;
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
                DM.PhoneUser user = m_dalServices.PhoneUserService.QueryPhoneUserByUsernameAndPassword(username, password);

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
            DM.Position location = new DM.Position
            {
                PhoneUserId = userId,
                Location = lonLat
            };

            Console.WriteLine(string.Format("Location data sent. (UserId: {0}, Location: {1}", userId, lonLat));

            foreach (IMapServiceCallback userCallback in m_registeredUsers.Values)
            {
                try
                {
                    userCallback.NewLocation(location);
                }
                catch (Exception)
                {
                    
                }
            }
        }
        #endregion
    }
}
