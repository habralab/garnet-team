import { ApolloClient }  from '@apollo/client'
import { InMemoryCache } from '@apollo/client'

export const getClient = () =>
  new ApolloClient({
    uri: 'https://stage.garnet.pet-project.habr.com/api/sandbox',
    cache: new InMemoryCache(),
    headers: {
      Authorization: 'ASldWxUASDwey231d82DH2b1',
      'Access-Control-Allow-Origin': '*',
    },
  })
