using Data.Queries.Telemetries;
using IoT.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi.ViewObjects;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class TelemetryController : ControllerBase
{
    private readonly IMediator _mediator;

    public TelemetryController(ILogger<TelemetryController> logger, IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("LastDay")]
    public Task<List<HourlyTelemetryVO>> GetLastDay() => _mediator.Send(new GetLastDayTelemetryQuery());

    [HttpGet("LastWeek")]
    public async Task<List<HourlyTelemetryVO>> GetLastWeek() => await _mediator.Send(new GetLastWeekTelemetryQuery());
    

    [HttpGet("LastMonth")]
    public Task<List<DailyTelemetryVO>> GetLastMonth() =>  _mediator.Send(new GetLastMonthTelemetryQuery());

    [HttpGet("Current")]
    public Task<List<Telemetry>> GetCurrent() => _mediator.Send(new GetCurrentTelemetryQuery());
}
