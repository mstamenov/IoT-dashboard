using IoT.Domain.Models;
using MediatR;

namespace Data.Queries.Telemetries;

public record GetCurrentTelemetryQuery() : IRequest<List<Telemetry>>;
