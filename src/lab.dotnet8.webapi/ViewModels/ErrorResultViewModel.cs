namespace lab.dotnet8.webapi.ViewModels;

public class ErrorResultViewModel
{
    public string? ApiVersion { get; set; }
    public string RequestPath { get; set; }
    public ErrorInformation Error { get; set; }
}