using DataAccess.Models;

namespace DataAccess.Data
{
    public interface IStationData
    {
        Task<IEnumerable<StationModel>> GetStations();
        Task<IEnumerable<StationStatisticModel>> GetStationStatistics(string stationId, int? month);
        Task<IEnumerable<StationTop5Model>> GetStationTop5Departures(string stationId);
        Task<IEnumerable<StationTop5Model>> GetStationTop5Returns(string stationId);
        Task InserStation(StationModel newStation);
        Task UpdateStation(StationModel newStation);
    }
}