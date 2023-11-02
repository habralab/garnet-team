import { gql } from '@apollo/client'

export const GET_PROJECTS = gql`
  query ProjectFilters($search: String, $skip: Int!, $tags: [String!], $take: Int!) {
    projectsFilter(input: { search: $search, skip: $skip, tags: $tags, take: $take }) {
      projects {
        id
        projectName
        avatarUrl
      }
    }
  }
`
