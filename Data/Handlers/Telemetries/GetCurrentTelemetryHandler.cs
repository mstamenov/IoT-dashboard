using Data.Queries.Telemetries;
using IoT.Data;
using IoT.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Data.Handlers.Telemetries
{
    public class GetCurrentTelemetryHandler : IRequestHandler<GetCurrentTelemetryQuery, List<Telemetry>>
    {
        private readonly IIoTContext _ioTContext;
        private readonly TimeZoneInfo _tzi;

        public GetCurrentTelemetryHandler(IIoTContext ioTContext, TimeZoneInfo tzi)
        {
            _ioTContext = ioTContext;
            _tzi = tzi;
        }
        public async Task<List<Telemetry>> Handle(GetCurrentTelemetryQuery request, CancellationToken cancellationToken)
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

            var output = await result.ToListAsync();
            output.ForEach(t => t.CreateDate = TimeZoneInfo.ConvertTimeFromUtc(t.CreateDate, _tzi));
            return output;
        }
    }
}
