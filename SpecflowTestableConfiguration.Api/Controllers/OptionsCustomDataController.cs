using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SpecflowTestableConfiguration.Api.Options;
using SpecflowTestableConfiguration.Domain.Models;

namespace SpecflowTestableConfiguration.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OptionsCustomDataController : ControllerBase
{
    private readonly IOptions<CustomDataOptions> _customDataOptions;

    public OptionsCustomDataController(IOptions<CustomDataOptions> customDataOptions)
    {
        _customDataOptions = customDataOptions;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CustomDataItem>> Get()
    {
        return Ok(_customDataOptions.Value.ToList());
    }
}
