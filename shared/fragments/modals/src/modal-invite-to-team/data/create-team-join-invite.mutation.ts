import { gql } from '@apollo/client'

export const CREATE_TEAM_JOIN_INVITE = gql`
  mutation TeamJoinInvite($userId: String!, $teamId: String!) {
    teamJoinInvite(input: { userId: $userId, teamId: $teamId }) {
      id
    }
  }
`
