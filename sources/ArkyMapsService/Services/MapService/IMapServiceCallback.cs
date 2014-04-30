using ArkyMapsDomainModel;
using System.ServiceModel;

namespace ArkyMapService
{
    /// <summary>
    /// The interface define map service callback methods.
    /// </summary>
    public interface IMapServiceCallback
    {
        /// <summary>
        /// Send new <see cref="Location"/> entity to clients.
        /// </summary>
        /// <param name="location">The new <see cref="Location"/>.</param>
        [OperationContract(IsOneWay = true)]
        void NewLocation(Location location);
    }
}
