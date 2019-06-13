using System.Collections.Generic;

namespace TravelPlanner.Application.Formats
{
    public interface IFormatsHandler
    {
        List<string> GetFormatsNames();
        IFormat GetFormatByName(string formatName);
    }
}
