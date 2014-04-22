using System.ServiceModel;

namespace ArkyMapService
{
    /// <summary>
    /// The interface define phone service methods.
    /// </summary>
    [ServiceContract(Namespace = "ArkyMaps.com/PhoneService")]
    public interface IPhoneService
    {
        /// <summary>
        /// Try to log in the user.
        /// </summary>
        /// <param name="username">Username of user.</param>
        /// <param name="password">Password of user.</param>
        /// <returns>The ID of user logged in.</returns>
        [OperationContract]
        long Login(string username, string password);


        /// <summary>
        /// Register a new location for user belongs to user ID.
        /// </summary>
        /// <param name="userId">ID of user.</param>
        /// <param name="longitude">Longitude value of location.</param>
        /// <param name="latitude">Latitiude value of location.</param>
        [OperationContract]
        void NewLocation(long userId, long longitude, long latitude);
    }
}
