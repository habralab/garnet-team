import { Configuration } from '@ory/kratos-client'
import { FrontendApi }   from '@ory/kratos-client'

import { getDomain }     from 'tldjs'

export class KratosClient extends FrontendApi {
  constructor(basePath?: string) {
    if (!basePath && typeof window !== 'undefined') {
      const { hostname, protocol } = window.location

      if (hostname === 'localhost') {
        basePath = 'http://localhost:4433' // eslint-disable-line no-param-reassign
      } else if (hostname === '127.0.0.1') {
        basePath = 'http://127.0.0.1:4433' // eslint-disable-line no-param-reassign
      } else if (hostname.startsWith('accounts.')) {
        basePath = origin.replace('accounts.', 'identity.') // eslint-disable-line no-param-reassign
      } else {
        basePath = `${protocol}//identity.${getDomain(hostname)}` // eslint-disable-line no-param-reassign
      }
    }

    super(
      new Configuration({
        basePath,
      })
    )
  }
}

export const kratos = new KratosClient()
