using System.Collections.Generic;
using System.IO;
using System.Text;
using TravelPlanner.Domain;

namespace TravelPlanner.Application.Formats
{
    public class CSVFormat : IFormat
    {
        public string Name => "CSV";

        public void SaveTravel(string filePath, Travel travel)
        {
            using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                foreach (var travelEvent in travel.Events)
                {
                    var fieldsList = new List<string> { travelEvent.Type };
                    fieldsList.AddRange(travelEvent.GetLocationsStrings());
                    fieldsList.AddRange(travelEvent.GetDatesStrings());
                    fieldsList.Add(travelEvent.Cost.ToStringValue());
                    writer.WriteLine(string.Join(";", fieldsList));
                }
            }
        }

    }
}
