import { gql } from '@apollo/client'

export const GET_USERS = gql`
  query UserFilters($search: String, $skip: Int!, $tags: [String!], $take: Int!) {
    usersFilter(input: { search: $search, skip: $skip, tags: $tags, take: $take }) {
      users {
        id
        userName
        tags
        avatarUrl
      }
    }
  }
`
