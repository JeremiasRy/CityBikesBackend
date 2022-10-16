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
    }

    static async Task<IResult> GetJourneys(IJourneyData data, string? date, string? durationOperator, string? distanceOperator, int? duration, int? distance, string? departureStationId, string? returnStationId, string? orderBy, string? orderDirection, int pageIndex = 1, int pageSize = 100)
    {
        return Results.Ok(await data.GetJourneys(date, durationOperator, distanceOperator, duration, distance, departureStationId, returnStationId, orderBy, orderDirection, pageIndex, pageSize));
    }

    static async Task<IResult> GetStations(IStationData data) => Results.Ok(await data.GetStations());

    static async Task<IResult> GetStationStatistics(IStationData data, string stationId) => Results.Ok(await data.GetStationStatistics(stationId));

    static async Task<IResult> GetStationTop5Returns(IStationData data, string stationId) => Results.Ok(await data.GetStationTop5Returns(stationId));

    static async Task<IResult> GetStationTop5Departures(IStationData data, string stationId) => Results.Ok(await data.GetStationTop5Departures(stationId));
}
