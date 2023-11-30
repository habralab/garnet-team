// eslint-disable-next-line no-shadow
export enum NotificationTypes {
  ProjectEditOwner = 'ProjectEditOwner',
  TeamJoinProjectRequest = 'TeamJoinProjectRequest',
  TeamJoinRequestDecide = 'TeamJoinRequestDecide',
  TeamLeaveProject = 'TeamLeaveProject',
  TeamEditOwner = 'TeamEditOwner',
  TeamInvite = 'TeamInvite',
  TeamInviteDecide = 'TeamInviteDecide',
  TeamParticipantLeaveTeam = 'TeamParticipantLeaveTeam',
  TeamUserJoinRequest = 'TeamUserJoinRequest',
  TeamUserJoinRequestDecide = 'TeamUserJoinRequestDecide',
}

export interface QuotedEntity {
  id?: string
  avatarUrl?: string
  quote?: string
}

export interface Notification {
  id?: string
  userId?: string
  linkedEntityId?: string
  title?: string
  body?: string
  type?: NotificationTypes
  createdAt?: string
  quotedEntities: QuotedEntity[]
}
