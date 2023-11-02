import { ApolloClient }     from '@apollo/client'
import { InMemoryCache }    from '@apollo/client'

import { createUploadLink } from 'apollo-upload-client'

import { mockAuthHeader }   from '../mock'

export const getClient = () =>
  new ApolloClient({
    // uri: 'https://stage.garnet.pet-project.habr.com/api/sandbox',
    cache: new InMemoryCache(),
    link: createUploadLink({
      uri: 'https://stage.garnet.pet-project.habr.com/api/sandbox',
      headers: {
        Authorization: mockAuthHeader,
        'GraphQL-preflight': '1',
      },
    }),
  })
