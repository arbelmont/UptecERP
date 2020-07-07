using System.ComponentModel.DataAnnotations;

namespace Definitiva.Security.Identity.AccountViewModels
{
    public class DeleteViewModel
    {
        [Required(ErrorMessage = "O Email é requerido")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
