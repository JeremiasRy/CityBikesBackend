using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Data;

public class StationData : IStationData
{
    private readonly ISqlAccess _DbAccess;

    public StationData(ISqlAccess dbAccess)
    {
        _DbAccess = dbAccess;
    }

    public Task<IEnumerable<StationModel>> GetStations()
    {
        return _DbAccess.LoadData<StationModel, dynamic>("[dbo].[Sp_GetStations]", new { });
    }

    public Task<IEnumerable<StationStatisticModel>> GetStationStatistics(string stationId)
    {
        return _DbAccess.LoadData<StationStatisticModel, dynamic>("[dbo].[Sp_GetStationStatistics]", new { stationId });
    }

    public Task<IEnumerable<StationTop5Model>> GetStationTop5Returns(string stationId)
    {
        return _DbAccess.LoadData<StationTop5Model, dynamic>("[dbo].[Sp_Top5RtnToDep]", new { stationId });
    }
    public Task<IEnumerable<StationTop5Model>> GetStationTop5Departures(string stationId)
    {
        return _DbAccess.LoadData<StationTop5Model, dynamic>("[dbo].[Sp_Top5DepToRtn]", new { stationId });
    }
}
