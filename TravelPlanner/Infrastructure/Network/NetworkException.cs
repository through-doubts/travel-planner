using System;

namespace TravelPlanner.Infrastructure.Network
{
    public class NetworkException : Exception
    {
        public NetworkException(string message) : base(message) { }
    }
}