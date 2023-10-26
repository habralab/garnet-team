import { PropsWithChildren } from 'react'

export interface TitleProps extends PropsWithChildren {
  hasBack?: boolean
  backUrl?: string
}
