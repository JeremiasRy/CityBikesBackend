using DataAccess.Models;

namespace DataAccess.Data
{
    public interface IStationData
    {
        Task<IEnumerable<StationModel>> GetStations();
    }
}