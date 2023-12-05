using Garnet.Common.Infrastructure.Api.Cancellation;
using Garnet.NewsFeed.Application.NewsFeedTeamParticipant;
using MongoDB.Driver;

namespace Garnet.NewsFeed.Infrastructure.MongoDB.NewsFeedTeamParticipant
{
    public class NewsFeedTeamParticipantRepository : INewsFeedTeamParticipantRepository
    {
        private readonly DbFactory _dbFactory;
        private readonly CancellationToken _ct;
        private readonly FilterDefinitionBuilder<NewsFeedTeamParticipantDocument> _f = Builders<NewsFeedTeamParticipantDocument>.Filter;

        public NewsFeedTeamParticipantRepository(DbFactory dbFactory, CancellationTokenProvider ctp)
        {
            _dbFactory = dbFactory;
            _ct = ctp.Token;
        }

        public async Task CreateTeamParticipant(string id, string teamId, string userId)
        {
            var db = _dbFactory.Create();

            var participant = NewsFeedTeamParticipantDocument.Create(id, teamId, userId);
            await db.NewsFeedTeamParticipant.InsertOneAsync(participant, cancellationToken: _ct);
        }

        public async Task DeleteTeamParticipantById(string participantId)
        {
            var db = _dbFactory.Create();
            await db.NewsFeedTeamParticipant.DeleteOneAsync(
                _f.Eq(x => x.UserId, participantId),
                cancellationToken: _ct
            );
        }

        public async Task DeleteTeamParticipantsByTeam(string teamId)
        {
            var db = _dbFactory.Create();
            await db.NewsFeedTeamParticipant.DeleteOneAsync(
                _f.Eq(x => x.TeamId, teamId),
                cancellationToken: _ct
            );
        }

        public async Task<NewsFeedTeamParticipantEntity?> EnsureUserIsTeamParticipant(string teamId, string userId)
        {
            var db = _dbFactory.Create();

            var participant = await db.NewsFeedTeamParticipant.Find(
                _f.And(
                    _f.Eq(x => x.UserId, userId),
                    _f.Eq(x => x.TeamId, teamId)
                )
            ).FirstOrDefaultAsync(_ct);

            return participant is null ? null : NewsFeedTeamParticipantDocument.ToDomain(participant);
        }
    }
}