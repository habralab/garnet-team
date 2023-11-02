import { gql } from '@apollo/client'

export const CREATE_TEAM = gql`
  mutation TeamCreate($name: String!, $description: String!, $tags: [String!]!, $avatar: Upload!) {
    teamCreate(input: { name: $name, description: $description, tags: $tags, file: $avatar }) {
      id
    }
  }
`
