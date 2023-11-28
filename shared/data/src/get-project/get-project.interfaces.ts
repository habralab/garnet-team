import { Project } from '@shared/data'

export interface ProjectTeamParticipant {
  id: string
  userName: string
  userAvatarUrl: string
}

export interface ProjectTeam {
  id?: string
  teamId?: string
  teamName?: string
  teamAvatarUrl?: string
  projects?: Project[]
  userParticipants?: ProjectTeamParticipant[]
}

export interface GetProjectResponse {
  projectGet: Project
  projectTeamParticipantsFilter?: {
    projectTeamParticipant?: ProjectTeam[]
  }
}

export interface GetProjectInput {
  id: Project['id']
}
