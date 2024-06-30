using System.ComponentModel.DataAnnotations;

namespace BlazorServerApp.OpenAPIs;

public class ApiClientOptions
{
    public const string SectionName = nameof(ApiClientOptions);

    [Required]
    public int TimeoutInSeconds { get; set; }

    [Url]
    [Required]
    public string BaseUrl { get; set; } = null!;
}