import { Configuration }   from '@ory/keto-client'
import { RelationshipApi } from '@ory/keto-client'

import { getDomain }       from 'tldjs'

export class KetoRelationWriteClient extends RelationshipApi {
  constructor(basePath?: string) {
    if (!basePath && typeof window !== 'undefined') {
      const { hostname, protocol } = window.location

      if (hostname === 'localhost') {
        basePath = 'http://localhost:4467' // eslint-disable-line no-param-reassign
      } else if (hostname === '127.0.0.1') {
        basePath = 'http://127.0.0.1:4467' // eslint-disable-line no-param-reassign
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

export const ketoRelationshipWrite = new KetoRelationWriteClient()
