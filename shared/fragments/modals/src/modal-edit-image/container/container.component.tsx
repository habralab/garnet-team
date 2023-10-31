import styled                  from '@emotion/styled'

import { Box }                 from '@ui/layout'

import { ContainerProps }      from './container.interfaces'
import { containerBaseStyle }  from './container.styles'
import { containerShapeStyle } from './container.styles'

export const Container = styled(Box)<ContainerProps>(containerBaseStyle, containerShapeStyle)
