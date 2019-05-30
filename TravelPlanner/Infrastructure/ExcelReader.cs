using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using ExcelDataReader;

namespace TravelPlanner.Infrastructure
{
    public class ExcelReader
    {
        public static IEnumerable<List<string>> TableToListOfRows(Stream stream, Encoding encoding)
        { 
            using (var reader = ExcelReaderFactory.CreateReader(stream, new ExcelReaderConfiguration
            {
                FallbackEncoding = encoding
            }))
            {
                var conf = new ExcelDataSetConfiguration
                {
                    ConfigureDataTable = _ => new ExcelDataTableConfiguration
                    {
                        UseHeaderRow = true
                    }
                };
                var dataSet = reader.AsDataSet(conf);
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    yield return dataSet.Tables[0].Columns.OfType<DataColumn>().Select(x => row[x.ColumnName])
                        .Where(x => x is string s && s.Length > 0).OfType<string>().ToList();
                }
            }
        }
    }
}
