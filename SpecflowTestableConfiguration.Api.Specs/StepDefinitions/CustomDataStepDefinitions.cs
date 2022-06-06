using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using SpecflowTestableConfiguration.Api.Options;
using SpecflowTestableConfiguration.Domain.Models;
using System.Net;
using System.Text.Json;

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

    [Given(@"the custom configuration file does not exist")]
    public void GivenTheCustomConfigurationFileDoesNotExist()
    {
        if (Directory.Exists("CustomOptions"))
            Directory.Delete("CustomOptions", true);
    }

    [Given(@"the custom configuration file has no entries")]
    public async Task GivenTheCustomConfigurationFileHasNoEntries()
    {
        if (!Directory.Exists("CustomOptions"))
            Directory.CreateDirectory("CustomOptions");

        if (File.Exists("CustomOptions/CustomData.json"))
            File.Delete("CustomOptions/CustomData.json");

        await File.WriteAllTextAsync("CustomOptions/CustomData.json", "{}");
    }

    [Given(@"the custom configuration file has an empty entry")]
    public async Task GivenTheCustomConfigurationFileHasEmptyEntry()
    {
        if (!Directory.Exists("CustomOptions"))
            Directory.CreateDirectory("CustomOptions");

        if (File.Exists("CustomOptions/CustomData.json"))
            File.Delete("CustomOptions/CustomData.json");

        var emptyCustomData = new Dictionary<string, object>
        {
            { CustomDataOptions.CustomData, new List<CustomDataItem>() }
        };

        var emptyCustomDataJson = JsonSerializer.Serialize(emptyCustomData);

        await File.WriteAllTextAsync("CustomOptions/CustomData.json", emptyCustomDataJson);
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

    [Then(@"the response should contain no custom data options")]
    public async Task ThenTheResponseShouldContainNoCustomDataOptions()
    {
        var responseContent = await _response.Content.ReadAsStringAsync();
        var deserializeOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var responseCustomDataItems = JsonSerializer.Deserialize<List<CustomDataItem>>(responseContent, deserializeOptions);

        Assert.NotNull(responseCustomDataItems);
        Assert.Empty(responseCustomDataItems);
    }
}

internal static class TestExtensions
{
    public static bool EqualsList(this IEnumerable<CustomDataItem> leftItems, IEnumerable<CustomDataItem> rightItems)
        => !leftItems.Except(rightItems).Any();
}
