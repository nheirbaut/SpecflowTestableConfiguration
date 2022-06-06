using Microsoft.AspNetCore.Mvc;
using SpecflowTestableConfiguration.Api.Options;

namespace SpecflowTestableConfiguration.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ConcreteTypeCustomDataController : Controller
{
    private readonly CustomDataOptions _customDataOptions;

    public ConcreteTypeCustomDataController(CustomDataOptions customDataOptions)
    {
        _customDataOptions = customDataOptions;
    }

    [HttpGet]
    public ActionResult<CustomDataOptions> Get()
    {
        return Ok(_customDataOptions);
    }
}