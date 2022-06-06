using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace SpecflowTestableConfiguration.Api.Specs.StepDefinitions;

[Binding]
public class CustomDataStepDefinitions
{
    private const string BaseAddress = "http://localhost/";

    public WebApplicationFactory<Program> WebApplicationFactory { get; }

    public Lazy<HttpClient> LazyClient => new(() => WebApplicationFactory.CreateDefaultClient(new Uri(BaseAddress)));

    private HttpResponseMessage Response { get; set; } = null!;

    public CustomDataStepDefinitions(WebApplicationFactory<Program> webApplicationFactory)
    {
        WebApplicationFactory = webApplicationFactory;
    }

    [Given(@"the repository has custom data")]
    public void GivenTheRepositoryHasWeatherData()
    {
        // TODO: Do nothing for now
    }

    [When(@"I make a GET request to '([^']*)'")]
    public async Task WhenIMakeAGetRequestTo(string endpoint)
    {
        Response = await LazyClient.Value.GetAsync(endpoint);
    }

    [Then(@"the response status code is '([^']*)'")]
    public void ThenTheResponseStatusCodeIs(int statusCode)
    {
        var expected = (HttpStatusCode) statusCode;
        Assert.Equal(expected, Response.StatusCode);
    }

    [Then(@"the response json should be the expected custom data items")]
    public async Task ThenTheResponseJsonShouldBeTheExpectedCustomDataItems()
    {
        var responseContent = await Response.Content.ReadAsStringAsync();
        Assert.False(string.IsNullOrWhiteSpace(responseContent));
    }
}