namespace UnitTests;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Net.Http.Json;
using DataAccess.Models;
using Xunit;

[TestClass]
public class EndPoints
{
    [TestMethod]
    public async Task GetJourneysTest()
    {
        using var app = new CityBikeApp();
        var client = app.CreateClient();

        var result = await client.GetFromJsonAsync<IEnumerable<JourneyModel>>("/journeys");
        if (result is not null)
            Assert.IsTrue(!result.Any());
        
    }

    [TestMethod]
    public async Task GetStations()
    {
        using var app = new CityBikeApp();
        var client = app.CreateClient();

        var result = await client.GetFromJsonAsync<IEnumerable<StationModel>>("/stations");
        if (result is not null)
            Assert.IsTrue(!result.Any());
    }
  
    [TestMethod]
    
    public async Task TestInsertStationAndDelete()    
    {    
        using var app = new CityBikeApp();        
        var client = app.CreateClient();

        StationModel newStation = new()
        {
            StationId = "999",
            Name = "Test Station"
        };
        var result = await client.PostAsJsonAsync("/stations", newStation);        
        Assert.IsTrue(result.IsSuccessStatusCode);
        var checkDbResult = await client.GetFromJsonAsync<IEnumerable<StationModel>>("/stations");
        if (checkDbResult != null)
        { 
            var resultStation = checkDbResult.Where(station => station.StationId == newStation.StationId).FirstOrDefault();
            if (resultStation != null)
            {
                Assert.AreEqual(resultStation.StationId, newStation.StationId);
                Assert.AreEqual("Test Station", newStation.Name);   
            }   
        }
        await client.DeleteAsync($"/stations?stationId={newStation.StationId}");
       
        checkDbResult = await client.GetFromJsonAsync<IEnumerable<StationModel>>("/stations");
        if (checkDbResult != null)
            Assert.IsTrue(!checkDbResult.Any());   
    }

    [TestMethod]
    public async Task TestInsertJourneyData()
    {
        using var app = new CityBikeApp();
        var client = app.CreateClient();
        var date = DateTime.Now;

        StationModel newStation = new()
        {
            StationId = "001",
            Name = "Test Station"
        };
        StationModel newStation2 = new()
        {
            StationId = "902",
            Name = "Test Station"
        };

        await client.PostAsJsonAsync("/stations", newStation);
        await client.PostAsJsonAsync("/stations", newStation2);


        JourneyModel newJourney = new()
        {
            DepartureDate = date,
            DepartureStationId = "001",
            ReturnStationId = "902",
            Distance = 1234567,
            Duration = 1234567
        };

        var result = await client.PostAsJsonAsync("/journeys", newJourney);
        Assert.IsTrue(result.IsSuccessStatusCode);

        var checkDbResult = await client.GetFromJsonAsync<IEnumerable<JourneyModel>>("/journeys?orderDirection=d");
        if (checkDbResult != null)
        {
            var resultJourney = checkDbResult.First();

            Assert.AreEqual(resultJourney.Distance, newJourney.Distance);
            Assert.AreEqual(resultJourney.Duration, newJourney.Duration);
            Assert.AreEqual(resultJourney.DepartureStationId, newJourney.DepartureStationId);
            Assert.AreEqual(resultJourney.ReturnStationId, newJourney.ReturnStationId);
            Assert.AreEqual(date.Date, resultJourney.DepartureDate);
            await client.DeleteAsync($"/journeys?id={resultJourney.Id}");
        }
        checkDbResult = await client.GetFromJsonAsync<IEnumerable<JourneyModel>>("/journeys");
        if (checkDbResult is not null)
            Assert.IsTrue(!checkDbResult.Any());
        await client.DeleteAsync($"/stations?stationId={newStation.StationId}");
        await client.DeleteAsync($"/stations?stationId={newStation2.StationId}");
    }
}

class CityBikeApp : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        return base.CreateHost(builder);
    }
}
