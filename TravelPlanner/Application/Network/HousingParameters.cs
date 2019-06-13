using System;

namespace TravelPlanner.Application.Network
{
    public class HousingParameters
    {
        public readonly string City;
        public readonly DateTime CheckInDate;
        public readonly DateTime CheckOutDate;
        public readonly int AdultsCount;
        public readonly string Currency;

        public HousingParameters(string city, DateTime checkInDate, DateTime checkOutDate, int adultsCount, string currency)
        {
            City = city;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            AdultsCount = adultsCount;
            Currency = currency;
        }
    }
}