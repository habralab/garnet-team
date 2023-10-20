using Garnet.Common.Application.Errors;

namespace Garnet.Users.Application.Errors
{
    public class UsernameCanNotBeEmptyError : ApplicationError
    {
        public UsernameCanNotBeEmptyError() : base("Никнейм пользователя не может быть пустым")
        {
        }

        public override string Code => nameof(UsernameCanNotBeEmptyError);
    }
}