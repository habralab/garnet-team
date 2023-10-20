import styled              from '@emotion/styled'

import { Box }             from '@ui/layout'

import { BackgroundProps } from './background.interfaces'

export const Background = styled(Box)<BackgroundProps>(({ theme, color }) => ({
  background: color ? theme.backgrounds[color] : 'none',
}))

export const Wrapper = styled(Box)(() => ({
  width: '100%',
  maxWidth: 1296,
}))
