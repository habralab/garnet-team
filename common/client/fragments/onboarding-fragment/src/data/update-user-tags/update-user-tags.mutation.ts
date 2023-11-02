import { gql } from '@apollo/client'

export const UPDATE_USER_TAGS = gql`
  mutation UserEditTags($tags: [String!]!) {
    userEditTags(input: { tags: $tags }) {
      id
      tags
    }
  }
`
