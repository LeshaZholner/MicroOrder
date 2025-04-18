using System.ComponentModel.DataAnnotations;

namespace MicroOrder.Identity.API.Pages.Login
{
    public class InputModel
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
        public bool RememberLogin { get; set; }
        public string? ReturnUrl { get; set; }
        public string? Button { get; set; }
    }
}
