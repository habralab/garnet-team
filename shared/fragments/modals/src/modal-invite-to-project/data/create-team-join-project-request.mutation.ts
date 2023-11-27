import { gql } from '@apollo/client'

export const CREATE_TEAM_JOIN_PROJECT_REQUEST = gql`
  mutation TeamJoinProjectRequest($projectId: String!, $teamId: String!) {
    teamJoinProjectRequest(input: { projectId: $projectId, teamId: $teamId }) {
      id
    }
  }
`
