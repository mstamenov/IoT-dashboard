using MediatR;
using webapi.ViewObjects;

namespace Data.Queries.Telemetries;

public record GetLastMonthTelemetryQuery() : IRequest<List<DailyTelemetryVO>>;
