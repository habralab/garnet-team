import { gql } from '@apollo/client'

export const DECIDE_TEAM_JOIN_REQUEST = gql`
  mutation TeamUserJoinRequestDecide($id: String!, $approve: Boolean!) {
    teamUserJoinRequestDecide(input: { userJoinRequestId: $id, isApproved: $approve }) {
      teamUserJoinRequestPayload {
        id
        userId
        teamId
      }
    }
  }
`
