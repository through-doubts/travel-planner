using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPlanner.Application.Formats
{
    public class FormatsHandler : IFormatsHandler
    {
        private readonly IFormat[] formats;

        public FormatsHandler(IFormat[] formats)
        {
            this.formats = formats;
        }

        public List<string> GetFormatsNames()
        {
            return formats.Select(e => e.Name).OrderBy(n => n).ToList();
        }

        public IFormat GetFormatByName(string formatName)
        {
            return formats.FirstOrDefault(e => e.Name == formatName);
        }
    }
}
