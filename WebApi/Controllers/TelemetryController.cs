using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using webapi.ViewObjects;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class TelemetryController : ControllerBase
{
    private readonly ILogger<TelemetryController> _logger;
    private readonly IoTContext _dbContext;

    public TelemetryController(ILogger<TelemetryController> logger, IoTContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet("LastDay")]
    public IEnumerable<HourlyTelemetryVO> GetLastDay()
    {
        var telemetry = from t in _dbContext.Set<Telemetry>().Where(t => t.CreateDate > DateTime.Now.AddDays(-1))
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
            .ThenBy(s => s.Hour);
                        
    }

    [HttpGet("LastWeek")]
    public IEnumerable<HourlyTelemetryVO> GetLastWeek()
    {
        var telemetry = from t in _dbContext.Set<Telemetry>().Where(t => t.CreateDate > DateTime.Now.AddDays(-7))
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
            .ThenBy(s => s.Hour);
    }

    [HttpGet("LastMonth")]
    public IEnumerable<DailyTelemetryVO> GetLastMonth()
    {
        var telemetry = from t in _dbContext.Set<Telemetry>().Where(t => t.CreateDate > DateTime.Now.AddMonths(-30))
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
            .OrderByDescending(s => s.Date);
    }

    [HttpGet("Current")]
    public IEnumerable<Telemetry> GetCurrent()
    {
        var maxDateBySensor = _dbContext.Set<Telemetry>()
            .GroupBy(t => t.DeviceId)
            .Select(g => new
            {
                DeviceId = g.Key,
                MaxDate = g.Max(t => t.CreateDate)
            });

        var result = from t in _dbContext.Set<Telemetry>()
                     join m in maxDateBySensor on new { t.DeviceId, Date = t.CreateDate } equals new { m.DeviceId, Date = m.MaxDate }
                     select t;

        return result.ToList(); ;
    }
}
