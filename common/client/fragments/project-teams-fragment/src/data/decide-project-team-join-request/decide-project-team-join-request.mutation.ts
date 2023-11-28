import { gql } from '@apollo/client'

export const DECIDE_PROJECT_TEAM_JOIN_REQUEST = gql`
  mutation ProjectTeamJoinRequestDecide($id: String!, $approve: Boolean!) {
    projectTeamJoinRequestDecide(input: { projectTeamJoinRequestId: $id, isApproved: $approve }) {
      id
      teamId
      teamName
      projectId
    }
  }
`
