using Data.Queries;
using Data.Queries.Telemetries;
using IoT.Data;
using IoT.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webapi.ViewObjects;

namespace Data.Handlers.Telemetries
{
    public class GetLastWeekTelemetryHandler : IRequestHandler<GetLastWeekTelemetryQuery, List<HourlyTelemetryVO>>
    {
        private readonly IIoTContext _ioTContext;

        public GetLastWeekTelemetryHandler(IIoTContext ioTContext)
        {
            _ioTContext = ioTContext;
        }

        public Task<List<HourlyTelemetryVO>> Handle(GetLastWeekTelemetryQuery request, CancellationToken cancellationToken)
        {
            var telemetry = from t in _ioTContext.Telemetries.Where(t => t.CreateDate > DateTime.Now.AddDays(-7))
                            group new
                            {
                                t.Temperature,
                                t.Humidity,
                                t.Pressure,
                            } by new
                            {
                                t.CreateDate.Date,
                                Hour = t.CreateDate.Hour < 12 ? 0 : 12,
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
