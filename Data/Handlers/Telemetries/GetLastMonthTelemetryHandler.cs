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
    public class GetLastMonthTelemetryHandler : IRequestHandler<GetLastMonthTelemetryQuery, List<DailyTelemetryVO>>
    {
        private readonly IIoTContext _ioTContext;

        public GetLastMonthTelemetryHandler(IIoTContext ioTContext)
        {
            _ioTContext = ioTContext;
        }

        public Task<List<DailyTelemetryVO>> Handle(GetLastMonthTelemetryQuery request, CancellationToken cancellationToken)
        {
            var telemetry = from t in _ioTContext.Telemetries.Where(t => t.CreateDate > DateTime.Now.AddMonths(-1))
                            group new
                            {
                                t.Temperature,
                                t.Humidity,
                                t.Pressure,
                            } by new
                            {
                                t.CreateDate.Date,
                                t.DeviceId
                            } into g
                            select new DailyTelemetryVO
                            {
                                Date = g.Key.Date,
                                DeviceId = g.Key.DeviceId,
                                Temperature = g.Average(t => t.Temperature),
                                Humidity = g.Average(t => t.Humidity),
                                Pressure = g.Average(t => t.Pressure)
                            };
            return telemetry
                .OrderByDescending(s => s.Date)
                .ToListAsync();
        }
    }
}
