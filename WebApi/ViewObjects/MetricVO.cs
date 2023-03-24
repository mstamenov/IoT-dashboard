namespace webapi.ViewObjects
{
    public class MetricVO
    {
        public decimal Temperature { get; set; }

        public decimal Humidity { get; set; }

        public decimal Pressure { get; set; }

        public string? DeviceId { get; set; }
    }
}
