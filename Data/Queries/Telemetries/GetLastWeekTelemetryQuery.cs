using MediatR;
using webapi.ViewObjects;

namespace Data.Queries.Telemetries;

public record GetLastWeekTelemetryQuery() : IRequest<List<HourlyTelemetryVO>>;
