import { gql } from '@apollo/client'

export const CREATE_TEAM_JOIN_REQUEST = gql`
  mutation TeamUserJoinRequestCreate($id: String!) {
    teamUserJoinRequestCreate(input: { teamId: $id }) {
      teamUserJoinRequestPayload {
        id
        userId
        createdAt
      }
    }
  }
`
