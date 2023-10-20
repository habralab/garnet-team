import type { UiNodeInputAttributes } from '@ory/kratos-client'
import type { UiNodeImageAttributes } from '@ory/kratos-client'
import type { UiNode }                from '@ory/kratos-client'

import { useMemo }                    from 'react'

import { useFlow }                    from './use-flow.hook'

export const useFlowNode = (nameOrId: string): UiNode | undefined => {
  const { flow } = useFlow()

  const node = useMemo(
    () =>
      flow?.ui?.nodes?.find(({ attributes }) => {
        if ((attributes as UiNodeInputAttributes).name) {
          return (attributes as UiNodeInputAttributes).name === nameOrId
        }

        if ((attributes as UiNodeImageAttributes).id) {
          return (attributes as UiNodeImageAttributes).id === nameOrId
        }

        return false
      }),
    [flow, nameOrId]
  )

  return node
}
