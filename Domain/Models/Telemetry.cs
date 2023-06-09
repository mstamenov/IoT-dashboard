﻿

namespace IoT.Domain.Models
{
    public class Telemetry
    {
        public int TelemetryId{ get; set; }
        
        public string? DeviceId { get; set; }

        public decimal Temperature { get; set; }

        public decimal Humidity { get; set; }

        public decimal Pressure { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
