using System.Text.Json.Serialization;

namespace webapi.ViewObjects
{
    public class DailyTelemetryVO : MetricVO
    {
        public DateTime Date { get; set; }

    }
}
