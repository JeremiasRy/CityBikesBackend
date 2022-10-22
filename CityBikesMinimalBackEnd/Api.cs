using Microsoft.Extensions.Logging;

namespace CityBikesMinimalBackEnd;

public static class Api
{
    static readonly LoggerFactory _loggerFactory = new();
    public static void ConfigureApi(this WebApplication app) 
    { 
        app.MapGet("/journeys", GetJourneys);
        app.MapGet("/stations", GetStations);
        app.MapGet("/station/stats", GetStationStatistics);
        app.MapGet("/station/stats/returns", GetStationTop5Returns);
        app.MapGet("/station/stats/departures", GetStationTop5Departures);
    }

    static async Task<IResult> GetJourneys(IJourneyData data, string? date, string? durationOperator, string? distanceOperator, int? duration, int? distance, string? departureStationId, string? returnStationId, string? orderBy, string? orderDirection, int pageIndex = 1, int pageSize = 50)
    {
        try
        {
            return Results.Ok(await data.GetJourneys(date, durationOperator, distanceOperator, duration, distance, departureStationId, returnStationId, orderBy, orderDirection, pageIndex, pageSize));

        } catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
        
    }

    static async Task<IResult> GetStations(IStationData data)
    {
        try
        {
            return Results.Ok(await data.GetStations());
        } catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    static async Task<IResult> GetStationStatistics(IStationData data, string stationId)
    {
        try
        {
            return Results.Ok(await data.GetStationStatistics(stationId));
        } catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }

    }

    static async Task<IResult> GetStationTop5Returns(IStationData data, string stationId)
    {
        try 
        { 
            return Results.Ok(await data.GetStationTop5Returns(stationId)); 
        } catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }  

    static async Task<IResult> GetStationTop5Departures(IStationData data, string stationId)
    {
        try
        {
           return Results.Ok(await data.GetStationTop5Departures(stationId));
        } catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    } 
}
