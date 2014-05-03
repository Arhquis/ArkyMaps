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
        /// Send new <see cref="Position"/> entity to clients.
        /// </summary>
        /// <param name="position">The new <see cref="Position"/>.</param>
        [OperationContract(IsOneWay = true)]
        void NewPosition(Position position);
    }
}
