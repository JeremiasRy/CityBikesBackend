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
        return _DbAccess.LoadData<StationModel, dynamic>("[dbo].[GetStations]", new { });
    }
}
