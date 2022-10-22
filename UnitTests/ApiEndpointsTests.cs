namespace UnitTests;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

[TestClass]
public class EndPoints
{
    [TestMethod]
    public async Task GetJourneysReturnsListWithOutParameters()
    {
        await using var app = new WebApplicationFactory<Program>();
        using var client = app.CreateClient();


        var response = await client.GetAsync("/journeys");
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }
    [TestMethod]
    public async Task GetJourneysReturnsListWithParameters()
    {
        await using var app = new WebApplicationFactory<Program>();
        using var client = app.CreateClient();

        var request = new HttpRequestMessage(HttpMethod.Get, "/journeys?departureStationId=001");

        var response = await client.SendAsync(request);
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }
    [TestMethod]
    public async Task GetStations()
    {
        await using var app = new WebApplicationFactory<Program>();
        using var client = app.CreateClient();

        var response = await client.GetAsync("/stations");
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }
}
