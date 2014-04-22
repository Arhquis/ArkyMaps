using System;
using System.ServiceModel;

namespace ArkyMapService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class MapService : IMapService
    {
        #region constructors
        public MapService()
        {
            // NOTE: intentionally left blank.
        }
        #endregion


        #region IMapService members
        public bool Login(string username, string password)
        {
            IMapServiceCallback callback = OperationContext.Current.GetCallbackChannel<IMapServiceCallback>();

            bool isSucceeded = ServiceController.Instance.LoginUser(username, password, callback);

            if (isSucceeded)
            {
                Console.WriteLine("Login happened");

                return true;
            }

            return false;
        }


        public bool Logout(long userId)
        {
            ServiceController.Instance.LogoutUser(userId);

            Console.WriteLine("Logout happened");

            return true;
        }
        #endregion
    }
}
