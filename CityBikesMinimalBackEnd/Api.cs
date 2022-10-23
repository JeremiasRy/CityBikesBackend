using Microsoft.Extensions.Logging;

namespace CityBikesMinimalBackEnd;

public static class Api
{
    public static void ConfigureApi(this WebApplication app) 
    { 
        app.MapGet("/journeys", GetJourneys);
        app.MapGet("/stations", GetStations);
        app.MapGet("/station/stats", GetStationStatistics);
        app.MapGet("/station/stats/returns", GetStationTop5Returns);
        app.MapGet("/station/stats/departures", GetStationTop5Departures);
        app.MapPost("/stations", PostStation);
        app.MapPut("/stations", UpdateStation);
        app.MapPost("/journeys", PostJourney);
        app.MapDelete("/journeys", DeleteJourney);
        app.MapDelete("/stations", DeleteStation);
    }

    public static async Task<IResult> GetJourneys(IJourneyData data, int? month, string? durationOperator, string? distanceOperator, int? duration, int? distance, string? departureStationId, string? returnStationId, string? orderBy, string? orderDirection, int pageIndex = 1, int pageSize = 50)
    {
        try
        {
            return Results.Ok(await data.GetJourneys(month, durationOperator, distanceOperator, duration, distance, departureStationId, returnStationId, orderBy, orderDirection, pageIndex, pageSize));

        } catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    static async Task<IResult> PostJourney(IJourneyData data, JourneyModel newJourney)
    {
        try
        {
            await data.InsertJourney(newJourney);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    static async Task<IResult> DeleteJourney(IJourneyData data, int id)
    {
        try
        {
            await data.DeleteJourney(id);
            return Results.Ok();
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

    static async Task<IResult> GetStationStatistics(IStationData data, string stationId, int? month)
    {
        try
        {
            return Results.Ok(await data.GetStationStatistics(stationId, month));
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

    static async Task<IResult> PostStation(IStationData data, StationModel newStation)
    {
        try
        {
            await data.InserStation(newStation);
            return Results.Ok();
        } catch(Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
    static async Task<IResult> UpdateStation(IStationData data, StationModel newStation)
    {
        var stations = await data.GetStations();
        if (!stations.Any(station => station.StationId == newStation.StationId))
            return Results.BadRequest(new Error("Not found error", $"Didn't find station with id {newStation.StationId}"));

        try
        {
            await data.UpdateStation(newStation);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    static async Task<IResult> DeleteStation(IStationData data, string stationId)
    {
        try
        {
            await data.DeleteStation(stationId);
            return Results.Ok();
        } catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
}
