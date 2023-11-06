import { ApolloClient }     from '@apollo/client'
import { InMemoryCache }    from '@apollo/client'

import { createUploadLink } from 'apollo-upload-client'

export const getClient = () =>
  new ApolloClient({
    cache: new InMemoryCache(),
    link: createUploadLink({
      uri: process.env.NEXT_BACKEND_URL,
      headers: {
        'GraphQL-preflight': '1',
      },
      credentials: 'include',
    }),
  })
