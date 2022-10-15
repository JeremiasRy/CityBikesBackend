namespace CityBikesMinimalBackEnd;

public static class Api
{
    public static void ConfigureApi(this WebApplication app)
    {
        app.MapGet("/journeys", GetJourneys);
    }

    static async Task<IResult> GetJourneys(IJourneyData data, string? date, string? durationOperator, string? distanceOperator, int? duration, int? distance, string? departureStationId, string? returnStationId, string? orderBy, string? orderDirection, int pageIndex = 1, int pageSize = 100)
    {
        return Results.Ok(await data.GetJourneys(date, durationOperator, distanceOperator, duration, distance, departureStationId, returnStationId, orderBy, orderDirection, pageIndex, pageSize));
    }
}
