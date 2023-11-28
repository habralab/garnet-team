import { gql } from '@apollo/client'

export const GET_PROJECT = gql`
  query ProjectGet($id: String!) {
    projectGet(projectId: $id) {
      id
      projectName
      description
      avatarUrl
      tags
      ownerUserId
    }
    projectTeamParticipantsFilter(input: { projectId: $id }) {
      projectTeamParticipant {
        id
        teamId
        teamName
        teamAvatarUrl
        userParticipants {
          id
          userName
          userAvatarUrl
        }
      }
    }
  }
`
