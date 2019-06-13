using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPlanner.Application.Formats
{
    public interface IFormatsHandler
    {
        List<string> GetFormatsNames();
        IFormat GetFormatByName(string formatName);
    }
}
