import { SVGProps } from 'react'

export interface IconProps extends Omit<SVGProps<SVGSVGElement>, 'color' | 'stroke'> {
  color?: string
  stroke?: string
}
