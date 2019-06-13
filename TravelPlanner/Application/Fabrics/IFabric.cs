namespace TravelPlanner.Application.Fabrics
{
    public interface IFabric<T>
    {
        T Get(string name, params object[] parameters);
    }
}
