using ArkyMapsDomainModel;
using System.Collections.Generic;
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
        /// <returns>The <see cref="ClientUser"/> logged in.</returns>
        [OperationContract]
        ClientUser Login(string username, string password);


        /// <summary>
        /// Log out the user.
        /// </summary>
        /// <param name="userId">ID of user.</param>
        [OperationContract(IsOneWay = true)]
        void Logout(long userId);


        /// <summary>
        /// Query <see cref="PhoneUser"/> entity by ID.
        /// </summary>
        /// <param name="userId">ID of a <see cref="PhoneUser"/> entity.</param>
        /// <returns>The <see cref="PhoneUser"/> entity with the specififed ID.</returns>
        [OperationContract]
        PhoneUser QueryPhoneUserById(long userId);


        /// <summary>
        /// Query all <see cref="DM.PhoneUser"/> entities.
        /// </summary>
        /// <returns>Collection of <see cref="PhoneUser"/> entities.</returns>
        [OperationContract]
        IEnumerable<PhoneUser> QueryPhoneUsers();


        /// <summary>
        /// Creates a new <see cref="PhoneUser"/> entity.
        /// </summary>
        /// <param name="phoneUser">A <see cref="PhoneUser"/> entity to save in database.</param>
        /// <returns>True if saving was successfull, false otherwise.</returns>
        [OperationContract]
        bool CreatePhoneUser(PhoneUser phoneUser);


        /// <summary>
        /// Modify the specified <see cref="PhoneUser"/> entity.
        /// </summary>
        /// <param name="phoneUser">A <see cref="PhoneUser"/> entity to modify in database.</param>
        /// <returns>True if modification was successfull, false otherwise.</returns>
        [OperationContract]
        bool ModifyPhoneUser(PhoneUser phoneUser);


        /// <summary>
        /// Delete the specified <see cref="PhoneUser"/> entity.
        /// </summary>
        /// <param name="phoneUser">A <see cref="PhoneUser"/> entity to delete.</param>
        /// <returns>True if delete was successfull, false otherwise.</returns>
        [OperationContract]
        bool DeletePhoneUser(PhoneUser phoneUser);
    }
}
