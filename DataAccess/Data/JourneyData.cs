﻿using System;
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
        string? date,
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

        return await _dbAccess.LoadData<JourneyModel, dynamic>("[dbo].[GetJourneys]", new { date, duration, durationOperator, distanceOperator, distance, departureStationId, returnStationId, orderBy, orderDirection, pageIndex, pageSize});
    }

}