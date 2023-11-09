import { gql } from '@apollo/client'

export const CREATE_PROJECT = gql`
  mutation ProjectCreate(
    $name: String!
    $description: String!
    $tags: [String!]!
    $avatar: Upload!
  ) {
    projectCreate(
      input: { projectName: $name, description: $description, tags: $tags, file: $avatar }
    ) {
      id
    }
  }
`
