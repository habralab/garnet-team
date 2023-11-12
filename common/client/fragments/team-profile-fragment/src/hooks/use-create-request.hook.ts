import { JoinRequest }              from '@shared/data'

import { useCreateTeamJoinRequest } from '../data'

export const useCreateRequest = () => {
  const { createTeamJoinRequest } = useCreateTeamJoinRequest()

  const createRequest = async (teamId?: string): Promise<JoinRequest | undefined> => {
    try {
      const { data } = await createTeamJoinRequest({ variables: { id: teamId } })

      return data?.teamUserJoinRequestCreate.teamUserJoinRequestPayload
    } catch (error) {
      /** @todo error notification */

      return undefined
    }
  }

  return { createRequest }
}
