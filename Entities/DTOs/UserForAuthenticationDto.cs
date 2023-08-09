using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public record UserForAuthenticationDto
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "{} is required")]
        public string? UserName { get; init; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "{} is required")]
        public string? Password { get; init; }
    }
}
