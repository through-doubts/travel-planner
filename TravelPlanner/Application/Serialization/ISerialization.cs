using System.Collections.Generic;

namespace TravelPlanner.Application.Serialization
{
    public interface ISerialization
    {
        void SaveUsers(List<User> users);
        List<User> LoadUsers();
    }
}
