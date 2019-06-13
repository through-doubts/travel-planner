using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.Domain;
using CsvHelper;
using CsvHelper.Configuration;

namespace TravelPlanner.Application.Formats
{
    public class CSVFormat : IFormat
    {
        public string Name => "CSV";

        public void SaveTravel(string filePath, Travel travel)
        {
            using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
            using (var csv = new CsvWriter(writer))
            {
                csv.Configuration.RegisterClassMap<TravelEventMap>();
                csv.Configuration.HasHeaderRecord = false;
                csv.WriteRecords(travel.Events);
            }
        }
    }

    public class TravelEventMap : ClassMap<ITravelEvent>
    {
        public TravelEventMap()
        {
            Map(e => e.Type).Index(0);
            Map(e => e.Locations).Index(1).ConvertUsing(
                e => string.Join(" – ", e.Locations.Select(l => l.Name)));
            Map(e => e.Dates).Index(2);
            Map(e => e.Cost).Index(3).ConvertUsing(e => $"{e.Cost.Amount} {e.Cost.Currency.Symbol}");
        }
    }
}
