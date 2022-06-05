using SpecflowTestableConfiguration.Domain.Models;

namespace SpecflowTestableConfiguration.Api.Options;

public class CustomDataOptions : List<CustomDataItem>
{
    public const string CustomData = "CustomData";
}