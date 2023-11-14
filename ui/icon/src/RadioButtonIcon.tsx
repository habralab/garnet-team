import { useTheme }  from '@emotion/react'

/* eslint-disable */
import React         from 'react'

import { IconProps } from '../icons.interfaces'

export const RadioButtonIcon = (props: IconProps) => {
  const theme: any = useTheme()
  return (
    <svg width='1em' height='1em' viewBox='0 0 16 16' xmlns='http://www.w3.org/2000/svg' {...props}>
      <rect x={1} y={1} width={14} height={14} rx={7} strokeWidth={1} />
      <rect x={4} y={4} width={8} height={8} rx={5} fill='currentColor' stroke='transparent' />
    </svg>
  )
}
