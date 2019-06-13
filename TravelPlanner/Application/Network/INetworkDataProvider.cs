namespace TravelPlanner.Application.Network
{
    public interface INetworkDataProvider<in TParameters, out TData>
    {
        TData GetData(TParameters parameters);
    }
}