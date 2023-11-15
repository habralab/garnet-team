import { gql } from '@apollo/client'

export const GET_TEAM_REQUESTS = gql`
  query TeamRequestsGet($id: String!) {
    teamUserJoinRequestsShow(teamId: $id) {
      teamUserJoinRequests {
        id
        userId
        createdAt
      }
    }
    teamJoinInvitationsShow(teamId: $id) {
      teamJoinInvites {
        id
        userId
        createdAt
      }
    }
  }
`
