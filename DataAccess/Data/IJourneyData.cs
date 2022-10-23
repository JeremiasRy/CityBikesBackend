using DataAccess.Models;

namespace DataAccess.Data
{
    public interface IJourneyData
    {
        Task<IEnumerable<JourneyModel>> GetJourneys(int? month, string? durationOperator, string? distanceOperator, int? duration, int? distance, string? departureStationId, string? returnStationId, string? orderBy, string? orderDirection, int pageIndex, int pageSize);
        Task InsertJourney(JourneyModel newJourney);
    }
}