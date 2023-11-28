import { gql } from '@apollo/client'

export const GET_TEAM = gql`
  query TeamGet($id: String!, $search: String!, $skip: Int!, $take: Int!) {
    teamGet(teamId: $id) {
      id
      name
      description
      avatarUrl
      tags
      ownerUserId
    }
    teamParticipantFilter(input: { teamId: $id, search: $search, skip: $skip, take: $take }) {
      teamParticipants {
        id
        userId
        teamId
        username
        avatarUrl
      }
    }
    projectFilterByTeamParticipantId(teamId: $id) {
      projects {
        id
        projectName
        avatarUrl
      }
    }
  }
`
