using System.ServiceModel;
using System.ServiceModel.Web;

namespace WcfServiceLibrary
{
    [ServiceContract]
    public interface IBiking
    {
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        string GetAddressCoordinates(string address);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        string GetNearestStations(double startLon, double startLat, double endLon, double endLat);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        string GetStartWalkDirections(double startLon, double startLat, double endLon, double endLat);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        string GetEndWalkDirections(double startLon, double startLat, double endLon, double endLat);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        string GetBikingDirections(double startLon, double startLat, double endLon, double endLat);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "getCoordinates?a={startAddress}&b={endAddress}")]
        string getCoordinates(string startAddress, string endAddress);
    }
}
