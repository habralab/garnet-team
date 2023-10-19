import { useTheme }  from '@emotion/react'

/* eslint-disable */
import React         from 'react'

import { IconProps } from '../icons.interfaces'
export const ChevronDownIcon = (props: IconProps) => {
  const theme: any = useTheme()
  return (
    <svg
      width='1em'
      height='1em'
      viewBox='0 0 24 24'
      fill='none'
      xmlns='http://www.w3.org/2000/svg'
      {...props}
    >
      <path
        d='M19 9L12.7071 15.2929C12.3166 15.6834 11.6834 15.6834 11.2929 15.2929L5 9'
        stroke='#353535'
        strokeWidth={2}
        strokeLinecap='round'
        strokeLinejoin='round'
      />
    </svg>
  )
}
