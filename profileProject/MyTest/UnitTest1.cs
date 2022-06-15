using System.Net.Http.Json;
using AngleSharp.Common;
using ProfileProject.Models;

namespace MyTest;
using Microsoft.AspNetCore.Mvc.Testing;

[TestClass]
public class UnitTest1
{
    private  HttpClient _httpClient;

    public UnitTest1()
    {
        var webAppFactory = new WebApplicationFactory<Program>();
        _httpClient = webAppFactory.CreateDefaultClient(); 
    }

    [TestMethod]
    public async Task TestMethod1()
    {
        var response = await _httpClient.GetAsync("/WeatherForecast");
        var stringResult = await response.Content.ReadAsStringAsync();
        Assert.AreEqual("Hello world",stringResult);
    }
    
    [TestMethod]
    public async Task TestGetProfile() {
        var response = await _httpClient.GetAsync("/api/Profile");
        var stringResult = await response.Content.ReadAsStringAsync();
        Assert.IsTrue(stringResult.Length > 0);
        Assert.IsTrue(stringResult.Contains("name"));
    }
    
    [TestMethod]
    public async Task TestGetProfileTestForNullData() {
        var response = await _httpClient.GetAsync("/api/Profile");
        var stringResult1 = response.Content.ReadFromJsonAsync<List<Profile>>();
        if (stringResult1.Result != null)
            foreach (var item in stringResult1.Result)
            {
                Assert.IsNotNull(item.name);
                Assert.IsNotNull(item.surname);
                Assert.IsNotNull(item.email);
                
            }
    }
    
    [TestMethod]
    public async Task TestGetProfileByName() {
        var response = await _httpClient.GetAsync("/api/Profile/name?name=Thabang");
        var stringResult = await response.Content.ReadAsStringAsync();
        Assert.IsTrue(stringResult.Length > 0);
        Assert.IsTrue(stringResult.Contains("name"));
    }
    public async Task TestGetProfileByExits() {
        var response = await _httpClient.GetAsync("/api/Profile/name?name=Thabang");
        var stringResult1 = response.Content.ReadFromJsonAsync<List<Profile>>();
        if (stringResult1.Result != null)
            foreach (var item in stringResult1.Result)
            {
                Assert.AreEqual("Thabang",item.name);
                Assert.IsNotNull(item.name);
                Assert.IsNotNull(item.surname);
                Assert.IsNotNull(item.email);
                
            }
    }
}