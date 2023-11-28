import { Team }                   from '@shared/data'
import { User }                   from '@shared/data'

import { ProjectTeam }            from '../get-project'
import { ProjectTeamParticipant } from '../get-project'

export const parseProjectTeamParticipants = (teamParticipants: ProjectTeamParticipant[]): User[] =>
  teamParticipants.map(({ id, userAvatarUrl, userName }) => ({
    id,
    userName,
    avatarUrl: userAvatarUrl,
  }))

export const parseProjectTeams = (teams: ProjectTeam[]): Team[] =>
  teams.map((item) => ({
    id: item.id,
    name: item.teamName,
    avatarUrl: item.teamAvatarUrl,
    projectCount: item.projects?.length,
    teamParticipants: parseProjectTeamParticipants(item.userParticipants || []),
  }))
