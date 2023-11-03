import { gql } from '@apollo/client'

export const CANCEL_TEAM_JOIN_REQUEST = gql`
  mutation TeamUserJoinRequestCancel($id: String!) {
    teamUserJoinRequestCancel(input: { userJoinRequestId: $id }) {
      id
      userId
      userId
      createdAt
    }
  }
`
