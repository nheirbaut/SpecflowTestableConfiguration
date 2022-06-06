using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using SpecflowTestableConfiguration.Api.Options;
using SpecflowTestableConfiguration.Domain.Models;

namespace SpecflowTestableConfiguration.Api.Specs.StepDefinitions;

[Binding]
public class CustomDataStepDefinitions
{
    private const string BaseAddress = "http://localhost/";

    private readonly WebApplicationFactory<Program> _webApplicationFactory;
    private HttpResponseMessage _response = null!;

    public Lazy<HttpClient> LazyClient => new(() => _webApplicationFactory.CreateDefaultClient(new Uri(BaseAddress)));

    public CustomDataStepDefinitions(WebApplicationFactory<Program> webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory;
    }

    [Given(@"the configuration has no custom data")]
    public void GivenTheRepositoryHasNoCustomData()
    {
        if (Directory.Exists("CustomOptions"))
            Directory.Delete("CustomOptions", true);
    }

    [When(@"I make a GET request to '([^']*)'")]
    public async Task WhenIMakeAGetRequestTo(string endpoint)
    {
        _response = await LazyClient.Value.GetAsync(endpoint);
    }

    [Then(@"the response status code is '([^']*)'")]
    public void ThenTheResponseStatusCodeIs(int statusCode)
    {
        var expected = (HttpStatusCode) statusCode;
        Assert.Equal(expected, _response.StatusCode);
    }

    [Then(@"the response should contain the default custom data options")]
    public async Task ThenTheResponseShouldContainTheDefaultCustomDataOptions()
    {
        var defaultConfigurationBuilder = new ConfigurationBuilder();

        defaultConfigurationBuilder.AddJsonFile("DefaultCustomOptions/CustomData.json");
        var defaultConfiguration = defaultConfigurationBuilder.Build();

        var defaultDataSection = defaultConfiguration.GetSection(CustomDataOptions.CustomData);
        var defaultCustomDataItems = defaultDataSection.Get<List<CustomDataItem>>();

        var responseContent = await _response.Content.ReadAsStringAsync();
        var deserializeOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var responseCustomDataItems = JsonSerializer.Deserialize<List<CustomDataItem>>(responseContent, deserializeOptions);

        Assert.True(defaultCustomDataItems.EqualsList(responseCustomDataItems ?? Enumerable.Empty<CustomDataItem>()));
    }
}

internal static class TestExtensions
{
    public static bool EqualsList(this IEnumerable<CustomDataItem> leftItems, IEnumerable<CustomDataItem> rightItems)
        => !leftItems.Except(rightItems).Any();
}
