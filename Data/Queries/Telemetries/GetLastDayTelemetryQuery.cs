using MediatR;
using webapi.ViewObjects;

namespace Data.Queries.Telemetries;

public record GetLastDayTelemetryQuery() : IRequest<List<HourlyTelemetryVO>>;
