using System.ServiceModel;

namespace ArkyMapService
{
    [ServiceContract(Namespace = "ArkyMaps.com/PhoneService")]
    public interface IPhoneService
    {
        [OperationContract]
        long Login(string username, string password);


        [OperationContract]
        void NewLocation(long userId, long lon, long lat);
    }
}
