using System.ComponentModel.DataAnnotations;

namespace Definitiva.Security.Identity.AccountViewModels
{
    public class ChangeRolesViewModel
    {
        [Required(ErrorMessage = "O Email é requerido")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Perfil não informado")]
        public string[] Roles { get; set; }

    }
}
