using MicroOrder.WebApp.Models.Profile;
using MicroOrder.WebApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IdentityModel.Tokens.Jwt;

namespace MicroOrder.WebApp.Pages.Profile;

[Authorize]
public class IndexModel : PageModel
{
    private readonly LogOutService _logoutService;

    public IndexModel(LogOutService logOutService)
    {
        _logoutService = logOutService;
    }

    public TokenViewModel IdToken { get; set; } = new();
    public TokenViewModel AccessToken { get; set; } = new();
    public TokenViewModel UserClaims { get; set; } = new();

    public async Task<IActionResult> OnGet()
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var idToken = await HttpContext.GetTokenAsync("id_token");
        var accessToken = await HttpContext.GetTokenAsync("access_token");

        IdToken.Name = "id_token";
        IdToken.Token = idToken ?? string.Empty;
        IdToken.Claims = [.. tokenHandler.ReadJwtToken(idToken).Claims];

        AccessToken.Name = "access_token";
        AccessToken.Token = accessToken ?? string.Empty;
        AccessToken.Claims = [.. tokenHandler.ReadJwtToken(accessToken).Claims];

        UserClaims.Name = "user_claims";
        UserClaims.Claims = [.. User.Claims];

        return Page();
    }

    //public async Task LogOut()
    //{
    //    await _logoutService.LogOutAsync(HttpContext);
    //}
}
