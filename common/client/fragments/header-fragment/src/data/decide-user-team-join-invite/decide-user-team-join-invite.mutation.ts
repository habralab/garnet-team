import { gql } from '@apollo/client'

export const DECIDE_USER_TEAM_JOIN_INVITE = gql`
  mutation TeamJoinInvitationDecide($id: String!, $approve: Boolean!) {
    teamJoinInvitationDecide(input: { joinInvitationId: $id, isApproved: $approve }) {
      teamJoinInvitePayload {
        id
        userId
        teamId
      }
    }
  }
`
