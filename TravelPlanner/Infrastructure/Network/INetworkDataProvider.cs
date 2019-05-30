namespace TravelPlanner.Infrastructure.Network
{
    public interface INetworkDataProvider<in TParameters>
    {
        string GetData(TParameters parameters);
    }
}