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
    public async Task GetJourneys_ReturnsJourneysWithOrWithoutParameters()
    {
        using var app = new CityBikeApp();
        var client = app.CreateClient();

        var result = await client.GetAsync("/journeys");

        Assert.IsTrue(HttpStatusCode.OK == result.StatusCode);

        var content = await result.Content.ReadFromJsonAsync<IEnumerable<JourneyModel>>();
        Assert.IsNotNull(content);
        if (content is not null)
            Assert.AreEqual(50, content.Count());

        var content2 = await client.GetFromJsonAsync <IEnumerable<JourneyModel>>("/journeys");
        var contentWithParameters = await client.GetFromJsonAsync<IEnumerable<JourneyModel>>("/journeys?pageSize=100&month=6");

        Assert.IsNotNull(contentWithParameters);
        if (contentWithParameters is not null && content is not null && content2 is not null)
        {
            Assert.AreEqual(content.First().Id, content2.First().Id);

            Assert.AreNotEqual(content.Count(), contentWithParameters.Count());
            Assert.AreNotEqual(content.First().Id, contentWithParameters.First().Id);
        }
    }

    [TestMethod]
    public async Task GetStations()
    {
        using var app = new CityBikeApp();
        var client = app.CreateClient();

        var result = await client.GetAsync("/stations");
        Assert.IsTrue(result.IsSuccessStatusCode);
    }

    [TestMethod]
    public async Task InsertJourney()
    {
        using var app = new CityBikeApp();
        var client = app.CreateClient();
        var date = DateTime.Now;

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
        }







    }
}

class CityBikeApp : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        return base.CreateHost(builder);
    }
}
