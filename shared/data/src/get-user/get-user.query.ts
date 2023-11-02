import { gql } from '@apollo/client'

export const GET_USER = gql`
  query UserGet($id: String!, $skip: Int!, $take: Int!) {
    userGet(id: $id) {
      id
      userName
      description
      tags
      avatarUrl
    }
    teamsListByUser(input: { userId: $id, skip: $skip, take: $take }) {
      teams {
        id
        name
        avatarUrl
      }
    }
  }
`
