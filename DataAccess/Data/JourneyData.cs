using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Data;

public class JourneyData : IJourneyData
{
    private readonly ISqlAccess _dbAccess;

    public JourneyData(ISqlAccess dbAccess)
    {
        _dbAccess = dbAccess;
    }

    public async Task<IEnumerable<JourneyModel>> GetJourneys(
        int? month,
        string? durationOperator,
        string? distanceOperator,
        int? duration,
        int? distance,
        string? departureStationId,
        string? returnStationId,
        string? orderBy,
        string? orderDirection,
        int pageIndex,
        int pageSize)
    {

        return await _dbAccess.LoadData<JourneyModel, dynamic>("[dbo].[Sp_GetJourneys]", new { month, duration, durationOperator, distanceOperator, distance, departureStationId, returnStationId, orderBy, orderDirection, pageIndex, pageSize });
    }

    public async Task InsertJourney(JourneyModel newJourney)
    {
        string date = newJourney.DepartureDate.ToString("yyyy-MM-dd");
        await _dbAccess.SaveData("[dbo].[Sp_InsertJourney]", new { date, newJourney.DepartureStationId, newJourney.ReturnStationId, newJourney.Distance, newJourney.Duration });
    }

}
