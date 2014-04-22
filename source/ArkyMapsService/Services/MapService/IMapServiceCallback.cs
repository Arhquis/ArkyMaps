using System.ServiceModel;

namespace ArkyMapService
{
    public interface IMapServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void NewLocation(long userId, long lon, long lat);

    }
}
