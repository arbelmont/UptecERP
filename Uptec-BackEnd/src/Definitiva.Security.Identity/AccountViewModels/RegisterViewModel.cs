using System.ComponentModel.DataAnnotations;

namespace Definitiva.Security.Identity.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O Nome é requerido")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O Email é requerido")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A senha deve conter no mínimo {2} e no máximo {1} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme a Senha")]
        [Compare("Password", ErrorMessage = "As senhas não coincidem.")]
        public string ConfirmPassword { get; set; }

        //[Required]
        public string ApiKey { get; set; }

        public string[] Roles { get; set; }

    }
}
