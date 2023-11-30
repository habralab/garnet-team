import { routes }            from '@shared/routes'

import { NotificationTypes } from '../data'

const routeToUserTypes = [
  NotificationTypes.TeamInviteDecide,
  NotificationTypes.TeamParticipantLeaveTeam,
]

const routeToProjectTypes = [
  NotificationTypes.ProjectEditOwner,
  NotificationTypes.TeamJoinRequestDecide,
]

const routeToTeamTypes = [
  NotificationTypes.TeamJoinProjectRequest,
  NotificationTypes.TeamLeaveProject,
  NotificationTypes.TeamEditOwner,
  NotificationTypes.TeamInvite,
  NotificationTypes.TeamUserJoinRequest,
  NotificationTypes.TeamUserJoinRequestDecide,
]

export const getPathFromNotificationType = (type?: NotificationTypes, id?: string): string => {
  if (!type) return routes.root

  if (routeToUserTypes.includes(type)) return `${routes.users}/${id}`

  if (routeToProjectTypes.includes(type)) return `${routes.projects}/${id}`

  if (routeToTeamTypes.includes(type)) return `${routes.teams}/${id}`

  return routes.root
}
