using System.Threading.Tasks;
using Definitiva.Security.Identity.AccountViewModels;
using Definitiva.Security.Identity.Interfaces;
using Definitiva.Shared.Domain.Bus;
using Definitiva.Shared.Domain.DomainNotifications;
using Definitiva.Shared.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Uptec.Erp.Api.Configurations;

namespace Uptec.Erp.Api.Controllers.v1.Security
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class SecurityController : BaseController
    {
        private readonly IIdentityService _identityService;

        public SecurityController(INotificationHandler<DomainNotification> notifications,
                                  IBus bus, 
                                  IUnitOfWork uow,
                                  IIdentityService identityService) : base(notifications, uow, bus)
        {
            _identityService = identityService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            return !IsValidModelState() ? Response(model) : Response(await _identityService.Login(model));
        }

        [HttpPost("AdicionarUsuario")]
        public async Task<IActionResult> Post([FromBody] RegisterViewModel model)
        {
            return !IsValidModelState() ? Response(model) : Response(await _identityService.AddUser(model));
        }
        
        [HttpPut("AlterarSenha")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordViewModel model)
        {
            return !IsValidModelState() ? Response(model) : Response(await _identityService.ChangePassword(model));
        }

        [HttpDelete("RemoverUsuario")]
        public async Task<IActionResult> Delete([FromBody] DeleteViewModel model)
        {
            return !IsValidModelState() ? Response(model) : Response(await _identityService.Delete(model));
        }

        [HttpPut("AlterarPerfis")]
        public async Task<IActionResult> ChangeRoles([FromBody] ChangeRolesViewModel model)
        {
            return !IsValidModelState() ? Response(model) : Response(await _identityService.ChangeRoles(model));
        }

        [HttpGet("Setup")]
        public async Task<IActionResult> Setup()
        {
            return Response(await _identityService.Setup(ApiConfiguration.GetApiKey()));
        }
    }
}
