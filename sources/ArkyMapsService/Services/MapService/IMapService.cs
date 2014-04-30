using ArkyMapsDomainModel;
using System.ServiceModel;

namespace ArkyMapService
{
    /// <summary>
    /// The interface define map service methods.
    /// </summary>
    [ServiceContract(Namespace = "ArkyMapService", SessionMode = SessionMode.Required, CallbackContract = typeof(IMapServiceCallback))]
    public interface IMapService
    {
        /// <summary>
        /// Try to log in the user.
        /// </summary>
        /// <param name="username">Username of user.</param>
        /// <param name="password">Password of user.</param>
        /// <returns>True if log in was successfull, false otherwise.</returns>
        [OperationContract]
        bool Login(string username, string password);


        /// <summary>
        /// Log out the user.
        /// </summary>
        /// <param name="userId">ID of user.</param>
        [OperationContract]
        void Logout(long userId);


        /// <summary>
        /// Query <see cref="PhoneUser"/> entity by ID.
        /// </summary>
        /// <param name="userId">ID of a <see cref="PhoneUser"/> entity.</param>
        /// <returns>The <see cref="PhoneUser"/> entity with the specififed ID.</returns>
        [OperationContract]
        PhoneUser QueryPhoneUserById(long userId);
    }
}
