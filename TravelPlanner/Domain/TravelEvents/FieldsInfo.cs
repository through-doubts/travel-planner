namespace TravelPlanner.Domain.TravelEvents
{
    public class FieldsInfo
    {
        public string[] PossibleTypes { get; }
        public string[] LocationsHeaders { get; }
        public string[] DatesHeaders { get; }

        public FieldsInfo(string[] possibleTypes, string[] locationsHeaders, string[] datesHeaders)
        {
            PossibleTypes = possibleTypes;
            LocationsHeaders = locationsHeaders;
            DatesHeaders = datesHeaders;
        }
    }
}
