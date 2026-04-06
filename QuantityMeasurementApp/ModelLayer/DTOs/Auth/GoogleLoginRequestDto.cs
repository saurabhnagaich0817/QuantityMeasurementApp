using System.ComponentModel.DataAnnotations;

namespace ModelLayer.DTOs.Auth
{
    public class GoogleLoginRequestDto
    {
        [Required(ErrorMessage = "IdToken is required")]
        public string IdToken { get; set; } = string.Empty;
    }
}


