import { gql } from '@apollo/client'

export const UPDATE_USER = gql`
  mutation UserEdit($userName: String!, $description: String!, $tags: [String!]!) {
    userEditUsername(input: { newUsername: $userName }) {
      id
      userName
    }
    userEditDescription(input: { description: $description }) {
      id
      description
    }
    userEditTags(input: { tags: $tags }) {
      id
      tags
    }
  }
`
