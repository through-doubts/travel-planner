using TravelPlanner.Domain;

namespace TravelPlanner.Application.Formats
{
    public interface IFormat : INameable
    {
        void SaveTravel(string filePath, Travel travel);
    }
}
