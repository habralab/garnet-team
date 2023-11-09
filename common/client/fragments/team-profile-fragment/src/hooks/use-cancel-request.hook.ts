import { useCancelTeamJoinRequest } from '../data'

export const useCancelRequest = () => {
  const { cancelTeamJoinRequest } = useCancelTeamJoinRequest()

  const cancelRequest = async (joinRequestId?: string) => {
    try {
      await cancelTeamJoinRequest({ variables: { id: joinRequestId } })
    } catch (error) {
      /** @todo error notification */
    }
  }

  return { cancelRequest }
}
