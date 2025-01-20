using System.ComponentModel.DataAnnotations;

namespace OpenIdConnectProvider.ViewModels.Shared;

public class ErrorViewModel
{
	[Display(Name = "Error")]
	public string Error { get; set; } = string.Empty;

    [Display(Name = "Description")]
	public string ErrorDescription { get; set; } = string.Empty;
}
