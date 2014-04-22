using System.ServiceModel;

namespace ArkyMapService
{
    [ServiceContract(Namespace = "ArkyMapService", SessionMode = SessionMode.Required, CallbackContract = typeof(IMapServiceCallback))]
    public interface IMapService
    {
        [OperationContract]
        bool Login(string userName, string password);


        [OperationContract]
        bool Logout(long userId);
    }
}
