import { SVGProps } from 'react'

export interface IconProps extends Omit<SVGProps<SVGSVGElement>, 'color' | 'stroke'> {
  color?: string | boolean | any
  stroke?: string | boolean | any
}
