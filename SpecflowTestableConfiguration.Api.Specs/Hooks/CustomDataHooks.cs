using BoDi;
using Microsoft.AspNetCore.Mvc.Testing;

namespace SpecflowTestableConfiguration.Api.Specs.Hooks;

[Binding]
public class CustomDataHooks
{
    private readonly IObjectContainer _objectContainer;

    public CustomDataHooks(IObjectContainer objectContainer)
    {
        _objectContainer = objectContainer;
    }

    [BeforeScenario]
    private void RegisterServices()
    {
        var factory = CreateWebApplicationFactory();
        _objectContainer.RegisterInstanceAs(factory);
    }

    private WebApplicationFactory<Program> CreateWebApplicationFactory()
        => new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder => { });
}