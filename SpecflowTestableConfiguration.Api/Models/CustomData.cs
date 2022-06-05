namespace SpecflowTestableConfiguration.Api.Models;

public class CustomData : List<CustomDataItem> { }

public readonly record struct CustomDataItem(string Name);
