using Garnet.Common.Application.Errors;

namespace Garnet.Teams.Application.Team.Errors
{
    public class TeamNameCanNotBeEmptyError : ApplicationError
    {
        public TeamNameCanNotBeEmptyError() : base("Название команды не может быть пустым")
        {
        }

        public override string Code => nameof(TeamNameCanNotBeEmptyError);
    }
}