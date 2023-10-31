namespace Garnet.Notifications.AcceptanceTests.Features.NotificationDelete
{
    [Binding]
    public class NotificationDeleteSteps : BaseSteps
    {
        public NotificationDeleteSteps(StepsArgs args) : base(args)
        {
        }

        [When(@"пользователь '(.*)' отмечает уведомление как прочитанное")]
        public Task WhenПользовательОтмечаетУведомлениеКакПрочитанное(string username)
        {
            return Task.CompletedTask;
        }
    }
}