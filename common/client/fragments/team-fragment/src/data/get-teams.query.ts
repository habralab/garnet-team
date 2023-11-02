import { gql } from '@apollo/client'

export const GET_TEAMS = gql`
  query TeamFilters($search: String, $skip: Int!, $tags: [String!], $take: Int!) {
    teamsFilter(input: { search: $search, skip: $skip, tags: $tags, take: $take }) {
      teams {
        id
        name
        avatarUrl
      }
    }
  }
`
