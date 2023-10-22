using Garnet.Users.Application.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Garnet.Users.Infrastructure.Api.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class UserCreatedController : ControllerBase
    {
        private readonly UserCreateCommand _userCreateCommand;
        public UserCreatedController(UserCreateCommand userCreateCommand)
        {
            _userCreateCommand = userCreateCommand;
        }

        [HttpPost]
        [Route("auth/registration/callback")]
        public async Task UserCreated(UserCreated user)
        {
            await _userCreateCommand.Execute(CancellationToken.None, user.IdentityId, user.Username);
        }
    }
}