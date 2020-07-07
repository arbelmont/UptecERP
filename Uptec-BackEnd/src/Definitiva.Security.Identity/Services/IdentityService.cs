using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Definitiva.Security.Identity.AccountViewModels;
using Definitiva.Security.Identity.Athorization;
using Definitiva.Security.Identity.Interfaces;
using Definitiva.Security.Identity.Models;
using Definitiva.Shared.Domain.Bus;
using Definitiva.Shared.Domain.DomainNotifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Definitiva.Security.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly TokenDescriptor _tokenDescriptor;
        private readonly IBus _bus;

        public IdentityService(UserManager<ApplicationUser> userManager,
                               RoleManager<ApplicationRole> roleManager,
                               SignInManager<ApplicationUser> signInManager,
                               TokenDescriptor tokenDescriptor, IBus bus)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _tokenDescriptor = tokenDescriptor;
            _bus = bus;
        }

        public async Task<object> Login(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, true);

            if (result.Succeeded)
            {
                //_logger.LogInformation(1, $"Usuário:{model.Email}, logado com sucesso");

                var response = await TokenGeneratorAsync(model);

                return response;
            }

            NotifyError("Credenciais inválidas.");
            return model;
        }

        public async Task<object> AddUser(RegisterViewModel model)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email, ApiKey = model.ApiKey };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                //Roles
                if (model.Roles.Any()) {
                    result = await _userManager.AddToRolesAsync(user, model.Roles);
                    if(!result.Succeeded) {
                        await _userManager.DeleteAsync(user);
                        NotifyIdentityErrors(result);
                        return null;
                    }
                }
                    
                //Claims
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Name, model.Nome));
                return null;
            }

            NotifyIdentityErrors(result);
            return null;
        }

        public async Task<object> ChangePassword(ChangePasswordViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if(user == null){
                NotifyError("Usuário não cadastrado");
                return null;
            }

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.ConfirmNewPassword);

            if(!result.Succeeded)
                NotifyIdentityErrors(result);
            
            return null;
        }

        public async Task<object> Delete(DeleteViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if(user == null){
                NotifyError("Usuário não cadastrado");
                return null;
            }

            var result = await _userManager.DeleteAsync(user);

            if(!result.Succeeded)
                NotifyIdentityErrors(result);
            
            return null;
        }

        public async Task<object> ChangeRoles(ChangeRolesViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if(user == null){
                NotifyError("Usuário não cadastrado");
                return null;
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            await _userManager.RemoveFromRolesAsync(user, userRoles);

            try
            {
                var result = await _userManager.AddToRolesAsync(user, model.Roles);
            }
            catch (System.Exception)
            {
                NotifyError("Perfil inexistente");
            }
            
            return null;
        }
        private async Task<object> TokenGeneratorAsync(LoginViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            userClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (var role in userRoles)
            {
                userClaims.Add(new Claim(ClaimTypes.Role, role));

                //Adiciona as Claims do Grupo ao token do usuário
                var userRole = await _roleManager.FindByNameAsync(role);
                var claimsRole = await _roleManager.GetClaimsAsync(userRole);
                foreach (var claim in claimsRole)
                {
                    userClaims.Add(claim);
                }
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(userClaims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var signingConf = new SigningConfituration();
            var securityToken = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                //Issuer = _tokenDescriptor.Issuer,
                Issuer = "UptecTokenServer",
                //Audience = _tokenDescriptor.Audience,
                Audience = "https://uptec-front.azurewebsites.net",
                SigningCredentials = signingConf.SigningCredentials,
                Subject = identityClaims,
                NotBefore = DateTime.Now,
                //Expires = DateTime.Now.AddMinutes(_tokenDescriptor.MinutesValid)
                Expires = DateTime.Now.AddMinutes(300)
            });

            var encodedToken = tokenHandler.WriteToken(securityToken);

            var response = new
            {
                access_token = encodedToken,
                //expires_in = DateTime.Now.AddMinutes(_tokenDescriptor.MinutesValid),
                expires_in = DateTime.Now.AddMinutes(300),
                user = new
                {
                    id = user.Id,
                    nome = (userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Name))?.Value,
                    email = user.Email,
                    claims = userClaims.Select(c => new { c.Type, c.Value })
                }
            };

            return response;
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                .TotalSeconds);

        protected void NotifyError(string erro)
        {
            _bus.PublishEvent(new DomainNotification("", erro));
        }

        private void NotifyIdentityErrors(IdentityResult result)
        {
            foreach (var identityError in result.Errors)
            {
                NotifyError(identityError.Description);
            }
        }

        public async Task<object> Setup(string apiKey)
        {
            await AddSystemRoles(apiKey);

            var userMaster = await _userManager.FindByEmailAsync("master@definitiva.com");

            if (userMaster != null)
            {
                await _userManager.DeleteAsync(userMaster);
            }

            var model = new RegisterViewModel { Nome = "Master", Email = "master@definitiva.com", ConfirmPassword = "master@definitiva.com", Password = "Senha@321", ApiKey = apiKey, Roles = new[] { "Master" } };
            var result = await AddUser(model);

            //uptecTest
            userMaster = null;
            userMaster = await _userManager.FindByEmailAsync("uptec@teste.com");

            if (userMaster != null)
            {
                await _userManager.DeleteAsync(userMaster);
            }

            model = new RegisterViewModel { Nome = "UptecTest", Email = "uptec@teste.com", ConfirmPassword = "uptec@teste.com", Password = "Uptec@2019", ApiKey = apiKey, Roles = new[] { "Master" } };
            result = await AddUser(model);


            return result;
        }

        private async Task<object> AddSystemRoles(string apiKey)
        {
            await DeleteSystemRoles();

            await _roleManager.CreateAsync(new ApplicationRole
            {
                ApiKey = apiKey,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                Id = "5bb2ee4d-bf9b-489d-b815-28d15c388e09",
                Name = "Master",
                NormalizedName = "MASTER"
            });

            await _roleManager.CreateAsync(new ApplicationRole
            {
                ApiKey = apiKey,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                Id = "5bb2ee4d-bf9b-489d-b815-28d15c388e10",
                Name = "Cadastro",
                NormalizedName = "CADASTRO"
            });

            await _roleManager.CreateAsync(new ApplicationRole
            {
                ApiKey = apiKey,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                Id = "5bb2ee4d-bf9b-489d-b815-28d15c388e11",
                Name = "Estoque",
                NormalizedName = "ESTOQUE"
            });

            await _roleManager.CreateAsync(new ApplicationRole
            {
                ApiKey = apiKey,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                Id = "5bb2ee4d-bf9b-489d-b815-28d15c388e12",
                Name = "Fiscal",
                NormalizedName = "FISCAL"
            });

            await _roleManager.CreateAsync(new ApplicationRole
            {
                ApiKey = apiKey,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                Id = "5bb2ee4d-bf9b-489d-b815-28d15c388e13",
                Name = "Producao",
                NormalizedName = "PRODUCAO"
            });

            await _roleManager.CreateAsync(new ApplicationRole
            {
                ApiKey = apiKey,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                Id = "5bb2ee4d-bf9b-489d-b815-28d15c388e14",
                Name = "Qualidade",
                NormalizedName = "QUALIDADE"
            });

            await _roleManager.CreateAsync(new ApplicationRole
            {
                ApiKey = apiKey,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                Id = "5bb2ee4d-bf9b-489d-b815-28d15c388e15",
                Name = "Financeiro",
                NormalizedName = "FINANCEIRO"
            });

            await _roleManager.CreateAsync(new ApplicationRole
            {
                ApiKey = apiKey,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                Id = "5bb2ee4d-bf9b-489d-b815-28d15c388e16",
                Name = "Seguranca",
                NormalizedName = "SEGURANCA"
            });

            /* await _roleManager.AddClaimAsync(await _roleManager.FindByIdAsync("5bb2ee4d-bf9b-489d-b815-28d15c388e09"),
                new Claim("5bb2-dash", "R")); */

            return Task.CompletedTask;
        }

        private async Task<object> DeleteSystemRoles()
        {
            var roleMaster = await _roleManager.FindByIdAsync("5bb2ee4d-bf9b-489d-b815-28d15c388e09");
            if (roleMaster != null)
                await _roleManager.DeleteAsync(roleMaster);
            
            var roleCadastro = await _roleManager.FindByIdAsync("5bb2ee4d-bf9b-489d-b815-28d15c388e10");
            if (roleCadastro != null)
                await _roleManager.DeleteAsync(roleCadastro);
            
            var roleEstoque = await _roleManager.FindByIdAsync("5bb2ee4d-bf9b-489d-b815-28d15c388e11");
            if (roleEstoque != null)
                await _roleManager.DeleteAsync(roleEstoque);
            
            var roleFiscal = await _roleManager.FindByIdAsync("5bb2ee4d-bf9b-489d-b815-28d15c388e12");
            if (roleFiscal != null)
                await _roleManager.DeleteAsync(roleFiscal);

            var roleProducao = await _roleManager.FindByIdAsync("5bb2ee4d-bf9b-489d-b815-28d15c388e13");
            if (roleProducao != null)
                await _roleManager.DeleteAsync(roleProducao);

            var roleQualidade = await _roleManager.FindByIdAsync("5bb2ee4d-bf9b-489d-b815-28d15c388e14");
            if (roleQualidade != null)
                await _roleManager.DeleteAsync(roleQualidade);

            var roleFinanceiro = await _roleManager.FindByIdAsync("5bb2ee4d-bf9b-489d-b815-28d15c388e15");
            if (roleFinanceiro != null)
                await _roleManager.DeleteAsync(roleFinanceiro);
            
            var roleSeguranca = await _roleManager.FindByIdAsync("5bb2ee4d-bf9b-489d-b815-28d15c388e16");
            if (roleSeguranca != null)
                await _roleManager.DeleteAsync(roleSeguranca);

            return Task.CompletedTask;
        }
    }
}