using TravelPlanner.Domain;

namespace TravelPlanner.Application.Fabrics
{
    public class TravelFabric : IFabric<Travel>
    {
        private int currentId;

        public TravelFabric()
        {
        }

        public Travel Get(string name, params object[] parameters)
        {
            currentId++;
            return new Travel(currentId, name);
        }
    }
}
