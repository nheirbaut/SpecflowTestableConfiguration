using Microsoft.AspNetCore.Mvc;
using SpecflowTestableConfiguration.Api.Models;

namespace SpecflowTestableConfiguration.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomDataController : ControllerBase
{
    [HttpGet]
    public ActionResult<CustomData> Get()
    {
        var customData = new CustomData
        {
            new CustomDataItem("Foo"),
            new CustomDataItem("Bar"),
            new CustomDataItem("Baz")
        };

        return Ok(customData);
    }
}
