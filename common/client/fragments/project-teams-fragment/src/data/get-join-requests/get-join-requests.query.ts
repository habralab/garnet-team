import { gql } from '@apollo/client'

export const GET_JOIN_REQUESTS = gql`
  query ProjectTeamJoinRequestsByProjectId($id: String!) {
    projectTeamJoinRequestsByProjectId(input: { projectId: $id }) {
      projectTeamJoinRequest {
        id
        projectId
        teamId
        teamName
        teamDescription
        teamAvatarUrl
        projectCount
        teamUserParticipants
      }
    }
  }
`
