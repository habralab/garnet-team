export interface User {
  id?: string
  userName?: string
  description?: string
  tags?: string[]
  avatarUrl?: string
}

export interface UserWithRequest extends User {
  requestType?: 'invite' | 'application'
  date?: string
}

export interface Team {
  id?: string
  name?: string
  description?: string
  tags?: string[]
  avatarUrl?: string
  ownerUserId?: string
  countProjects?: number
  countUsers?: number
}

export interface Project {
  id?: string
  projectName?: string
  description?: string
  tags?: string[]
  avatarUrl?: string
  ownerUserId?: string
  countTeams?: number
  countUsers?: number
}

export interface MockUser {
  userGet?: User
  teamsListByUser?: {
    teams?: Team[]
  }
  projectsListByUser?: {
    projects?: Project[]
  }
}

export interface FilterVariables {
  search: string
  tags: string[]
  take: number
  skip: number
}

export interface JoinRequest {
  id?: string
  userId?: string
  teamId?: string
  createdAt?: string
}
