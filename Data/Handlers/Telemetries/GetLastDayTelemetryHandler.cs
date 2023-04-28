using Data.Queries.Telemetries;
using IoT.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using webapi.ViewObjects;

namespace Data.Handlers.Telemetries
{
    public class GetLastDayTelemetryHandler : IRequestHandler<GetLastDayTelemetryQuery, List<HourlyTelemetryVO>>
    {
        private readonly IIoTContext _ioTContext;

        public GetLastDayTelemetryHandler(IIoTContext ioTContext)
        {
            _ioTContext = ioTContext;
        }

        public Task<List<HourlyTelemetryVO>> Handle(GetLastDayTelemetryQuery request, CancellationToken cancellationToken)
        {
            var telemetry = from t in _ioTContext.Telemetries.Where(t => t.CreateDate > DateTime.Now.AddDays(-1))
                            group new
                            {
                                t.Temperature,
                                t.Humidity,
                                t.Pressure,
                            } by new
                            {
                                t.CreateDate.Date,
                                t.CreateDate.Hour,
                                t.DeviceId
                            } into g
                            select new HourlyTelemetryVO
                            {
                                Date = g.Key.Date,
                                Hour = g.Key.Hour,
                                DeviceId = g.Key.DeviceId,
                                Temperature = g.Average(t => t.Temperature),
                                Humidity = g.Average(t => t.Humidity),
                                Pressure = g.Average(t => t.Pressure)
                            };
            return telemetry
                .OrderBy(s => s.Date)
                .ThenBy(s => s.Hour)
                .ToListAsync();
        }
    }
}
