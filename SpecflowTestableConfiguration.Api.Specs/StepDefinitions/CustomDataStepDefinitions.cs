using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

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

    [Given(@"the repository has custom data")]
    public void GivenTheRepositoryHasWeatherData()
    {
        // TODO: Do nothing for now
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

    [Then(@"the response json should be the expected custom data items")]
    public async Task ThenTheResponseJsonShouldBeTheExpectedCustomDataItems()
    {
        var responseContent = await _response.Content.ReadAsStringAsync();
        Assert.False(string.IsNullOrWhiteSpace(responseContent));
    }
}