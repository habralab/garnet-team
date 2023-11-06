using System.Threading;
using System.Threading.Tasks;
using Garnet.Common.Infrastructure.Identity;
using Garnet.Users.Application.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Garnet.Users.Infrastructure.Api.Controllers
{
    [ApiController]
    [Route("api/v1")]
    [Authorize(AuthenticationSchemes = AuthSchemas.SecretKey)]
    public class UserCreatedController : ControllerBase
    {
        private readonly UserCreateCommand _userCreateCommand;
        public UserCreatedController(UserCreateCommand userCreateCommand)
        {
            _userCreateCommand = userCreateCommand;
        }

        [HttpPost]
        [Route("auth/registration/callback")]
        public async Task UserCreated(CancellationToken ct, UserCreated user)
        {
            await _userCreateCommand.Execute(user.IdentityId, user.Username);
        }
    }
}