using System.Security.Claims;

namespace MicroOrder.WebAppNew.Components.Pages.Profile;

public class TokenViewModel
{
    public string Name { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public Claim[] Claims { get; set; } = [];
}
