using HtmlTags;
using System.IO;
using System.Text;
using TravelPlanner.Domain;

namespace TravelPlanner.Application.Formats
{
    public class HtmlFormat : IFormat
    {
        public string Name => "HTML";
        public void SaveTravel(string filePath, Travel travel)
        {
            var table = new TableTag().Caption(travel.Name);
            table.Attr("table border", 1)
                .Attr("cellpadding", 20)
                .Attr("bgcolor", "fafeff")
                .Attr("bordercolor", "white");
            foreach (var travelEvent in travel.Events)
            {
                table.AddBodyRow(b =>
                {
                    b.Cell(travelEvent.Type);
                    b.Cell(travelEvent.GetLocationsStrings()[0]);
                    b.Cell(travelEvent.GetLocationsStrings()[1]);
                    b.Cell(travelEvent.GetDatesStrings()[0]);
                    b.Cell(travelEvent.GetDatesStrings()[0]);
                    b.Cell(travelEvent.Cost.ToStringValue());
                });
            }
            using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                writer.Write(table.ToHtmlString());
            }
        }
    }
}
