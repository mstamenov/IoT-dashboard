using Data.Queries.Telemetries;
using IoT.Data;
using IoT.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Data.Handlers.Telemetries
{
    public class GetCurrentTelemetryHandler : IRequestHandler<GetCurrentTelemetryQuery, List<Telemetry>>
    {
        private readonly IIoTContext _ioTContext;

        public GetCurrentTelemetryHandler(IIoTContext ioTContext)
        {
            this._ioTContext = ioTContext;
        }
        public Task<List<Telemetry>> Handle(GetCurrentTelemetryQuery request, CancellationToken cancellationToken)
        {
            var maxDateBySensor = _ioTContext.Telemetries
            .GroupBy(t => t.DeviceId)
            .Select(g => new
            {
                DeviceId = g.Key,
                MaxDate = g.Max(t => t.CreateDate)
            });

            var result = from t in _ioTContext.Telemetries
                         join m in maxDateBySensor on new { t.DeviceId, Date = t.CreateDate } equals new { m.DeviceId, Date = m.MaxDate }
                         select t;

            return result.ToListAsync();
        }
    }
}
