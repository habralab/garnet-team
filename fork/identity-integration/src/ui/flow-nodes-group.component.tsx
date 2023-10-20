import { ReactElement } from 'react'
import { FC }           from 'react'
import { useMemo }      from 'react'

import { ActualUiNode } from './ui.interfaces'
import { useFlow }      from '../providers'

export type FlowNodesGroupChildren = (node: ActualUiNode[]) => ReactElement<any>

export interface FlowNodesGroupProps {
  name: string
  children: ReactElement<any> | FlowNodesGroupChildren
}

export const FlowNodesGroup: FC<FlowNodesGroupProps> = ({ name, children }) => {
  const { flow } = useFlow()

  const nodes = useMemo(() => flow?.ui?.nodes?.filter((node) => node.group === name), [flow, name])

  if (!(nodes && nodes.length > 0)) {
    return null
  }

  if (typeof children === 'function') {
    return children(nodes as ActualUiNode[])
  }

  return children as ReactElement<any>
}
