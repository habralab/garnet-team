import { NotificationTypes }               from '../data'
import { useDecideProjectTeamJoinRequest } from '../data'
import { useDecideTeamJoinRequest }        from '../data'
import { useDecideUserTeamJoinInvite }     from '../data'
import { useGetNotifications }             from '../data'

export const useDecideRequest = (id?: string, type?: NotificationTypes) => {
  const { decideProjectTeamJoinRequest, loading: loadingDecideProjectTeamJoinRequest } =
    useDecideProjectTeamJoinRequest()

  const { decideTeamJoinRequest, loading: loadingDecideTeamJoinRequest } =
    useDecideTeamJoinRequest()

  const { decideUserTeamJoinInvite, loading: loadingDecideUserTeamJoinInvite } =
    useDecideUserTeamJoinInvite()

  const { refetch } = useGetNotifications()

  const decideRequest = async (approve: boolean) => {
    try {
      const variables = { approve, id }

      if (type === NotificationTypes.TeamUserJoinRequest) {
        await decideTeamJoinRequest({ variables })
      } else if (type === NotificationTypes.TeamJoinProjectRequest) {
        await decideProjectTeamJoinRequest({ variables })
      } else if (type === NotificationTypes.TeamInvite) {
        await decideUserTeamJoinInvite({ variables })
      }

      refetch()
    } catch (error) {
      if (process.env.NODE_ENV !== 'production') throw error
    }
  }

  return {
    decideRequest,
    loading:
      loadingDecideProjectTeamJoinRequest ||
      loadingDecideTeamJoinRequest ||
      loadingDecideUserTeamJoinInvite,
  }
}
