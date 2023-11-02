import { gql } from '@apollo/client'

export const UPDATE_USER_DESCRIPTION = gql`
  mutation UserEditDescription($description: String!) {
    userEditDescription(input: { description: $description }) {
      id
      description
    }
  }
`
