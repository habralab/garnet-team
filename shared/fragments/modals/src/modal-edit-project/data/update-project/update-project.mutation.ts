import { gql } from '@apollo/client'

export const UPDATE_PROJECT = gql`
  mutation ProjectEdit($id: String!, $name: String!, $description: String!, $tags: [String!]!) {
    projectEditName(input: { id: $id, newName: $name }) {
      id
      projectName
    }
    projectEditDescription(input: { projectId: $id, description: $description }) {
      id
      description
    }
    projectEditTags(input: { projectId: $id, tags: $tags }) {
      id
      tags
    }
  }
`
