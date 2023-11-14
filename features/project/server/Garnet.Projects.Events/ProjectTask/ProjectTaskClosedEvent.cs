namespace Garnet.Projects.Events.ProjectTask;

public record ProjectTaskClosedEvent(
    string Id,
    int TaskNumber,
    string ProjectId,
    string ResponsibleUserId,
    string Name,
    string? Description,
    string Status,
    string[] TeamExecutorIds,
    string[] UserExecutorIds,
    string[] Tags,
    string[] Labels,
    bool Reopened,
    RatingCalculation? RatingCalculation
);

public record RatingCalculation(
    float ProjectOwnerTotalScore,
    string[] UserExecutorIds,
    Dictionary<string, float> SkillScorePerUser,
    Dictionary<string, float> TeamsTotalScore
);