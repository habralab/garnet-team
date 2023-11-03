import { gql } from '@apollo/client'

export const UPDATE_TEAM = gql`
  mutation TeamEdit($id: String!, $name: String!, $description: String!, $tags: [String!]!) {
    teamEditName(input: { id: $id, name: $name }) {
      id
      name
    }
    teamEditDescription(input: { id: $id, description: $description }) {
      id
      description
    }
    teamEditTags(input: { id: $id, tags: $tags }) {
      id
      tags
    }
  }
`
