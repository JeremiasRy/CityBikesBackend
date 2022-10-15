using DataAccess.Models;

namespace DataAccess.Data
{
    public interface IJourneyData
    {
        Task<IEnumerable<JourneyModel>> GetJourneys(string? date, string? durationOperator, string? distanceOperator, int? duration, int? distance, string? departureStationId, string? returnStationId, string? orderBy, string? orderDirection, int pageIndex, int pageSize);
    }
}