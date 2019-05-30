using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.Domain;
using TravelPlanner.Infrastructure;

namespace TravelPlanner.Application
{
    public class MainApplication : IApplication
    {
        public IEventHandler EventHandler { get; }

        private List<User> users;
        private int currentUserId;
        private User currentUser;

        public MainApplication(IEventHandler eventHandler)
        {
            EventHandler = eventHandler;
            
            users = new List<User>();
            AddUser();
        }

        public void AddUser() // срабатывает мб при нажатии какой-нибудь кнопочки
        {
            var user = new User(currentUserId);
            currentUserId++;
            users.Add(user);
            currentUser = user;
        }

        public void AddTravel(string travelName)
        {
            currentUser.AddTravel(travelName);
        }

        public void AddEvent(ITravelEvent travelEvent)
        {
            currentUser.AddEvent(travelEvent);
        }

        public void ChangeCurrentTravel(string travelName)
        {
            currentUser.ChangeCurrentTravel(travelName);
        }

        public List<ITravelEvent> GetTravelEvents()
        {
            return currentUser.GetTravelEvents();
        }



        public List<Travel> GetTravels()
        {
            return currentUser.GetTravels();
        }

    }
}
