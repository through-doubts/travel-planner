using System;
using System.Collections.Generic;
using TravelPlanner.Domain;
using TravelPlanner.Domain.TravelEvents;


namespace TravelPlanner.Application.MetaInfoHandlers
{
    public interface IEventHandler
    {
        List<string> GetEventsNames();
        Type GetEventType(string eventName);
        FieldsInfo GetFieldsInfo(string eventName);
    }
}
