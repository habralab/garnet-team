import { useCancelTeamJoinRequest } from '../data'

export const useCancelRequest = () => {
  const { cancelTeamJoinRequest, loading } = useCancelTeamJoinRequest()

  const cancelRequest = async (joinRequestId?: string) => {
    try {
      await cancelTeamJoinRequest({ variables: { id: joinRequestId } })
    } catch (error) {
      if (process.env.NODE_ENV !== 'production') throw error
    }
  }

  return { cancelRequest, loading }
}
